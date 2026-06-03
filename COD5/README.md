# Call of Duty: World at War Research Wiki

Research information and technical documentation for **Call of Duty: World at War (CoD5/WaW)**.

## Purpose

This research wiki preserves community knowledge collected over the years from various forum threads and posts—much of which has become difficult to access or lost as forums have been shut down—and organizes it in a central location.

## Supported Platforms

Documentation covers platform-specific differences where applicable:
- **PS3** - Big-endian, includes all shader types
- **Xbox 360** - Big-endian, signed MP files, no vertexshader
- **PC** - Little-endian, no pixelshader or vertexshader

## Directory

### FastFiles
Documentation on the FastFile (.ff) archive format and Zone files.

- [FastFiles.md](FastFiles/FastFiles.md) - FastFile compression/decompression format
- [Zone.md](FastFiles/Zone.md) - Zone file structure (header, asset pool, memory pointers)
- [AssetTypes.md](FastFiles/AssetTypes.md) - Asset type IDs for WaW/CoD4/MW2 (platform-specific)

### Asset Documentation
- [Assets/RawFile.md](FastFiles/Assets/RawFile.md) - Raw script/config files
- [Assets/Localize.md](FastFiles/Assets/Localize.md) - Localized string entries
- [Assets/StringTable.md](FastFiles/Assets/StringTable.md) - CSV-style string tables
- [Assets/Weapon.md](FastFiles/Assets/Weapon.md) - Weapon definitions
- [Assets/XAnim.md](FastFiles/Assets/XAnim.md) - Animation data
- [Assets/Image.md](FastFiles/Assets/Image.md) - Texture images
- [Assets/Material.md](FastFiles/Assets/Material.md) - Material/shader definitions
- [Assets/Menu.md](FastFiles/Assets/Menu.md) - Menu assets
- [Assets/MapEnts.md](FastFiles/Assets/MapEnts.md) - Map entity data
- And more...

### Memory Research
- [Offsets/](Offsets/) - Memory offsets for PS3 and PC
- [Structs/](Structs/) - Data structures used by the game engine

### Other
- [Ultimate_WaW_DVAR_List.md](Ultimate_WaW_DVAR_List.md) - Comprehensive list of DVars

## External References

- [COD Research Wiki](https://codresearch.dev/) - Community wiki with additional documentation
- [OpenAssetTools](https://github.com/Laupetin/OpenAssetTools) - FastFile parsing/compilation tools

## Contributing

Contributions are welcome! If you have additional research or corrections, please submit a pull request.
