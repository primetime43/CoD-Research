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

### MW2 PS3 Zone Header (48 bytes)

| Offset | Size | Field | Description |
|--------|------|-------|-------------|
| 0x00 | 4 | totalSize1 | Points to footer header start |
| 0x04 | 4 | externalSize | External allocation (usually 0) |
| 0x08 | 4 | blockSizeTemp | Memory allocation hint (`0x000003B4` for MW2) |
| 0x0C | 4 | blockSizePhysical | Usually 0 |
| 0x10 | 4 | blockSizeRuntime | Usually 0 |
| 0x14 | 4 | blockSizeVirtual | Usually 0 |
| 0x18 | 4 | totalSize2 | Points to end of zone data |
| 0x1C | 4 | blockSizeCallback | Usually 0 |
| 0x20 | 4 | blockSizeVertex | Memory allocation hint (`0x00001000` for MW2) |
| 0x24 | 4 | scriptStringCount | Number of script strings (usually 0 for patches) |
| 0x28 | 4 | scriptStringsPtr | Pointer to script strings (usually 0) |
| 0x2C | 4 | assetCount | Number of assets in the zone |

**Important**: MW2 PS3 zone header is **48 bytes** (ends at offset 0x2F). The `FFFFFFFF` at offset 0x30 is the first asset table entry, NOT a header marker.

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
- `headerSize` = 48 bytes for MW2, 52 bytes for CoD4/WaW
- `assetTableSize` = `assetCount * 8` bytes
- `rawFilesSize` = sum of all raw file entries (headers + names + data)
- `footerSize` = 16 bytes + zone name length + 2 null terminators

---

## Asset Table

The asset table immediately follows the zone header (at offset 0x30 for MW2 with no script strings).

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
[Header: 48 bytes]
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
| Zone header size | 52 bytes | 48 bytes |
| Header trailing FFFFFFFF | Yes | No |
| Asset table entry format | `[ptr][type]` | `[ptr][type]` |
| Raw file asset type | 0x21 (CoD4), 0x22 (WaW) | 0x23 |
| Localize asset type | 0x18 (CoD4), 0x19 (WaW) | 0x1A |
| First raw file header | 12 bytes | 20 bytes |
| Subsequent raw file headers | 12 bytes | 16 bytes |
| Footer size | 12 bytes | 16 bytes |
| Raw file compression | No | Yes (zlib) |

---

## Common Pitfalls

### 1. Adding Null Separators Between Raw Files
**Wrong**: Adding `0x00` after each raw file's data
**Correct**: Raw files are packed tightly with no separators

### 2. Using Wrong Zone Header Size
**Wrong**: 52-byte header with trailing `0xFFFFFFFF`
**Correct**: 48-byte header for MW2 (the FFFFFFFF at 0x30 is part of asset table)

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
