# Physics Preset Asset

Physics behavior presets controlling dynamic object behavior.

## Structure (WaW)

```c
struct PhysPreset
{
    const char *name;
    int type;
    float mass;
    float bounce;
    float friction;
    float bulletForceScale;
    float explosiveForceScale;
    const char *sndAliasPrefix;
    float piecesSpreadFraction;
    float piecesUpwardVelocity;
    bool tempDefaultToCylinder;
    int bCanFloat;                // WaW addition
    float gravityScale;           // WaW addition
};
```

> **Note:** WaW adds `bCanFloat` and `gravityScale` fields compared to CoD4.

## Key Fields

| Field | Description |
|-------|-------------|
| `mass` | Object mass for physics calculations |
| `bounce` | Bounciness/restitution coefficient |
| `friction` | Surface friction |
| `bulletForceScale` | Force multiplier from bullet impacts |
| `explosiveForceScale` | Force multiplier from explosions |
| `sndAliasPrefix` | Sound alias prefix for impact sounds |
| `piecesSpreadFraction` | How much pieces spread on destruction |
| `piecesUpwardVelocity` | Upward velocity of debris |
| `bCanFloat` | Whether object floats in water (WaW) |
| `gravityScale` | Gravity multiplier (WaW) |

## Source Format

Source files use backslash-delimited format with an `isFrictionInfinity` setting that converts 0.5 friction to maximum float value when compiled.

## Game Differences

| Game | Additional Fields |
|------|-------------------|
| CoD4 | Base structure |
| WaW | `bCanFloat`, `gravityScale` |
| BO1+ | `centerOfMassOffset`, `buoyancyBoxMin/Max` |

## References

- [PhysPreset Asset - COD Research Wiki](https://codresearch.dev/index.php/PhysPreset_Asset)
