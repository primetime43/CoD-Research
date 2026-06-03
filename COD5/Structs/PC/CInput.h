#pragma once

#include "CUserCmd.hpp"

class CInput
{
public:
    inline CUserCmd* GetCommand(const int nCmdNum)
    {
        return &m_Commands[(nCmdNum & 0x7F)];
    }

    CUserCmd m_Commands[128];
    int m_nCurrentCommand;
};
