# DemonWare Authentication Protocol Research

This document contains reverse engineering findings from analyzing Call of Duty: Black Ops PC dedicated server (`CoDMPServer.exe`) with debug symbols. cod4/5 Uses SHA256 for hashing (BO1 uses Tiger-192)

**Source:** `CoDMPServer.exe` (Black Ops Server, 2011-08-31, with MAP+PDB symbols)
**DemonWare Source Path:** `C:\projects_pc\cod\codsrc\DemonWare\bdLobby\`

---

## Table of Contents

1. [Auth Response Format](#auth-response-format)
2. [Message Types](#message-types)
3. [Encryption Scheme](#encryption-scheme)
4. [bdAuthTicket Structure](#bdauthticket-structure)
5. [Cryptographic Constants](#cryptographic-constants)
6. [Key Functions](#key-functions)
7. [Implementation Notes](#implementation-notes)

---

## Auth Response Format

The auth response is read using `bdBitBuffer` and follows this structure:

```
┌─────────────────────────────────────────────────────────┐
│ uint32   error_code      (700 = success)                │
│ uint32   iv_seed         (random, used for IV calc)     │
│ byte[128] encrypted_user_ticket                         │
│ byte[128] encrypted_lsg_ticket  (optional, some types)  │
└─────────────────────────────────────────────────────────┘
```

### Error Codes

| Code | Meaning |
|------|---------|
| 700 | Success |
| 706 | Auth failed (magic number mismatch) |
| 715 | Generic failure |
| 716 | Auth failed (magic number mismatch, type 0x0B) |

---

## Message Types

The `bdAuthService::handleReply` function switches on message type (`a2` parameter):

| Type (Dec) | Type (Hex) | Description |
|------------|------------|-------------|
| 11 | 0x0B | Standard auth response |
| 13 | 0x0D | Alternative auth |
| 15 | 0x0F | Alternative auth |
| 17 | 0x11 | Alternative auth |
| 21 | 0x15 | Get usernames for license |
| 25 | 0x19 | LSG (Live Settings Gateway) auth |
| 27 | 0x1B | RSA/CD key auth |
| 29 | 0x1D | Steam auth |

### Standard Auth (Type 0x0B) Flow

```c
// 1. Read IV seed
bdBitBuffer::readUInt32(a3, &iv_seed);

// 2. Calculate IV from seed
bdCryptoUtils::calculateInitialVector(iv_seed, iv);  // Tiger192 hash

// 3. Read encrypted ticket (1024 bits = 128 bytes)
bdBitBuffer::readBits(a3, encrypted_ticket, 0x400u);

// 4. Decrypt ticket
bdCryptoUtils::decrypt(key, iv, encrypted_ticket, decrypted_ticket, 0x80u);

// 5. Deserialize and validate
bdAuthTicket::deserialize(ticket, decrypted_ticket);
if (ticket->m_magicNumber == BD_MAGIC_NUMBER) {
    // Success - copy session key, store user ID
}
```

---

## Encryption Scheme

### Algorithm: 3DES-CBC

```c
void bdCryptoUtils::decrypt(
    unsigned __int8 *key,      // 24-byte 3DES key
    unsigned __int8 *iv,       // 24-byte IV (from Tiger192)
    unsigned __int8 *input,    // Encrypted data
    unsigned __int8 *output,   // Decrypted output
    unsigned int size          // Data size (128 bytes)
)
{
    bdCypher3Des cipher;
    bdCypher3Des::init(&cipher, key, 0x18);  // 0x18 = 24 bytes
    bdCypher3Des::decrypt(&cipher, iv, input, output, size);
}
```

### IV Calculation

```c
void bdCryptoUtils::calculateInitialVector(unsigned int iv_seed, unsigned __int8 *iv_out)
{
    // IV = Tiger192 hash of the 4-byte iv_seed
    bdHashTiger192 hasher;
    uint8_t seed_bytes[4] = { iv_seed as bytes };
    bdHashTiger192::hash(&hasher, seed_bytes, 4, iv_out, 24);
}
```

**Summary:**
- IV seed (4 bytes) → Tiger-192 hash → IV (24 bytes)

### Key Derivation

```c
void bdAuthUtility::getLicenseKey(char *license_data, char *key_out)
{
    if (license_data != NULL) {
        // Key = Tiger192 hash of license data
        bdHashTiger192::hash(license_data, strlen(license_data), key_out, 24);
    } else {
        // Use hardcoded fallback key
        memcpy(key_out, &BD_MAGIC_LICENSE_KEY, 24);
    }
}
```

---

## bdAuthTicket Structure

**Total Size: 128 bytes (0x80)**

```c
struct bdAuthTicket {
    uint32_t  m_magicNumber;           //   4 bytes - Must be 0xEFBDADDE
    uint8_t   m_type;                  //   1 byte  - Ticket type
    uint32_t  m_titleID;               //   4 bytes - Game title ID
    uint32_t  m_timeIssued;            //   4 bytes - Unix timestamp (issued)
    uint32_t  m_timeExpires;           //   4 bytes - Unix timestamp (expires)
    uint64_t  m_licenseID;             //   8 bytes - License ID
    uint64_t  m_userID;                //   8 bytes - User ID
    char      m_username[64];          //  64 bytes - Null-terminated username
    uint8_t   m_sessionKey[24];        //  24 bytes - Session key for future crypto
    uint8_t   m_usingHashMagicNumber[3]; // 3 bytes - Hash magic indicator
    uint8_t   m_hash[4];               //   4 bytes - Ticket hash/checksum
};                                     // Total: 128 bytes
```

### Field Details

| Offset | Size | Field | Description |
|--------|------|-------|-------------|
| 0 | 4 | m_magicNumber | Must be `0xEFBDADDE` for valid ticket |
| 4 | 1 | m_type | Ticket type identifier |
| 5 | 4 | m_titleID | Game title ID (e.g., COD5 title ID) |
| 9 | 4 | m_timeIssued | Unix timestamp when ticket was created |
| 13 | 4 | m_timeExpires | Unix timestamp when ticket expires |
| 17 | 8 | m_licenseID | Unique license identifier |
| 25 | 8 | m_userID | Unique user identifier |
| 33 | 64 | m_username | Player username (null-terminated string) |
| 97 | 24 | m_sessionKey | Session key for subsequent encrypted comms |
| 121 | 3 | m_usingHashMagicNumber | Hash magic bytes |
| 124 | 4 | m_hash | Checksum/hash of ticket |

---

## Cryptographic Constants

### BD_MAGIC_NUMBER

```c
const uint32_t BD_MAGIC_NUMBER = 0xEFBDADDE;
```

Located at `.rdata:00DB4698` in the executable.

This value must be the first 4 bytes of every valid decrypted auth ticket.

### BD_MAGIC_LICENSE_KEY

```c
const uint8_t BD_MAGIC_LICENSE_KEY[24] = {
    0xDE, 0xAD, 0xBE, 0xEF,  // "DEADBEEF"
    0xDE, 0xAD, 0xBE, 0xEF,  // "DEADBEEF"
    0xDE, 0xAD, 0xBE, 0xEF,  // "DEADBEEF"
    0xDE, 0xAD, 0xBE, 0xEF,  // "DEADBEEF"
    0x00, 0x00, 0x00, 0x00,  // Null padding
    0x00, 0x00, 0x00, 0x00   // Null padding
};
```

Located at `.rdata:00DB4680` in the executable.

**Hex string:** `DEADBEEFDEADBEEFDEADBEEFDEADBEEF0000000000000000`

This is the fallback 3DES key used when no license data is provided.

### XOR Key (for LSG auth type 0x19)

```c
const char LSG_XOR_KEY[] = "43FCB2ACF2D72593DD7CD1C69E0F03C07229F4C83166F7B05BA0C5FE3AA3A2D93EK2495783KDKN92939DK";
// 86 bytes
```

Used to XOR license data in LSG authentication flow.

---

## Key Functions

### bdAuthService::handleReply
- **Purpose:** Parses auth server response
- **Location:** `bdLobby\bdAuthService.cpp`
- **Key operations:** Reads error code, IV seed, encrypted ticket; decrypts and validates

### bdAuthTicket::deserialize
- **Purpose:** Deserializes decrypted ticket bytes into structure
- **Uses:** `bdBytePacker::removeBuffer` for sequential field extraction

### bdCryptoUtils::decrypt
- **Purpose:** 3DES decryption wrapper
- **Algorithm:** 3DES-CBC with 24-byte key and IV

### bdCryptoUtils::calculateInitialVector
- **Purpose:** Generates IV from seed
- **Algorithm:** Tiger-192 hash of 4-byte seed

### bdAuthUtility::getLicenseKey
- **Purpose:** Derives 3DES key from license or uses fallback
- **Fallback:** `BD_MAGIC_LICENSE_KEY`

---

## Implementation Notes

### Building a Valid Auth Response

1. **Create bdAuthTicket struct:**
   ```c
   bdAuthTicket ticket = {
       .m_magicNumber = 0xEFBDADDE,
       .m_type = 0,
       .m_titleID = <game_title_id>,
       .m_timeIssued = time(NULL),
       .m_timeExpires = time(NULL) + 86400,  // 24 hours
       .m_licenseID = <random_64bit>,
       .m_userID = <user_id>,
       .m_username = "PlayerName",
       .m_sessionKey = <random_24_bytes>,
       .m_usingHashMagicNumber = {0, 0, 0},
       .m_hash = {0, 0, 0, 0}
   };
   ```

2. **Generate IV seed and calculate IV:**
   ```c
   uint32_t iv_seed = random();
   uint8_t iv[24];
   tiger192_hash(&iv_seed, 4, iv);
   ```

3. **Encrypt ticket with 3DES:**
   ```c
   uint8_t key[24] = BD_MAGIC_LICENSE_KEY;
   uint8_t encrypted[128];
   des3_cbc_encrypt(key, iv, &ticket, encrypted, 128);
   ```

4. **Build response packet:**
   ```c
   packet.writeUInt32(700);           // Success
   packet.writeUInt32(iv_seed);       // IV seed
   packet.writeBits(encrypted, 1024); // 128 bytes = 1024 bits
   ```

### Libraries Needed

- **Tiger-192 hash:** For IV calculation and key derivation
- **3DES:** For ticket encryption (CBC mode)
- **Bit buffer:** For packing response in bdBitBuffer format

### Potential Issues

1. **Endianness:** DemonWare uses mixed endianness (check `bdBitBuffer` implementation)
2. **Bit packing:** Data is read with `readBits()`, may have specific bit alignment
3. **Game-specific variations:** COD:WAW may have slight differences from Black Ops

---

## References

- Executable: `CoDMPServer.exe` (Call of Duty: Black Ops Server)
- Debug symbols: MAP+PDB files (2011-08-31 build)
- Source paths found in binary: `C:\projects_pc\cod\codsrc\DemonWare\`
- Related projects:
  - [Open BitDemon Emulator](https://github.com/Laupetin/open-bitdemon-emulator)
  - [DemonWare COD4](https://github.com/Demonware-Custom-Server/demonware-cod4)

