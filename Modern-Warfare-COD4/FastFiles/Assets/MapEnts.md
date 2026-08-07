# Map Entities Asset

Entity definitions for maps - spawn points, triggers, scripts.

> **Note:** Asset type IDs vary by platform. See [AssetTypes.md](FastFiles/AssetTypes.md) for the full list.

## Structure

```c
struct MapEnts
{
    const char *name;       // Map name
    char *entityString;     // Entity definition string
    int numEntityChars;     // Length of entity string
};
```

## Overview

Map entities define interactive elements in a map including:
- Spawn points
- Trigger volumes
- Script entities
- Physics objects

## References

- [MapEnts Asset - COD Research Wiki](https://codresearch.dev/index.php/MapEnts_Asset)
