# FX / Visual Effects Asset

Visual effects system for particles, sprites, lights, and models.

## Structure

```c
struct FxEffectDef
{
    const char *name;
    int flags;
    int totalSize;
    int msecLoopingLife;
    int elemDefCountLooping;
    int elemDefCountOneShot;
    int elemDefCountEmission;
    FxElemDef *elemDefs;
};
```

## Element Definition

```c
struct FxElemDef
{
    int flags;
    FxSpawnDef spawn;
    FxFloatRange spawnRange;
    FxFloatRange fadeInRange;
    FxFloatRange fadeOutRange;
    float spawnFrustumCullRadius;
    FxIntRange spawnDelayMsec;
    FxIntRange lifeSpanMsec;
    FxFloatRange spawnOrigin[3];
    FxFloatRange spawnOffsetRadius;
    FxFloatRange spawnOffsetHeight;
    FxFloatRange spawnAngles[3];
    FxFloatRange angularVelocity[3];
    FxFloatRange initialRotation;
    FxFloatRange gravity;
    FxFloatRange reflectionFactor;
    FxElemAtlas atlas;
    char elemType;
    char visualCount;
    char velIntervalCount;
    char visStateIntervalCount;
    FxElemVelStateSample *velSamples;
    FxElemVisStateSample *visSamples;
    FxElemVisuals visuals;
    // ... collision, trail, sort order fields
};
```

## Element Types

```c
enum FxElemType
{
    FX_ELEM_TYPE_SPRITE_BILLBOARD,
    FX_ELEM_TYPE_SPRITE_ORIENTED,
    FX_ELEM_TYPE_TAIL,
    FX_ELEM_TYPE_TRAIL,
    FX_ELEM_TYPE_CLOUD,
    FX_ELEM_TYPE_MODEL,
    FX_ELEM_TYPE_LIGHT,
    FX_ELEM_TYPE_SPOT_LIGHT,
    FX_ELEM_TYPE_SOUND,
    FX_ELEM_TYPE_DECAL,
    FX_ELEM_TYPE_RUNNER
};
```

| Type              | Description                    |
|-------------------|--------------------------------|
| Sprite Billboard  | Camera-facing sprites          |
| Sprite Oriented   | Fixed-orientation sprites      |
| Tail              | Stretched trail effects        |
| Trail             | Motion trails                  |
| Cloud             | Volumetric effects             |
| Model             | 3D model particles             |
| Light             | Dynamic point lights           |
| Spot Light        | Dynamic spot lights            |
| Sound             | Audio elements                 |
| Decal             | Surface decals                 |
| Runner            | Nested/chained effects         |

## Visual Components

Elements use `FxElemVisuals` unions to reference:
- Materials (for sprites)
- Models (for model elements)
- Effect definitions (for runners)
- Sounds (for audio)

## Animation

Animation is controlled through `FxElemAtlas`:
- Fixed, random, or indexed start behaviors
- Various playback modes

Velocity and visual states are sampled across frames via:
- `FxElemVelStateSample` - velocity over time
- `FxElemVisStateSample` - color, rotation, size variations

## Behavioral Flags

Flags control:
- Looping behavior
- Randomization of properties
- Collision detection
- Relative positioning
- Conditional triggering (on touch, death, or attachment)

## References

- [FX Asset (WaW) - COD Research Wiki](https://codresearch.dev/index.php/FX_Asset_(WaW))
