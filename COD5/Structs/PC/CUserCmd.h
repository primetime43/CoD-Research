#pragma once

class CUserCmd
{
public:
    int servertime;
    int buttons;
    int viewangles[3];
    int weapon_index;

private:
    char __pad00[20];
};
