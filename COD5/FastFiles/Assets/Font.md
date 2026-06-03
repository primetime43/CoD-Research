# Font Asset

Font data for text rendering.

## Structure

```c
struct Font_s
{
    const char *name;
    int pixelHeight;
    int glyphCount;
    Material *material;
    Material *glowMaterial;
    Glyph *glyphs;
};
```

## Glyph Structure

```c
struct Glyph
{
    unsigned short letter;
    char x0;
    char y0;
    char dx;
    char pixelWidth;
    char pixelHeight;
    float s0;
    float t0;
    float s1;
    float t1;
};
```

## Key Fields

| Field | Description |
|-------|-------------|
| `name` | Font asset name |
| `pixelHeight` | Font height in pixels |
| `glyphCount` | Number of glyphs in font |
| `material` | Main font texture material |
| `glowMaterial` | Glow effect material |
| `glyphs` | Array of glyph definitions |

## Glyph Fields

| Field | Description |
|-------|-------------|
| `letter` | Character code |
| `x0`, `y0` | Position offset |
| `dx` | Horizontal advance |
| `pixelWidth`, `pixelHeight` | Glyph dimensions |
| `s0`, `t0`, `s1`, `t1` | Texture coordinates |

## Game Differences

| Game | Additional Features |
|------|---------------------|
| CoD4 - BO1 | Base structure |
| BO2+ | Added `KerningPairs` for character spacing, `isScalingAllowed` flag |

## References

- [Font Asset - COD Research Wiki](https://codresearch.dev/index.php/Font_Asset)
