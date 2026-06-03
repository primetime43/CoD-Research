# Material Asset

Material definitions linking textures to shaders.

> **Note:** Asset type IDs vary by platform. See [AssetTypes.md](../AssetTypes.md) for the full list.

## Structure

```c
struct Material
{
    MaterialInfo info;
    char textureCount;
    char constantCount;
    char stateBitsCount;
    char stateFlags;
    char cameraRegion;
    MaterialTechniqueSet *techniqueSet;
    MaterialTextureDef *textureTable;
    MaterialConstantDef *constantTable;
    GfxStateBits *stateBitsTable;
};
```

## Key Components

**MaterialInfo**: Primary descriptor with name, draw surface data, game flags, and surface type.

**MaterialTextureDef**: Texture properties including semantic type (2D, color map, normal map, specular map).

**MaterialConstantDef**: Shader constants with hash identifier and 4-component values.

## References

- [Material Asset - COD Research Wiki](https://codresearch.dev/index.php/Material_Asset)
