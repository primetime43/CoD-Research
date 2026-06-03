# Technique Set Asset

Shader technique sets defining rendering passes.

## Structure

```c
struct MaterialTechniqueSet
{
    char *name;
    MaterialWorldVertexFormat worldVertFormat;
    MaterialTechnique *techniques[MAX_TECHNIQUES];
};
```

## Key Fields

| Field | Description |
|-------|-------------|
| `name` | Asset identifier |
| `worldVertFormat` | Vertex format enumeration |
| `techniques` | Array of technique pointers |

## World Vertex Format

The `worldVertFormat` enumeration specifies vertex format with combinations of texture coordinates and normal maps:

| Value | Description |
|-------|-------------|
| 0x0 | Basic format |
| 0x1 - 0xB | Various texture coordinate and normal map combinations |

## Technique Count

The maximum technique count varies by platform:

| Platform | MAX_TECHNIQUES |
|----------|----------------|
| PS3 | 26-54 |
| Xbox 360 | 26-54 |
| PC | Up to 130 |

## Notes

- BO2 uses `const char *name` instead of `char *name`
- Technique sets define how materials are rendered across different passes (depth, color, shadow, etc.)
- Each technique contains shader programs and render states

## References

- [Technique Set Asset - COD Research Wiki](https://codresearch.dev/index.php/Technique_Set_Asset)
