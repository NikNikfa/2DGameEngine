## **Duration**

**Start Date:** November 6, 2025  
**End Date:** November 13, 2025

---

## **Sprint Goal**

Establish the core architecture and foundational systems of the 2D Game Engine, including timing, input handling, and a basic controllable player entity. This sprint provides the initial engine loop and interaction mechanics upon which future systems will build.

---
## **User Stories Completed (From Product Backlog)**

|ID|User Story|Priority|Status|
|---|---|---|---|
|US-01|As a developer, I want a centralized time system (DeltaTime and TotalTime) so that gameplay behaves consistently across different frame rates.|High|Done|
|US-02|As a developer, I want a unified input system so that input handling is consistent and easy across the engine.|High|Done|
|US-03|As a developer, I want a stable and predictable Update/Draw game loop so that engine systems run in the correct order.|High|Done|
|US-07|As a developer, I want a basic controllable Player object so that I can validate the input and timing systems.|High|Done|
|US-05|As a developer, I want the Game1 class to integrate core systems (EngineTime, InputManager, Player) so that the main loop is functional.|High|Done|

---

## **Completed Tasks**

### **1. EngineTime Implementation**

Linked Document: [[S1-EngineTime]]

- Implemented a timing system that calculates `DeltaTime` each frame.
    
- Provides access to elapsed and total time values.
    
- Integrated into `Game1.Update()` as the first update step.
    
- Ensures consistent movement and behavior across different frame rates.
    

**File:** `Core/EngineTime.cs`

---

### **2. InputManager Implementation**

Linked Document: [[S1-InputManager]]

- Handles keyboard state retrieval using `Keyboard.GetState()` (MonoGame).
    
- Provides convenience methods such as `IsKeyDown(Keys key)`.
    
- Updated once per frame from `Game1.Update()` for consistent input behavior.
    

**File:** `Core/InputManager.cs`

---

### **3. Player Class Implementation**

Linked Document: [[S1-Player]]

- Implemented a basic player entity containing position, speed, and texture.
    
- Processes WASD input to move across the game window.
    
- Includes `Update()` for movement logic and `Draw()` for rendering.
    
- Integrated into `Game1` with proper initialization and method calls.
    

**File:** `Entities/Player.cs`

---

### **4. Game1 Integration**

Linked Document: [[S1-Game1]]

- Integrated EngineTime, InputManager, and Player into the main loop.
    
- Structured `Update()` to execute system updates in a consistent sequence.
    
- Verified proper rendering pipeline using `SpriteBatch`.
    
- Implemented application exit via the `Escape` key.
    
- Player updates correctly based on input and DeltaTime.
    

**File:** `Game1.cs`

---

## **Implemented Components Summary**

- **EngineTime** — Core timing system
    
- **InputManager** — Centralized input handling
    
- **Player** — Basic controllable game entity
    
- **Game1** — Primary entry point and main loop orchestration
    

---

## **Lessons Learned**

- Decoupling engine systems from `Game1` simplifies maintenance and future extension.
    
- DeltaTime is essential for predictable movement and frame-independent behavior.
    
- Abstracting input logic significantly reduces complexity inside gameplay classes.
    
- Early separation of concerns lays a strong foundation for scalable engine architecture.
    

---

## **Sprint Outcome**

- Core engine systems designed, implemented, and integrated successfully.
    
- Game loop verified with functioning input and timing.
    
- Engine is ready for expansion in Sprint 2, including entity architecture, scene management, and rendering improvements.