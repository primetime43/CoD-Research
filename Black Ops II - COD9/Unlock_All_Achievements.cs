private void unlockAchivements()
        {
            string[] Achievements = new string[80];
            Achievements[0] = "# ZM_DLC2_TRAPPED_IN_TIME";
            Achievements[1] = "# ZM_DLC2_POP_GOES_THE_WEASEL";
            Achievements[2] = "# ZM_DLC2_FULL_LOCKDOWN";
            Achievements[3] = "# ZM_DLC2_GG_BRIDGE";
            Achievements[4] = "# ZM_DLC2_PARANORMAL_PROGRESS";
            Achievements[5] = "# ZM_DLC2_A_BURST_OF_FLAVOR";
            Achievements[6] = "# ZM_DLC2_FEED_THE_BEAST";
            Achievements[7] = "# ZM_DLC2_ACID_DRIP";
            Achievements[8] = "# ZM_DLC2_MAKING_THE_ROUNDS";
            Achievements[9] = "# ZM_DLC1_MONKEY_SEE_MONKEY_DOOM";
            Achievements[10] = "# ZM_DLC2_PRISON_SIDEQUEST";
            Achievements[11] = "# ZM_DLC1_I_SEE_LIVE_PEOPLE";
            Achievements[12] = "# ZM_DLC1_FACING_THE_DRAGON";
            Achievements[13] = "# ZM_DLC1_POLYARMORY";
            Achievements[14] = "# ZM_DLC1_IM_MY_OWN_BEST_FRIEND";
            Achievements[15] = "# ZM_DLC1_MAD_WITHOUT_POWER";
            Achievements[16] = "# ZM_DLC1_SLIPPERY_WHEN_UNDEAD";
            Achievements[17] = "# ZM_DLC1_SHAFTED";
            Achievements[18] = "# ZM_HAPPY_HOUR";
            Achievements[19] = "# ZM_DLC1_VERTIGONER";
            Achievements[20] = "# ZM_DLC1_HIGHRISE_SIDEQUEST";
            Achievements[21] = "# ZM_YOU_HAVE_NO_POWER_OVER_ME";
            Achievements[22] = "# ZM_FUEL_EFFICIENT";
            Achievements[23] = "# ZM_I_DONT_THINK_THEY_EXIST";
            Achievements[24] = "# ZM_UNDEAD_MANS_PARTY_BUS";
            Achievements[25] = "# ZM_STANDARD_EQUIPMENT_MAY_VARY";
            Achievements[26] = "# ZM_DANCE_ON_MY_GRAVE";
            Achievements[27] = "# ZM_TRANSIT_SIDEQUEST";
            Achievements[28] = "# ZM_THE_LIGHTS_OF_THEIR_EYES";
            Achievements[29] = "# ZM_DONT_FIRE_UNTIL_YOU_SEE";
            Achievements[30] = "# MP_MISC_3";
            Achievements[31] = "# MP_MISC_5";
            Achievements[32] = "# MP_MISC_4";
            Achievements[33] = "# SP_MISC_10K_SCORE_ALL";
            Achievements[34] = "# MP_MISC_2";
            Achievements[35] = "# MP_MISC_1";
            Achievements[36] = "# SP_BACK_TO_FUTURE";
            Achievements[37] = "# SP_MISC_WEAPONS";
            Achievements[38] = "# SP_STORY_99PERCENT";
            Achievements[39] = "# SP_STORY_CHLOE_LIVES";
            Achievements[40] = "# SP_MISC_ALL_INTEL";
            Achievements[41] = "# SP_STORY_MENENDEZ_CAPTURED";
            Achievements[42] = "# SP_STORY_HARPER_LIVES";
            Achievements[43] = "# SP_STORY_LINK_CIA";
            Achievements[44] = "# SP_STORY_OBAMA_SURVIVES";
            Achievements[45] = "# ZM_DLC3_WHEN_THE_REVOLUTION_COMES";
            Achievements[46] = "# SP_STORY_FARID_DUEL";
            Achievements[47] = "# SP_STORY_HARPER_FACE";
            Achievements[48] = "# SP_STORY_MASON_LIVES";
            Achievements[49] = "# SP_RTS_SOCOTRA";
            Achievements[50] = "# SP_RTS_PAKISTAN";
            Achievements[51] = "# SP_RTS_CARRIER";
            Achievements[52] = "# SP_RTS_DRONE";
            Achievements[53] = "# SP_RTS_AFGHANISTAN";
            Achievements[54] = "# SP_RTS_DOCKSIDE";
            Achievements[55] = "# SP_ONE_CHALLENGE";
            Achievements[56] = "# SP_ALL_CHALLENGES_IN_LEVEL";
            Achievements[57] = "# SP_ALL_CHALLENGES_IN_GAME";
            Achievements[58] = "# SP_VETERAN_FUTURE";
            Achievements[59] = "# SP_VETERAN_PAST";
            Achievements[60] = "# SP_COMPLETE_HAITI";
            Achievements[61] = "# SP_COMPLETE_LA";
            Achievements[62] = "# SP_COMPLETE_BLACKOUT";
            Achievements[63] = "# SP_COMPLETE_YEMEN";
            Achievements[64] = "# SP_COMPLETE_PANAMA";
            Achievements[65] = "# SP_COMPLETE_KARMA";
            Achievements[66] = "# SP_COMPLETE_PAKISTAN";
            Achievements[67] = "# SP_COMPLETE_NICARAGUA";
            Achievements[68] = "# SP_COMPLETE_AFGHANISTAN";
            Achievements[69] = "# SP_COMPLETE_MONSOON";
            Achievements[70] = "# SP_COMPLETE_ANGOLA";
            Achievements[71] = "# ZM_DLC3_FSIRT_AGAINST_THE_WALL";
            Achievements[72] = "# ZM_DLC3_AWAKEN_THE_GAZEBO";
            Achievements[73] = "# ZM_DLC3_MAZED_AND_CONFUSED";
            Achievements[74] = "# ZM_DLC3_BURIED_SIDEQUEST";
            Achievements[75] = "# ZM_DLC3_IM_YOUR_HUCKLEBERRY";
            Achievements[76] = "# ZM_DLC3_ECTOPLASMIC_RESIDUE";
            Achievements[77] = "# ZM_DLC3_DEATH_FROM_BELOW";
            Achievements[78] = "# ZM_DLC3_CANDYGRAM";
            Achievements[79] = "# ZM_DLC3_REVISIONIST_HISTORIAN";
            //Achievements[69]="# SP_COMPLETE_ANGOLA";
            for (int i = 0; i < 80; i++)
            {
                this.SV_GameSendServerCommand(-1, Achievements[i]);
                System.Threading.Thread.Sleep(50);
                this.SV_GameSendServerCommand(-1, Achievements[i]);
            }
            //Do The Same Thing Again Just To Make Sure You Get Everything!
            System.Threading.Thread.Sleep(50);
            for (int i = 0; i < 80; i++)
            {
                this.SV_GameSendServerCommand(-1, Achievements[i]);
                System.Threading.Thread.Sleep(50);
                
            }
            System.Threading.Thread.Sleep(50);
           //Same as Above
            for (int i = 0; i < 80; i++)
            {
                this.SV_GameSendServerCommand(-1, Achievements[i]);
                System.Threading.Thread.Sleep(50);
                
            }
            this.SV_GameSendServerCommand(-1, ") \"^2All Achievementss Unlocked!!\"");
 
        }
