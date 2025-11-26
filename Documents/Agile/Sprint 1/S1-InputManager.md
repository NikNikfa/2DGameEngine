
## **1. Summary of Implementation**

- Implemented a global `InputManager` to centralize keyboard handling.
    
- Uses MonoGame’s `Keyboard.GetState()` each frame to capture input.
    
- Provides simple query methods such as:
    
    - `IsKeyDown(Keys key)`
        
    - `IsKeyPressed(Keys key)` _(if implemented)_
        
    - `IsKeyReleased(Keys key)` _(if implemented)_
        
- Integrated into the main game loop (`Game1.Update()`), ensuring input is updated once per frame.
    

---

## **2. Purpose / Motivation**

The main goal is to isolate input logic from:

- Game entities
    
- Game1 boilerplate
    
- Gameplay code
    

This aligns with SRP and makes input reusable and consistent.

Having a dedicated InputManager allows:

- A single source of truth for input state
    
- Cleaner gameplay code
    
- Easy extension (e.g., adding gamepad/mouse later)
    
- Pausing or disabling input in future scenes
    

---

## **3. Current Design Notes**

- Currently handles **keyboard only**.
    
- Uses two keyboard states:
    
    - **CurrentState**
        
    - **PreviousState**
        

This enables detection of:

- Continuous presses
    
- Single-frame key presses
    
- Key releases
    

Even if only `IsKeyDown` is implemented now, the internal structure supports expansions.

---

## **4. Integration Notes**

- Called at the **top** of the `Game1.Update()` method:
```csharp
InputManager.Update();
```

- Other classes (e.g., `Player`) rely on the InputManager for clean, readable input checks.
    
- Works seamlessly with EngineTime for frame-based logic.
    

Example usage in gameplay code:
```csharp
if (InputManager.IsKeyDown(Keys.W))
    MoveUp();
```

---

## **5. Decisions Made**

- Implemented as a **static class** for simplicity and global access.
    
- Input captured **once per frame** to ensure consistent results across the engine.
    
- MonoGame’s keyboard API wrapped inside an abstraction for better testability and clarity.
    

---

## **6. Known Limitations / To-Do**

- ❗ No mouse or gamepad support yet (planned for future sprints).
    
- ❗ No capability to disable input when the game is paused.
    
- ❗ Does not currently handle text input or character events.
    
- ❗ Only keyboard — no support for rebinding or input mapping.
    
- ❗ Must eventually integrate with a Scene system (e.g., disable input in menus).