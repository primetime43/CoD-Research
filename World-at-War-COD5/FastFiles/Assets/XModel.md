# XModel Asset

3D model data including geometry, materials, and LODs.

## Overview

XModel assets contain complete 3D model definitions used for characters, weapons, props, and world objects.

## Contents

- **Model geometry** - Vertices, faces, and UV coordinates
- **Bone hierarchy** - Skeleton for animation
- **Collision bounds** - Physics collision shapes
- **Material references** - Surface materials and textures
- **Level-of-detail (LOD)** - Multiple detail levels for distance rendering

## Key Components

| Component | Description |
|-----------|-------------|
| Surfaces | Renderable mesh data |
| LODs | Multiple detail levels (typically 4) |
| Bones | Skeleton hierarchy for animation |
| Collision | Physics collision geometry |
| Materials | Surface material assignments |

## Notes

- Models reference materials by name
- LOD distances control when detail levels switch
- Bone data is used for animation and attachment points
- Collision data is separate from render geometry

## References

- [COD Research Wiki - WaW Assets](https://codresearch.dev/index.php/Category:WaW)
