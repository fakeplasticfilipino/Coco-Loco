# Falling Buko — Build Tracker

Coco Loco / Unity 6000.5.9f1 / 2D URP / PC build

Legend: `[x]` done · `[ ]` not started · `[~]` partial

---

## Submission requirements

| # | Requirement | Covered by | Status |
|---|---|---|---|
| a | Game mechanic | Falling coconuts, collision, health loss | Done |
| b | Player mode application | Single-player character with keyboard + drag control | Done |
| c | Time interval element | Survival timer, spawn interval ramp, difficulty curve | Done |
| d | Game platform | Windows / PC standalone, 16:9 | Partial — no build produced yet |

---

## Block 1 — Project setup

- [x] Unity project created (2D URP template)
- [x] Folder structure: `Art/`, `Prefabs/`, `Scripts/`, `Scenes/`
- [x] Playable scene `FallingBuko.unity` (SampleScene left untouched)

## Block 2 — Core mechanics

- [x] Player moves left/right, clamped to screen edges
- [x] Keyboard control (arrows / A-D)
- [x] Pointer drag control (works for touch later)
- [x] Coconuts spawn at random X above the screen
- [x] Coconuts fall and are destroyed off-screen
- [x] Trigger collision: coconut hits player
- [x] 3 health, lose 1 per hit, run ends at 0

## Block 3 — Scoring and difficulty

- [x] Score rises with survival time (10 pts/sec)
- [x] Spawn interval ramps 1.1s → 0.3s over 90s
- [x] Fall speed ramps 4.5 → 13 units/sec over 90s
- [x] Score multiplier increases every 30s survived (x1, x2, x3... with a pop on each step)
- [x] High score saved between runs (PlayerPrefs)
- [x] High score shown in the HUD and on the game over screen, with a NEW BEST callout

## Block 4 — Art and UI

- [x] Player, coconut, background, heart sprites
- [x] HUD: score readout + 3 heart icons
- [x] Game over panel with final score and survival time
- [x] Restart without reloading the scene
- [ ] Mango sprite
- [ ] Durian sprite
- [ ] Power-up pickup feedback (flash or pop)

## Block 5 — Power-ups

- [x] Power-up spawner (random, rarer than coconuts)
- [ ] Mango: temporary speed boost, with a visible timer or tint
- [ ] Durian: restores 1 health, capped at 3
- [x] Power-ups fall and despawn like coconuts

## Block 6 — Feedback and audio

- [x] Screen shake on hit
- [ ] Score pop / float-up on milestones
- [ ] SFX: coconut hit
- [ ] SFX: power-up collected
- [ ] SFX: new high score
- [ ] Background music (optional)

## Block 7 — Platform and delivery

- [ ] Scene added to Build Settings
- [ ] Windows build produced and tested
- [ ] Balance pass — is the first 30s too easy, is 90s survivable
- [ ] Fixed 16:9 resolution or letterboxing so the layout holds
- [ ] Optional: Android build target + touch/tilt controls

---

## Suggested order

1. ~~Score multiplier + high score (Block 3)~~ — done
2. Power-ups (Block 5 + the two sprites in Block 4) — next
3. Audio (Block 6)
4. Build and balance (Block 7)
