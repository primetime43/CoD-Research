# Map Entities Asset

Entity definitions for maps - spawn points, waypoints, physics objects, destructibles.

> **Note:** Structure is the same across CoD4, WaW, and Black Ops 1.

## Structure

```c
struct MapEnts
{
    const char *name;         // Asset name
    char *entityString;       // Entity definition string
    int numEntityChars;       // Length of entity string
};
```

## Purpose

The MapEnts asset handles entity definitions within the D3DBSP mapping system produced by Radiant, including:

- Spawn points
- Helicopter path waypoints
- Physics-enabled objects
- Destructible map elements
- Triggers and scripted events

## Notes

- The `entityString` contains a text-based entity definition format
- `numEntityChars` specifies the exact length of the entity string
- WaW uses the same simplified three-member structure as CoD4
- MW2 introduced additional buffer data and stage information

## References

- [MapEnts Asset - COD Research Wiki](https://codresearch.dev/index.php/MapEnts_Asset)
