# Falling Buko

A small 2D arcade dodge game made in Unity. Coconuts (*buko*) are falling from the palm trees. Move left and right to dodge them, stay alive as long as you can, and beat your best score.

## How to play

| Action | Keyboard | Mouse / touch |
|---|---|---|
| Move | ← → or A / D | Hold and drag |
| Restart after game over | Space, R or Enter | Click / tap |

- You start with **3 hearts**, and each coconut that hits you takes one away.
- Your **score** increases by 10 points every second you survive.
- Every **30 seconds**, your score multiplier goes up (x2, x3, ...).
- Over 90 seconds, coconuts start falling **faster and more often**.
- Your **best score** is saved between sessions.

## Features

- Keyboard and drag controls
- Difficulty that ramps up with survival time
- Score multiplier and a saved high score, with a NEW BEST callout
- Screen shake when you get hit
- Instant restart with no loading

Planned (see [TRACKER.md](TRACKER.md)): mango and durian power-ups, sound effects, and a Windows build.

## Running the project

1. Install **Unity 6000.5.9f1** through Unity Hub.
2. Clone the repo and open the project folder in Unity Hub.
3. Open `Assets/Scenes/FallingBuko.unity` and press **Play**.

To build, go to **File → Build Profiles → Windows** and click Build.

## Project structure

```
Assets/
  Art/        sprites
  Prefabs/    Coconut prefab
  Scenes/     FallingBuko.unity (the game)
  Scripts/
    GameManager.cs       health, score, multiplier, high score, game over, restart
    PlayerController.cs  keyboard and drag movement
    CoconutSpawner.cs    spawning and difficulty curve
    Coconut.cs           falling, spinning, hitting the player
TRACKER.md    progress checklist
```

## Tech

Unity 6 · 2D URP · Input System · uGUI
