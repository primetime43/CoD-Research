#pragma once

typedef struct
{
    char pad_0000[8];
    int32_t ScreenX;
    int32_t ScreenY;
    char pad_0010[16];
    char GameMode[4];
    char pad_0024[28];
    char ServerName[32];
    char pad_0060[224];
    int32_t MaxClients;
    char MapName[32];
    char pad_0164[24];
} CGS_t;
