# Asset Type IDs (Call of Duty: Ghosts / IW6)

Each asset in a zone's pool carries a type ID. The table below is the **IW6 PS3** enum
(54 types, `0x00`–`0x35`) — the only platform wired up and verified. Type IDs differ on
other platforms because some asset slots are platform-specific (see notes below).

In the asset pool each record is 8 bytes in `[pointer][type]` order, with the type word
stored big-endian:

```
PP PP PP PP 00 00 00 XX     XX = type ID (≤ 0x35)
```

See [Zone.md](FastFiles/Zone.md) for pool-walking details.

---

## IW6 PS3 Asset Types

| ID | Type | ID | Type | ID | Type |
|----|------|----|------|----|------|
| 0x00 | physpreset | 0x12 | aipaths | 0x24 | aitype |
| 0x01 | phys_collmap | 0x13 | vehicle_track | 0x25 | mptype |
| 0x02 | xanim | 0x14 | map_ents | 0x26 | character |
| 0x03 | xmodelsurfs | 0x15 | fx_map | 0x27 | xmodelalias |
| 0x04 | xmodel | 0x16 | gfx_map | 0x28 | **rawfile** |
| 0x05 | material | 0x17 | lightdef | 0x29 | **scriptfile** |
| 0x06 | vertexshader | 0x18 | ui_map | 0x2A | stringtable |
| 0x07 | pixelshader | 0x19 | font | 0x2B | leaderboarddef |
| 0x08 | techset | 0x1A | menufile | 0x2C | structureddatadef |
| 0x09 | image | 0x1B | menu | 0x2D | tracer |
| 0x0A | sound | 0x1C | animclass | 0x2E | vehicle |
| 0x0B | sndcurve | 0x1D | localize | 0x2F | addon_map_ents |
| 0x0C | lpfcurve | 0x1E | attachment | 0x30 | netconststrings |
| 0x0D | reverbsendcurve | 0x1F | weapon | 0x31 | reverbpreset |
| 0x0E | loaded_sound | 0x20 | snddriverglobals | 0x32 | **luafile** |
| 0x0F | col_map | 0x21 | fx | 0x33 | scriptable |
| 0x10 | com_map | 0x22 | impactfx | 0x34 | equipsndtable |
| 0x11 | glass_map | 0x23 | surfacefx | 0x35 | dopplerpreset |

> **Note:** `col_map` (0x0F) is a single slot — unlike CoD4/WaW/MW2 which split
> `col_map_sp` / `col_map_mp`. IW6 also adds types not present in earlier IW engines:
> `glass_map`, `aipaths`, `vehicle_track`, `surfacefx`, `animclass`, `attachment`,
> `scriptfile`, `netconststrings`, `reverbpreset`, `reverbsendcurve`, `lpfcurve`,
> `luafile`, `scriptable`, `equipsndtable`, `dopplerpreset`.

---

## Commonly Used Types

| Asset Type | PS3 ID | Description |
|------------|--------|-------------|
| rawfile | 0x28 | Scripts/configs (.gsc, .cfg, .vision, .bin, …) — zlib-wrapped |
| scriptfile | 0x29 | Compiled GSC/script — zlib-wrapped, "long" header shape |
| luafile | 0x32 | LUI Lua 5.1 bytecode — flat header, not zlib-wrapped |
| stringtable | 0x2A | CSV data tables |
| localize | 0x1D | Localized text strings |
| image | 0x09 | Textures |
| material | 0x05 | Material definitions |
| xmodel | 0x04 | 3D models |
| weapon | 0x1F | Weapon definitions |

---

## Platform Differences

Only PS3 IDs are verified. The shifts on other platforms are:

- **Xbox 360** — no `vertexshader` slot, so IDs shift **−1** for everything ≥ `0x07`.
- **Wii U** — adds `fonticon` at `0x1A` and shifts the rest accordingly.
- **PC** — differs more substantially: PC adds `computeshader`, `hullshader`,
  `domainshader`, and `vertexdecl` slots that consoles don't have.

No samples for those platforms have been tested.

---

## References

- [FastFiles.md](FastFiles/FastFiles.md) — container format
- [Zone.md](FastFiles/Zone.md) — zone structure and per-asset headers
- [COD Research Wiki](https://codresearch.dev/)
