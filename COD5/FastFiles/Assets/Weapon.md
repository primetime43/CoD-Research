# Weapon Asset

Weapon definitions with ~400+ fields covering models, animations, audio, ballistics, and gameplay.

## Key Info

- Header size on Xbox 360: **0x9AC bytes**
- Source format: Backslash-delimited key-value pairs starting with `WEAPONFILE`
- Includes damage multipliers for 19 body parts and 31 surface types

## Weapon Types

```c
enum weapType_t
{
    WEAPTYPE_BULLET,
    WEAPTYPE_GRENADE,
    WEAPTYPE_PROJECTILE,
    WEAPTYPE_BINOCULARS,
    WEAPTYPE_GAS,
    WEAPTYPE_BOMB,
    WEAPTYPE_MINE
};
```

| Type       | Description          |
|------------|----------------------|
| Bullet     | Standard firearms    |
| Grenade    | Thrown explosives    |
| Projectile | Rockets, etc.        |
| Binoculars | Observation tools    |
| Gas        | Gas weapons          |
| Bomb       | Planted explosives   |
| Mine       | Proximity explosives |

## Weapon Classes

```c
enum weapClass_t
{
    WEAPCLASS_RIFLE,
    WEAPCLASS_MG,
    WEAPCLASS_SMG,
    WEAPCLASS_SPREAD,
    WEAPCLASS_PISTOL,
    WEAPCLASS_GRENADE,
    WEAPCLASS_ROCKETLAUNCHER,
    WEAPCLASS_TURRET,
    WEAPCLASS_NON_PLAYER,
    WEAPCLASS_GAS,
    WEAPCLASS_ITEM
};
```

## Fire Types

- Full Auto
- Single Shot
- Burst Fire (2, 3, or 4-round variants)

## Penetration Types

- None
- Small
- Medium
- Large

## WeaponDef Structure Contents

The primary `WeaponDef` structure contains approximately 400+ fields organizing:

- **Models & Animation**: 16 gun models, hand models, world models, reload/fire/melee animations
- **Audio**: Pickup sounds, fire sounds, reload sounds, impact sounds across 31 surface types
- **Ballistics**: Damage values, range, spread, recoil (hip and ADS variants), penetration
- **Gameplay**: Ammo capacity, fire rate, reload times, stance modifiers, aim assist ranges
- **Visual Effects**: Muzzle flashes, shell ejection, reticles, overlay interfaces

## Source Format

Weapon files use backslash-delimited key-value pairs:

```
WEAPONFILE
displayName\rifle_m1garand
...
damage\100
damageRange\1000
```

Settings include damage multipliers for 19 body part locations and 31 surface bounce characteristics.

## References

- [Weapon Asset (WaW) - COD Research Wiki](https://codresearch.dev/index.php/Weapon_Asset_(WaW))
