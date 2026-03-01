# [cite_start]Skyblade: Flap & Fight [cite: 88]

## Background
[cite_start]This project is developed for SENG 4660 Agile Game Development[cite: 4]. [cite_start]**Skyblade: Flap & Fight** is a 2D game where the player controls a flying character with a core gameplay loop focused on survival and combat[cite: 88, 89, 90, 93]. 

**Core Mechanics:**
* [cite_start]The player flaps to stay airborne and must avoid obstacles[cite: 90, 91].
* [cite_start]Enemy birds appear, which the player can attack mid-air[cite: 92, 93].
* [cite_start]The objective is to gain points while managing health; taking hits reduces health, and the game ends when health reaches zero[cite: 94, 95, 96].

**Development Approach:**
* [cite_start]The project will be developed iteratively over 4 sprints using Agile methodology[cite: 31, 107].
* [cite_start]We will track tasks, user stories, and bugs using a Kanban sprint board[cite: 36].

## Setup & Cloning Instructions

### ⚠️ IMPORTANT: Git LFS Requirement
[cite_start]This project uses Unity, which generates large binary files (like `.png`, `.wav`, and `.fbx`)[cite: 166, 167, 168]. [cite_start]To prevent our repository from bloating and slowing down, we are using Git Large File Storage (LFS)[cite: 155, 169].

[cite_start]**You MUST install Git LFS before cloning this repository, or you will only download pointer files instead of the actual game assets.** [cite: 194, 195, 197]

[cite_start]**Windows Setup (For Partner):** [cite: 175]
1. [cite_start]Download and install Git LFS from git-lfs.github.com. [cite: 176]
2. [cite_start]Open your terminal or command prompt and run the following command once: `git lfs install` [cite: 177, 178]
3. [cite_start]You can now securely clone the repository. [cite: 179]

[cite_start]*(Note for Mac users: Install via Homebrew using `brew install git-lfs`, then run `git lfs install`.)* [cite: 172, 173, 174]

### Unity Versioning
[cite_start]To avoid compatibility issues, we must both use the exact same Unity version (major/minor/patch). [cite: 211, 212]
* [cite_start]Our agreed-upon version is tracked in `ProjectSettings/ProjectVersion.txt`. [cite: 214, 215]
* [cite_start]Please check this file and ensure you install the matching version (preferably an LTS release) via Unity Hub. [cite: 204, 213, 216]
