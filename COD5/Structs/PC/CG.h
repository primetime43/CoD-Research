#pragma once

typedef struct
{
    int32_t ClientNum;
    char pad_0004[28];
    CSnapShot_t* CurrentSnap;
    CSnapShot_t* NextSnap;
    char pad_0028[48];
    vec3_t vOrigin;
    char pad_0064[72];
    float Strafe;
    char pad_00B0[84];
    byte unknown1;
    byte unknown2;
    byte InMenu;
    byte unknown3;
    char pad_0108[132];
    int32_t DamageCounter;
    char pad_0190[12];
    int32_t Health;
    int32_t Damage;
    int32_t StartHealth;
    char pad_01A8[12];
} CG_t;
