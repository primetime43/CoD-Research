# Asset Types (World at War)

## Overview

Asset types identify the kind of data stored in a zone file's asset pool. Each 8-byte asset record contains a type ID that tells the engine what kind of asset the data represents.

**Important:** Asset type IDs differ between platforms due to shader availability:
- **PS3**: Includes both pixelshader and vertexshader
- **Xbox 360**: Has pixelshader but NO vertexshader - types >= 0x08 shift by -1
- **PC**: Has NEITHER pixelshader nor vertexshader - types >= 0x07 shift by -2

See [Zone Files](Zone.md) for information about how assets are stored in zones.

---

## World at War (WaW/CoD5) Asset Types

### Platform Comparison

| Type Name         | PS3 (Hex) | Xbox 360 (Hex) | PC (Hex) | Description |
|-------------------|-----------|----------------|----------|-------------|
| xmodelpieces      | 0x00      | 0x00           | 0x00     | Model pieces |
| physpreset        | 0x01      | 0x01           | 0x01     | Physics presets |
| physconstraints   | 0x02      | 0x02           | 0x02     | Physics constraints |
| destructibledef   | 0x03      | 0x03           | 0x03     | Destructible definitions |
| xanim             | 0x04      | 0x04           | 0x04     | Animation data |
| xmodel            | 0x05      | 0x05           | 0x05     | 3D model data |
| material          | 0x06      | 0x06           | 0x06     | Material/shader definitions |
| pixelshader       | 0x07      | 0x07           | -        | Pixel shader programs (PS3/Xbox only) |
| vertexshader      | 0x08      | -              | -        | Vertex shader programs (PS3 only) |
| techset           | 0x09      | 0x08           | 0x07     | Technique sets |
| image             | 0x0A      | 0x09           | 0x08     | Texture images |
| sound             | 0x0B      | 0x0A           | 0x09     | Sound definitions |
| loaded_sound      | 0x0C      | 0x0B           | 0x0A     | Loaded sound data |
| col_map_sp        | 0x0D      | 0x0C           | 0x0B     | Collision map (SP) |
| col_map_mp        | 0x0E      | 0x0D           | 0x0C     | Collision map (MP) |
| com_map           | 0x0F      | 0x0E           | 0x0D     | Common map data |
| game_map_sp       | 0x10      | 0x0F           | 0x0E     | Game map (SP) |
| game_map_mp       | 0x11      | 0x10           | 0x0F     | Game map (MP) |
| map_ents          | 0x12      | 0x11           | 0x10     | Map entities |
| gfx_map           | 0x13      | 0x12           | 0x11     | Graphics map data |
| lightdef          | 0x14      | 0x13           | 0x12     | Light definitions |
| ui_map            | 0x15      | 0x14           | 0x13     | UI map data |
| font              | 0x16      | 0x15           | 0x14     | Font data |
| menufile          | 0x17      | 0x16           | 0x15     | Menu definition files |
| menu              | 0x18      | 0x17           | 0x16     | Menu assets |
| localize          | 0x19      | 0x18           | 0x17     | Localized strings |
| weapon            | 0x1A      | 0x19           | 0x18     | Weapon definitions |
| snddriverglobals  | 0x1B      | 0x1A           | 0x19     | Sound driver globals |
| fx                | 0x1C      | 0x1B           | 0x1A     | Visual effects |
| impactfx          | 0x1D      | 0x1C           | 0x1B     | Impact effects |
| aitype            | 0x1E      | 0x1D           | 0x1C     | AI type definitions |
| mptype            | 0x1F      | 0x1E           | 0x1D     | Multiplayer type definitions |
| character         | 0x20      | 0x1F           | 0x1E     | Character definitions |
| xmodelalias       | 0x21      | 0x20           | 0x1F     | Model alias definitions |
| **rawfile**       | 0x22      | 0x21           | 0x20     | Raw script/config files |
| **stringtable**   | 0x23      | 0x22           | 0x21     | String table (CSV data) |
| packindex         | 0x24      | 0x23           | 0x22     | Pack index data |

### Key Differences Explained

The ID shift between platforms occurs because:

1. **PS3** includes `vertexshader` at position 0x08
2. **Xbox 360** does NOT have `vertexshader`, so all types >= 0x08 are shifted down by 1
3. **PC** has NEITHER `pixelshader` NOR `vertexshader`, so all types >= 0x07 are shifted down by 2

**Example:** `rawfile` asset type:
- PS3: 0x22 (34 decimal)
- Xbox 360: 0x21 (33 decimal) - shifted by -1
- PC: 0x20 (32 decimal) - shifted by -2

---

## Quick Reference (Common Asset Types)

| Asset Type    | PS3     | Xbox 360 | PC      |
|---------------|---------|----------|---------|
| rawfile       | 0x22    | 0x21     | 0x20    |
| stringtable   | 0x23    | 0x22     | 0x21    |
| localize      | 0x19    | 0x18     | 0x17    |
| xanim         | 0x04    | 0x04     | 0x04    |
| xmodel        | 0x05    | 0x05     | 0x05    |
| weapon        | 0x1A    | 0x19     | 0x18    |
| image         | 0x0A    | 0x09     | 0x08    |
| map_ents      | 0x12    | 0x11     | 0x10    |
| menu          | 0x18    | 0x17     | 0x16    |
| menufile      | 0x17    | 0x16     | 0x15    |

> **Important:** Always verify the platform before parsing asset types.

---

## Asset Pool Record Format

Each asset in the zone has an 8-byte record in the asset pool:

### PS3 Format (Big-Endian)
```
[4-byte type: 00 00 00 XX] [4-byte memory pointer: FF FF FF FF]
```

### PC Format (Little-Endian)
```
[4-byte type: XX 00 00 00] [4-byte memory pointer: FF FF FF FF]
```

### Example (WaW PS3 rawfile)
```
00 00 00 22 FF FF FF FF
```
- `00 00 00 22` = Asset type 0x22 (rawfile on PS3)
- `FF FF FF FF` = Memory pointer placeholder

### Example (WaW Xbox 360 rawfile)
```
00 00 00 21 FF FF FF FF
```
- `00 00 00 21` = Asset type 0x21 (rawfile on Xbox 360)

### End Marker

The asset pool ends with all `0xFF` bytes:
```
FF FF FF FF FF FF FF FF
```

---

## Detecting Platform

When parsing a FastFile, determine the platform by checking:

1. **Magic bytes**:
   - `IWffu100` = Unsigned (PS3 SP, Xbox SP, PC)
   - `IWff0100` = Signed (Xbox 360 MP only)

2. **Signed files** have a 0x4000-byte signature block after the header

3. Use the appropriate asset type table based on detected platform

---

## References

- [COD Research Wiki - WaW Assets](https://codresearch.dev/index.php/Category:WaW)
- [COD Research Wiki - Assets](https://codresearch.dev/index.php/Category:Assets)
- [Zone Files](Zone.md)
- [FastFiles](FastFiles.md)
