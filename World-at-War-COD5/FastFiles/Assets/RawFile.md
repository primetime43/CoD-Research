# Raw File Asset

Script and configuration files embedded in the zone.

> **Note:** Asset type IDs vary by platform. See [AssetTypes.md](../AssetTypes.md) for the full list.

## Structure

```c
struct RawFile
{
    const char *name;    // File path/name
    int len;             // Length of buffer
    const char *buffer;  // File content (len + 1 for null terminator)
};
```

## Supported Extensions

| Extension | Description                                      |
|-----------|--------------------------------------------------|
| `.gsc`    | Game Script - server-side game logic             |
| `.csc`    | Client Script - client-side effects/UI           |
| `.cfg`    | Configuration file                               |
| `.vision` | Vision settings - lighting/color grading at `raw/vision/` |
| `.atr`    | Animation attributes                             |
| `.rmb`    | Rumble data - haptic feedback definitions        |
| `.shock`  | Shock files - audio/visual effects               |
| `.arena`  | Arena/map definitions                            |
| `.txt`    | Text files                                       |

## Notes

- Buffer length equals `len` plus one null terminator byte
- GSC scripts are stored as basic rawfiles in CoD4, WaW, and BO1
- Vision files are text-based lighting settings located at `raw/vision/`
- Shock files (`.shock`) add audio/visual effects
- Rumble files define haptic feedback for controllers

## References

- [Rawfile Asset - COD Research Wiki](https://codresearch.dev/index.php/Rawfile_Asset)
