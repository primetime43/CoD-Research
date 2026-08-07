# Menu File Asset

Menu definition files.

## Overview

MenuFile assets contain compiled menu definition data loaded from `.menu` source files.

## Source Format

Menu files are text-based definitions that describe UI layouts:

```
{
    menuDef
    {
        name "main_menu"
        fullScreen 1
        rect 0 0 640 480

        itemDef
        {
            name "button1"
            text "Start Game"
            rect 100 100 200 30
            action { exec "map mp_castle"; }
        }
    }
}
```

## Related Assets

- [Menu (0x18)](FastFiles/Assets/Menu.md) - Compiled menu definitions
