Credits: https://www.thetechgame.com/Archives/t=3520623/ultimate-world-at-war-dvar-list-w-explanations.html

# Ultimate World at War DVAR List

### Basic Colors

- `^0` - Black
- `^1` - Red
- `^2` - Green
- `^3` - Yellow
- `^4` - Blue
- `^5` - Cyan
- `^6` - Brown
- RGBA Colors (Thanks to CFGmodding)
- `1 0 0 1` - Red
- `0 1 0 1` - Green
- `0 0 1 1` - Blue
- `1 1 0 1` - Yellow
- `0 1 1 1` - Cyan
- `1 0 1 1` - Pink

### Perks

- `specialty_bulletaccuracy` - Steady Aim
- `specialty_bulletpenetration` - Deep impact
- `specialty_fastreload` - Sleight of hand//self explanatory
- `specialty_rof` - Double Tap
- `specialty_altmelee` - Bowie Knife Acquired
- `specialty_weapon_satchel` - Satchel Charges
- Perk DVARS
- `perk_weapSpreadMultiplier` - Steady aim spread multipled by value, 0 - No spread
- `perk_weapReloadMultiplier` - Sleight of hand time, 0 - Instant reload, no reload time.
- `perk_weapRateMultiplier` - Weapon rate percentage, 0 - Fastest rate
- `perk_altmeleedamage` - Damage of individual bowie knife, 0 - none.
- `perk_bulletPenetrationMultiplier` - Multiplier for bullet penatration, max - 30

### Ping Manipulation

- `cg_ScoresPing_LowColor` - Color of Ping/ Low Ping
- `cg_ScoresPing_MedColor` - Color of Ping/Medium ping (Around 2-3 Bars)
- `cg_scoreboardpingtext` - Shows ping via numbers, instead of bars
- `cg_scoresPing_HighColor` - Color of Ping/High Ping, around 4 bars or so.
- `ui_scoresPing_maxbars` - Max number of Ping Bars

### Disable Spawning

- `ai_disableSpawn` - Disables spawn(0 default value)

### Fall/Jump Height

- `bg_fallDamageMaxHeight` - The player will have to take the integer height of falling to take max damage
- `bg_fallDamageMinHeight` - The player will have to take the integer height of falling to take min. damage
- Jump Height
- `jump_height` - Jump height range, (range 0-1000)

### Thirdperson View

- `cg_thirdperson` - Enables third person (Default value-0)

### Tracers

- `cg_firstpersontracerchance` - Mere chance of Tracer, randomly chosen.
- Tracer Manipulation
- `cg_tracerlength` - Length of specific tracer
- `cg_tracerscrewradius` - Radius of spinning tracer (Looks like corkscrew)
- `cg_tracerspeed` - Speed of specific units/second
- `cg_tracerwidth` - Tracer width
- Length of Corkscrew
- `tracer_screwdist` - Distance of tracer in complete corkscrew

### Revive Manipulation

- `arcademode_score_revive` - Arcade mode enabled, set points per revive
- Sound
- `r_revivefx_debug` - Enables sounds that would enable in last stand
- Distance
- `revive_Trigger_Radius` - How far away you must be from someone to revive them

### Compass

- `compass` - gives or takes away compass

### Gravity

- `g_gravity` - Gravity in/per second
- `phy_gravity` - Zombie death gravity

### Miniconsole

- `con_minicon` - Enables Miniconsole
- Minicon Manipulation
- `con_miniconlines` - Amount of lines/minicon
- `con_minicontime` - Time for miniconsole message

### Magic Box/Chest Manipulation

- `magic_chest_movable_0` - Disables moving of Mystery Box

### Speed/Friction

- `g_speed` - Player speed, indicated by integer
- `player_sprintSpeedScale` - Speed applied when sprinting, indicated by integers 0-5
- Friction
- `friction "5.5"` - Friction applied
- `phys_frictionScale "1"` - Phys friction applied, 0-5

### Melee/Melee Range

- `ai_meleerange` - Max range per melee per dog.
- Bowie Knife Manipulation
- `perk_altMeleeDamage` - Amount of damage per Bowie Knife
- Melee Manipulation
- `player_meleedamagemultiplier` - Amount of damage per knife.
- `player_meleerange` - Range of melee knife

### Ammo Manipulation

- `player_sustainammo` - No ammo lost when firing from clip

### Thermal Vision

- `self setClientDvar( "sf_use_invert", "1" );` - Enables thermal vision.

### Contrast Vision

- `self setClientDvar( "sf_use_contrast", "1" );` - Enables contrast vision.

### Day Time Vision

- self setClientDvar( "r_lightTweakSunLight",  "1.0" );
- self setClientDvar( "r_lightTweakSunColor", "2.0 2.0" );
- `self setClientDvar( "r_fog", "0" );` - Enables day time vision.

### Purple Vision

- self SetClientDvar( "r_revivefx_lighttintcenter", "2 2 2" );
- self SetClientDvar( "r_revivefx_lighttintedge", "2 0 2" );
- self SetClientDvar( "r_revivefx_contrastcenter", "1.5" );
- self SetClientDvar( "r_revivefx_contrastedge", "4" );
- self SetClientDvar( "r_revivefx_darktintcenter", "1.5 1 1.5" );
- self SetClientDvar( "r_revivefx_darktintedge", "0 0 1" );
- self SetClientDvar( "r_revivefx_blurradiusedge", "3" );
- `self SetClientDvar( "r_revivefx_debug", "1" );` - Enables Purple vision

### Dog Manipulations

- `Zombie_dog_animset` - Dogs do not hurt you

### 11th Prestige

- `self maps\_challenges_coop::statSet( "plevel", 11 );` - Sets prestige to 11

### 10th Prestige

- `self maps\_challenges_coop::statSet( "plevel", 10 );` - sets prestige to 10

### Prestige DVAR Explanation

- self maps\_challenges_coop::statSet( "plevel",
- here
- `);` - The desired prestige that you would like to entire goes in the bold text, here. The prestige goes from 1-11, so if you want to make it 8th prestige it would be;
- self maps\_challenges_coop::statSet( "plevel", 8 );
- To manipulate this, simply exchange the bold here, for the desired prestige.

### Laser on Gun

- `self setClientDvar( "cg_laserForceOn", "1" );` - Enables laser from gun, points red dot towards crosshair.

### Aimbot

- AIMBOT aim_autoaim_enabled "1"
- aim_autoaim_lerp "999"
- aim_lockon_debug "1"
- aim_lockon_enabled "1"
- aim_lockon_strength "9"
- `aim_lockon_deflection "0.0005"` - Aimbot enabled, every shot is directed towards zombie.

### Promod

- `self setClientDvar( "cg_fov", "95" );` - Enables promod, gun is stretched out in front for those who don't know.

### Wallhack

- wallhack ( self setClientDvar( "r_znear_depthhack", "2" );
- self setClientDvar( "r_znear", "57" );
- self setClientDvar( "r_zFeather", "4" );
- `self setClientDvar( "r_zfar", "0" );` - Enables wallhack, can see enemies through walls.

### Overhead Name Color Manipulation

- `self setClientDvar( "cg_overheadNamesGlow", "1 0 0 1" );` - Color RGBa, change accordingly.

### Message of the Day/Text on Screen

- `self setClientDvar( "g_motd", "Place text here" );` - When game is started, text appears on screen.
- `self setClientDvar( "motd", "Place text here" );` - When Game is started, text appears on screen.

### Custom Class Names

- `self setClientDvar( "customclass1", "Text here" );` - Changes custom class name, color codes can be placed.

### Score Editing (Wins, Kills, Deaths, Ratio, Etc.)
Credits to XxTheShotgunxX from XMB, for this code, I just copied and pasted this from his thread, all credits to him for this code. Self explanatory;
<details>

```csharp
 self maps\_challenges_coop::statSet( "kills", 2147473647 ); 
 self maps\_challenges_coop::statSet( "wins", 2147473647 ); 
 self maps\_challenges_coop::statSet( "score", 2147473647 ); 
 self maps\_challenges_coop::statset( "kill_streak", 2147483647 ); 
 self maps\_challenges_coop::statset( "win_streak", 2147483647 ); 
 self maps\_challenges_coop::statSet( "headshots", 2147473647 ); 
 self maps\_challenges_coop::statSet( "deaths", -2147473647 ); 
 self maps\_challenges_coop::statSet( "assists", 2147473647 ); 
 self maps\_challenges_coop::statSet( "dm_kills", 2147473647 ); 
 self maps\_challenges_coop::statSet( "ctf_kills", 2147473647 ); 
 self maps\_challenges_coop::statSet( "dom_kills", 2147473647 ); 
 self maps\_challenges_coop::statSet( "koth_kills", 2147473647 ); 
 self maps\_challenges_coop::statSet( "sd_kills", 2147473647 ); 
 self maps\_challenges_coop::statSet( "twar_kills", 2147473647 ); 
 self maps\_challenges_coop::statSet( "sur_kills", 2147473647 ); 
 self maps\_challenges_coop::statSet( "sab_kills", 2147473647 ); 
 self maps\_challenges_coop::statSet( "dm_wins", 2147473647 ); 
 self maps\_challenges_coop::statSet( "koth_wins", 2147473647 ); 
 self maps\_challenges_coop::statSet( "dom_wins", 2147473647 ); 
 self maps\_challenges_coop::statSet( "sab_wins", 2147473647 ); 
 self maps\_challenges_coop::statSet( "twar_wins", 2147473647 ); 
 self maps\_challenges_coop::statSet( "sd_wins", 2147473647 ); 
 self maps\_challenges_coop::statSet( "sur_wins", 2147473647 ); 
 self maps\_challenges_coop::statSet( "ctf_wins", 2147473647 ); 
 self maps\_challenges_coop::statSet( "dm_score", 2147473647 ); 
 self maps\_challenges_coop::statSet( "dom_score", 2147473647 ); 
 self maps\_challenges_coop::statSet( "koth_score", 2147473647 ); 
 self maps\_challenges_coop::statSet( "sab_score", 2147473647 ); 
 self maps\_challenges_coop::statSet( "sd_score", 2147473647 ); 
 self maps\_challenges_coop::statSet( "twar_score", 2147473647 ); 
 self maps\_challenges_coop::statSet( "sur_score", 2147473647 ); 
 self maps\_challenges_coop::statSet( "ctf_score", 2147473647 ); 
 self maps\_challenges_coop::statset( "dm_win_streak", 2147483647 ); 
 self maps\_challenges_coop::statset( "dom_win_streak", 2147483647 ); 
 self maps\_challenges_coop::statset( "koth_win_streak", 2147483647 ); 
 self maps\_challenges_coop::statset( "sab_win_streak", 2147483647 ); 
 self maps\_challenges_coop::statset( "sd_win_streak", 2147483647 );  
```

</details>

### Triple Tap

- `self setClientDvar( "perk_weapRateMultiplier", "0.001" );` - Enables triple tap, extreme rate of fire.

### Juggernog x2

- `self setClientDvar( "perk_armorvest", "0" );` - Juggernog increases to demi-god mode.

### Instant Sleight of Hand

- `self setClientDvar( "perk_weapReloadMultiplier", "0.001"` - Sleight of hand is instant.

### No zombie spawn

- `self setClientDvar( "ai_disableSpawn", "1" )` - Disables all zombie spawning.

### Unbound Clan Tag

- `self setClientDvar( "clanname", "@@@@" );` - Sets clan tag to "@@@@"

### Custom Clan Tag Manipulation

- self setClientDvar( "clanname", "
- here
- `" );` - to manipulate the custom clan tag, you would just need to change where it says "here" to whatever clan tag that you want to enable. So if you wanted it to be "TTG" as your clan tag, just do the following.
- `self setClientDvar( "clanname", "TTG" );` - This will work with other clan tags as well.

### Developer Clan Tags

- `self setClientDvar( "developeruser", "1" );` - Enables developer clan tags (Rain, CYCL, move, etc.)

### Force Host

- `self setClientDvar( "party_hostname", "GT" );` - Enables force host, change GT value.

### Multicolored No Ammo Indicator

- `self setClientDvar( "lowAmmoWarningNoReloadColor1", "0 0 1 1" );` - Changes color of low ammo warning.

### Noclip/UFO Mode

- `no clip` - Enables noclip, noclip allows you to pass through walls, and fly under no restrictions.
- `ufo` - enables UFO, ufo allows you to basically fly through walls, under certain restrictions though.

### Longer Last Stand Time

- `self setClientDvar( "player_lastStandBleedoutTime", "250" );` - Enables last stand time, for about 4 or so minutes, gives time for friends to take out enemies, then revive you.

### God Mode/Demi God Mode

- `god` - Enables god mode, can not die in any way. Must be disable in order to die.
- `demigod` - Enables demi god, takes large amount of zombie hits in order to die, also have similar qualities to god mode.

### Give All Weapons

- `give_all` - Enables give all, all weapons are acquired to the person who enables the DVAR, all guns specified for that map are given to them.

### AC130
- Complete credits to Toxic X Plague
<details>

```csharp
 AC130() //only thing that needs threading 
 { 
 self endon( "AC130Done" ); 
 { 
 wait 1; 
 self EnableInvulnerability(); 
 self setClientDvar( "cg_drawcrosshair", "0" ); 
 self setClientDvar( "cg_drawGun", "0" ); 
 self setClientDvar( "ui_hud_hardcore", "1" ); 
 self setClientDvar( "cg_fov", "100" ); 
 self setClientDvar( "g_gravity", "1" ); 
 self setClientDvar( "jump_height", "999" ); 
 self VisionSetNaked( "cheat_bw_invert_contrast", 1); 
 self SetPerk("specialty_rof"); 
 self thread FadeToBlack(); 
 self TakeAllWeapons(); 
 wait .5; 
 self giveWeapon( "panzerschrek_zombie_upgraded" ); 
 wait .2; 
 self giveWeapon( "zombie_colt_upgraded" ); 
 wait .2; 
 self giveWeapon( "zombie_ppsh_upgraded" ); 
 wait .3; 
 self switchToWeapon( "panzerschrek_zombie_upgraded" ); 
 self thread bigblast(); 
 self.bigblast = 1; 
 self.smallblast = 0; 
 self.acmachine = 0; 
 self.ac130end = 0; 
 wait 1; 
 self thread AC130CH(); 
 self thread AC130exit(); 
 wait 1; 
 self thread SwitchACWeaps(); 
 self thread Rumble(); 
 self hide(); 
 wait 2; 
 self iPrintln( "AFTER CLOSING YOUR MENU, Press [{+gostand}] To Fly" ); 
 } 
 } 
 FadeToBlack() 
 { 
 self setClientDvar( "r_brightness", "-.2" ); 
 wait .3; 
 self setClientDvar( "r_brightness", "-.4" ); 
 wait .3; 
 self setClientDvar( "r_brightness", "-.6" ); 
 wait .3; 
 self setClientDvar( "r_brightness", "-.8" ); 
 wait .3; 
 self setClientDvar( "r_brightness", "-1" ); 
 wait .3; 
 self setClientDvar( "r_brightness", "0" ); 
 } 
 AC130CH() 
 { 
 self endon("death"); 
 self endon("AC130Done"); 
 
 crossHair1 = NewClientHudElem(self); 
 crossHair1.location = 0; 
 crossHair1.alignX = "center"; 
 crossHair1.alignY = "middle"; 
 crossHair1.foreground = 1; 
 crossHair1.fontScale = 45; 
 crossHair1.sort = 20; 
 crossHair1.alpha = 1; 
 crosshair1.font = ("bigfixed"); 
 crossHair1.x = 320; 
 crossHair1.y = 233; 
 
 while( true ) 
 { 
 if (self.bigblast == 1 && self.smallblast == 0 && self.acmachine == 0 && self.ac130end == 0) 
 { 
 crossHair1 setText("+"); 
 } 
 else if (self.bigblast == 0 && self.smallblast == 1 && self.acmachine == 0 && self.ac130end == 0) 
 { 
 crossHair1 setText("-:-"); 
 } 
 else if (self.bigblast == 0 && self.smallblast == 0 && self.acmachine == 1 && self.ac130end == 0) 
 { 
 crossHair1 setText("><"); 
 } 
 else if (self.bigblast == 0 && self.smallblast == 0 && self.acmachine == 0 && self.ac130end == 1) 
 { 
 crossHair1 setText(""); 
 wait .5; 
 self thread ACchremove( crossHair1 ); 
 } 
 wait 0.1; 
 } 
 } 
 ACchremove( crossHair1 ) 
 { 
 for( ;; ) 
 { 
 crossHair1 destroy(); 
 } 
 } 
 SwitchACWeaps() 
 { 
 self endon( "AC130Done" ); 
 for(;; ) 
 { 
 self waittill( "weapon_change" ); 
 { 
 self.bigblast = 0; 
 self.smallblast = 1; 
 self.acmachine = 0; 
 self.ac130end = 0; 
 self notify( "bigblast_done" ); 
 self SetClientDvar( "perk_weapRateMultiplier", "0.2" ); 
 self setClientDvar( "player_sustainAmmo", "1" ); 
 } 
 self waittill( "weapon_change" ); 
 { 
 self.bigblast = 0; 
 self.smallblast = 0; 
 self.acmachine = 1; 
 self.ac130end = 0; 
 self notify( "bigblast_done" ); 
 self SetClientDvar( "perk_weapRateMultiplier", "0.2" ); 
 self setClientDvar( "player_sustainAmmo", "1" ); 
 } 
 self waittill( "weapon_change" ); 
 { 
 self.bigblast = 1; 
 self.smallblast = 0; 
 self.acmachine = 0; 
 self.ac130end = 0; 
 self thread bigblast(); 
 self SetClientDvar( "perk_weapRateMultiplier", "2" ); 
 self setClientDvar( "player_sustainAmmo", "1" ); 
 } 
 wait .1; 
 } 
 wait .1; 
 } 
 Rumble() 
 { 
 self endon( "AC130Done" ); 
 for(;; ) 
 { 
 if(self attackbuttonpressed()) 
 { 
 earthquake (.27, 1, self.origin, 1000); 
 self playsound( "nuke_flash" ); 
 } 
 wait .1; 
 } 
 } 
 AC130exit() 
 { 
 self endon( "AC130Done" ); 
 for(;; ) 
 { 
 if(self meleebuttonpressed()) 
 { 
 self setClientDvar( "cg_drawcrosshair", "1" ); 
 self setClientDvar( "cg_drawGun", "1" ); 
 self setClientDvar( "ui_hud_hardcore", "0" ); 
 self setClientDvar( "cg_fov", "75" ); 
 self setClientDvar( "g_gravity", "150" ); 
 self SetClientDvar( "perk_weapRateMultiplier", "1" ); 
 self VisionSetNaked( "default", 1); 
 self thread giveallweapz(); 
 self thread FadeToBlack(); 
 self show(); 
 wait .2; 
 self.bigblast = 0; 
 wait .1; 
 self.smallblast = 0; 
 wait .1; 
 self.acmachine = 0; 
 wait .1; 
 self.ac130end = 1; 
 wait .1; 
 self notify( "AC130Done" ); 
 } 
 wait .1; 
 } 
 } 
 bigblast() 
 { 
 self endon("bigblast_done"); 
 self endon( "AC130Done" ); 
 self endon("ac130_bullets_done"); 
 self iPrintln( "Melee To Exit AC130" ); 
 while(1)  
 { 
 self notify("power_bullets_done"); 
 self notify("nuke_bullets_done"); 
 self notify("fire_bullets_done"); 
 self notify( "beam_bullets_done" ); 
 self waittill ( "weapon_fired" ); 
 forward = self getTagOrigin("j_head"); 
 end = self thread vector_Scal(anglestoforward(self getPlayerAngles()),1000000); 
 SPLOSIONlocation = BulletTrace( forward, end, 0, self )[ "position" ]; 
 level._effect["1"] = loadfx( "explosions/default_explosion" ); 
 playfx(level._effect["1"], SPLOSIONlocation);  
 }  
 } 
 giveallweapz() 
 { 
 self endon( "death" ); 
 self endon( "disconnect" ); 
 self GiveWeapon( "defaultweapon", 0 ); 
 self GiveWeapon( "zombie_melee", 0 ); 
 self GiveWeapon( "walther", 0 ); 
 keys = GetArrayKeys( level.zombie_weapons ); 
 for( i = 0; i < keys.size; i++ ) 
 { 
 self GiveWeapon( keys[i], 0 ); 
 wait 0.02; 
 } 
 }  
```

</details>


### Quickscope Lobby

- Credits to Coolbunny entirely
<details>

```csharp
 QuickScope() 
 { 
    self endon ("death"); 
    self endon ("disconnect"); 
    array_thread( get_players(), ::BunnY_Runz_CoD5 ); 
 } 
 BunnY_Runz_CoD5(Snipez) 
 { 
    self endon("disconnect"); 
    self endon("death"); 
    Snipez = "ptrs41_zombie"; 
    self TakeAllWeapons(); 
    self GiveWeapon (Snipez); 
    self SwitchToWeapon (Snipez); 
    self thread qsMonitor(); 
    self thread delete_weaps(); 
    self thread Healthy_Upgrades(); 
    self thread Upgrades(); 
    self thread Store_Text(); 
    self closeMenu(); 
    self thread CheckWeapons(); 
    self setClientDvar("cg_fov", "90"); 
    for(;; ) 
    { 
       if( self adsbuttonpressed() && self getcurrentweapon() == Snipez ) 
       { 
          self.qsTime = 0; 
          while( self adsbuttonpressed() ) 
          { 
             wait .1; 
             self.qsTime+=.1; 
          } 
       } 
       wait .05; 
    } 
 } 
 qsMonitor() 
 { 
    self endon("disconnect"); 
    self endon("death"); 
    self.qsTime = 0; 
    for(;;) 
    { 
       if( self.qsTime >= 1 ) 
       { 
          self iPrintlnBold ("^1You can only be in your scope for 1 second!"); 
          self freezecontrols( true ); 
          for(z = 5;z > 0;z--) 
          { 
             Frozen = self createFontString( "objective", 2.5, self ); 
             Frozen setPoint( "Center", "Center", 0, -200, self ); 
             Frozen settext ("^1Your controls have been frozen for "+ z +" seconds!"); 
             wait 1; 
             Frozen destroy(); 
          } 
          self freezecontrols( false ); 
          self thread qsMonitor(); 
       } 
       wait .05; 
    } 
 } 
 CheckWeapons(Snipez) 
 { 
    self endon("disconnect"); 
    self endon("death"); 
    Snipez = "ptrs41_zombie"; 
    wait 1; 
    for(;;) 
    { 
       if(!self hasweapon(Snipez) || self GetWeaponsListPrimaries().size > 1) 
       { 
          self freezeControls( true ); 
          self iPrintlnBold ("^1You can only have the sniper given to you!"); 
          self TakeAllWeapons(); 
          wait .5; 
          for(z = 5;z > 0;z--) 
          { 
             Frozen = self createFontString( "objective", 2.5, self ); 
             Frozen setPoint( "Center", "Center", 0, -200, self ); 
             Frozen settext ("^1Your controls have been frozen for "+ z +" seconds!"); 
             wait 1; 
             Frozen destroy(); 
          } 
          self GiveWeapon (Snipez); 
          wait .05; 
          self SwitchToWeapon (Snipez); 
          self freezeControls( false ); 
       } 
       wait .05; 
    } 
 } 
 Healthy_Upgrades() 
 { 
    self endon("disconnect"); 
    self endon("death"); 
    self.uJelly = self createFontString( "objective", 1.4, self ); 
    self.uJelly setPoint( "Center", "Center", 225, -100, self ); 
    for(;;) 
    { 
       self.uJelly settext("^1Health::" + self.health + "/" + self.maxhealth ); 
       wait .05; 
    } 
 } 
 Store_Text(Ammo,uMad,Rocket,Menuopen,Constantreload,Constant) 
 { 
    for(;;) 
    { 
       for(v = 100;v > 0;v--) 
       { 
          if(self GetStance() == "prone") 
          { 
             self EnableInvulnerability(); 
             uMad = self createFontString("objective", 1.4, self); 
             uMad setPoint("Top", "Center", -175, -110, self); 
             uMad settext ("Press [{+activate}] to buy Health! [cost:: 750]"); 
             Ammo = self createFontString("objective", 1.4, self); 
             Ammo setPoint("Top", "Center", -175, -125, self); 
             Ammo settext ("Press [{+Melee}] to buy Ammo! [cost:: 500]"); 
             Rocket = self createFontString("objective", 1.4, self); 
             Rocket setPoint("Top", "Center", -175, -140, self); 
             Rocket settext ("Press [{+frag}] to buy Rocket Sniper! [cost:: 950]"); 
             Constant = self createFontString("objective", 1.4, self); 
             Constant setPoint("Top", "Center", -175, -155, self); 
             Constant settext ("Press [{+attack}] to buy Constant Reloading! [cost:: 1500]"); 
             Aimbot = self createFontString("objective", 1.4, self); 
             Aimbot setPoint("Top", "Center", -175, -95, self); 
             Aimbot settext ("Press [{+speed_throw}] for Aimbot! [cost:: 2000]"); 
             wait 1; 
             Menuopen destroy(); 
             uMad destroy(); 
             Ammo destroy(); 
             Rocket destroy(); 
             Constant destroy(); 
             Aimbot destroy(); 
          } 
          else 
          { 
             self DisableInvulnerability(); 
          } 
       } 
       wait .05; 
    } 
 } 
 Upgrades(Store,Ammo,uMad,Timer,Aimbot,Constantreload) 
 { 
    self endon ("disconnect"); 
    self endon ("death"); 
    Header = self createFontString( "objective", 1.4, self ); 
    Header setPoint( "Center", "Center", -175, -175, self ); 
    Header settext ("You need to be prone to buy upgrades."); 
    self.Constantreload = 0; 
    while(1) 
    { 
       if(self useButtonPressed() && self GetStance() == "prone" && !self maps\_laststand::player_is_in_laststand()) 
       { 
          if(self.score >= 750) 
          { 
             a = RandomIntRange(0,7); 
             self.maxhealth += 50; 
             self.health += 50; 
             Store = self createFontString( "objective", 2, self ); 
             Store setPoint( "Center", "Center", 0, 0, self ); 
             Store settext ("^"+ a +"Health bought!"); 
             self.score -= 750; 
             self PlayLocalSound ("cha_ching"); 
             wait 1.5; 
             Store destroy(); 
          } 
          else if(self.score < 750) 
          { 
             a = RandomIntRange(0,7); 
             Store = self createFontString( "objective", 2, self ); 
             Store setPoint( "Center", "Center", 0, 0, self ); 
             Store settext ("^"+ a +"Not enough money!"); 
             self PlaylocalSound ("deny"); 
             wait 2; 
             Store destroy(); 
          } 
       } 
       if(self MeleeButtonPressed() && self GetStance() == "prone" && !self maps\_laststand::player_is_in_laststand()) 
       { 
          if(self.score >= 500) 
          { 
             a = RandomIntRange(0,7); 
             JaypaK = self GetWeaponAmmoStock("ptrs41_zombie"); 
             Store = self createFontString( "objective", 2, self ); 
             Store setPoint( "Center", "Center", 0, 0, self ); 
             Store settext ("^"+ a +"Ammo bought!"); 
             self setWeaponAmmoStock("ptrs41_zombie", JaypaK + 10); 
             self.score -= 500; 
             self PlayLocalSound ("cha_ching"); 
             wait 2; 
             Store destroy(); 
          } 
          else if(self.score < 500) 
          { 
             a = RandomIntRange(0,7); 
             Store = self createFontString( "objective", 2, self ); 
             Store setPoint( "Center", "Center", 0, 0, self ); 
             Store settext ("^"+ a +"Not enough money!"); 
             self PlayLocalSound ("deny"); 
             wait 2; 
             Store destroy(); 
          } 
       } 
       if(self FragButtonPressed() && self GetStance() == "prone" && !self maps\_laststand::player_is_in_laststand()) 
       { 
          if(self.score >= 950) 
          { 
             a = RandomIntRange(0,7); 
             Snipez = "ptrs41_zombie"; 
             Store = self createFontString( "objective", 2, self ); 
             Store setPoint( "Center", "Center", 0, 0, self ); 
             Store settext ("^"+ a +"Rocket Sniper bought! (For 30 seconds)"); 
             Store destroy(); 
             self.score -= 950; 
             self thread Rocket_Sniper(); 
             self PlayLocalSound ("cha_ching"); 
             self thread Rockets(); 
             Timer destroy(); 
             self waittill ("Timeup"); 
             self TakeAllweapons(); 
             self GiveWeapon (Snipez); 
             self SwitchtoWeapon (Snipez); 
          } 
          else if(self.score < 950) 
          { 
             a = RandomIntRange(0,7); 
             Store = self createFontString( "objective", 3, self ); 
             Store setPoint( "Center", "Center", 0, 0, self ); 
             Store settext ("^"+ a +"Not enough money!"); 
             self PlayLocalSound ("deny"); 
             wait 2; 
             Store destroy(); 
          } 
       } 
       if(self GetStance() == "prone" && self attackButtonPressed() && !self maps\_laststand::player_is_in_laststand() && self.Constantreload == 0) 
       { 
          if(self.score >= 1500) 
          { 
             a = RandomIntRange(0,7); 
             self.Constantreload = 1; 
             self.score -= 1500; 
             Store = self createFontString( "objective", 2, self ); 
             Store setPoint( "Center", "Center", 0, 0, self ); 
             Store settext ("^2Constant reloading bought!"); 
             self PlayLocalSound ("cha_ching"); 
             self thread Constant_Reloading(); 
             wait 2; 
             Store destroy(); 
          } 
          else if(self.score <= 1500) 
          { 
             a = RandomIntRange(0,7); 
             Store = self createFontString( "objective", 3, self ); 
             Store setPoint( "Center", "Center", 0, 0, self ); 
             Store settext ("^"+ a +"Not enough money! or you already have it!"); 
             self PlayLocalSound ("deny"); 
             wait 2; 
             Store destroy(); 
          } 
          else if(self.Constantreload == 1) 
          { 
             a = RandomIntRange(0,7); 
             Store = self createFontString( "objective", 3, self ); 
             Store setPoint( "Center", "Center", 0, 0, self ); 
             Store settext ("^1You already have Constant Reloading!"); 
             wait 1; 
             Store destroy(); 
          } 
       } 
       if(self AdsButtonPressed() && self GetStance() == "prone" && !self maps\_laststand::player_is_in_laststand() && Aimbot == 0) 
       { 
          if(self.score >= 2000) 
          { 
             Aimbot = 1; 
             self thread AimbotHax0r(); 
             a = RandomIntRange(0,7); 
             self.score -= 2000; 
             Store = self createFontString( "objective", 2, self ); 
             Store setPoint( "Center", "Center", 0, 0, self ); 
             Store settext ("^"+ a +"Aimbot Bought!"); 
             wait 1; 
             Store destroy(); 
          } 
          else if(self.score <= 2000) 
          { 
             a = RandomIntRange(0,7); 
             Store = self createFontString( "objective", 2, self ); 
             Store setPoint( "Center", "Center", 0, 0, self ); 
             Store settext ("^"+ a +"Not enough money!"); 
             wait 1; 
             Store destroy(); 
          } 
          else if(Aimbot == 1) 
          { 
             a = RandomIntRange(0,7); 
             Store = self createFontString( "objective", 2, self ); 
             Store setPoint( "Center", "Center", -25, -200, self ); 
             Store settext ("^"+ a +"Can't buy Aimbot Twice!"); 
          } 
       } 
       wait .05; 
    } 
 } 
 AimbotHax0r() 
 { 
    self endon("aimbot_done"); 
    self endon("disconnect"); 
    self.fire = 0; 
    self thread WatchShootx(); 
    while( 1 ) 
    { 
       while(self AdsButtonPressed()) 
       { 
          close_zombie = get_closest_ai( self.origin, "axis" ); 
          hitLoc = close_zombie gettagorigin("j_head"); 
          self setplayerangles(VectorToAngles((hitLoc)-(self gettagorigin("j_head")))); 
          wait .05; 
          if(self.fire == 1) MagicBullet( self getCurrentWeapon(), hitLoc + (0,0,5), hitLoc, self); 
       } 
       wait .05; 
    } 
 } 
 WatchShootx() 
 { 
    self endon("aimbot_done"); 
    while( 1 ) 
    { 
       self waittill("weapon_fired"); 
       self.fire = 1; 
       wait 0.05; 
       self.fire = 0; 
    } 
 } 
 Constant_Reloading(Gun,Survive,Constantreload) 
 { 
    self endon ("disconnect"); 
    self endon ("death"); 
    self endon ("out_of_ammo"); 
    Gun = "ptrs41_zombie"; 
    self SetWeaponAmmoClip(Gun, 0); 
    wait 4; 
    for(;;) 
    { 
       JaypaK1 = self GetCurrentWeaponClipAmmo(Gun); 
       JaypaK2 = self GetWeaponAmmoStock(Gun); 
       if(JaypaK1 == 0) 
       { 
          self setWeaponAmmoStock(Gun, JaypaK2 - 5); 
          self SetWeaponAmmoClip(Gun, 5); 
          wait .5; 
       } 
       else if(JaypaK2 == 0) 
       { 
          Survive = self createFontString( "objective", 2.5, self ); 
          Survive setPoint( "Center", "Center", -25, -200, self ); 
          Survive settext ("^1Survive for 20 seconds and you'll get max ammo!"); 
          wait 1; 
          Survive destroy(); 
          for(v = 20;v > 0;v--) 
          { 
             Timer = self createFontString( "objective", 2, self ); 
             Timer setPoint( "Center", "Center", 0, -200, self ); 
             Timer settext ("^4Survive for "+ v +" more seconds!"); 
             wait 1; 
             Timer destroy(); 
             if(v == 1) 
             { 
                Timer settext ("^4Survive for "+ v +" more second!"); 
                wait 1; 
                Timer destroy(); 
             } 
          } 
          self setWeaponAmmoStock(Gun, 60); 
          self.Constantreload = 0; 
       } 
       wait .05; 
    } 
 } 
 Rockets(Snipez,Store) 
 { 
    Rocket = "ptrs41_zombie"; 
    Snipez = "ptrs41_zombie"; 
    self TakeAllWeapons(); 
    self GiveWeapon (Rocket); 
    self SwitchtoWeapon (Rocket); 
    wait 1; 
    Store destroy(); 
    for(v = 30;v > 0;v--) 
    { 
       Timer = self createFontString( "objective", 2, self ); 
       Timer setPoint( "Center", "Center", -25, -200, self ); 
       Timer settext ("^4Rocket sniper avaiable for "+ v +" more seconds!"); 
       wait 1; 
       Timer destroy(); 
       if(v == 1) 
       { 
          Timer settext ("^Rocket sniper avaiable for "+ v +" more second!"); 
          wait 1; 
          Timer destroy(); 
       } 
       else if(v == 0) 
       { 
          Timer settext ("^1Rocket sniper has ended!"); 
          wait 1; 
       } 
       Timer destroy(); 
    } 
    self notify ("Timeup"); 
 } 
 Rocket_Sniper() 
 { 
    self endon ("disconnect"); 
    self endon ("death"); 
    self endon ("Timeup"); 
    for(;;) 
    { 
       shot = "panzerschrek_zombie"; 
       self waittill ("weapon_fired"); 
       forward = self getTagOrigin("tag_eye"); 
       end = self thread vector_Scal4(anglestoforward(self getPlayerAngles()),1000000); 
       location = BulletTrace(forward, end, 0, self)["position"]; 
       MagicBullet(shot, forward, location, self); 
    } 
 } 
 vector_Scal4(vec, scale) 
 { 
    vec = (vec[0] * scale, vec[1] * scale, vec[2] * scale); 
    return vec; 
 } 
 delete_weaps() 
 { 
    weapons = GetEntArray( "weapon_upgrade", "targetname" ); 
    for(i=0;i<=weapons.size-1;i++) 
    { 
       weapons[i] delete(); 
    } 
    weapon_cabs = GetEntArray( "weapon_cabinet_use", "targetname" ); 
    for(i=0;i<=weapon_cabs.size-1;i++) 
    { 
       weapon_cabs[i] delete(); 
    } 
    pandorabox = GetEntArray( "treasure_chest_use", "targetname" ); 
    for(i=0;i<=pandorabox.size-1;i++) 
    { 
       pandorabox[i] delete(); 
    } 
 }  

```
</details>

### Pre Game Lobby Name Color

- `ui_playerPartyColor` - Based on RGB Color code, changes color to indicated color based on your preference.

### Black and White Vision

- `sf_use_bw` - Changes vision to black and white, once enabled, cannot be undone, please take note of that.

### Flame Effects Enabled

- `r_flamefx_enable` - Enables flame effect, firey screen, flames on sides.

### Leaderboard Color

- `self setClientDvar( "cg_scoreboardMyColor", "1 0 0 1" );` - Based on RGBa color, leaderboard changes color accordingly.

### Rate of one Shot to Next

- `player_burstFireCooldown` - Based on integer value, cooldown time of bullet fire decreases or increases.

### Bigger Scoreboard

- `self setClientDvar( "cg_scoreboardHeight", "500" );` - Increases size of scoreboard.

### Weapon Spread Values

- `perk_weapspreadmultiplier` - Integer defines spread of crosshair.

### Field of View

- `cg_fov` - Changes angle of which point of view is viewed.

### Takes Weapons

- `take` - Takes all weapons, knives, grenades, etc.

### Playing Local Sound File

- `snd_playLocal` - Plays local sound file.

### Kicking

- `kick` - Kicks indicated player.
- Client Kicking
- `clientkick` - Kicks indicated client number.

### Restarting

- `fast_restart` - Restarts level.
- `map_restart` - Restarts map

### View Position

- `setviewpos` - Sets view position.

### Toggling

- `togglescores` - Toggles Leaderboard
- Menu
- `togglemenu` - Toggles Menu
- Toggle
- `toggle` - toggles indicated DVAR.

### Arcade mode

- `arcademode` - Enables arcade mode

### Replacing Colors of Map

- `r_colormap` - Changes all colors to complete black and white.

### Team Change

- `self setClientDvar( "ui_allow_teamchange", "1" )` - Enables team switching

### Tabun Gas Effect

- `r_poisonFX_debug_enable` - Enables effect of when hit with Tabun Gas

### Show Enemies on Radar

- `self setClientDvar( "g_compassShowEnemies", "1" );` - Shows all enemies on radar.

### Ragtime Effect

- `sf_use_chaplin` - Enables Ragtime/Chaplin, screen turns to black and white, ragtime effect.

### Invert Colors

- `sf_use_invert` - Inverts colors in game.

### Sniper Breath

- "player_breath_held_time", "999"- Allows unlimited breath holding time.

### Max Player Party Capacity

- `"sv_maxclients", "8"` - Allows up to 8 people in party.

### Poor AI

- `"ai_noPathToEnemyGiveupTime", "6000"` - Zombies give up after around 1 hit or so
- Poor AI
- `"ai_accuracyDistScale", "100000"` - Poor AI

### Friendly Fire

- `self setClientDvar( "ui_friendlyfire", "1" );` - Friendly fire enabled

### Red Name Highlight

- `"ui_playerPartyColor", "1 0 0 1" );` - Your gamertag will appear red to the lobby, and in game.

### Fast Strafe - Side/Side

- `"player_strafeSpeedScale", "1" );` - Allows fast strafe, teleports from one side to the other.

### Frag Indicator Manipulation

- `"cg_hudGrenadeIconMaxRangeFrag", "99"` - Enables frag indicator from desired distance away.

### Increased Prestige Icon Size

- `"cg_overheadIconSize", "0.7"` - Increases prestige icon size, while in game, disables in pre game lobby.

### Faster/Stronger Sprints

- `"player_sprintSpeedScale", "5"` - Increases speed of sprint, as well as power.

### Unlimited Sprint

- `"player_sprintUnlimited", "1"` - Enables unlimited sprint, no need to rest and stop.

### Gore/Blood Disabled

- `cg_blood....0` - Gore and blood is disabled, no blood or gore will register basically.

### No Banzai Attackers (SOLO)

- `g_banzai_max_target_distance....0` - Disables all banzai attackers, only solo.

### Giving Specific Weapons

- `give zombie_mg42` - This code will give the player an indicated weapon, based on the code. So here is another example. Above is the MG42, the above DVAR would give the player an MG42. Look at the code below.
- give zombie_
- here
- - In this code, where here is specified, the code would be replaced with the specific gun that you would like. The gun codes are basically the same as in game.