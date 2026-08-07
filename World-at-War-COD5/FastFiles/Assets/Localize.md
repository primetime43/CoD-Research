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

Basic entry:
```
REFERENCE           SERVERISFULL
LANG_ENGLISH        "Server is full."
```

With multiple languages:
```
REFERENCE           SERVERISFULL
LANG_ENGLISH        "Server is full."
LANG_GERMAN         "Der Server ist voll."
```

Using `#same` copies the English text:
```
REFERENCE           SERVERISFULL
LANG_ENGLISH        "Server is full."
LANG_FRENCH         #same
```

### Optional Metadata

```
REFERENCE           SERVERISFULL
NOTES               "The string returned when a client tries to join a full server"
FLAGS               "0"
LANG_ENGLISH        "Server is full."
```

## Compiled Output

When compiled, entries produce an asset ID combining filename and reference name.

Example: `EXE_SERVERISFULL`

## References

- [Localize Asset - COD Research Wiki](https://codresearch.dev/index.php/Localize_Asset)
