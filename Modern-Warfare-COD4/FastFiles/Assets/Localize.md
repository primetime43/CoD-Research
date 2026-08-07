# Localize Asset

In-game text translations, menu labels, and dialog strings.

> **Note:** Asset type IDs vary by platform. See [AssetTypes.md](FastFiles/AssetTypes.md) for the full list.

## Structure

```c
struct LocalizeEntry
{
    const char *value;    // Translated string
    const char *name;     // Developer reference ID
};
```

## Source Format

Located at `raw/english/localizedstrings/` with `.str` extension.

### File Header

```
VERSION             "1"
CONFIG              "C:\trees\cod3\cod3\bin\StringEd.cfg"
FILENOTES           ""
```

### String Definition

```
REFERENCE           SERVERISFULL
LANG_ENGLISH        "Server is full."
```

## References

- [Localize Asset - COD Research Wiki](https://codresearch.dev/index.php/Localize_Asset)
