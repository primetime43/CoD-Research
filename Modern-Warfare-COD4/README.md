# CoD4: Modern Warfare Research

Documentation for Call of Duty 4: Modern Warfare file formats, memory offsets, and modding info.

## Contents

### FastFiles
- [FastFile Format](FastFiles/FastFiles.md) - .ff compression and structure
- [Zone Files](FastFiles/Zone.md) - Decompressed zone structure
- [Asset Types](FastFiles/AssetTypes.md) - Asset IDs by platform

### Assets
- [RawFile](FastFiles/Assets/RawFile.md) - Scripts and config files
- [Localize](FastFiles/Assets/Localize.md) - Localized strings
- [StringTable](FastFiles/Assets/StringTable.md) - CSV data tables
- [Weapon](FastFiles/Assets/Weapon.md) - Weapon definitions
- [XModel](FastFiles/Assets/XModel.md) - 3D models
- [XAnim](FastFiles/Assets/XAnim.md) - Animations
- [Material](FastFiles/Assets/Material.md) - Material definitions
- [Image](FastFiles/Assets/Image.md) - Textures
- [Sound](FastFiles/Assets/Sound.md) - Sound definitions
- [FX](FastFiles/Assets/FX.md) - Visual effects
- [Menu](FastFiles/Assets/Menu.md) - UI menus
- [MapEnts](FastFiles/Assets/MapEnts.md) - Map entities

### PS3
- [Client Offsets](PS3/Offsets/Clients.md) - Memory offsets for PS3 modding
- [Sound List](PS3/Sounds.md) - Multiplayer sound references

## Quick Reference

### FastFile Versions
| Platform | Version |
|----------|---------|
| PS3 | 0x00000001 |
| Xbox 360 | 0x00000001 |
| PC | 0x00000005 |
| Wii | 0x000001A2 |

### Common Asset IDs (PS3)
| Asset | ID |
|-------|-----|
| rawfile | 0x21 |
| stringtable | 0x22 |
| localize | 0x18 |
| weapon | 0x19 |

## Other Games

- [`World-at-War-COD5`](../World-at-War-COD5/) - World at War documentation
- [Main index](../README.md) - All games

## Resources

- [codresearch.dev](https://codresearch.dev) - Community wiki
- [OpenAssetTools](https://github.com/Laupetin/OpenAssetTools) - Reference implementation
