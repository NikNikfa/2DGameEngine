
## Asset and Entity Architecture
**Project:** 2D Game Engine  
**Sprint Number:** 2 / ?
**Status:** Completed (Implementation)  
**Sprint Type:** Architecture and Core Systems
**Duration**: November 15, 2025 - November 27, 2025
## 1. Sprint Goal

The goal of Sprint 2 was to move from a single-object, MonoGame-centric setup towards a small but real **engine architecture**.

Concretely:

- Centralize asset loading.
    
- Introduce a reusable **Entity** abstraction.
    
- Add an **EntityManager** to manage all active objects.
    
- Integrate **EngineTime** and **InputManager** with the new architecture.
    
- Implement basic, frame-rate–independent **Player movement**.
    

Sprint 2 is the point where the project stops being “just a MonoGame project” and becomes a structured 2D engine.

---

## 2. User Stories Completed (From [[Product-Backlog]])

### Entity System

|ID|User Story|Priority|Status|Notes|
|---|---|---|---|---|
|**US-04**|Base Entity Class|High|**Done**|Entity.cs created with Update/Draw/Position|
|**US-05**|EntityManager|High|**Done**|Manages entities, central Update/Draw loops|
|**US-06**|Transform System|High|**Partially Done**|Position implemented; rotation/scale deferred|

### Player & Gameplay

|ID|User Story|Priority|Status|Notes|
|---|---|---|---|---|
|**US-07**|Player (Initial Version)|High|**Done**|Basic renderable player created|
|**US-08**|Player Inherits From Entity|Medium|**Done**|Player : Entity implemented|
|**US-09**|Player Movement Component|Medium|**Done**|Movement handled in Player.Update (DeltaTime-based)|

### Asset Loading

| ID        | User Story    | Priority | Status   | Notes                                 |
| --------- | ------------- | -------- | -------- | ------------------------------------- |
| **US-10** | Asset Loader  | High     | **Done** | AssetLoader.cs created and integrated |


### Rendering System

| ID        | User Story             | Priority | Status           | Notes                                    |
| --------- | ---------------------- | -------- | ---------------- | ---------------------------------------- |
| **US-12** | Entity-Based Rendering | High     | **Done (Basic)** | Entities draw themselves using base Draw |

---

## 3. Scope of Work

Sprint 2 consisted of the following tasks:

1. **Task 1:** Implement [[1-AssetLoader]]
    
2. **Task 2:** Implement abstract [[2-Entity]] base class and refactor `Player` to inherit from it
    
3. **Task 3:** Implement [[3-EntityManager]]
    
4. **Task 4:** Integrate `EngineTime` and `InputManager` into the entity-based architecture ([[4-Integration]])
    
5. **Task 5:** Implement [[5-PlayerMovement]] using `InputManager` and `EngineTime`
    
6. **(Deferred / Optional)** Task 6: Project structure cleanup and additional folders (scenes, components, etc.)
    

All tasks from 1 to 5 were completed in this sprint. Task 6 was considered optional and is deferred to later sprints where it better fits the scope.

---

## 4. Implemented Features

### 4.1 AssetLoader (Core/AssetLoader.cs)

A static `AssetLoader` class was introduced to centralize asset loading.

Key characteristics:

- Stores a reference to MonoGame’s `ContentManager` via `Initialize(ContentManager content)`.
    
- Provides `LoadTexture(string assetName)` as the first asset API.
    
- Used in `Game1.LoadContent()` to load the Player texture.
    

Effects:

- `Game1` no longer uses `Content.Load<T>()` directly.
    
- Asset loading is now part of the engine layer, not the game application layer.
    
- The design is open for future extensions such as `LoadFont`, `LoadSound`, `LoadJson`, etc.
    

This improves modularity and follows SRP and OCP.

---

### 4.2 Entity Base Class (Entities/Entity.cs) and Player Refactor (Entities/Player.cs)

An abstract `Entity` class was introduced as the base for all game objects.

`Entity` provides:

- `Vector2 Position`
    
- `Texture2D Texture` (protected)
    
- `bool IsActive`
    
- `virtual void Update(GameTime gameTime)`
    
- `virtual void Draw(SpriteBatch spriteBatch)`
    

The `Player` class was refactored to:

- Inherit from `Entity`
    
- Implement `Update` with Player-specific logic
    
- Use the base `Draw` implementation (no duplication)
    

Effects:

- All future entities (Enemy, Coin, Projectile, etc.) can inherit from `Entity`.
    
- Common functionality (position, texture, draw) is defined once.
    
- The structure is ready for shared systems (collision, animation, physics).
    

This reduces duplication and prepares the engine for extension.

---

### 4.3 EntityManager (Core/EntityManager.cs)

The `EntityManager` class was introduced to manage all active entities.

Responsibilities:

- Store a list of `Entity` objects.
    
- Provide `Add(Entity entity)` and `Remove(Entity entity)`.
    
- Call `Update` for each active entity.
    
- Call `Draw` for each active entity.
    

Integration:

- `Game1` now creates a single `EntityManager` instance.
    
- Player is added to the entity manager in `LoadContent()`.
    
- `Game1.Update()` calls `_entityManager.Update(gameTime)`.
    
- `Game1.Draw()` calls `_entityManager.Draw(_spriteBatch)`.
    

Effects:

- `Game1` is no longer responsible for updating and drawing individual entities.
    
- New entities can be added without modifying `Game1`.
    
- The engine now has a central place to manage objects, which is necessary for later features (scenes, collision, etc.).
    

---

### 4.4 EngineTime and InputManager Integration (Core/EngineTime.cs, Core/InputManager.cs, Game1.cs, Player.cs)

The engine loop in `Game1` was updated to follow a clear order:

```csharp
protected override void Update(GameTime gameTime)
{
    EngineTime.Update(gameTime);
    InputManager.Update();

    if (InputManager.IsKeyPressed(Keys.Escape))
        Exit();

    _entityManager.Update(gameTime);

    base.Update(gameTime);
}
```

Key points:

- `EngineTime.Update(gameTime)` is called first to compute `DeltaTime` and `TotalTime`.
    
- `InputManager.Update()` is called second to store current and previous keyboard states.
    
- Global shortcuts (e.g., `Escape` to exit) are processed next.
    
- Finally, all entities are updated through `EntityManager`.
    

The `Player` now accesses:

- `InputManager` to read input.
    
- `EngineTime.DeltaTime` to make movement frame-rate independent.
    

This wires the existing subsystems into the new architecture.

---

### 4.5 Player Movement (Entities/Player.cs)

Movement was implemented in `Player.Update` using both WASD and Arrow keys.

Core logic:

- A `direction` vector is built based on currently pressed keys.
    
- The vector is normalized to avoid faster diagonal movement.
    
- The position is updated using:
```csharp
Position += direction * _speed * EngineTime.DeltaTime;
```

Where `_speed` is a configurable float value (units per second).

Effects:

- Player movement is smooth and consistent across different frame rates.
    
- Input and time systems are fully utilized by the entity.
    
- Movement logic is encapsulated inside `Player`, not in `Game1`.
    

No screen-clamping or collision logic was added in this sprint by design.  
Those topics are reserved for future sprints (rendering, camera, collision).

---

## 5. Architecture Overview After Sprint 2

The engine now has the following high-level structure:

- **Core**
    
    - `AssetLoader` – centralized asset loading
        
    - `EngineTime` – global delta time / total time
        
    - `InputManager` – global keyboard input handling
        
    - `EntityManager` – centralized entity management
        
- **Entities**
    
    - `Entity` – abstract base class for game objects
        
    - `Player` – concrete implementation of a controllable entity
        
- **Game Layer**
    
    - `Game1` – composition root, coordinates engine systems and delegates update/draw to managers
        

Key architectural properties:

- `Game1` orchestrates, but does not implement detailed behavior.
    
- Game objects are represented by `Entity` and managed by `EntityManager`.
    
- Subsystems (time, input, asset loading) are separated and reusable.
    
- The design is ready for scenes, rendering layers, and additional entities.
    

---

## 6. SOLID Principles in Sprint 2

Sprint 2 explicitly pushed the project toward SOLID design:

- **[[Single responsibility Principle]] (SRP)**
    
    - `AssetLoader` only handles asset loading.
        
    - `EntityManager` only manages entities.
        
    - `EngineTime` only tracks time.
        
    - `InputManager` only tracks input.
        
    - `Player` only contains Player-specific behavior.
        
- **O[[Open-closed Principle]](OCP)**
    
    - New entities can be added by extending `Entity` without modifying core systems.
        
    - Asset types can be added by extending `AssetLoader` with new methods.
        
- **[[Liskov substitution Principle]] (LSP)**
    
    - Any `Entity` subclass can be stored and processed by `EntityManager`.
        
- **[[Interface Segregation Principle]] (ISP)**
    
    - `Entity` is intentionally minimal. No entity is forced to implement unused features.
        
    - Later, more specific interfaces (e.g., collidable, drawable layers) can be added if needed.
        
- **[[Dependency inversion Principle]] (DIP)**
    
    - `Player` depends on engine-level abstractions (`EngineTime`, `InputManager`) instead of raw MonoGame APIs.
        
    - `Game1` depends on core engine components (`EntityManager`, `AssetLoader`) rather than individual entities.
        

---

## 7. Deviations and Deferred Work

The following items were intentionally deferred to later sprints:

- Clamping Player inside the window bounds.
    
- Refined project folder structure beyond current Core/Entities organization.
    
- Collision detection and world interaction.
    
- Rendering layers and camera system.
    
- Scene management.
    

These will be addressed primarily in Sprint 3 and Sprint 4.

---

## 8. Outcome of Sprint 2

By the end of Sprint 2, the engine has:

- A working Player that moves smoothly using WASD/Arrow keys.
    
- A clear entity model (`Entity`, `Player`).
    
- A central entity lifecycle manager (`EntityManager`).
    
- Core services for time, input, and assets integrated into the engine loop.
    
- A cleaned-up `Game1` that focuses on orchestration, not implementation details.
    

The engine is now ready for the next stage: **rendering pipeline, camera, and scenes**, which will be addressed in Sprint 3.