# Hazard Escape Adventure

## Overview

Hazard Escape Adventure is a first-person exploration and survival game developed in Unity.

Players must explore the environment, collect points, locate special items, survive environmental hazards, and reach the End Zone. To complete the game successfully, the player must earn enough points while overcoming obstacles and avoiding dangerous areas.

---

## Platform Requirements

### Supported Platform

* Windows PC

### Minimum Hardware Requirements

* Intel Core i3 Processor
* 4 GB RAM
* DirectX 11 Compatible Graphics
* Keyboard and Mouse
* 500 MB Available Storage

### Recommended Hardware Requirements

* Intel Core i5 Processor or better
* 8 GB RAM
* Dedicated Graphics Card
* Keyboard and Mouse

### Software Requirements

* Windows 10 / Windows 11
* Unity Build Runtime

---

## Installation Instructions

1. Download and extract the game folder.
2. Open the **Build** folder.
3. Launch the executable file (`.exe`).
4. Wait for the game to load.

---

## Controls

| Action        | Key   |
| ------------- | ----- |
| Move Forward  | W     |
| Move Backward | S     |
| Move Left     | A     |
| Move Right    | D     |
| Look Around   | Mouse |
| Jump          | Space |
| Interact      | E     |
| Pause Menu    | ESC   |

---

## Game Objective

The objective of the game is to:

1. Collect points throughout the level.
2. Obtain special items such as the Keycard and Gasmask.
3. Survive environmental hazards.
4. Reach the End Zone.
5. Have at least **50 points** before entering the End Zone.

---

## Features

### Collectibles

Standard collectibles are scattered throughout the map.

When collected, they:

* Increase the collectible count.
* Award points.
* Play a collection sound effect.
* Spawn a collection particle effect.

There are **40 collectibles** available in the level.

### Inventory System

The inventory panel tracks special items collected by the player.

Possible inventory items:

* Keycard
* Gasmask

---

## Special Items

### Keycard

**Purpose**

* Unlocks the Keycard Door.
* Grants access to restricted areas.

**How to Use**

1. Locate the Keycard.
2. Approach it.
3. Press **E** to collect.
4. Return to the locked door and press **E** to open it.

---

### Gasmask

**Purpose**

* Protects the player from poison gas damage.

**How to Use**

1. Locate the Gasmask.
2. Approach it.
3. Press **E** to collect.
4. Enter poison gas areas safely.

---

## Hazard System

### Poison Gas Zone

**Effect**

* Deals damage over time.

**Damage**

* 2 HP per second.

**Protection**

* The Gasmask completely prevents poison gas damage.

---

### Lava Zone

**Effect**

* Deals heavy damage over time.

**Damage**

* 10 HP per second.

**Protection**

* None.

---

### Water Zone

**Effect**

* Causes instant death.

**Protection**

* None.

---

## Health System

Player statistics:

| Stat        | Value |
| ----------- | ----- |
| Maximum HP  | 100    |
| Starting HP | 100    |

When HP reaches 0:

* A Game Over screen is displayed.
* A respawn countdown begins.
* The player respawns after 3 seconds.

---

## Door System

### Regular Doors

**Interaction**

* Press **E** near the door.

**Features**

* Opens and closes.
* Plays sound effects.
* Automatically closes when the player moves too far away.

---

### Keycard Door

**Requirement**

* Keycard must be collected.

**Interaction**

* Press **E** while near the door.

**Without Keycard**

* Door remains locked.
* Locked sound effect plays.

**With Keycard**

* Door opens.
* Access to restricted areas is granted.

---

## Winning the Game

To complete the game:

1. Collect at least **50 points**.
2. Reach the End Zone.

### If the player has fewer than 50 points:

A message will appear:

> Achieve 50 points to win

### If the player has 50 points or more:

* The victory screen is displayed.
* Final score is shown.
* Completion time is shown.
* The game timer stops.

---

## Puzzle Solutions / Answer Key

### Puzzle 1 – Unlocking the Restricted Area

**Objective**
Open the Keycard Door.

**Solution**

1. Explore the level.
2. Locate the Keycard.
3. Press **E** to collect it.
4. Return to the Keycard Door.
5. Press **E** to unlock and open the door.

---

### Puzzle 2 – Crossing the Poison Gas Area

**Objective**
Safely pass through the poison gas zone.

**Solution**

1. Locate the Gasmask.
2. Press **E** to collect it.
3. Verify the Gasmask appears in the Inventory.
4. Enter the poison gas zone.
5. Proceed safely without taking damage.

---

### Puzzle 3 – Completing the Game

**Objective**
Reach the End Zone and win.

**Solution**

1. Collect enough collectibles to earn at least 50 points.
2. Obtain any required special items.
3. Navigate through hazards safely.
4. Reach the End Zone.
5. Enter the End Zone to trigger the victory screen.

---

## Game Hints

### Hint 1

The Keycard is required to access restricted areas.

### Hint 2

Do not enter poison gas areas without first obtaining the Gasmask.

### Hint 3

Lava causes heavy damage. Cross it quickly.

### Hint 4

Water causes instant death.

### Hint 5

Check the Inventory panel to confirm collected special items.

---

## Known Limitations / Bugs

- No save/load system implemented.
- Progress resets when the game is closed.
- Inventory only tracks special items (Keycard and Gasmask).
- Collectibles do not respawn after collection.
- Timer resets when the game restarts.
- Door animations may occasionally appear out of sync if interrupted.
- Poison gas damage only checks whether the player owns a gasmask and does not visually equip the item.
- Player respawns at a fixed respawn point rather than the last checkpoint.
---

## Development Information

### Engine

Unity 6

### Programming Language

C#

### Development Tools

* Unity Editor
* Visual Studio
* TextMeshPro

---

## References and Credits

### Audio Assets

#### Nature Sound FX
Source: Unity Asset Store  
Asset: Nature Sound FX  
Publisher: Nox Sound  
URL: https://assetstore.unity.com/packages/audio/sound-fx/nature-sound-fx-180413

Used for:
- Environmental ambience
- Background nature sounds
- Atmospheric audio effects

---

#### Doors Small Sound Pack
Source: Unity Asset Store  
Asset: Doors Small Sound Pack  
Publisher: MRF Team  
URL: https://assetstore.unity.com/packages/audio/sound-fx/doors-small-sound-pack-262071

Used for:
- Door opening sound effects
- Door closing sound effects
- Locked door interaction sounds

---

### Visual Effects

#### Cartoon FX Remaster Free
Source: Unity Asset Store  
Asset: Cartoon FX Remaster Free  
Publisher: Jean Moreno  
URL: https://assetstore.unity.com/packages/vfx/particles/cartoon-fx-remaster-free-109565

Used for:
- Collectible pickup effects
- Particle effects
- Visual feedback during gameplay

---

#### Free Lava Shader
Source: Unity Asset Store  
Asset: Free Lava Shader  
Publisher: Blue Moon Studio  
URL: https://assetstore.unity.com/packages/vfx/shaders/free-lava-shader-292492

Used for:
- Lava hazard materials
- Animated lava surface effects
- Environmental hazard visuals

---

### Unity Packages

#### TextMeshPro
Source: Unity Technologies

Used for:
- User Interface text
- Inventory display
- Health display
- Score display
- Win and Game Over screens

#### Unity Input System
Source: Unity Technologies

Used for:
- Player movement input
- Interaction controls
- Keyboard and mouse support

---

### Development Tools

- Unity 6
- Visual Studio 2022
- TextMeshPro

---

### Acknowledgements

Special thanks to:

- Unity Technologies for the game engine and development tools.
- Asset creators on the Unity Asset Store for providing free assets used in this project.

---

## Author

**Student Name:** Lim Hong Wei

**Student ID:** 10272098K

**Module:** Interactive 3D Environments (I3E)

**Institution:** Ngee Ann Polytechnic

**Year:** 2026
