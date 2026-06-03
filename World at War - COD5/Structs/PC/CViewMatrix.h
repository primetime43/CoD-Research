#pragma once

typedef struct
{
    vec3_t Recoil[3];
    char pad_000C[24];
    float ViewAngleX;
    float viewAngleY;
    char pad_002C[56];
    vec3_t ViewAxis[3];
    char pad_0070[24];
    vec3_t vOrigin;
} CViewMatrix_t;
