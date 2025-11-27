
## 1. Purpose of the Task

The objective of Task 4 was to integrate two core engine subsystems:

- **EngineTime** (provides `DeltaTime` and `TotalTime`)
    
- **InputManager** (provides keyboard input state)
    

into the updated engine architecture introduced in Tasks 2 and 3.

The result should ensure:

1. All entities update after input and time are processed.
    
2. Player and future entities receive accurate and synchronized engine data.
    
3. Game1 becomes a high-level coordinator rather than handling low-level logic.
    

This task prepares the engine for implementing real Player movement in Task 5.

---

## 2. Problem Before Implementation

Before this task:

- EngineTime and InputManager were only used directly inside Game1.
    
- Player and future entities did not have access to input or delta time.
    
- The update loop in Game1 was not aligned with the new architecture involving `Entity` and `EntityManager`.
    
- There was no structured flow to ensure
    
    - time updates
        
    - input updates
        
    - entity updates  
        happened in the correct order.
        

This made real-time movement, animation, and consistent gameplay timing impossible.

---

## 3. Solution Overview

The solution was implemented in three coordinated steps:

### 3.1. Update the main engine loop in Game1

Game1 now updates the systems in the following order:

1. `EngineTime.Update()`
    
2. `InputManager.Update()`
    
3. Global shortcut handling
    
4. `_entityManager.Update()`
    
5. Rendering
    

This ensures all entities receive correct delta time and input state before updating.

---

### 3.2. Verify and align EngineTime implementation

EngineTime provides two essential values:

```csharp
public static float DeltaTime { get; private set; }
public static float TotalTime { get; private set; }
```

and updates them based on MonoGame's GameTime.  
This ensures consistent and frame-rate-independent movement.

---

### 3.3. Verify and align InputManager implementation

InputManager stores both the current and previous keyboard states to support:

- `IsKeyDown`
    
- `IsKeyPressed` (edge detection)
    

This enables entities to react correctly to continuous and one-time key presses.

---

### 3.4. Prepare Player.Update for future movement logic

Player.Update() now reads keyboard input through InputManager but does not yet move.  
Movement will be implemented in Task 5.

---

## 4. Final Implementation Details

### 4.1. Game1 Update Loop

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

This is the correct update order for the engine.

---

### 4.2. Player Update Preparation

```csharp
public override void Update(GameTime gameTime)
{
    bool moveLeft  = InputManager.IsKeyDown(Keys.A) || InputManager.IsKeyDown(Keys.Left);
    bool moveRight = InputManager.IsKeyDown(Keys.D) || InputManager.IsKeyDown(Keys.Right);
    bool moveUp    = InputManager.IsKeyDown(Keys.W) || InputManager.IsKeyDown(Keys.Up);
    bool moveDown  = InputManager.IsKeyDown(Keys.S) || InputManager.IsKeyDown(Keys.Down);

    // Movement logic will be implemented in Task 5
}
```

The Player class now reads keyboard input correctly.

---

## 5. Architectural Impact

### 5.1. Single Responsibility Principle (SRP)

- EngineTime handles timing only.
    
- InputManager handles input only.
    
- Player handles Player behavior only.
    
- Game1 orchestrates system updates and nothing more.
    

### 5.2. Open/Closed Principle (OCP)

Future entities will automatically receive updated input and delta time, without changes to Game1 or core systems.

### 5.3. Dependency Inversion Principle (DIP)

High-level modules now depend on engine abstractions (EngineTime, InputManager), not MonoGame’s raw APIs (Keyboard.GetState, GameTime).

### 5.4. Predictable Engine Flow

The update order is now deterministic, which is essential for gameplay consistency.

---

## 6. Future Extensions

This integration enables:

- Player movement (Task 5)
    
- Animation timing
    
- Physics updates based on delta time
    
- Input-driven actions (attacks, interactions)
    
- AI updates driven by time-based logic
    

No modifications will be needed to Game1 for any of these additions.

---

## 7. Summary

Task 4 successfully integrated EngineTime and InputManager into the engine’s update loop and connected these systems to the entity architecture.

This completes the foundational engine loop, allowing entities to:

- React to real-time input
    
- Update consistently across different frame rates
    

The engine is now ready for Task 5, where Player movement will be implemented.