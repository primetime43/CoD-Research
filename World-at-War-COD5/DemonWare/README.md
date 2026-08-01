# DemonWare in World at War (PS3)

This section documents the DemonWare client embedded in the US PS3 release of
Call of Duty: World at War. It describes executable code, RTTI, virtual
addresses, wire serializers, result objects, and game-side call paths.

It intentionally does **not** document a replacement server, database schema,
or emulator implementation. Server behavior is useful only as runtime evidence
when it confirms what the unmodified client sends, accepts, or rejects.

## Canonical images

All addresses in this section are PS3 virtual addresses for one exact image.
They are not file offsets.

| Image | SHA-256 | Entry descriptor | Code entry | TOC |
| --- | --- | ---: | ---: | ---: |
| `BLUS30192-mp.ELF` | `79CA733D01D17821D22620CED23A4B5C6075629B3562674373CE3802DD77BA09` | `0x008886C8` | `0x00010230` | `0x00897620` |
| `BLUS30192-sp.ELF` | `EE35B674E7E8F57567163B50D00E6D47E52A5CDA85B04C9EB7729FDA770D7DAF` | `0x008CB5B0` | `0x00010230` | `0x008DAD48` |
| `EBOOT.ELF` | `8910D88524D03700B987B68B965C41E6D78E480768F65319FB4ED0FAEFB3672D` | `0x00020090` | `0x00010230` | `0x000280B8` |

The identical code-entry value does not make these images interchangeable.
Function descriptors carry an image-specific TOC, and matching virtual
addresses can name different code. An address without an MP/SP/EBOOT qualifier
is incomplete.

## Client architecture

The game uses four distinct networking layers:

1. **PSN/RPCN platform authentication** supplies an NP ticket to
   `bdAuthService`.
2. **DemonWare authentication** returns a PS3 type-19 ticket containing the
   24-byte lobby session key and a separate 128-byte LSG token.
3. **Lobby remote tasks** run over encrypted TCP records and expose services
   such as storage, stats, matchmaking, messaging, and KeyArchive.
4. **Peer networking** uses `bdCommonAddr`, NAT traversal, QoS, socket routing,
   and DTLS session material after matchmaking or invite acceptance.

The 24-byte auth cookie, 24-byte lobby key, 128-byte LSG token, eight-byte
session ID, sixteen-byte peer session key, and DTLS shared key are different
objects with different lifetimes.

## Recovered service and task table

| Service | ID | Tasks observed in BLUS30192 | Client purpose |
| --- | ---: | --- | --- |
| `bdStats` | `4` | `1` write; `4` entity read; `5` pivot/rank read; `10` bulk write | MP leaderboards and SP/Zombies map statistics |
| `bdMatchMaking` | `5` | `1` create; `2` update; `3` delete; `5` find | Advertise and discover peer-hosted sessions |
| `bdMessaging` | `6` | `8` global instant message | Native game-invite transport |
| `bdStorage` | `10` | `1` upload; `2` update; `5` get; `7` list by owner; `8` list publisher files | Publisher settings and opaque profile files |
| `bdTitleUtilities` | `12` | `1` verify string | Synchronous generated-name/profanity check |
| `bdKeyArchive` | `16` | `1` write; `3` read keys; `4` multi-entity read | Typed persistent scalar values |
| `bdPerformance` | `17` | `1` submit; `2` get values | Party-member performance input |
| `bdBandwidthTestClient` | `18` | `1` bootstrap and final report | Public-host upload/download measurement |

These are legacy five-bit `bdBitBuffer` tasks. They are not the later
byte-oriented DemonWare protocol used by newer titles.

## Actual ELF evidence

The class and method names are grounded in several independent sources:

- GNU RTTI names such as `bdAuthService`, `bdLobbyConnection`,
  `bdLobbyServiceImpl`, `bdLobbyFile`, and `bdListFilesResult`;
- retained source strings under
  `c:/cod5/cod/codsrc/DemonWare/bdLobby/`;
- vtables and PS3 function descriptors;
- typed constants and service/task IDs in serializer control flow;
- consumer-side structure offsets;
- unmodified-client packet captures and RPCS3 breakpoints.

Names ending in `_candidate` remain high-confidence structural identities
rather than recovered PDB symbols. A matching string or matching address in a
second image is not sufficient proof.

## Documentation map

- [ELF APIs and tables](ELF-APIs.md) — functions, vtables, globals, wrapper
  APIs, and recovered object fields.
- [Wire formats and objects](Wire-Formats.md) — typed fields, crypto framing,
  task payloads, result layouts, stats rows, invites, and NAT packets.
- [ELF call-flow charts](Flow-Charts.md) — actual client-side control flow from
  authentication through lobby tasks and peer networking.

## High-value research areas

The current maps are strongest around MP authentication/lobby internals,
cross-image storage, MP matchmaking, native invites, and SP Zombies stats.
Useful next ELF work includes:

- independently mapping the SP auth/lobby classes instead of assuming MP
  addresses;
- locating the SP service-4 task wrappers that select four- versus ten-column
  row classes;
- recovering complete `bdKeyArchive` task-1 field construction;
- naming the unresolved task-10 entity byte from class or enum evidence;
- mapping the full `bdLobbyServiceImpl` factory/service table;
- identifying the exact ownership and lifetime of global service singletons;
- tracing successful peer admission from `Party_StartNetwork` through
  `bdSocketRouter`/DTLS completion.

