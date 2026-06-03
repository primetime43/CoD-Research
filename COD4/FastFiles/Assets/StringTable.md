# String Table Asset

CSV-like tabular data for game configuration.

> **Note:** Asset type IDs vary by platform. See [AssetTypes.md](../AssetTypes.md) for the full list.

## Structure

```c
struct StringTable
{
    const char *name;       // Table name/path
    int columnCount;        // Number of columns
    int rowCount;           // Number of rows
    const char **values;    // Pointer to cell values
};
```

## Usage

String tables store structured data like:
- Weapon statistics
- Perk definitions
- Challenge requirements
- Rank progression

## References

- [StringTable Asset - COD Research Wiki](https://codresearch.dev/index.php/StringTable_Asset)
