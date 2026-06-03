#pragma once

#include "Defines.h"

typedef struct
{
    int32_t Valid;
    char pad_0004[4];
    int32_t ClientNum;
    char Name[32];
    int32_t Team;
    char pad_0030[12];
    int32_t Rank;
    char pad_0040[12];
    char ClanTag[4];
    char pad_0050[16];
    char BodyModel[32];
    char pad_0080[32];
    char HeadModel[32];
    char pad_00C0[988];
    int32_t Pose;
    char pad_04A0[20];
    int32_t Shooting;
    char pad_04B8[4];
    int32_t Zoomed;
    char pad_04C0[80];
    int32_t WeaponNum;
    char pad_0514[72];
} ClientInfo_t;
