#pragma once

typedef struct
{
    int32_t x;
    int32_t y;
    int32_t iWidth;
    int32_t iHeight;
    float FovX;
    float FovY;
    float FovTotal;
    vec3_t ViewOrg;
    char pad_0028[4];
    vec3_t ViewAxis[3];
    char pad_0050[17044];
    vec3_t RefdefViewAngles;
} CRefdef_t;
