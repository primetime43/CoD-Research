# Material Asset

Material definitions linking textures to shaders.

> **Note:** Structure is the same across CoD4, WaW, MW2, and MW3.

## Structure

```c
struct Material
{
    MaterialInfo info;
    char stateBitsEntry[TECHNIQUE_COUNT];
    char textureCount;
    char constantCount;
    char stateBitsCount;
    char stateFlags;
    char cameraRegion;
    MaterialTechniqueSet *techniqueSet;
    MaterialTextureDef *textureTable;
    MaterialConstantDef *constantTable;
    GfxStateBits *stateBitTable;
};
```

## Texture Definition

```c
struct MaterialTextureDef
{
    unsigned int nameHash;
    char nameStart;
    char nameEnd;
    char sampleState;
    char semantic;
    unsigned char isMatureContent;
    unsigned char pad[3];
    MaterialTextureDefInfo u;
};
```

## Constant Definition

```c
struct MaterialConstantDef
{
    int nameHash;
    char name[12];
    vec4_t literal;
};
```

## Key Fields

| Field | Description |
|-------|-------------|
| `info` | Basic material information |
| `textureCount` | Number of textures used |
| `constantCount` | Number of shader constants |
| `techniqueSet` | Pointer to rendering technique set |
| `textureTable` | Array of texture definitions |
| `constantTable` | Array of shader constants |
| `stateBitTable` | Render state configuration |

## References

- [Material Asset - COD Research Wiki](https://codresearch.dev/index.php/Material_Asset)
