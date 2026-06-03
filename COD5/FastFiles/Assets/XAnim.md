# XAnim Asset

Animation data for models.

## Structure

```c
struct XAnimParts
{
    const char *name;
    unsigned short dataByteCount;
    unsigned short dataShortCount;
    unsigned short dataIntCount;
    unsigned short randomDataByteCount;
    unsigned short randomDataIntCount;
    unsigned short numframes;
    char bLoop;
    char bDelta;
    unsigned char boneCount[12];    // 12 elements for WaW
    char notifyCount;
    char assetType;
    unsigned int randomDataShortCount;
    unsigned int indexCount;
    float framerate;
    float frequency;
    char *dataByte;
    short *dataShort;
    int *dataInt;
    short *randomDataShort;
    char *randomDataByte;
    int *randomDataInt;
    XAnimIndices indices;
    XAnimNotifyInfo *notify;
    XAnimDeltaPart *deltaPart;
};
```

## Supporting Structures

### Translation Data

```c
struct XAnimPartTrans
{
    unsigned short size;
    char smallTrans;
    union
    {
        char *frames;
        XAnimPartTransFrames frames2;
    } u;
};

struct XAnimPartTransFrames
{
    float mins[3];
    float size[3];
    XAnimDynamicFrames frames;
};
```

### Rotation Data

```c
struct XAnimDeltaPartQuat
{
    unsigned short size;
    union
    {
        short *frames;
        XAnimDeltaPartQuatDataFrames frames2;
    } u;
};
```

### Animation Events

```c
struct XAnimNotifyInfo
{
    unsigned short name;    // Script string index
    float time;             // Normalized time (0.0 - 1.0)
};
```

## Key Fields

| Field | Description |
|-------|-------------|
| `numframes` | Total frame count |
| `bLoop` | Whether animation loops |
| `bDelta` | Whether animation uses delta compression |
| `boneCount[12]` | Bone counts per category (12 categories in WaW) |
| `framerate` | Playback speed in FPS |
| `frequency` | Animation frequency |
| `notify` | Array of animation event triggers |
| `deltaPart` | Delta compression data |

## Notes

- The architecture uses unions for memory efficiency
- Different data types (char vs. unsigned short) are used based on animation requirements
- This design remained relatively stable across early Call of Duty titles

## References

- [XAnim Asset - COD Research Wiki](https://codresearch.dev/index.php/XAnim_Asset)
