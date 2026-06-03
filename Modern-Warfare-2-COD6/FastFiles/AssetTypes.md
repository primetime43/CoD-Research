# Asset Type IDs (Modern Warfare 2)

## Overview

Each asset in a zone file has a type ID that identifies what kind of asset it is. These IDs differ between platforms due to shader asset differences.

## C Structure Definition

```c
struct XAsset
{
  XAssetType type;
  XAssetHeader *header;
};
```

---

## Complete Asset Type List

| Asset Type | Xbox ID | PS3 ID | PC ID | Used | Max Count | Asset Size (PC) |
|------------|---------|--------|-------|------|-----------|-----------------|
| physpreset | 0x00 | 0x00 | 0x00 | Yes | 0x40 | 0x2C |
| phys_collmap | 0x01 | 0x01 | 0x01 | Yes | 0x400 | 0x48 |
| xanim | 0x02 | 0x02 | 0x02 | Yes | 0x1000 | 0x58 |
| xmodelsurfs | 0x03 | 0x03 | 0x03 | Yes | 0x1000 | 0x24 |
| xmodel | 0x04 | 0x04 | 0x04 | Yes | 0x600 | 0x130 |
| material | 0x05 | 0x05 | 0x05 | Yes | 0x1000 | 0x60 |
| pixelshader | 0x06 | 0x06 | 0x06 | Yes | 0x1FA0 | 0x10 |
| vertexshader | N/A | 0x07 | 0x07 | Yes | 0x400 | 0x10 |
| vertexdecl | N/A | N/A | 0x08 | Yes | 0x30 | 0x64 |
| techset | 0x07 | 0x08 | 0x09 | Yes | 0x300 | 0xCC |
| image | 0x08 | 0x09 | 0x0A | Yes | 0xE00 | 0x20 |
| sound | 0x09 | 0x0A | 0x0B | Yes | 0x3E80 | 0x0C |
| sndcurve | 0x0A | 0x0B | 0x0C | Yes | 0x40 | 0x88 |
| loaded_sound | 0x0B | 0x0C | 0x0D | Yes | 0x546 | 0x2C |
| col_map_sp | 0x0C | 0x0D | 0x0E | Yes | 1 | 0x100 |
| col_map_mp | 0x0D | 0x0E | 0x0F | Yes | 1 | 0x100 |
| com_map | 0x0E | 0x0F | 0x10 | Yes | 1 | 0x10 |
| game_map_sp | 0x0F | 0x10 | 0x11 | Yes | 1 | 0x38 |
| game_map_mp | 0x10 | 0x11 | 0x12 | Yes | 1 | 0x08 |
| map_ents | 0x11 | 0x12 | 0x13 | Yes | 2 | 0x2C |
| fx_map | 0x12 | 0x13 | 0x14 | Yes | 1 | 0x74 |
| gfx_map | 0x13 | 0x14 | 0x15 | Yes | 1 | 0x274 |
| lightdef | 0x14 | 0x15 | 0x16 | Yes | 0x20 | 0x10 |
| ui_map | 0x15 | 0x16 | 0x17 | No | 0 | N/A |
| font | 0x16 | 0x17 | 0x18 | Yes | 0x10 | 0x18 |
| menufile | 0x17 | 0x18 | 0x19 | Yes | 0x80 | 0x0C |
| menu | 0x18 | 0x19 | 0x1A | Yes | 0x264 | 0x190 |
| localize | 0x19 | 0x1A | 0x1B | Yes | 0x1B58 | 0x08 |
| weapon | 0x1A | 0x1B | 0x1C | Yes | 0x578 | 0x684 |
| snddriverglobals | 0x1B | 0x1C | 0x1D | Yes | 1 | N/A |
| fx | 0x1C | 0x1D | 0x1E | Yes | 0x258 | 0x20 |
| impactfx | 0x1D | 0x1E | 0x1F | Yes | 4 | 0x08 |
| aitype | 0x1E | 0x1F | 0x20 | No | 0 | N/A |
| mptype | 0x1F | 0x20 | 0x21 | No | 0 | N/A |
| character | 0x20 | 0x21 | 0x22 | No | 0 | N/A |
| xmodelalias | 0x21 | 0x22 | 0x23 | No | 0 | N/A |
| rawfile | 0x22 | 0x23 | 0x24 | Yes | 0x400 | 0x10 |
| stringtable | 0x23 | 0x24 | 0x25 | Yes | 0x190 | 0x10 |
| leaderboarddef | 0x24 | 0x25 | 0x26 | Yes | 0x64 | 0x18 |
| structureddatadef | 0x25 | 0x26 | 0x27 | Yes | 0x18 | 0x0C |
| tracer | 0x26 | 0x27 | 0x28 | Yes | 0x20 | 0x70 |
| vehicle | 0x27 | 0x28 | 0x29 | Yes | 0x80 | 0x2D0 |
| addon_map_ents | 0x28 | 0x29 | 0x2A | Yes | 1 | 0x24 |

---

## Commonly Used Asset Types

| Asset Type | Xbox 360 | PS3 | PC | Description |
|------------|----------|-----|-----|-------------|
| rawfile | 0x22 | 0x23 | 0x24 | Scripts, configs, GSC files |
| localize | 0x19 | 0x1A | 0x1B | Localized text strings |
| stringtable | 0x23 | 0x24 | 0x25 | CSV data tables |
| material | 0x05 | 0x05 | 0x05 | Material definitions |
| image | 0x08 | 0x09 | 0x0A | Textures |
| xmodel | 0x04 | 0x04 | 0x04 | 3D models |
| weapon | 0x1A | 0x1B | 0x1C | Weapon definitions |
| fx | 0x1C | 0x1D | 0x1E | Visual effects |
| sound | 0x09 | 0x0A | 0x0B | Sound definitions |
| menu | 0x18 | 0x19 | 0x1A | UI menu definitions |

---

## Platform ID Offset Explanation

Asset type IDs are shifted between platforms because of shader asset differences:

| Platform | Shader Assets | Notes |
|----------|---------------|-------|
| PC | pixelshader + vertexshader + vertexdecl | Has all shader types |
| PS3 | pixelshader + vertexshader | No vertexdecl |
| Xbox 360 | pixelshader only | No vertexshader or vertexdecl |

The IDs diverge starting at `techset` (0x07 Xbox, 0x08 PS3, 0x09 PC) due to the missing shader asset types on each platform.

---

## Usage in Zone Files

Asset types appear in the asset table as 4-byte big-endian values (on PS3/Xbox):

```
Asset Entry Format (8 bytes):
[pointer: FF FF FF FF] [type: 00 00 00 XX]

Examples:
FF FF FF FF 00 00 00 23  <- rawfile (PS3)
FF FF FF FF 00 00 00 1A  <- localize (PS3)
FF FF FF FF 00 00 00 22  <- rawfile (Xbox 360)
```

---

## References

- [COD Research Wiki - MW2 FastFiles](https://codresearch.dev/index.php/FastFiles_and_Zone_files_(MW2))
- Verified through analysis of MW2 PS3/Xbox 360 zone files
