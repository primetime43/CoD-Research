# Image Asset

Texture images for materials and UI.

## Structure

```c
struct GfxImage
{
    MapType mapType;
    GfxTextureLoad texture;
    CardMemory cardMemory;
    int size;
    unsigned short width;
    unsigned short height;
    unsigned short depth;
    char category;
    bool streaming;
    char *data;
    const char *name;
};
```

## Image Load Definition

```c
struct GfxImageLoadDef
{
    char levelCount;
    char flags;
    short dimensions[3];
    _D3DFormat format;
    GfxTexture texture;
};
```

## Texture Union

```c
union GfxTexture
{
    D3DBaseTexture *basemap;
    D3DTexture *map;
    D3DVolumeTexture *volmap;    // if flags & 8
    D3DCubeTexture *cubemap;     // if flags & 4
};
```

## Key Fields

| Field | Description |
|-------|-------------|
| `mapType` | Type of texture map |
| `width` | Texture width in pixels |
| `height` | Texture height in pixels |
| `depth` | Texture depth (for 3D textures) |
| `category` | Image category classification |
| `streaming` | Whether image is streamed |
| `data` | Pointer to image data |
| `name` | Asset name |

## Texture Types

Based on `flags` field:
- Standard 2D texture (default)
- Volume texture (`flags & 8`)
- Cube map (`flags & 4`)

## WaW Note

If `streaming` is set to true, the image data is loaded at the end of the zone file rather than inline with the asset definition.

## References

- [Image Asset - COD Research Wiki](https://codresearch.dev/index.php/Image_Asset)
