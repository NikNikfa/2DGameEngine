# 🌀 Core Engine Foundations

## 📆 Duration
**Start Date:** November 6, 2025  
**End Date:** November 13, 2025  

---

## 🎯 Sprint Goal
Establish the **core architecture** and basic systems of the 2D Game Engine, including input management, timing, and player entity control — forming the foundation for the engine loop and interaction layers.

---

## 📋 User Stories

| ID    | User Story                                                                                                     | Priority | Status |
| ----- | -------------------------------------------------------------------------------------------------------------- | -------- | ------ |
| US-01 | As a developer, I want a clean and modular engine architecture so that systems can be developed independently. | High     | ✅ Done |
| US-02 | As a developer, I want an `EngineTime` system to manage delta time for smooth frame updates.                   | High     | ✅ Done |
| US-03 | As a developer, I want an `InputManager` class to manage keyboard input in a unified way.                      | High     | ✅ Done |
| US-04 | As a developer, I want a `Player` entity that reacts to input and moves across the screen.                     | High     | ✅ Done |
| US-05 | As a developer, I want the `Game1` class to integrate core systems (EngineTime, InputManager, Player).         | High     | ✅ Done |

---

## 🧩 Completed Tasks

### 🔹 1. [[EngineTime]] Implementation
- Implemented a class to calculate **DeltaTime** for each frame.  
- Exposed methods to retrieve elapsed and total game time.  
- Integrated into `Game1.Update()` loop.  
- Ensures consistent frame-based updates regardless of system performance.

**File:** `Core/EngineTime.cs`

---

### 🔹 2. [[InputManager]] Implementation
- Handles keyboard state using `Keyboard.GetState()` (MonoGame).  
- Provides methods like `IsKeyDown(Keys key)` for easy querying.  
- Integrated globally in the `Game1` loop for centralized input control.

**File:** `Core/InputManager.cs`

---

### 🔹 3. [[Player]] Class Implementation
- Created `Player` entity with **position**, **speed**, and **texture**.  
- Receives input (WASD) through the input system to move on screen.  
- Added `Update()` and `Draw()` methods for movement and rendering.  
- Integrated into `Game1` with proper initialization and update calls.

**File:** `Entities/Player.cs`

---

### 🔹 4. Game1 Integration
- Integrated `EngineTime`, `InputManager`, and `Player` into `Game1`.  
- Organized `Update()` to handle system updates sequentially.  
- Confirmed rendering and input work as expected.  
- Game exits with `Escape` key and updates player position smoothly.

**File:** `Game1.cs`

---

## 🧱 Architecture Overview

Currently, our engine is built on an architecture that can be called:

- Layered Game Engine Architecture (Layered & Modular)

This means that different parts of the engine are defined in separate layers and communicate with each other only through defined interfaces or references.

🔹 Role of Each Layer:

| Layer             | Responsibility                                | Example                  |
| ----------------- | --------------------------------------------- | ------------------------ |
| Application Layer | Game entry point, overall flow control        | Game1                    |
| Engine Core       | Game-independent logic (time, input, physics) | EngineTime, InputManager |
| Gameplay Layer    | Game entities and behaviors                   | Player                   |
| Resources Layer   | Images, sounds, data                          | Assets                   |

This simple yet extensible structure forms the foundation on which we can later build more advanced systems for Scene, Physics, and Renderer.

---


Current implemented components:
- [[EngineTime]] (Core Layer)
- [[InputManager]] (Core Layer)
- [[Player]] (Game Layer)
- `Game1` (Main Loop entry point)

---

## 🧠 Lessons Learned
- Proper decoupling between `Game1` and systems improves maintainability.  
- `DeltaTime` is essential for consistent movement across devices.  
- Input abstraction simplifies player and future entity control.  
- Clear separation of systems reduces coupling and prepares for scalability.

---

## 🏁 Sprint Outcome
✅ Core systems successfully built and integrated.  
✅ Game loop functional with timing and input.  
✅ Ready for graphical system expansion in Sprint 2.

