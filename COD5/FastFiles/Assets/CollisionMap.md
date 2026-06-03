# Collision Map Asset (0x0D, 0x0E)

Collision geometry for single player (0x0D) and multiplayer (0x0E) maps.

## Overview

Uses `clipMap_t` structure containing the complete spatial and collision database for map geometry and dynamic objects.

## Primary Components

### Geometry

- **Plane definitions** (`cplane_s`) - Collision plane equations
- **Brush geometry** - Sides, edges, and vertices
- **Static models** (`cStaticModel_s`) - With transformation data
- **Material properties** (`dmaterial_t`) - Surface types

### Spatial Acceleration

- **Nodes and leaves** - BSP tree structure
- **Leaf-brush node hierarchy** - For efficient queries
- **AABB trees** - Axis-aligned bounding box trees
- **Collision partitions** - For optimized raycasting

### Dynamic Entities

Support for four dynamic entity types:
- Client models
- Server models
- Client brushes
- Server brushes

Each with associated:
- Definitions
- Poses
- Client-side state
- Server-side state
- Collision information

### Additional Data

- **Visibility cluster data** - PVS information
- **Physics constraints** - Physical connections
- **Checksum validation** - Verified via `mapcrc` DVAR across clients
- **Submodels** - Brush collections

## Validation

Map checksums are verified across clients using the `mapcrc` DVAR to ensure all players have matching collision data.

## Notes

- Complete collision maps are complex structures
- Current documentation focuses on vertices and faces exportable to .obj files
- Full serialization methods for all collision data remain underdeveloped

## References

- [Collision Map Asset (WaW) - COD Research Wiki](https://codresearch.dev/index.php/Collision_Map_Asset_(WaW))
