#pragma once

class CCGS
{
private:
    char __pad00[35];

public:
    char m_szGameMode[4];

private:
    char __pad01[28];

public:
    char m_szServerName[16];

private:
    char __pad04[236];

public:
    int m_nMaxClients;
};
