# Zone File Structure (CoD4)

The zone file is the decompressed contents of a FastFile. It contains the asset pool, script strings, and all game assets.

## Header

The zone header is **52 bytes** (`0x34`) and consists of two structures: **XFile**
(memory block allocation, `0x00`–`0x23`) and **XAssetList** (asset metadata,
`0x24`–`0x33`). All values are big-endian on PS3/Xbox 360.

| Offset | Size | Field | Description |
|--------|------|-------|-------------|
| 0x00 | 4 | ZoneSize | Total zone data size (= zone bytes − 36) |
| 0x04 | 4 | ExternalSize | External allocation (usually 0) |
| 0x08 | 4 | BlockSizeTemp | **MemAlloc1** — XFILE_BLOCK_TEMP |
| 0x0C | 4 | BlockSizePhysical | Usually 0 |
| 0x10 | 4 | BlockSizeRuntime | Usually 0 |
| 0x14 | 4 | BlockSizeVirtual | Usually 0 |
| 0x18 | 4 | BlockSizeLarge | XFILE_BLOCK_LARGE allocation |
| 0x1C | 4 | BlockSizeCallback | Usually 0 |
| 0x20 | 4 | BlockSizeVertex | **MemAlloc2** — XFILE_BLOCK_VERTEX (PS3/PC) |
| 0x24 | 4 | ScriptStringCount | Number of script strings (tags) |
| 0x28 | 4 | ScriptStringsPtr | `FF FF FF FF` placeholder |
| 0x2C | 4 | AssetCount | Number of assets in the pool |
| 0x30 | 4 | AssetsPtr | `FF FF FF FF` placeholder |

The asset pool begins at **0x34**, immediately after the header.

> **Earlier revisions of this page placed AssetCount at 0x28 and the string count at
> 0x30 — that was off by one field.** AssetCount is at **0x2C**; ScriptStringCount is at
> **0x24**. Both `0x28` and `0x30` hold `FF FF FF FF` pointer placeholders, which is what
> made the offsets easy to miscount.

### Memory Allocation Values

These values must be correct for the game to load the zone:

| Value | Offset | Bytes (BE) |
|-------|--------|------------|
| MemAlloc1 (BlockSizeTemp) | 0x08 | `00 00 0F 70` |
| MemAlloc2 (BlockSizeVertex) | 0x20 | `00 00 00 00` |

## Asset Pool

Starts at offset 0x34. Each asset record is 8 bytes:

| Offset | Size | Description |
|--------|------|-------------|
| 0x00 | 4 | Asset type ID (see AssetTypes.md) |
| 0x04 | 4 | Memory pointer (usually `FF FF FF FF`) |

The pointer value `FF FF FF FF` is a placeholder that gets replaced with actual memory addresses when the game loads the zone.

### Asset Count

The asset count at offset **0x2C** should match:
```
AssetCount = RawFileCount + LocalizeCount + StringTableCount + OtherAssets + 1
```

The `+1` is for a final terminating entry. Missing this causes infinite loading.

## Script Strings

Script strings (also called "tags") are listed by `ScriptStringCount` at offset **0x24**.
When present, the null-terminated tag strings sit **between the header and the asset
pool** (starting at 0x34), so the pool is pushed back past them. Patch zones with no tags
(`ScriptStringCount = 0`) have the asset pool start at 0x34 directly.

## Asset Data

Following the headers and string table, each asset's data appears in order:

### Rawfile Structure
- 4-byte name pointer (`FF FF FF FF`)
- 4-byte content length
- 4-byte content pointer (`FF FF FF FF`)
- Null-terminated name string
- File content bytes

### Localize Structure
- 4-byte name pointer (`FF FF FF FF`)
- 4-byte value pointer (`FF FF FF FF`)
- Null-terminated reference string
- Null-terminated localized string

### StringTable Structure
- 4-byte name pointer
- 4-byte column count
- 4-byte row count
- Column/row data pointers
- Null-terminated name string
- Cell values

## Building a Zone

When building a zone from scratch:

1. Calculate total asset count (including final entry)
2. Write header with correct MemAlloc values
3. Write asset pool records (8 bytes each)
4. Write script strings
5. Write asset data in order

## References

- [codresearch.dev - FastFiles and Zone files](https://codresearch.dev/index.php/FastFiles_and_Zone_files_(MW2))
