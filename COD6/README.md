# Call of Duty: Modern Warfare 2 Research Wiki

Research information and technical documentation for **Call of Duty: Modern Warfare 2 (CoD6/MW2)**.

## Purpose

This research wiki preserves community knowledge collected over the years from various forum threads and posts—much of which has become difficult to access or lost as forums have been shut down—and organizes it in a central location.

## Supported Platforms

Documentation covers platform-specific differences where applicable:
- **PS3** - Big-endian, includes pixelshader and vertexshader
- **Xbox 360** - Big-endian, signed MP files, no vertexshader
- **PC** - Little-endian, includes all shader types plus vertexdecl

## Directory

### FastFiles
Documentation on the FastFile (.ff) archive format and Zone files.

- [FastFiles.md](FastFiles/FastFiles.md) - FastFile compression/decompression format, DB_Header structure
- [Zone.md](FastFiles/Zone.md) - Zone file structure (header, asset pool, raw files, footer)
- [AssetTypes.md](FastFiles/AssetTypes.md) - Complete asset type IDs for all platforms

### Key MW2 Differences from CoD4/WaW

| Feature | CoD4/WaW | MW2 |
|---------|----------|-----|
| FF Version | 0x01 (CoD4), 0x183 (WaW) | 0x10D |
| Zone Header | 52 bytes | 48 bytes |
| Raw File Compression | No | Yes (zlib) |
| First Raw File Header | 12 bytes | 20 bytes |
| Two-Level Compression | No | Yes (FF blocks + zone files) |

## External References

- [COD Research Wiki](https://codresearch.dev/) - Community wiki with additional documentation
- [OpenAssetTools](https://github.com/Laupetin/OpenAssetTools) - FastFile parsing/compilation tools
- [CoD-FF-Tools](https://github.com/primetime43/CoD-FF-Tools) - FastFile editor for PS3/Xbox 360

## Contributing

Contributions are welcome! If you have additional research or corrections, please submit a pull request.
