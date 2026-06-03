# Sound Asset

Sound definitions with volume, pitch, distance, and speaker mapping.

## WaW Additions (vs CoD4)

- Hash-based file identification for streamed sounds
- Enhanced priority system (`minPriorityThreshold`, `maxPriorityThreshold`)
- Reverb and occlusion level controls

## Sound Types

```c
enum snd_alias_type_t
{
    SAT_UNKNOWN,
    SAT_LOADED,
    SAT_STREAMED,
    SAT_PRIMED
};
```

| Type         | Description                        |
|--------------|------------------------------------|
| SAT_LOADED   | Fully loaded in memory             |
| SAT_STREAMED | Streamed from disk                 |
| SAT_PRIMED   | Partially pre-loaded               |

## Core Structure

```c
struct snd_alias_t
{
    const char *aliasName;
    const char *subtitle;
    const char *secondaryAliasName;
    const char *chainAliasName;
    SoundFile *soundFile;
    int sequence;
    float volMin;
    float volMax;
    float pitchMin;
    float pitchMax;
    float distMin;
    float distMax;
    int flags;
    float slavePercentage;
    float probability;
    float lfePercentage;
    float centerPercentage;
    int startDelay;
    // ... additional fields
};
```

## Sound File Structure

```c
struct SoundFile
{
    char type;              // SAT_LOADED, SAT_STREAMED, or SAT_PRIMED
    char exists;
    union
    {
        LoadedSound *loadSnd;
        StreamedSound streamSnd;
        PrimedSound primedSnd;
    } u;
};

struct StreamedSound
{
    const char *dir;
    const char *name;
};

struct PrimedSound
{
    const char *dir;
    const char *name;
    // Pre-loaded partial data
};
```

## References

- [Sound Asset - COD Research Wiki](https://codresearch.dev/index.php/Sound_Asset)
