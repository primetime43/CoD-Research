#pragma once

class CClientInfo
{
public:
    int m_nValid;

private:
    char __pad00[4];

public:
    int m_nClientNum;
    char m_szName[32];
    int m_nTeam;

private:
    char __pad01[1324];
};
