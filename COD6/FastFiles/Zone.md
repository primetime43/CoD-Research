# Zone Files (Modern Warfare 2)

## What is a Zone File?

A zone file is the **decompressed content** inside a FastFile. It contains the actual game assets (scripts, localized strings, textures, etc.) in a structured binary format.

**Key Characteristics:**
- Zone files are extracted by decompressing the FastFile's zlib blocks
- PS3/Xbox 360 use **Big Endian** byte order
- The zone header contains memory allocation hints used by the game engine

---

## C Structure Definitions

```c
enum
{
  XFILE_BLOCK_TEMP = 0,
  XFILE_BLOCK_PHYSICAL = 1,
  XFILE_BLOCK_RUNTIME = 2,
  XFILE_BLOCK_VIRTUAL = 3,
  XFILE_BLOCK_LARGE = 4,
  XFILE_BLOCK_CALLBACK = 5,
#if defined(PC) || defined(PS3)
  XFILE_BLOCK_VERTEX = 6,
  #ifdef PC
  XFILE_BLOCK_INDEX = 7,
  #endif
#endif
  MAX_XFILE_COUNT
};

struct XFile
{
  int size;
  int externalSize;
  int blockSize[MAX_XFILE_COUNT];
};

struct XAsset
{
  XAssetType type;
  XAssetHeader *header;
};

struct XAssetList
{
  int scriptStringCount;
  const char **scriptStrings;
  int assetCount;
  XAsset *assets;
};
```

**Note on XAsset Binary Format**: While the C struct defines `type` before `header`, in MW2 PS3 mod zone files the binary format is `[header/ptr][type]` (`FF FF FF FF 00 00 00 XX`). This was verified by comparing working vs non-working mod files.

---

## Zone Header Structure

### MW2 PS3 Zone Header (52 bytes)

MW2 PS3 uses the **same 52-byte (`0x34`) header layout as CoD4/WaW** — XFile
(`0x00`–`0x23`) + XAssetList (`0x24`–`0x33`):

| Offset | Size | Field | Description |
|--------|------|-------|-------------|
| 0x00 | 4 | totalSize1 (ZoneSize) | Points to footer header start |
| 0x04 | 4 | externalSize | External allocation (usually 0) |
| 0x08 | 4 | blockSizeTemp | Memory allocation hint (`0x000003B4` for MW2) |
| 0x0C | 4 | blockSizePhysical | Usually 0 |
| 0x10 | 4 | blockSizeRuntime | Usually 0 |
| 0x14 | 4 | blockSizeVirtual | Usually 0 |
| 0x18 | 4 | totalSize2 (BlockSizeLarge) | Points to end of zone data |
| 0x1C | 4 | blockSizeCallback | Usually 0 |
| 0x20 | 4 | blockSizeVertex | Memory allocation hint (`0x00001000` for MW2) |
| 0x24 | 4 | scriptStringCount | Number of script strings (usually 0 for patches) |
| 0x28 | 4 | scriptStringsPtr | `FFFFFFFF` placeholder |
| 0x2C | 4 | assetCount | Number of assets in the zone |
| 0x30 | 4 | assetsPtr | `FFFFFFFF` placeholder |

The asset pool begins at **0x34**.

> **Correction:** Earlier revisions of this page described the MW2 PS3 header as **48
> bytes** with the pool starting at `0x30`. That was off by one field — MW2 PS3 actually
> uses the full 52-byte XFile+XAssetList layout (matching CoD4/WaW), with the `assetsPtr`
> placeholder at `0x30` and the pool at `0x34`. The genuinely MW2-specific finding
> (verified against working vs. broken mods) is the **asset-entry order** `[ptr][type]`,
> which is unaffected by this correction. MW2 **Xbox 360** is the 48-byte variant (it
> drops `blockSizeVertex`) — see below.

### Memory Allocation Values by Game

| Game | blockSizeTemp (0x08) | blockSizeVertex (0x20) |
|------|---------------------|------------------------|
| CoD4 | `0x00000F70` | `0x00000000` |
| WaW | `0x000010B0` | `0x0005F8F0` |
| MW2 | `0x000003B4` | `0x00001000` |

These values are required - incorrect values cause game crashes.

### Size Field Calculations

```
totalSize1 = headerSize + assetTableSize + rawFilesSize + localizedSize
totalSize2 = headerSize + assetTableSize + rawFilesSize + localizedSize + footerSize
```

Where:
- `headerSize` = 52 bytes for MW2 PS3 and CoD4/WaW (48 for MW2 Xbox 360, 56 for MW2 PC)
- `assetTableSize` = `assetCount * 8` bytes
- `rawFilesSize` = sum of all raw file entries (headers + names + data)
- `footerSize` = 16 bytes + zone name length + 2 null terminators

---

## Platform Zone Header Variants (Xbox 360 / PC)

The PS3 layout above is one of three. The console/PC variants differ in size, which
block-size slots exist, and (on PC) byte order. All three agree that **`assetCount`
precedes the asset pool**, but the absolute offsets shift.

### MW2 Xbox 360 Zone Header (48 bytes, big-endian)

Xbox 360 drops `BlockSizeVertex`, so the XFile block is 4 bytes shorter and the
`XAssetList` fields shift down by 4 vs PS3:

| Offset | Field |
|--------|-------|
| 0x08 | blockSizeTemp |
| 0x20 | scriptStringCount |
| 0x24 | scriptStringsPtr (`FFFFFFFF`) |
| 0x28 | assetCount |
| 0x2C | assetsPtr (`FFFFFFFF`) |
| 0x30 | asset pool start |

Asset IDs use the `MW2AssetTypeXbox360` enum (no `vertexshader`; IDs ≥ `0x07` shift −1
from PS3). Entry order is `[ptr][type]` big-endian, same as PS3.

### MW2 PC Zone Header (56 bytes, little-endian)

MW2 PC adds a `blockSizeIndex` slot (8 blockSize slots total) — the same geometry as
**Wii WaW**, just little-endian. The asset pool starts at `0x38`.

| Offset | Field |
|--------|-------|
| 0x08 | blockSizeTemp |
| 0x20 | blockSizeVertex |
| 0x24 | blockSizeIndex (PC/Wii only) |
| 0x28 | scriptStringCount |
| 0x2C | scriptStringsPtr (`FFFFFFFF`) |
| 0x30 | assetCount |
| 0x34 | assetsPtr (`FFFFFFFF`) |
| 0x38 | asset pool start |

All values are **little-endian**. The asset entry order is **`[type LE][ptr]`** —
*type first*, the opposite of PS3/Xbox 360:

```
MW2 PC:   XX 00 00 00 FF FF FF FF     ([type LE][ptr])
MW2 PS3:  FF FF FF FF 00 00 00 XX     ([ptr][type BE])
```

Rawfile `compressedLen` / `len` size fields are also **little-endian** on PC (reading
them big-endian yields GB-scale nonsense). Asset IDs use the `MW2AssetTypePC` enum
(both `vertexshader` at `0x07` and `vertexdecl` at `0x08`; IDs ≥ `0x09` shift +1 from PS3).

Sample headers verified from retail files:

| File | scriptStringCount @0x28 | assetCount @0x30 |
|------|------------------------:|-----------------:|
| code_post_gfx.zone | 4 | 1,900 |
| common.zone | 524 | 9,288 |
| patch_mp.zone | 11 | 225 |

---

## Asset Table

The asset table immediately follows the zone header (at offset 0x34 for MW2 PS3 with no script strings).

### Entry Format (8 bytes per entry)

**MW2 PS3 mod files** use `[ptr][type]` format:

| Offset | Size | Field | Value |
|--------|------|-------|-------|
| 0x00 | 4 | pointer | `0xFFFFFFFF` (runtime placeholder) |
| 0x04 | 4 | type | `0x000000XX` (asset type ID, big-endian) |

```
Example: FF FF FF FF 00 00 00 23  (rawfile, type 0x23)
```

**Verified**: This format was confirmed by comparing working vs broken mod zone files.

### Asset Type IDs (MW2 PS3)

| Type | ID (Decimal) | ID (Hex) |
|------|--------------|----------|
| rawfile | 35 | 0x23 |
| localize | 26 | 0x1A |

### Asset Table Layout

```
[Header: 52 bytes]
[Asset Entry 0: rawfile]   FF FF FF FF 00 00 00 23
[Asset Entry 1: rawfile]   FF FF FF FF 00 00 00 23
...
[Asset Entry N: rawfile]   FF FF FF FF 00 00 00 23  <- Final entry (for footer)
[Raw File Data...]
[Localized Data...]
[Footer]
```

The asset count includes: raw files + localized entries + 1 final entry for the footer.

---

## Raw File Format

Raw files in MW2 PS3 zones can be compressed using zlib.

### First Raw File Header (20 bytes)

| Offset | Size | Field | Description |
|--------|------|-------|-------------|
| 0x00 | 4 | marker1 | `0xFFFFFFFF` |
| 0x04 | 4 | marker2 | `0xFFFFFFFF` |
| 0x08 | 4 | compressedLen | Compressed data size (big-endian) |
| 0x0C | 4 | uncompressedLen | Original data size (big-endian) |
| 0x10 | 4 | pointer | `0xFFFFFFFF` |
| 0x14 | var | filename | Null-terminated ASCII string |
| var | var | data | Zlib-compressed data |

### Subsequent Raw File Headers (16 bytes)

| Offset | Size | Field | Description |
|--------|------|-------|-------------|
| 0x00 | 4 | marker | `0xFFFFFFFF` |
| 0x04 | 4 | compressedLen | Compressed data size (big-endian) |
| 0x08 | 4 | uncompressedLen | Original data size (big-endian) |
| 0x0C | 4 | pointer | `0xFFFFFFFF` |
| 0x10 | var | filename | Null-terminated ASCII string |
| var | var | data | Zlib-compressed data |

### Raw File Packing

**Critical**: Raw files are packed **tightly with NO separators**. Each file's header starts immediately after the previous file's data.

```
[File 0: 20-byte header][filename\0][compressed data]
[File 1: 16-byte header][filename\0][compressed data]  <- No gap!
[File 2: 16-byte header][filename\0][compressed data]
...
```

### Compression Detection

A raw file is compressed if:
1. `compressedLen > 0`
2. `compressedLen != uncompressedLen`
3. Data starts with zlib header (`0x78` + `0x01`, `0x5E`, `0x9C`, or `0xDA`)

---

## Localized Strings

Localized string entries follow the raw files section.

### Entry Format

| Offset | Size | Field | Description |
|--------|------|-------|-------------|
| 0x00 | 4 | marker1 | `0xFFFFFFFF` |
| 0x04 | 4 | marker2 | `0xFFFFFFFF` |
| 0x08 | var | value | Null-terminated localized text |
| var | var | key | Null-terminated reference key |

```
FF FF FF FF FF FF FF FF [localized text\0] [reference key\0]
```

---

## Footer

The footer is the final entry in the zone, containing the zone name.

### MW2 PS3 Footer (16 bytes + name)

| Offset | Size | Field | Value |
|--------|------|-------|-------|
| 0x00 | 4 | marker | `0xFFFFFFFF` |
| 0x04 | 4 | compressedLen | `0x00000000` |
| 0x08 | 4 | uncompressedLen | `0x00000000` |
| 0x0C | 4 | pointer | `0xFFFFFFFF` |
| 0x10 | var | zoneName | Null-terminated + extra null |

```
FF FF FF FF 00 00 00 00 00 00 00 00 FF FF FF FF [zonename] 00 00
```

The `totalSize1` field in the zone header points to offset 0x00 of this footer.

---

## Differences from CoD4/WaW

| Feature | CoD4/WaW | MW2 PS3 |
|---------|----------|---------|
| Zone header size | 52 bytes | 52 bytes (Xbox 360: 48, PC: 56) |
| Asset table entry format | `[ptr][type]` | `[ptr][type]` |
| Raw file asset type | 0x21 (CoD4), 0x22 (WaW) | 0x23 |
| Localize asset type | 0x18 (CoD4), 0x19 (WaW) | 0x1A |
| First raw file header | 12 bytes | 20 bytes |
| Subsequent raw file headers | 12 bytes | 16 bytes |
| Footer size | 12 bytes | 16 bytes |
| Raw file compression | No | Yes (zlib) |

> MW2 PS3's zone **header** is the same 52-byte layout as CoD4/WaW. What's genuinely
> different on MW2 is the **raw file framing** (compressed, larger headers) and the
> per-platform header variants (Xbox 360 drops `blockSizeVertex` → 48 bytes; PC adds
> `blockSizeIndex` and is little-endian → 56 bytes).

---

## Common Pitfalls

### 1. Adding Null Separators Between Raw Files
**Wrong**: Adding `0x00` after each raw file's data
**Correct**: Raw files are packed tightly with no separators

### 2. Using Wrong Zone Header Size
**Wrong**: Treating MW2 PS3 as a 48-byte header (pool starting at `0x30`)
**Correct**: MW2 PS3 uses the 52-byte CoD4/WaW layout — `assetCount` at `0x2C`,
`assetsPtr` placeholder at `0x30`, asset pool at `0x34`. (48 bytes is the MW2 **Xbox 360**
variant, which drops `blockSizeVertex`.)

### 3. Wrong First Raw File Header Size
**Wrong**: Using 16-byte header for first file
**Correct**: First file needs 20-byte header (has extra FFFFFFFF marker)

### 4. Wrong Asset Table Entry Order
**Wrong**: `[type][ptr]` format (`00 00 00 23 FF FF FF FF`)
**Correct**: `[ptr][type]` format (`FF FF FF FF 00 00 00 23`)

### 5. Incorrect Memory Allocation Values
**Wrong**: Using CoD4/WaW values for MW2
**Correct**: MW2 requires `0x03B4` at offset 0x08 and `0x1000` at offset 0x20

---

## References

- Verified through analysis of working MW2 PS3 mod files
- Tested with RPCS3 emulator
