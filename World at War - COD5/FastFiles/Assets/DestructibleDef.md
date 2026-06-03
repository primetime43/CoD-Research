# Destructible Definition Asset

Destructible object definitions - how objects break apart.

## Structure

```c
struct DestructibleDef
{
    const char *name;
    XModel *pristineModel;        // Undamaged model
    unsigned int numPieces;       // Number of destructible pieces
    DestructiblePiece *pieces;    // Array of piece definitions
    int clientOnly;               // Client-side rendering only flag
};
```

## Key Fields

| Field | Description |
|-------|-------------|
| `name` | Asset identifier |
| `pristineModel` | XModel pointer for undamaged state |
| `numPieces` | Count of destructible pieces |
| `pieces` | Array of `DestructiblePiece` structures |
| `clientOnly` | Whether rendering is client-side only |

## Destructible Pieces

Each `DestructiblePiece` can have:
- Multiple destruction stages
- Damage scaling properties
- Physics constraints
- Audio-visual effects
- Spawn behavior on destruction

## Notes

- Different game engine versions (WaW vs Black Ops) have conditional compilation affecting field availability
- Pieces can spawn debris, effects, and sounds when destroyed
- Damage thresholds control when pieces transition between stages

## References

- [DestructibleDef Asset - COD Research Wiki](https://codresearch.dev/index.php/DestructibleDef_Asset)
