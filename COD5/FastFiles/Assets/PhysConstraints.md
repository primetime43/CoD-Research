# Physics Constraints Asset

Physics constraint definitions for connected objects.

## Key Info

- Header size (WaW): **0x988 bytes**
- Contains up to 16 constraints in a static array

## Structure

```c
struct PhysConstraints
{
    const char *name;
    unsigned int count;
    PhysConstraint data[16];    // Static array of 16 constraints
};
```

## Constraint Definition

Each `PhysConstraint` includes:

- **Identifiers**: targetname, type, attachment point configurations
- **Target References**: Entity/bone targets with separate configurations for two attachment points
- **Spatial Data**: offset, position (pos, pos2), direction vectors
- **Physical Properties**: flags, timeout, health range (min/max), distance, damping, power, scale
- **Rotation**: spin scale, angle constraints (minAngle, maxAngle)
- **Resources**: material reference, constraint handle, rope index

## Header Size by Game

| Game | Header Size |
|------|-------------|
| WaW | 0x988 bytes |
| BO1/BO2 | 0xA88 bytes |
| BO3 | 0x0C bytes (uses pointer instead of static array) |

## Notes

- BO3 changed from static array to pointer-based allocation
- Earlier games use fixed 16-constraint arrays
- BO1/BO2 include additional entity number array

## References

- [PhysConstraints Asset - COD Research Wiki](https://codresearch.dev/index.php/PhysConstraints_Asset)
