# Weapon Asset

Weapon definitions with ~400 fields covering models, animations, audio, ballistics, and gameplay.

> **Note:** Asset type IDs vary by platform. See [AssetTypes.md](FastFiles/AssetTypes.md) for the full list.

## Key Info

- Header size on Xbox 360: **0x878 bytes**
- Source format: Text files with no extension at `raw/weapons/sp/` and `raw/weapons/mp/`
- Files start with `WEAPONFILE` identifier

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
    WEAPCLASS_ITEM
};
```

## WeaponDef Contents

The WeaponDef structure contains:

- **Models**: 16 gun models, hand models, world models, clip model
- **Animation**: Idle, fire, reload, melee, sprint animations
- **Audio**: Fire sounds, reload sounds, impact sounds by surface type
- **Ballistics**: Damage values, range, spread, recoil, penetration
- **Gameplay**: Ammo capacity, fire rate, reload times, aim assist

## References

- [Weapon Asset (CoD4) - COD Research Wiki](https://codresearch.dev/index.php/Weapon_Asset_(CoD4))
