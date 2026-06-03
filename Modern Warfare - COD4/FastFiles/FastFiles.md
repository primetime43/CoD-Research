# FastFile Format (CoD4)

FastFiles (.ff) are compressed archives containing zone data. CoD4 uses the IW3.0 engine format.

## Header Structure

| Offset | Size | Description |
|--------|------|-------------|
| 0x00 | 8 | Magic (`IWffu100` unsigned, `IWff0100` signed) |
| 0x08 | 4 | Version (big-endian) |

### Platform Summary

| Platform | Version | Hex Bytes | Endianness | Compression |
|----------|---------|-----------|------------|-------------|
| PS3 | 1 | `00 00 00 01` | Big Endian | 64KB blocks |
| Xbox 360 | 1 | `00 00 00 01` | Big Endian | 64KB blocks |
| PC | 5 | `05 00 00 00` * | Little Endian * | Single stream * |
| Wii (Reflex) | 418 | `00 00 01 A2` | Big Endian | Single stream |

\* **CoD4 PC is presumed, not verified** — no PC samples have been tested. The shape is
assumed to match WaW PC (single LE zlib stream), so the version would store little-endian
as `05 00 00 00`, but the exact byte order is unconfirmed. The **Wii (Reflex Edition)**
row *is* verified: single big-endian zlib stream, version `0x1A2`.

## Compression

### PS3 / Xbox 360
The zone data is split into 64KB (0x10000 byte) blocks, each compressed with zlib deflate.

```
[2-byte compressed size][compressed block data]
[2-byte compressed size][compressed block data]
...
[00 01] (end marker)
```

- Each block is preceded by a 2-byte big-endian length prefix
- The zlib header bytes (`78 DA`) are stripped from the compressed data
- The 4-byte Adler-32 checksum is retained at the end of each block
- Final marker `00 01` indicates end of compressed data

### PC
PC FastFiles use a different compression scheme with the entire zone as a single zlib stream.

### Wii
Wii FastFiles use a single continuous zlib stream (not block-based). The zlib header (`78 01`) is present at the start of the compressed data.

```
[8-byte magic][4-byte version][zlib stream...]
```

## Decompression

To extract the zone file:

1. Read and validate the 8-byte magic header
2. Read the 4-byte version to identify platform
3. For PS3/Xbox 360:
   - Read 2-byte block size
   - If block size is `00 01`, decompression is complete
   - Read that many bytes of compressed data
   - Prepend `78 DA` zlib header and decompress
   - Repeat until end marker
4. For Wii:
   - Read all remaining data as a single zlib stream
   - Decompress with standard zlib

## Recompression

To create a FastFile from a zone:

1. Write the 8-byte magic (`IWffu100` for unsigned)
2. Write the 4-byte version for target platform
3. Split zone into 64KB blocks
4. For each block:
   - Compress with zlib deflate
   - Strip the 2-byte zlib header (`78 DA`)
   - Write 2-byte compressed length (big-endian)
   - Write compressed data (with Adler-32 checksum)
5. Write end marker `00 01`

## References

- [codresearch.dev - FastFiles and Zone files](https://codresearch.dev/index.php/FastFiles_and_Zone_files_(MW2))
