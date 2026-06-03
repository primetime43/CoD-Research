# Menu Asset

Menu UI definitions.

## Structure

```c
struct menuDef_t
{
    windowDef_t window;
    const char *font;
    int fullScreen;
    int itemCount;
    int fontIndex;
    int cursorItem;
    int fadeCycle;
    float fadeClamp;
    float fadeAmount;
    float fadeInAmount;
    float blurRadius;
    MenuEventHandlerSet *onOpen;
    MenuEventHandlerSet *onCloseRequest;
    MenuEventHandlerSet *onClose;
    MenuEventHandlerSet *onESC;
    ItemKeyHandler *onKey;
    Statement_s *visibleExp;
    const char *allowedBinding;
    const char *soundLoop;
    int imageTrack;
    vec4_t focusColor;
    vec4_t disableColor;
    Statement_s *rectXExp;
    Statement_s *rectYExp;
    Statement_s *rectWExp;
    Statement_s *rectHExp;
    Statement_s *openSoundExp;
    Statement_s *closeSoundExp;
    itemDef_s **items;
};
```

## Item Definition

```c
struct itemDef_s
{
    windowDef_t window;
    rectDef_s textRect[1];
    int type;
    int dataType;
    int alignment;
    int fontEnum;
    int textAlignMode;
    float textAlignX;
    float textAlignY;
    float textScale;
    int textStyle;
    int gameMsgWindowIndex;
    int gameMsgWindowMode;
    const char *text;
    int itemFlags;
    menuDef_t *parent;
    MenuEventHandlerSet *mouseEnterText;
    MenuEventHandlerSet *mouseExitText;
    MenuEventHandlerSet *mouseEnter;
    MenuEventHandlerSet *mouseExit;
    MenuEventHandlerSet *action;
    MenuEventHandlerSet *accept;
    MenuEventHandlerSet *onFocus;
    MenuEventHandlerSet *leaveFocus;
    const char *dvar;
    const char *dvarTest;
    ItemKeyHandler *onKey;
    const char *enableDvar;
    const char *localVar;
    int dvarFlags;
    snd_alias_list_t *focusSound;
    float special;
    int cursorPos;
    itemDefData_t typeData;
    // ... expression fields
};
```

## Supporting Structures

### List Box

```c
struct listBoxDef_s
{
    int mousePos;
    int startPos[1];
    int endPos[1];
    int drawPadding;
    float elementWidth;
    float elementHeight;
    int elementStyle;
    int numColumns;
    columnInfo_s columnInfo[16];
    MenuEventHandlerSet *onDoubleClick;
    int notselectable;
    int noScrollBars;
    int usePaging;
    vec4_t selectBorder;
    Material *selectIcon;
};
```

### Edit Field

```c
struct editFieldDef_s
{
    float minVal;
    float maxVal;
    float defVal;
    float range;
    int maxChars;
    int maxCharsGotoNext;
    int maxPaintChars;
    int paintOffset;
};
```

### Multi-Select

```c
struct multiDef_s
{
    const char *dvarList[32];
    const char *dvarStr[32];
    float dvarValue[32];
    int count;
    int strDef;
};
```

## Event Handlers

Menus support various event callbacks:
- `onOpen` - When menu opens
- `onClose` - When menu closes
- `onESC` - When escape is pressed
- `onFocus` - When item gains focus
- `mouseEnter` / `mouseExit` - Mouse hover events
- `action` - When item is activated
- `accept` - When selection is confirmed

## References

- [Menu Asset (WaW) - COD Research Wiki](https://codresearch.dev/index.php/Menu_Asset_(WaW))
