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

| Extension | Description |
|-----------|-------------|
| `.gsc` | Game Script - server-side game logic |
| `.cfg` | Configuration file |
| `.vision` | Vision settings at `raw/vision/` |
| `.atr` | Animation attributes |
| `.rmb` | Rumble data at `raw/rumble/` |
| `.shock` | Shock files at `raw/shock/` |
| `.arena` | Arena/map definitions |
| `.txt` | Text files |

## Notes

- Buffer length equals `len` plus one null terminator byte
- CoD4 stores GSC scripts as uncompressed rawfiles
- Vision files are text-based lighting settings
- Shock files temporarily modify dvars for effects (radiation, explosions)
- Rumble files define haptic feedback for controllers

## References

- [Rawfile Asset - COD Research Wiki](https://codresearch.dev/index.php/Rawfile_Asset)
