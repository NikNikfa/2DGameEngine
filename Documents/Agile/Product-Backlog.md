
This document describes the complete set of **User Stories** for the development of a custom 2D Game Engine.  
Each story is written in standard Agile format:

**As a [role], I want [feature], so that [benefit].**

---

# Legend

- **Priority:**
    
    - High
        
    - Medium
        
    - Low
        
- **Status:**
    
    - TODO
        
    - In Progress
        
    - Done
        

---

# 1. Core Engine Systems

### US-01 — Engine Time System

**Priority:** High  
**Status:** Done  
As a developer, I want a centralized time system (DeltaTime and TotalTime) so that gameplay behaves consistently across different frame rates.

---

### US-02 — Input Manager

**Priority:** High  
**Status:** Done  
As a developer, I want a unified input system so that input handling is consistent and easy across the engine.

---

### US-03 — Core Game Loop in Game1

**Priority:** High  
**Status:** Done  
As a developer, I want a stable and predictable Update/Draw game loop so that engine systems run in the correct order.

---

# 2. Entity System

### US-04 — Base Entity Class

**Priority:** High  
**Status:** TODO  
As a developer, I want a base Entity class so that all game objects follow a consistent update/render structure.

---

### US-05 — EntityManager

**Priority:** High  
**Status:** TODO  
As a developer, I want an EntityManager to manage the lifecycle of all entities so that Game1 is not responsible for updating them manually.

---

### US-06 — Transform System

**Priority:** High  
**Status:** TODO  
As a developer, I want each entity to have position, rotation, and scale so that objects can move and be transformed in the game world.

---

# 3. Player and Gameplay

### US-07 — Player (Initial Version)

**Priority:** High  
**Status:** Done  
As a developer, I want a basic controllable Player object so that I can validate the input and timing systems.

---

### US-08 — Player Inherits From Entity

**Priority:** Medium  
**Status:** TODO  
As a developer, I want the Player to extend the Entity class so that it integrates cleanly with the entity system.

---

### US-09 — Player Movement Component

**Priority:** Medium  
**Status:** TODO  
As a developer, I want movement logic separated from core Player data so that the design remains modular and maintainable.

---

# 4. Asset Loading and Content Pipeline

### US-10 — Asset Loader

**Priority:** High  
**Status:** Done  
As a developer, I want a centralized asset loading system with caching so that textures and other assets are loaded efficiently.

---

### US-11 — Asset Unloading / Cleanup

**Priority:** Medium  
**Status:** TODO  
As a developer, I want unused assets to unload during scene transitions so that memory is managed properly.

---

# 5. Rendering System

### US-12 — Entity-Based Rendering

**Priority:** High  
**Status:** TODO  
As a developer, I want entities to handle their own drawing logic so that rendering is scalable for multiple objects.

---

### US-13 — LayerDepth Rendering

**Priority:** Medium  
**Status:** TODO  
As a developer, I want layer support so that objects can appear in front of or behind one another.

---

### US-14 — Basic Camera System

**Priority:** Medium  
**Status:** TODO  
As a developer, I want a camera with view/zoom capabilities so that the game world can scroll or expand.

---

# 6. Scene Management

### US-15 — Scene Base Class

**Priority:** High  
**Status:** TODO  
As a developer, I want a Scene abstraction so that different screens (menu, gameplay, pause) can be separated cleanly.

---

### US-16 — SceneManager

**Priority:** High  
**Status:** TODO  
As a developer, I want a SceneManager so that scenes can be switched at runtime.

---

### US-17 — Level Loading

**Priority:** Medium  
**Status:** TODO  
As a developer, I want a scene to be able to load levels or maps so that gameplay can scale.

---

# 7. Physics and Collision

### US-18 — Collision Bounds

**Priority:** Medium  
**Status:** TODO  
As a developer, I want entities to have bounding boxes so that collision detection is possible.

---

### US-19 — Simple Collision System

**Priority:** Medium  
**Status:** TODO  
As a developer, I want a basic collision detection system so that entities can interact physically.

---

### US-20 — Movement with Collision Handling

**Priority:** Medium  
**Status:** TODO  
As a developer, I want entity movement to respect collisions so that gameplay behavior is realistic.

---

# 8. Animation System

### US-21 — Sprite Animation Component

**Priority:** Medium  
**Status:** TODO  
As a developer, I want the engine to support sprite animations so that entities can have movement and idle animations.

---

### US-22 — Animation Controller

**Priority:** Medium  
**Status:** TODO  
As a developer, I want a system to control and switch between animations so that animation behavior is structured and flexible.

---

# 9. Audio System

### US-23 — Sound Effects

**Priority:** Low  
**Status:** TODO  
As a developer, I want to play sound effects so that the game feels responsive.

---

### US-24 — Background Music System

**Priority:** Low  
**Status:** TODO  
As a developer, I want music playback support so that different scenes can have soundtracks.

---

# 10. Debugging and Tools

### US-25 — Debug Overlay

**Priority:** Low  
**Status:** TODO  
As a developer, I want an on-screen debug overlay showing FPS and entity count so that I can monitor engine performance.

---

### US-26 — Logging System

**Priority:** Low  
**Status:** TODO  
As a developer, I want a simple logging utility so that debugging is easier during development.

---

# 11. UI System (Future Work)

### US-27 — Basic UI Elements

**Priority:** Low  
**Status:** TODO  
As a developer, I want buttons, labels, and panels so that I can build interfaces such as menus or HUDs.

---

### US-28 — UI Input Routing

**Priority:** Low  
**Status:** TODO  
As a developer, I want input routing for UI so that gameplay input does not interfere with UI interactions.

---

# 12. Documentation

### US-29 — Sprint Documentation (Living Notes)

**Priority:** Medium  
**Status:** In Progress  
As a developer, I want documentation for each sprint so that the project’s progress and decisions remain traceable.

---

### US-30 — Final Implementation Documentation

**Priority:** Medium  
**Status:** TODO  
As a developer, I want complete final documentation for each subsystem so that the final engine is professionally documented.