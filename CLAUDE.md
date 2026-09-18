# CLAUDE.md

Guidance for Claude when working on this repo.

## What this is

**Falling Buko** (repo: Coco Loco): a small 2D dodge game made in Unity for a minor-subject course project. The player moves left and right to avoid falling coconuts, survives as long as possible, and tries to beat their high score.

- Unity **6000.5.9f1**, 2D URP template, **new Input System**, uGUI (`UnityEngine.UI.Text`).
- Target: Windows / PC standalone, 16:9.
- Playable scene: `Assets/Scenes/FallingBuko.unity`. `SampleScene` is the template scene and is not used.

## Guiding principles

1. **Keep it simple.** This is a small project on purpose. Don't add new systems, frameworks, packages, or architecture layers unless they're asked for. If there are two ways to build something, pick the one with fewer moving parts.
2. **Replayability is the priority.** Since the game is simple, any time spent on design should go into making people want to "go again": the score multiplier, the high score, the difficulty ramp, power-ups, and snappy feedback. Before adding a feature, ask whether it makes the next run more interesting.
3. **Small, incremental steps.** Progress is tracked through the git history, so work on one tracker item at a time (or a small group of closely related ones). That way each finished step can be committed separately.

## Workflow

- `TRACKER.md` is the source of truth for scope and progress. Before starting, find the next unchecked item. When it's done, tick it (`[x]`, or `[~]` if it's only partly done) in the same change.
- Keep each change small enough to describe in one line, and don't mix unrelated work into it.
- **Git is off-limits.** The user handles all version control. Don't run git commands, don't write commit messages, and don't bring up committing, branching or pushing. Just finish the step and say what changed.
- Claude can't run the Unity Editor. After a code change, list exactly what the user needs to do in the Editor (for example "assign X to GameManager's Y field") and what to check in Play mode.
- Scene (`.unity`) and prefab (`.prefab`) files are Unity YAML. It's better for the user to wire things up in the Editor than to hand-edit these files. If a YAML edit can't be avoided, keep it minimal and point it out.

## Code conventions (match what's already there)

- One `MonoBehaviour` per file, with the file named after the class. Scripts go in `Assets/Scripts/`.
- Tunable values are `public` fields grouped under `[Header("...")]`, so designers can tweak them in the Inspector.
- `GameManager.Instance` is the only singleton. Other scripts read state from it (`IsOver`, `Elapsed`, `Multiplier`) and call into it (`TakeDamage()`).
- Null-check scene references and `GameManager.Instance` before using them, like the existing code does.
- Input goes through `Keyboard.current` / `Pointer.current` (Input System). Don't use legacy `Input.GetKey`.
- Restarting resets state in place (`GameManager.Begin()`) and doesn't reload the scene. Anything new that spawns or holds state needs to be cleaned up or reset there too.
- The high score is saved in `PlayerPrefs` under `FallingBuko.HighScore`.
- Use plain, readable C#. No LINQ in per-frame code, no async, no extra packages.

## Layout

```
Assets/
  Art/        sprites (player, coconut, background, heart)
  Prefabs/    Coconut.prefab
  Scenes/     FallingBuko.unity (game), SampleScene.unity (unused)
  Scripts/    GameManager, PlayerController, CoconutSpawner, Coconut
  Settings/   URP, Input System actions, build profile
TRACKER.md    build checklist and submission requirements
```

Don't touch `Library/`, `Logs/`, `Temp/` or `UserSettings/`. They're generated and git-ignored.
