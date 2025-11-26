
## **1. Summary of Implementation**

- Fully integrated **EngineTime**, **InputManager**, and **Player** into the `Game1` class.
    
- Implemented a **sequential and deterministic update loop**:
    
    1. `EngineTime.Update(gameTime)`
        
    2. `InputManager.Update()`
        
    3. `_player.Update(gameTime)`
        
- Rendering confirmed working with `SpriteBatch.Begin()` → drawing the Player → `End()`.
    
- Escape key successfully exits the game through InputManager.
    
- Player movement updates smoothly thanks to DeltaTime usage.
    

This marks the first working version of the engine’s **core game loop**.

---

## **2. Purpose / Motivation**

The goal of Sprint 1’s Game1 implementation was to:

- Establish the engine’s **fundamental update/draw flow**
    
- Ensure subsystems (time, input, player logic) interact correctly
    
- Validate that the game responds to user input
    
- Prove that the engine base is functional before expanding architecture
    

Game1 acts as the orchestrator until more advanced systems (Scene, EntityManager) are introduced.

---

## **3. Current Design Notes**

### **Update Loop Structure**

```csharp
EngineTime.Update(gameTime);
InputManager.Update();

if (InputManager.IsKeyPressed(Keys.Escape))
    Exit();

_player.Update(gameTime);
```

- Updates occur in a clear, correct order
    
- Input checks happen before gameplay logic
    
- DeltaTime ensures consistent movement regardless of hardware
    

### **Rendering**

```csharp
spriteBatch.Begin();
_player.Draw(spriteBatch);
spriteBatch.End();
```

- Only one drawable entity exists in Sprint 1
    
- Rendering pipeline behaves correctly
    
- No flickering, stuttering, or draw-order issues
    

### **Initialization**

- Player texture is loaded via `Content.Load<Texture2D>()` (AssetLoader does not exist yet)
    
- SpriteBatch is instantiated in `LoadContent()`
    
- Player is created with an initial position
    

---

## **4. Integration Notes**

- Game1 now coordinates all engine systems from a single place
    
- Input and timing are updated exactly once per frame
    
- All gameplay logic is triggered after input
    
- Render order is simple and predictable
    
- Escape-to-exit logic confirms clean shutdown behavior
    

This establishes the minimal foundation for future Sprints.

---

## **5. Decisions Made**

- Kept Game1 responsible for orchestrating system updates (intentionally simple for Sprint 1)
    
- Did **not** introduce EntityManager or Scene system yet
    
- Used direct Content loading for textures
    
- Chose a simple movement test to validate timing + input correctness
    
- Kept Player update and draw calls direct for clarity
    

These decisions kept the early codebase small, testable, and easy to iterate on.

---

## **6. Known Limitations / To-Do**

- ❗ No `EntityManager` — Player is updated manually
    
- ❗ No Scene or State system — Game1 contains everything
    
- ❗ No asset caching or loaders — textures loaded directly
    
- ❗ No camera or world abstraction
    
- ❗ Game1 will require refactoring in future sprints to reduce responsibilities
    
- ❗ Update and draw calls are hard-coded to the Player object
    

These limitations are acceptable during Sprint 1 but will be addressed soon.