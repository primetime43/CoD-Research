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

| Magic       | Hex Bytes                          | Description                |
|-------------|------------------------------------|----------------------------|
| `IWffu100`  | `49 57 66 66 75 31 30 30`          | Unsigned (PS3, PC)         |
| `IWff0100`  | `49 57 66 66 30 31 30 30`          | Signed (Xbox 360)          |

---

## Version Numbers

Version numbers identify the game/engine version. They're stored at offset 0x08 in the header.

| Build Type     | Version | Hex Value   | Description                    |
|----------------|---------|-------------|--------------------------------|
| Release        | 269     | `0x10D`     | Retail MW2                     |
| PC Release     | 276     | `0x114`     | PC version                     |
| Dev Build      | 253     | `0xFD`      | Pre-release development build  |

### Platform Endianness

| Platform       | Hex Bytes (Release 0x10D)         |
|----------------|-----------------------------------|
| PS3            | `00 00 01 0D` (Big Endian)        |
| Xbox 360       | `00 00 01 0D` (Big Endian)        |
| PC             | `0D 01 00 00` (Little Endian)     |

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

### Signed Xbox 360 Files

Signed Xbox 360 FastFiles have compressed data starting at a variable offset after the signature block. The compressed data is typically a single zlib stream rather than multiple blocks.

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
| Release Xbox 360 (Signed)    | `IWff0100` | `0x10D` | Supported     |
| Release PC                   | `IWffu100` | `0x114` | Supported     |
| Dev Build (Unsigned)         | `IWffu100` | `0xFD`  | Supported     |
| Dev Build (Signed)           | `IWff0100` | `0xFD`  | Not Supported |

---

## References

- [COD Research Wiki](https://codresearch.dev/)
- [OpenAssetTools](https://github.com/Laupetin/OpenAssetTools)
