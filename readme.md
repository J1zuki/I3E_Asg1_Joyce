I3E Assignment 1 – Interactive 3D Experience
Project Overview
---
This project is a first-person 3D interactive experience made in Unity. The player explores the level, collects coins, avoids hazards, passes moving obstacles, unlocks a Stage 3 door, and reaches the finish line to win the game.

---
Game Objective
- The goal of the game is to collect coins, survive the hazards, unlock the Stage 3 door, and reach the finish line.

Controls
- W, A, S, D – Move
- Mouse – Look around
- Space – Jump
- E – Interact / Collect coin / Open door
- R – Restart after winning or losing

---
Features Implemented

Player Controller
- Project uses a first-person player controller so the player can move around the 3D environment.

Collectibles
- Coins can be collected when the player is near them and presses the 'E' key. Each coin adds score to the player.

Stage 3 Door
- The Stage 3 door only opens after the player collects all required Stage 3 coins. The player then can press 'E' near the door to open it. The door closes again when the player press 'E' once more.

Hazards
- Hazard objects damage the player if the player ever jump into the water.

Moving Obstacles
- Some obstacles move back and forth to create challenge for the player.

Health System
- The player has health. When the player jump into the hazard the game over screen appears.

UI System
- The game includes UI text for score, health, Stage 3 coin progress, messages, win screen, and game over screen.

Finish Line
- When the player reaches the finish line, the win screen appears and shows the final score.

Restart System
- After winning or losing, the player can press R to restart the scene.

Scripts Used
- GameManager.cs – Controls score, UI messages, Stage 3 coin count, win screen, game over screen, and restart.
- CoinCollect.cs – Allows the player to collect coins using the E key.
- Stage3Door.cs – Opens the Stage 3 door after enough coins are collected.
- DamageHazard.cs – Damages the player over time while inside a hazard.
- PlayerHealth.cs – Controls player health and death.
- MovingObstacle.cs – Moves obstacles back and forth.
- FinishLine.cs – Detects when the player reaches the finish line.

---
Unity Setup
- The project was created in Unity using the Universal 3D / URP setup. The project contains GameObjects, Components, Colliders, Triggers, Rigidbody, TextMeshPro UI, Audio Source, and animation triggers.

AI Usage
- AI was used to help refine the code structure, improve comments, check for errors, and make sure the scripts were easier to understand. The final code was tested and adjusted in Unity.

Credits
- Lecturer notes for UI
- Lecturer recorded video for Moving Obstacles and Door