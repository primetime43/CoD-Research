#pragma once

#include "Defines.h"

typedef struct
{
    int16_t pad_0000;
    bool Alive;
    char pad_0003[37];
    vec3_t vOrigin;
    float vAngleY;
    float vAngleX;
    char pad_003C[160];
    int32_t Flag;
    char pad_00E0[12];
    vec3_t vOldOrigin;
    char pad_00F8[24];
    float vOldAngleX;
    float vOldAngleY;
    char pad_0118[80];
    int32_t ClientNum;
    int32_t Type;
    int32_t Pose;
    char pad_0174[12];
    vec3_t vNewOrigin;
    char pad_018C[24];
    float vNewAngleX;
    float vNewAngleY;
    char pad_01AC[104];
    int32_t G_WeaponNum;
    char pad_0218[56];
    int32_t WeaponNum;
    char pad_0254[100];
    int32_t Valid;
} CEntity_t;
