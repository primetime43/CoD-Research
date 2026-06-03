# Call of Duty: Ghosts Research Wiki

Research information and technical documentation for **Call of Duty: Ghosts (CoD10 / IW6)**.

## Purpose

This research wiki preserves community knowledge collected over the years from various
forum threads and posts—much of which has become difficult to access or lost as forums
have been shut down—and organizes it in a central location.

## Supported Platforms

Format documentation here is verified against **PS3 retail** samples only. Xbox 360,
Wii U, and PC variants use shifted asset type IDs and have not been tested.

## Directory

### FastFiles
Documentation on the IW6 FastFile (.ff) signed container format and Zone files.

- [FastFiles.md](FastFiles/FastFiles.md) — FastFile layout, `DB_AuthHeader`, raw-deflate block compression, decompression algorithm
- [Zone.md](FastFiles/Zone.md) — Zone structure, asset pool walking, per-asset headers, IW6 Lua bytecode
- [AssetTypes.md](FastFiles/AssetTypes.md) — IW6 PS3 asset type IDs (0x00–0x35)

### Assets
Dumped asset references from Ghosts (animations, FX, materials, models, sounds, weapons,
DVARs). See the [Assets](Assets/README.md) folder.

### Key IW6 Format Notes

| Aspect | Detail |
|--------|--------|
| Outer magic / version | `IWff0100` + `0x22E` (BE) |
| Inner magic | `IWffS100` (capital S, vs MW3's lowercase) |
| Outer compression | Raw-deflate 64 KB blocks (2-byte BE size prefix) |
| Auth hashes | SHA-1 (20 B + 12 zero-pad) |
| Rebuild support | None — re-signing needs IW's RSA-2048 private key |

## Resources

- [Wii U — Call of Duty: Ghosts (Title Update 112) MP+SP Symbols](https://www.mediafire.com/file/5zlpo95p533bl2r/WiiU+-+Call+of+Duty+Ghosts+%28Title+Update+112%29+MP%2BSP+Symbols.rar)

## External References

- [COD Research Wiki](https://codresearch.dev/) — community wiki with additional documentation
- [OpenAssetTools](https://github.com/Laupetin/OpenAssetTools) — FastFile parsing/compilation tools
- [CoD-FF-Tools](https://github.com/primetime43/CoD-FF-Tools) — FastFile editor; IW6 PS3 decompress/extract support

## Contributing

Contributions are welcome! If you have additional research or corrections, please submit a pull request.
