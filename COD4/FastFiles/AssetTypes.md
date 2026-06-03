# Asset Types (CoD4)

## Platform Details

| Platform | Endianness | Shaders | Notes |
|----------|------------|---------|-------|
| PS3 | Big Endian | pixelshader, vertexshader | Both shader types present |
| Xbox 360 | Big Endian | pixelshader | No vertexshader |
| PC | Little Endian | - | No shader assets in zone |
| Wii | Big Endian | - | No shader assets |

Asset type IDs vary by platform due to differences in shader support. This causes all asset IDs after the shader types to shift between platforms.

## Asset Type IDs by Platform

| Asset | PS3 | Xbox 360 | PC | Used | Pool Count |
|-------|-----|----------|-----|------|------------|
| xmodelpieces | 0x00 | 0x00 | 0x00 | No | 0x40 |
| physpreset | 0x01 | 0x01 | 0x01 | Yes | 0x40 |
| xanim | 0x02 | 0x02 | 0x02 | Yes | 0x1000 |
| xmodel | 0x03 | 0x03 | 0x03 | Yes | 0x3E8 |
| material | 0x04 | 0x04 | 0x04 | Yes | 0x800 |
| pixelshader | 0x05 | 0x05 | - | Yes | 0x600 |
| vertexshader | 0x06 | - | - | Yes | 0x200 |
| techset | 0x07 | 0x06 | 0x05 | Yes | 0x200 |
| image | 0x08 | 0x07 | 0x06 | Yes | 0x960 |
| sound | 0x09 | 0x08 | 0x07 | Yes | 0x3E80 |
| sndcurve | 0x0A | 0x09 | 0x08 | Yes | 0x40 |
| loaded_sound | 0x0B | 0x0A | 0x09 | Yes | 0x4B0 |
| col_map_sp | 0x0C | 0x0B | 0x0A | Yes | 1 |
| col_map_mp | 0x0D | 0x0C | 0x0B | Yes | 1 |
| com_map | 0x0E | 0x0D | 0x0C | Yes | 1 |
| game_map_sp | 0x0F | 0x0E | 0x0D | Yes | 1 |
| game_map_mp | 0x10 | 0x0F | 0x0E | Yes | 1 |
| map_ents | 0x11 | 0x10 | 0x0F | Yes | 2 |
| gfx_map | 0x12 | 0x11 | 0x10 | Yes | 1 |
| lightdef | 0x13 | 0x12 | 0x11 | Yes | 0x20 |
| ui_map | 0x14 | 0x13 | 0x12 | No | 0 |
| font | 0x15 | 0x14 | 0x13 | Yes | 0x10 |
| menufile | 0x16 | 0x15 | 0x14 | Yes | 0x80 |
| menu | 0x17 | 0x16 | 0x15 | Yes | 0x200 |
| localize | 0x18 | 0x17 | 0x16 | Yes | 0x1800 |
| weapon | 0x19 | 0x18 | 0x17 | Yes | 0x80 |
| snddriverglobals | 0x1A | 0x19 | 0x18 | Yes | 1 |
| fx | 0x1B | 0x1A | 0x19 | Yes | 0x190 |
| impactfx | 0x1C | 0x1B | 0x1A | Yes | 4 |
| aitype | 0x1D | 0x1C | 0x1B | No | 0 |
| mptype | 0x1E | 0x1D | 0x1C | No | 0 |
| character | 0x1F | 0x1E | 0x1D | No | 0 |
| xmodelalias | 0x20 | 0x1F | 0x1E | No | 0 |
| rawfile | 0x21 | 0x20 | 0x1F | Yes | 0x400 |
| stringtable | 0x22 | 0x21 | 0x20 | Yes | 0x32 |

## Column Definitions

- **Used**: Whether the asset type is actually used in game files
- **Pool Count**: Maximum number of assets of this type that can be loaded (hex)

## Unused Asset Types

These types exist in the engine but are not used:
- `xmodelpieces` (0x00) - Reserved but unused
- `ui_map` (0x14) - UI map, not used
- `aitype` (0x1D) - AI type definitions
- `mptype` (0x1E) - MP type definitions
- `character` (0x1F) - Character definitions
- `xmodelalias` (0x20) - Model aliases

## Comparison with WaW

CoD4 has fewer asset types than WaW. WaW adds:
- xanimparts
- destructibledef
- addon_map_ents
- tracer
- vehicle
- leaderboarddef

WaW rawfile is 0x22 (vs 0x21 on CoD4 PS3).

## References

- [codresearch.dev - Category:Assets](https://codresearch.dev/index.php/Category:Assets)
