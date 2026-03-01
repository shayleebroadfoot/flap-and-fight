# Skyblade: Flap & Fight

## Background
This project is developed for SENG 4660 Agile Game Development. **Skyblade: Flap & Fight** is a 2D game where the player controls a flying character with a core gameplay loop focused on survival and combat. 

The Project Proposal document can be viewed here: [Project Proposal] (https://docs.google.com/document/d/1RaUm9SKqCFLQlJPG8B6xBdN2P6oCvjCG7F2bayTgCtI/edit?usp=sharing)

**Core Mechanics:**
* The player flaps to stay airborne and must avoid obstacles.
* Enemy birds appear, which the player can attack mid-air.
* The objective is to gain points while managing health; taking hits reduces health, and the game ends when health reaches zero.

**Development Approach:**
* The project will be developed iteratively over 4 sprints using Agile methodology.
* We will track tasks, user stories, and bugs using a Kanban sprint board.

## Setup & Cloning Instructions

### ⚠️ IMPORTANT: Git LFS Requirement
This project uses Unity, which generates large binary files (like `.png`, `.wav`, and `.fbx`). To prevent our repository from bloating and slowing down, we are using Git Large File Storage (LFS).

**You MUST install Git LFS before cloning this repository, or you will only download pointer files instead of the actual game assets.**

**Windows Setup (For Partner):**
1. Download and install Git LFS from git-lfs.github.com.
2. Open your terminal or command prompt and run the following command once: `git lfs install`
3. You can now securely clone the repository.

*(Note for Mac users: Install via Homebrew using `brew install git-lfs`, then run `git lfs install`.)*

### Unity Versioning
To avoid compatibility issues, we must both use the exact same Unity version (major/minor/patch).
* Our agreed-upon version is tracked in `ProjectSettings/ProjectVersion.txt`.
* Please check this file and ensure you install the matching version (preferably an LTS release) via Unity Hub.
