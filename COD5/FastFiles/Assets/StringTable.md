# String Table Asset

CSV-like tabular data for game configuration and localization.

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

## Cell Indexing

To access a specific cell:

```
desiredEntry = (columnCount * desiredRow) + desiredColumn
```

## Source Format

StringTables are stored as comma-separated values in `raw/` directory paths.

Example structure:
```csv
#,name,unlock_level,cost,type,frame
0,clan_tag_1,1,0,standard,frame_1
1,clan_tag_2,5,100,premium,frame_2
```

- Lines prefixed with `#` are treated as header/comment rows
- Data entries follow the header

## References

- [StringTable Asset - COD Research Wiki](https://codresearch.dev/index.php/StringTable_Asset)
