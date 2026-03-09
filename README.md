# Skyblade: Flap & Fight

## Table of Contents

- [Background](#background)
- [Documentation](#documentation)
- [Setup & Cloning Instructions](#setup--cloning-instructions)
- [Unity Version](#unity-version)
- [Unity Team Tip](#unity-team-tip)

## Background

This project is developed for **SENG 4660 Agile Game Development**.  
**Skyblade: Flap & Fight** is a 2D game where the player controls a flying character, with gameplay focused on aerial movement, survival, and combat.

## Documentation

This project uses **GitHub Projects** to manage the Kanban board:  
https://github.com/users/shayleebroadfoot/projects/3

All deliverable documents can be found in the **[Documentation](./documentation)** folder in the `main` branch.

### Core Mechanics

- The player must **flap to stay airborne** while avoiding obstacles.
- **Enemy birds appear** and can be attacked mid-air.
- The objective is to **score points while managing health**.
- Taking damage reduces health, and the game ends when health reaches zero.

### Development Approach

- Development follows an **Agile process across four sprints**.
- Tasks, user stories, and bugs are tracked using a **Kanban board**.

---

## Setup & Cloning Instructions

### ⚠️ Git LFS Requirement

This project uses **Unity**, which generates large binary files (such as `.png`, `.wav`, and `.fbx`). To prevent the repository from becoming slow or oversized, **Git Large File Storage (LFS)** is used.

You **must install Git LFS before cloning this repository**, otherwise only pointer files will be downloaded instead of the actual game assets.

### Windows Setup

1. Download and install Git LFS from: https://git-lfs.github.com
2. Open a terminal or command prompt and run:

```bash
git lfs install
```

3. You can now safely clone the repository.

### Mac Setup

Install with Homebrew:

```bash
brew install git-lfs
git lfs install
```

---

## Unity Version

To avoid compatibility issues, **all team members must use the exact same Unity version (major/minor/patch).**

- The required version is listed in  
  `ProjectSettings/ProjectVersion.txt`
- Install the matching version (preferably an **LTS release**) using **Unity Hub**.

---

## Unity Team Tip

After cloning, it’s common to run:

```bash
git lfs pull
```

This ensures all Git LFS assets download correctly.
