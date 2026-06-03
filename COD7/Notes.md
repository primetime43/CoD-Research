Xbox 360:
nop these to use all the protected dvars. Works off host and host (BO1 MP). (Credits to Smokey xKoVx)

0x823E2350 <br>
0x823E2368 <br>
0x823E2380 <br>

## Sending a CFG Mod Menu via `Cbuf_AddText` in Black Ops 1  
**Credit: Smokey xKoVx**

While it's not commonly explored, it *is* possible to send a CFG-style mod menu to *Call of Duty: Black Ops 1* using `Cbuf_AddText`. This method allows you to inject command strings directly into the engine's command buffer at runtime.

That said, its practical use is limited—Black Ops 1 heavily restricts many developer variables (*dvars*), and most are protected by cheat flags. However, this remains a valuable finding for modders and researchers interested in understanding deeper engine behavior.

### Notes
- Unlike some other CoD titles, **Black Ops 1 does not support `vstr`**.
- However, you *can* use `dvarString` to simulate multi-line logic using bind combinations or scripted redirection.

### Example in C#

```csharp
// Injects a command string into the game engine (Black Ops 1).
void Cbuf_AddText(string text)
{
    jtag.CallVoid(0x8233E8D8, new object[] { 0, text });
}
