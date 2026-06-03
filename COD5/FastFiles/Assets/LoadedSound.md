# Loaded Sound Asset

Raw sound data loaded into memory.

## Overview

Loaded sounds are audio files that are fully loaded into memory for immediate playback, as opposed to streamed sounds.

## Use Cases

- Short sound effects (gunshots, footsteps, UI sounds)
- Frequently played audio
- Sounds requiring low latency

## Notes

- Loaded sounds consume memory but have no disk access latency
- Typically used for gameplay-critical audio
- Referenced by Sound assets (0x0B)

## Related Assets

- [Sound (0x0B)](Sound.md) - Sound definitions that reference loaded sounds
