# FastFiles (Modern Warfare 2)

## What is a FastFile?

FastFile (`.ff`) is a proprietary archive format used by the IW Engine in Call of Duty games. It stores **zone files** - bundled, precompiled game assets such as menus, scripts, models, sounds, and videos.

**Key Characteristics:**
- FastFiles contain compressed zone files using **zlib deflate compression**
- PS3/Xbox 360 files are **Big Endian**, PC is **Little Endian**
- Xbox 360 MP files may be **signed** with RSA signatures

---

## File Header Structure

The FastFile header is **12 bytes** total:

| Name    | Offset | Size | Type    | Description                                              |
|---------|--------|------|---------|----------------------------------------------------------|
| Magic   | 0x00   | 8    | char[8] | `"IWffu100"` (unsigned) or `"IWff0100"` (signed)         |
| Version | 0x08   | 4    | int32   | Version number (endian-dependent)                        |

### Magic Identifiers

| Magic       | Hex Bytes                          | Description                                |
|-------------|------------------------------------|--------------------------------------------|
| `IWffu100`  | `49 57 66 66 75 31 30 30`          | Unsigned (PS3, unsigned Xbox 360, PC SP)   |
| `IWff0100`  | `49 57 66 66 30 31 30 30`          | Signed (Xbox 360 MP **and** PC MP/patch)   |

> **Note:** Unlike CoD4/WaW, the signed magic `IWff0100` is **not** exclusive to Xbox 360
> on MW2 — PC retail multiplayer/patch FFs are also signed (Infinity Ward "authed chunks").
> See [MW2 PC](#mw2-pc-format) below.

---

## Version Numbers

Version numbers identify the game/engine version. They're stored at offset 0x08 in the header.

| Build Type     | Version | Hex Value   | Description                    |
|----------------|---------|-------------|--------------------------------|
| Release        | 269     | `0x10D`     | Retail MW2                     |
| PC Release     | 276     | `0x114`     | PC version                     |
| Dev Build      | 253     | `0xFD`      | Pre-release development build  |

### Platform Endianness

Console builds use version `0x10D` (big-endian); PC uses a **different version**, `0x114`,
stored **little-endian**:

| Platform       | Version | Hex Bytes (as stored)             |
|----------------|---------|-----------------------------------|
| PS3            | 0x10D   | `00 00 01 0D` (Big Endian)        |
| Xbox 360       | 0x10D   | `00 00 01 0D` (Big Endian)        |
| PC             | 0x114   | `14 01 00 00` (Little Endian)     |

> The version byte order is itself the quickest platform tell: console FFs read `00 00 ..`
> first, PC FFs read the low byte (`14`) first.

---

## DB_Header Structure

MW2 FastFiles use a `DB_Header` structure:

```c
enum language_t
{
  LANGUAGE_ENGLISH = 0x1,
  LANGUAGE_FRENCH = 0x2,
  LANGUAGE_GERMAN = 0x3,
  LANGUAGE_ITALIAN = 0x4,
  LANGUAGE_SPANISH = 0x5,
  LANGUAGE_BRITISH = 0x6,
  LANGUAGE_RUSSIAN = 0x7,
  LANGUAGE_POLISH = 0x8,
  LANGUAGE_KOREAN = 0x9,
  LANGUAGE_TAIWANESE = 0xA,
  LANGUAGE_JAPANESE = 0xB,
  LANGUAGE_CHINESE = 0xC,
  LANGUAGE_THAI = 0xD,
  LANGUAGE_LEET = 0xE,
  LANGUAGE_CZECH = 0xF,
  MAX_LANGUAGES
};

struct DB_Header
{
  char magic[8];
  int version;
  bool allowOnlineUpdate;
  unsigned __int64 fileCreationTime;
  language_t region;
  int entryCount;
  Entry entries[];
  int fileSize;
  int maxFileSize;
};
```

### Field Layout (PS3 Big-Endian)

| Offset | Size | Field | Description |
|--------|------|-------|-------------|
| 0x00 | 8 | magic | `IWffu100` (unsigned) or `IWff0100` (signed) |
| 0x08 | 4 | version | `0x0000010D` for MW2 |
| 0x0C | 1 | allowOnlineUpdate | Usually `0x01` for patch files |
| 0x0D | 8 | fileCreationTime | Windows FILETIME format |
| 0x15 | 4 | region | Language enum value (usually `0x00000001`) |
| 0x19 | 4 | entryCount | Number of entries (usually 0) |
| 0x1D | var | entries | `entryCount * 0x14` bytes (if any) |
| var | 4 | fileSize | Actual FF file size (big-endian) |
| var+4 | 4 | maxFileSize | Same as fileSize (big-endian) |

**Critical**: The `fileSize` and `maxFileSize` fields must contain the **actual FF file size AFTER compression**. If these values are wrong, the game will fail to load the FastFile.

### Example Header Hex Dump

```
00: 49 57 66 66 75 31 30 30  IWffu100 (magic)
08: 00 00 01 0D              version = 0x10D
0C: 01                       allowOnlineUpdate = 1
0D: XX XX XX XX XX XX XX XX  fileCreationTime
15: 00 00 00 01              region = LANGUAGE_ENGLISH
19: 00 00 00 00              entryCount = 0
1D: 00 02 DA 21              fileSize (example)
21: 00 02 DA 21              maxFileSize (same)
25: [compressed blocks start]
```

---

## Two-Level Compression

MW2 PS3 uses **two distinct compression layers**:

### FF-Level Compression (Outer Layer)

The FastFile itself is compressed in 64KB blocks using **raw deflate** (zlib WITHOUT header).

| Property | Value |
|----------|-------|
| Algorithm | Deflate (NO zlib header) |
| Block size | 65536 bytes (0x10000) |
| Length prefix | 2 bytes, big-endian |
| End marker | `0x00 0x01` |

**Critical**: The compressed blocks do NOT have the zlib header bytes (`0x78 0x9C`). The 2-byte zlib header must be **stripped** when compressing.

```
[2-byte length BE][raw deflate data][2-byte length BE][raw deflate data]...[0x00 0x01]
```

### Zone-Level Compression (Inner Layer)

Individual raw files within the zone CAN be compressed using **standard zlib** (WITH header).

| Property | Value |
|----------|-------|
| Algorithm | Zlib (with `0x78` header) |
| Header bytes | `0x78` + `0x01`, `0x5E`, `0x9C`, or `0xDA` |
| Per-file | Each raw file compressed independently |

**Note**: Not all raw files are compressed. Check the `compressedLen` field in the raw file header.

> **Important:** The two-level (block + per-file zlib) scheme above is **PS3-specific**.
> MW2 Xbox 360 and MW2 PC do **not** use 64KB outer blocks — they use a single outer zlib
> stream (unsigned) or authed chunks (signed). See the platform sections below.

---

## Platform Compression Summary

| Platform | Unsigned outer compression | Signed outer compression |
|----------|----------------------------|--------------------------|
| PS3      | 64KB blocks (raw deflate)  | — (PS3 retail is unsigned) |
| Xbox 360 | Single zlib stream         | Authed chunks (IW4) |
| PC       | Single zlib stream @ `0x15`| Authed chunks (IW4) |

Inner per-rawfile zlib compression (the "zone-level" layer above) applies on **all**
platforms — only the *outer* container differs.

---

## MW2 Xbox 360 Format

- **Header:** 12-byte standard header + the full **25-byte extended header** (same
  `DB_Header` as PS3).
- **Unsigned** (`IWffu100`): a **single zlib stream** immediately after the 25-byte header
  (not 64KB blocks).
- **Signed** (`IWff0100`): Infinity Ward "authed chunks" beginning at `0x25` — the same
  format as signed MW2 PC, just with the full 25-byte `DB_Header` instead of PC's 9-byte
  preamble.
- **Zone:** 48-byte header (drops `BlockSizeVertex`); asset IDs use the `MW2AssetTypeXbox360`
  enum (no `vertexshader`, IDs ≥ `0x07` shift −1 from PS3). See [Zone.md](Zone.md).

---

## MW2 PC Format

MW2 PC is **distinct from CoD4/WaW PC** and from MW2 console. It pairs MW2's
compressed-rawfile model with a little-endian, PC-style zone.

- **Version:** `0x114` stored little-endian (`14 01 00 00`).
- **Preamble:** only **9 bytes** between the standard header and the stream
  (`allowOnlineUpdate` (1) + `fileCreationTime` (8)) — *shorter* than the 25-byte
  PS3/Xbox 360 extended header. There is no `region` / `entryCount` / `fileSize`.
- **Zone header:** **56 bytes** (8 blockSize slots, asset table at `0x38`) — same geometry
  as Wii WaW, but **little-endian**.
- **Rawfile size fields** (`compressedLen`, `len`): **little-endian** (reading them BE
  yields GB-scale nonsense).

### Unsigned MW2 PC (SP/campaign)

```
00..07  IWffu100
08..0B  14 01 00 00            version 0x114 (LE)
0C      01                     allowOnlineUpdate
0D..14  ........               fileCreationTime (8 bytes)
15..EOF [single zlib stream]   starts with 78 DA / 78 9C / 78 5E / 78 01
```

Decompression simply feeds bytes from `0x15` to EOF into one zlib stream.

### Signed MW2 PC (MP/patch) — Authed Chunks

```
00..07     IWff0100              signed magic
08..0B     14 01 00 00           version 0x114 (LE)
0C..14     preamble (9 bytes)    allowOnlineUpdate + fileCreationTime
15..2024   DB_AuthHeader         8,144 bytes (IWffs100 + RSA-2048 sig + 244 SHA-256 hashes)
2025..2054 48 bytes padding      pad to AUTHED_CHUNK_SIZE 0x2000
2055..EOF  Authed chunks         groups of 257 × 0x2000-byte chunks
```

Each group is **257 chunks of 8KB**: chunk 0 is a hash table (256 × SHA-256, **skipped**
for decompression), chunks 1–256 are zlib-stream payload. Concatenate the payload chunks
across all groups and feed one zlib stream. The first data chunk lands at `0x4015`;
subsequent groups at `0x4015 + N × 0x202000`.

> **Save note:** Recompression always writes the **unsigned** PC layout (12-byte header +
> 9-byte preamble + single zlib at `0x15`). Signed inputs round-trip to unsigned outputs —
> re-signing the `DB_AuthHeader` requires Infinity Ward's RSA-2048 private key. Unsigned
> FFs are a valid loadable variant (used for SP/campaign in retail).

---

## Dev Build FastFiles (.ffm)

MW2 development builds use `.ffm` file extension and version `0xFD` (253).

### Dev Build Version

| Field   | Value       | Description                    |
|---------|-------------|--------------------------------|
| Version | `0xFD`      | 253 decimal                    |
| Hex     | `00 00 00 FD` | Big-endian representation    |

### Unsigned Dev Builds (Supported)

**Magic:** `IWffu100`

Unsigned dev build FFM files can be decompressed. They use a single zlib stream that starts at a variable offset within the file (not immediately after the header).

**Verified Example:** `trainer.ffm`
- File size: ~47 MB
- Zlib stream starts at offset `0xB5D5`
- Decompressed zone size: ~93 MB
- Compression: Standard zlib (78 DA header)

**Decompression Method:**
1. Scan first 256KB for zlib header bytes (`78 9C`, `78 DA`, `78 01`, `78 5E`)
2. Attempt decompression from each candidate offset
3. First successful decompression (output > 10KB) is the valid stream

### Signed Dev Builds (Not Supported)

**Magic:** `IWff0100`

Signed dev build FFM files use a different internal format that cannot be decompressed with standard zlib methods. The bytes that appear to be zlib headers (`78 DA`) are false positives within encrypted or differently-formatted data.

**Observed Example:** `mp_underpass.ffm`
- File size: ~37 MB
- Multiple apparent zlib headers found, but none decompress successfully
- Internal format differs from both unsigned dev builds and signed release files

### Identifying Dev Builds

```
Offset 0x00-0x07: Magic (IWffu100 or IWff0100)
Offset 0x08-0x0B: Version = 00 00 00 FD (253)
Offset 0x0C-0x0F: Structure identifier = 01 01 CA 03 (dev) vs 01 01 CA EC (release)
```

| Byte Pattern at 0x0C | Build Type     |
|----------------------|----------------|
| `01 01 CA 03`        | Dev Build      |
| `01 01 CA EC`        | Release Build  |

---

## Compatibility Summary

| File Type                    | Magic      | Version | Decompression |
|------------------------------|------------|---------|---------------|
| Release PS3                  | `IWffu100` | `0x10D` | Supported     |
| Release Xbox 360 (Unsigned)  | `IWffu100` | `0x10D` | Supported     |
| Release Xbox 360 (Signed)    | `IWff0100` | `0x10D` | Supported (authed chunks) |
| Release PC SP (Unsigned)     | `IWffu100` | `0x114` | Supported     |
| Release PC MP/Patch (Signed) | `IWff0100` | `0x114` | Supported (authed chunks) |
| Dev Build (Unsigned)         | `IWffu100` | `0xFD`  | Supported     |
| Dev Build (Signed)           | `IWff0100` | `0xFD`  | Not Supported |

---

## References

- [COD Research Wiki](https://codresearch.dev/)
- [OpenAssetTools](https://github.com/Laupetin/OpenAssetTools)
