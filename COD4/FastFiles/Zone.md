# Zone File Structure (CoD4)

The zone file is the decompressed contents of a FastFile. It contains the asset pool, script strings, and all game assets.

## Header

| Offset | Size | Description |
|--------|------|-------------|
| 0x00 | 4 | Size/count field |
| 0x04 | 4 | Size/count field |
| 0x08 | 4 | MemAlloc1 (memory allocation hint) |
| 0x0C | 16 | Padding/reserved |
| 0x1C | 4 | Unknown |
| 0x20 | 4 | MemAlloc2 (memory allocation hint) |
| 0x24 | 4 | Unknown |
| 0x28 | 4 | Asset count |
| 0x2C | 4 | Pointer placeholder (`FF FF FF FF`) |
| 0x30 | 4 | String count |

### Memory Allocation Values

These values must be correct for the game to load the zone:

| Value | Offset | Bytes (BE) |
|-------|--------|------------|
| MemAlloc1 | 0x08 | `00 00 0F 70` |
| MemAlloc2 | 0x20 | `00 00 00 00` |

## Asset Pool

Starts at offset 0x34. Each asset record is 8 bytes:

| Offset | Size | Description |
|--------|------|-------------|
| 0x00 | 4 | Asset type ID (see AssetTypes.md) |
| 0x04 | 4 | Memory pointer (usually `FF FF FF FF`) |

The pointer value `FF FF FF FF` is a placeholder that gets replaced with actual memory addresses when the game loads the zone.

### Asset Count

The asset count at offset 0x28 should match:
```
AssetCount = RawFileCount + LocalizeCount + StringTableCount + OtherAssets + 1
```

The `+1` is for a final terminating entry. Missing this causes infinite loading.

## Script Strings

After the asset pool comes the script string table. The string count at offset 0x30 indicates how many strings follow.

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
