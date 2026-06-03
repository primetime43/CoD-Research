# Graphics Map Asset

Rendering data for maps including visibility, lighting, and draw surfaces.

## Overview

The Graphics Map (gfx_map) contains all rendering-related data for a map level.

## Contents

- **Draw surfaces** - Renderable world geometry
- **Visibility data** - PVS (Potentially Visible Set) information
- **Lighting** - Lightmaps and light grid data
- **Reflection probes** - Environment reflection data
- **Static model instances** - Placed prop models

## Key Components

| Component | Description |
|-----------|-------------|
| BSP Tree | Binary space partitioning for rendering |
| Lightmaps | Baked lighting textures |
| Light Grid | Volumetric lighting for dynamic objects |
| Portals | Visibility portals between areas |
| Cells | Spatial cells for culling |

## Related Assets

- [Collision Map (0x0D/0E)](CollisionMap.md) - Physics collision data
- [Map Entities (0x12)](MapEnts.md) - Entity definitions
- [Light Def (0x14)](LightDef.md) - Light source definitions
