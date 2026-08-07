# FastFiles (Call of Duty: Ghosts / IW6)

FastFiles (`.ff`) for Call of Duty: Ghosts use the IW6 engine's **signed** container
format. Everything documented here is verified against **PS3 retail** samples only —
no PC, Xbox 360, or Wii U samples have been tested, so the layout below applies to
PS3. Other platforms use shifted asset type IDs and are not covered.

> **Scope note:** Ghosts FastFiles are signed and **cannot be rebuilt** — re-signing
> the `DB_AuthHeader` requires Infinity Ward's RSA-2048 private key. The format is
> fully decompressable (read/extract), but there is no recompression path.

---

## Header Structure

| Offset | Size | Field | Notes |
|--------|------|-------|-------|
| 0x00 | 8 | Magic `IWff0100` | ASCII, signed magic (same as Xbox 360 / MW2 PC signed) |
| 0x08 | 4 | Version `0x0000022E` | BE u32 = 558. Identifies IW6 |

Ghosts is detected by the combination of **`IWff0100` outer magic + version `0x22E` +
`IWffS100` inner magic** (note the capital `S`, unlike MW3's lowercase `IWffs100`).

### Two FastFile variants

Both variants share the same downstream layout once the `IWffS100` inner magic is
located. Call its file offset **A**; the rule `A + 0x20000 = start of the deflate-block
stream` is what lets both variants use one extractor.

| Variant | Example | `IWffS100` offset (A) | Notes |
|---------|---------|-----------------------|-------|
| **Patch FF** | `patch_mp_dome_ns.ff` | `0x24` (right after the 36-byte outer header) | Most common modding target |
| **Base FF** | `ghosts ps3 common.ff` | `0x3294` (sample) | A ~12 KB index table sits between the outer header and `IWffS100` |

The base-FF index table's record format has not been reverse-engineered; it is not
needed for decompression and is skipped.

---

## FastFile Layout (PS3)

### Patch FF

```
0x00000..0x00023   Outer header                     36 bytes
0x00024..0x01FF3   IWffS100 + DB_AuthHeader       8,144 bytes   (A = 0x24)
0x01FF4..0x02023   Padding                           48 bytes
0x02024..0x20023   "LO" region                  114,688 bytes   (14 × 0x2000)
0x20024..EOF       Raw-deflate block stream      rest of file
```

### Base FF

```
0x00000..0x0000B   Outer header (magic + version)    12 bytes
0x0000C..0x03293   Index table (~12 KB, file-offset pairs, unknown semantics)
0x03294..0x05263   IWffS100 + DB_AuthHeader       8,144 bytes   (A = 0x3294)
0x05264..0x05293   Padding                           48 bytes
0x05294..0x23293   "LO" region                  114,688 bytes
0x23294..EOF       Raw-deflate block stream      rest of file
```

### Outer header fields (patch FFs)

| Offset | Size | Field |
|--------|------|-------|
| 0x00 | 8 | `IWff0100` outer magic |
| 0x08 | 4 | Version `0x0000022E` (BE) |
| 0x0C | 4 | Flags `01 00 04 04` (identical across patch samples) |
| 0x10 | 12 | Zeros |
| 0x1C | 4 | File size (BE u32; matches `.ff` size) |
| 0x20 | 4 | Max file size (BE u32; equals 0x1C) |

Base FFs put the index table at `0x0C` onward instead of these fields.

### `DB_AuthHeader` (8,144 bytes from `IWffS100`)

Same overall shape as MW3 (IW5), but hashes are **SHA-1** (20 bytes + 12 zero-pad in a
32-byte slot) instead of SHA-256. Offsets are relative to **A**:

| Offset (from A) | Size | Field |
|-----------------|------|-------|
| +0x00 | 8 | `IWffS100` inner magic |
| +0x08 | 4 | Reserved (zeros) |
| +0x0C | 32 | `subheaderHash` — 20 B SHA-1 + 12 B zero pad |
| +0x2C | 256 | RSA-2048 signature (Activision public key) |
| +0x12C | 32 | `fastfileName` — ASCII, null-padded |
| +0x14C | 4 | Reserved (zeros) |
| +0x150 | 7,808 | `masterBlockHashes[244]` — 244 × (20 B SHA-1 + 12 B zero pad) |

### "LO" region (`A+0x2000 .. A+0x20000`)

14 chunks of `0x2000` (8 KB) = `0x1C000` bytes (112 KB). Every byte has **bit 7 = 0**
(values `0x00..0x7F`) — a deliberate constraint, not statistical. Its purpose has not
been reverse-engineered; decompression does not need it. Bit-plane analysis rules out
any standard stream cipher (AES-CTR/CBC, Salsa20, ChaCha20, RC4) or repeating-XOR.

---

## Compression — Raw-deflate blocks

From `A + 0x20000` to EOF, the file is a sequence of raw-deflate blocks:

```
[srcSize BE u16][raw-deflate payload, srcSize bytes]
[srcSize BE u16][raw-deflate payload, srcSize bytes]
...
```

- Each block decompresses to **exactly 0x10000 bytes (64 KB)**.
- There is **no end marker** — the stream ends at EOF.
- This is the *outer* layer. Many individual assets are then **zlib-compressed a second
  time** inside the zone (see [Zone.md](FastFiles/Zone.md)).

### Verified block counts

| File | FF size | Blocks | Decompressed zone |
|------|--------:|-------:|------------------:|
| `patch_mp_dome_ns.ff` | 145,493 B | 1 | 65,536 |
| `patch_homecoming.ff` | 194,709 B | 2 | 131,072 |
| `ghosts_patch_common_mp.ff` | 525,354 B | 9 | 589,824 |
| `ghosts ps3 common.ff` | 29,796,135 B | 717 | 46,989,312 |

---

## Decompression Algorithm

1. Read outer magic `IWff0100` + version `0x22E`.
2. Locate the `IWffS100` inner magic → offset **A** (patch: `0x24`; base: after the
   index table).
3. Seek to `A + 0x20000` (skips the auth header, 48-byte padding, and the "LO" region).
4. Walk the deflate-block stream: read a 2-byte BE `srcSize`, raw-inflate `srcSize`
   bytes (each block → 64 KB), repeat to EOF. Concatenate to form the raw zone.
5. **Second pass:** walk each asset header and expand any inner zlib stream inline (see
   [Zone.md](FastFiles/Zone.md)). A zone fully processed this way has zero residual `78 XX` zlib
   streams.

No encryption is involved anywhere — the outer layer is raw deflate, inner per-asset
streams are standard zlib.

---

## Comparison with the IW4/IW5 signed format

| Aspect | MW2 PC / MW3 signed (IW4/IW5) | **Ghosts (IW6) PS3** |
|--------|-------------------------------|----------------------|
| Outer magic | `IWff0100` | `IWff0100` |
| Version | `0x114` (LE) / MW3 | `0x22E` (BE) |
| Inner magic | `IWffs100` (lowercase) | **`IWffS100` (capital S)** |
| Outer compression | Authed chunks (8 KB chunks, groups of 257) | **Raw-deflate 64 KB blocks** |
| Master block hash | SHA-256 (32 bytes) | **SHA-1 (20 B + 12 zero-pad)** |
| Pre-payload metadata | 48 B padding | 48 B padding **+ 112 KB "LO" region** |
| Asset pool entry | `[type LE][ptr]` (PC) | `[ptr][type BE]` (like MW2 PS3) |

---

## References

- [Zone.md](FastFiles/Zone.md) — zone structure, asset pool, per-asset headers, Lua bytecode
- [AssetTypes.md](FastFiles/AssetTypes.md) — IW6 PS3 asset type IDs
- [COD Research Wiki](https://codresearch.dev/)
- Verified against PS3 retail samples (patch, DLC, and base zones)
