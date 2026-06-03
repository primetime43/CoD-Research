#pragma once

class CTrace
{
private:
    char __pad00[16];

public:
    float m_flFraction;

private:
    char __pad01[110];

public:
    int m_nSurfaceFlags;
};
