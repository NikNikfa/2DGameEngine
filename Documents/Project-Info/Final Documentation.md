
# 2D Game Engine Final Project Documentation

## Project Type

Software Engineering / Game Engine Architecture Project

---

## 1. Project Overview

This project implements a **custom 2D Game Engine** built on top of the MonoGame framework.  
The engine was designed with a strong emphasis on **clean architecture**, **separation of concerns**, and **extensibility**, rather than feature quantity.

The goal was not to build a full commercial engine, but to design and implement a **solid foundational engine** that demonstrates professional software engineering practices and core game engine concepts.

---

## 2. Project Objectives

The main objectives of this project were:

- Design a clean and modular 2D game engine architecture
    
- Apply **SOLID principles** throughout the codebase
    
- Separate engine logic from framework-specific code
    
- Implement core systems required for a playable game loop
    
- Provide a foundation that can be extended with future systems
    

---

## 3. Technology Stack

- **Language:** C#
    
- **Framework:** MonoGame
    
- **Architecture Style:** Layered Architecture
    
- **Paradigms:** Object-Oriented Design, SOLID Principles
    

MonoGame is used strictly as a **low-level framework** for:

- Rendering
    
- Input
    
- Window management
    

All engine logic is implemented independently of MonoGame wherever possible.

---

## 4. High-Level Architecture

The engine is structured into three main layers:

### 4.1 Core Engine Layer

Contains reusable engine systems that are independent of any specific game:

- Time management
    
- Input handling
    
- Entity system
    
- Collision system
    
- Rendering order and camera logic
    

### 4.2 Application Layer (Game Logic)

Contains game-specific logic:

- Player entity
    
- Obstacles
    
- Scenes
    
- Gameplay rules
    

### 4.3 Framework Layer

MonoGame is treated as an external dependency and is not referenced directly by gameplay logic.

---

## 5. Core Systems Implemented

### 5.1 Game Loop

The engine relies on MonoGame’s `Update` and `Draw` loop but delegates responsibilities to engine systems.

This ensures:

- Predictable execution order
    
- Centralized control over update flow
    

---

### 5.2 Time System

A centralized time system provides:

- DeltaTime
    
- Frame-independent movement
    

This ensures consistent gameplay behavior across different frame rates.

---

### 5.3 Input System

Input handling is centralized through an `InputManager`.

Benefits:

- No direct dependency on framework input APIs in gameplay code
    
- Consistent input access across the engine
    

---

### 5.4 Entity System

All game objects inherit from a base `Entity` class.

Entities:

- Have lifecycle methods (`Update`, `Draw`)
    
- Are managed centrally by an `EntityManager`
    
- Do not manage other entities directly
    

This ensures:

- Loose coupling
    
- Predictable update and render order
    

---

### 5.5 Transform System

A `Transform` class was introduced to standardize spatial data.

Each entity has:

- Position
    
- Visual size
    
- Collision size
    
- Collision offset
    

This allows the engine to reason about space consistently and enables collision detection and response.

---

### 5.6 Collision System

Collision handling was implemented in multiple stages:

1. **Collision Bounds (AABB)**  
    Axis-Aligned Bounding Boxes are used for simplicity and performance.
    
2. **Centralized Collision Detection**  
    A `CollisionSystem` detects overlapping entities and returns collision pairs.
    
3. **Collision Response**  
    A simple and deterministic response prevents entities from passing through obstacles by reverting movement.
    

This approach avoids premature physics complexity while ensuring correct gameplay behavior.

---

### 5.7 Rendering System & Layers

Rendering is handled through a clear layering system:

- Background
    
- World
    
- Foreground (Player)
    
- UI
    

Entities define their render layer, and the engine guarantees deterministic draw order.  
This ensures visual correctness without relying on scene-specific hacks.

---

### 5.8 Camera System

A 2D camera system was implemented using transformation matrices.

The camera:

- Follows the player
    
- Moves the world relative to the viewport
    
- Supports zoom and rotation (rotation unused but supported)
    

This creates a clean separation between world space and screen space.

---

## 6. Design Principles Applied (SOLID)

### Single Responsibility Principle (SRP)

Each class has one clear responsibility:

- `CollisionSystem` detects collisions
    
- `EntityManager` manages entities
    
- `Transform` manages spatial data
    

---

### Open/Closed Principle (OCP)

Systems are designed to be extended without modification:

- New entities can be added without changing engine systems
    
- Collision logic can be extended without modifying entity code
    

---

### Liskov Substitution Principle (LSP)

All entities can be treated uniformly as `Entity` instances without breaking behavior.

---

### Interface Segregation Principle (ISP)

Systems expose minimal required functionality and avoid forcing unused dependencies.

---

### Dependency Inversion Principle (DIP)

High-level systems depend on abstractions rather than framework details.

---

## 7. Development Process

The project was developed incrementally across **four sprints**:

- Sprint 1: Core setup and basic game loop
    
- Sprint 2: Asset loading and entity architecture
    
- Sprint 3: Rendering and camera systems
    
- Sprint 4: Spatial logic, collision detection, and response
    

Each sprint produced working, testable results and accompanying documentation.

---

## 8. Final System State

At completion, the engine supports:

- Frame-independent movement
    
- Centralized input handling
    
- Entity-based architecture
    
- Collision detection and response
    
- Camera-based world rendering
    
- Deterministic render ordering
    

The engine provides a **fully playable interaction loop** while remaining simple, clean, and extensible.

---

## 9. Limitations and Intentional Scope Decisions

The following systems were intentionally **not implemented**:

- Physics simulation
    
- Animation system
    
- Audio system
    
- UI widgets
    
- Level loading
    

These were excluded to maintain focus on **architecture and correctness**, not feature breadth.

---

## 10. Future Work

The engine is designed to be extended with:

- Sliding collision resolution
    
- Physics-based movement
    
- Animation components
    
- Audio management
    
- Scene transitions and UI systems
    

No refactoring of existing systems would be required to add these features.

---

## 11. Conclusion

This project successfully demonstrates the design and implementation of a clean, extensible 2D game engine foundation.

By prioritizing architecture, separation of concerns, and professional design principles, the result is a robust engine suitable for further development and learning.

The project fulfills its academic and technical objectives and provides a strong basis for future expansion.