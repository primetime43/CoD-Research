# Light Definition Asset

Light source definitions.

## Structure

```c
struct GfxLightDef
{
    const char *name;
    GfxLightImage attenuation;
    int lmapLookupStart;
};

struct GfxLightImage
{
    GfxImage *image;
    char samplerState;
};
```

## Key Fields

| Field | Description |
|-------|-------------|
| `name` | Asset identifier |
| `attenuation` | Light attenuation image/texture |
| `lmapLookupStart` | Lightmap lookup start index |

## Notes

- `lmapLookupStart` is a constant value for all lightdefs
- Only `name` and `attenuation` change between different light definitions
- MW3+ added a `cucoloris` image field for additional light effects

## Game Differences

| Game | Structure |
|------|-----------|
| CoD4 - BO3 | Base structure (name, attenuation, lmapLookupStart) |
| MW3 - AW | Added `cucoloris` GfxLightImage field |

## References

- [Lightdef Asset - COD Research Wiki](https://codresearch.dev/index.php/Lightdef_Asset)
