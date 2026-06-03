# Zone Files (Call of Duty: Ghosts / IW6)

A zone is the decompressed content of a Ghosts FastFile. The structure follows the same
general shape as CoD4/WaW/MW2 — a fixed XFile header whose counts drive navigation
through a tag region and then the asset pool — but with IW6-specific details. All values
are **big-endian** (PS3 is PowerPC). Verified against PS3 retail patch, DLC, and base
zones only.

> **Two-level compression:** the outer FastFile deflate (see [FastFiles.md](FastFiles.md))
> produces the *raw* zone, in which many assets are still individually **zlib-compressed**.
> A complete extraction expands those inner streams inline, leaving readable content.

---

## Zone Header

```
0x00..0x27   Fixed XFile fields (zone size, block sizes, …)
0x28..0x2B   tagCount    (BE u32)
0x2C..0x2F   placeholder (0xFFFFFFFF)
0x30..0x33   assetCount  (BE u32)
0x34..0x37   placeholder (0xFFFFFFFF)
0x38..?      Either count3 + placeholder (zones with tagCount > 0),
             or the first pool entry (patch FFs where tagCount == 0)
```

The two fields that matter for navigation are **`tagCount` @ 0x28** and
**`assetCount` @ 0x30**. The remaining u32s in `0x00..0x27` are the zone size and block
size slots; their exact roles are unconfirmed and the pool walker does not need them.

---

## Locating the Asset Pool

After the header comes a tag region (tag pointer placeholders + null-terminated tag
strings), then the asset pool. The pool location is driven by `tagCount`:

1. **`tagCount == 0`** → pool starts at **0x38**, immediately after the `assetCount`
   placeholder. Covers most patch FFs.
2. **`tagCount > 0`** → skip `tagCount × 4` placeholder bytes (starting at 0x3C), then
   skip `tagCount` null-terminated tag strings; the pool follows. A small trailing field
   can sit between the last tag string and `pool[0]` (e.g. `00 00 00 30` in a DLC zone) —
   a 32-byte forward probe skips it.
3. **Fallback** — brute-scan for the longest run of valid pool entries when header counts
   are missing or the layout is unexpected.

### Verified counts

| Sample | tagCount | assetCount | Pool offset |
|--------|---------:|-----------:|------------:|
| `patch_mp_dome_ns.zone` | 0 | 4 | 0x38 |
| `ghosts_patch_common_mp.zone` | 0 | 122 | 0x38 |
| `mp_character_room_dlc_updated.zone` | 212 | 1880 | 0xED5 |
| `patch_ui_mp.zone` | 249 | 1256 | 0x1997 |

> The header `assetCount` is consistently one greater than the number of entries the
> walker locates — the last "entry" reads like a sentinel without a valid type byte. The
> impact is cosmetic.

---

## Asset Pool Records

Each entry is **8 bytes**, in **`[pointer placeholder][type ID]`** order (pointer first —
matching MW2 PS3, *not* the `[type][ptr]` order used by MW2 PC):

```
PP PP PP PP  00 00 00 XX
└─ pointer ┘ └─ type ─┘     type word: BE u32, high 3 bytes zero, low byte = type ID (≤ 0x35)
```

The pointer field uses **four observed conventions** — any 4-byte value is accepted; the
strict type-word structure plus the header `assetCount` are what delimit the pool:

| Pattern | Meaning |
|---------|---------|
| `FF FF FF FF` | Standard inline placeholder (most common) |
| `00 00 00 00` | NULL — e.g. the first scriptfile entry of `patch_mp_prisonbreak.zone` |
| `80 XX XX XX` | High-bit-set resolved pointer (CoD4/WaW convention) |
| `40 XX XX XX` | **0x40-flagged resolved pointer — IW6-specific** (e.g. `40 1F DF 85` in `patch_ui_mp.zone` material→image references). Code that only accepted the `0x80` form found just 87 of 1256 entries in that zone. |

See [AssetTypes.md](AssetTypes.md) for the type IDs.

---

## Per-Asset Data

Each asset is a small header followed by its content. Two zlib-wrapped header shapes have
been observed, distinguished by how many `0xFF` bytes precede the name. **`luafile` uses
a separate flat layout** (below).

### "Short" shape — 16 bytes (rawfile, type 0x28)

```
[FF FF FF FF][compLen u32 BE][decLen u32 BE][FF FF FF FF]<name>\0<zlib stream>
```

Worked example — `vision/mp_alien_town_thermal.vision`:

```
FF FF FF FF 00 00 03 39 00 00 0E E7 FF FF FF FF
└─ FF*4 ──┘ └─compLen─┘ └─ decLen─┘ └─ FF*4 ──┘
             = 825        = 3,815
```

Body is an 825-byte zlib stream decompressing to 3,815 bytes of ASCII vision config.

### "Long" shape — 28 bytes (scriptfile, type 0x29)

```
[FF FF FF FF][compLen u32 BE][decLen u32 BE][??? u32 BE][FF FF FF FF FF FF FF FF]<name>\0<zlib stream>
```

The third u32 is **not a size** (observed values range from below `decLen` to far above
it). Its meaning is unconfirmed.

### Key facts (verified across 20+ assets)

- The first u32 after the leading-`FF` block is **always the exact zlib stream byte
  count** (`compLen`); `decLen` matches `len(zlib.decompress(stream))`.
- Asset bodies are either a standard zlib stream (`0x78 DA` / `9C` / `5E` / `01`) — used
  by every scriptfile and rawfile in patch zones — or flat binary (xmodel / image / sound
  / world data), which the outer deflate already compresses once.
- For enumeration, **walk the pool**, don't scan for zlib magic — dense binary assets can
  contain byte sequences that look like zlib streams.

### Luafile bodies (type 0x32) — flat 16-byte header, NOT zlib-wrapped

```
[FF FF FF FF][size u32 BE][unk u32][FF FF FF FF]<name>\0<Lua 5.1 bytecode>
```

- `size` is the exact byte count of the bytecode body.
- `unk` is consistently `0x02000000` (purpose unconfirmed).
- Name is path-style ASCII ending in `.lua`.
- Bodies start with the Lua 5.1 signature `1B 4C 75 61 51` (`\x1B LuaQ`).

#### IW6 Lua bytecode (custom format byte `0x0D`)

The Lua **source is not in the FF** — IW6 ships compiled bytecode only. The 12-byte Lua
header is standard except the format byte:

| Offset | Value | Meaning |
|--------|-------|---------|
| 0x00 | `1B` | escape |
| 0x01..0x03 | `Lua` | signature |
| 0x04 | `51` | Lua 5.1 |
| 0x05 | **`0D`** | **format byte — non-zero = IW6 custom dialect** |
| 0x06 | `00` | endianness flag (big-endian) |
| 0x07 | `04` | sizeof(int) |
| 0x08 | `04` | sizeof(size_t) |
| 0x09 | `04` | sizeof(Instruction) |
| 0x0A | `04` | sizeof(lua_Number) — **single-precision** (stock Lua 5.1 default is 8) |
| 0x0B | `00` | integral flag (floating-point) |

Past the 12-byte header the chunk layout diverges from stock Lua 5.1 and is **not fully
reverse-engineered**. Practical tooling ASCII-scans the bytecode for printable strings
(menu / widget / function / identifier names) rather than decompiling. Note: JariK's
`CoDLuaDecompiler` lists Ghosts as supported but **crashes on retail IW6 luafiles** — it
reads the constant count as little-endian and overruns on IW6's big-endian count.

---

## Asset Type Coverage

Bodies that can be parsed today: **rawfile** (short zlib header), **scriptfile** (long
zlib header), and **luafile** (flat header → extracted-strings summary). Flat-binary types
(xmodel / image / sound / techset / weapon / material / …) are listed in the pool with
type names but their internal structs are not parsed — each would need its own
reverse-engineering.

---

## Known Unknowns

- Exact roles of the XFile header u32s in `0x00..0x27` (zone size + block sizes).
- The base-FF 12 KB index table record format.
- The "LO" region encoding (bit 7 is deliberately zero across all 112 KB).
- The long-shape header's third u32.
- The luafile `unk` field (`0x02000000`).
- The full IW6 Lua bytecode chunk format.
- Internal struct layouts for non-wrapped asset types.
- Non-PS3 platforms (Xbox 360 / Wii U / PC use shifted asset IDs; untested).

---

## References

- [FastFiles.md](FastFiles.md) — container format and decompression
- [AssetTypes.md](AssetTypes.md) — IW6 PS3 asset type IDs
- [COD Research Wiki](https://codresearch.dev/)
