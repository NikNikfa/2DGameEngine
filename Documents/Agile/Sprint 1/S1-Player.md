
## **1. Summary of Implementation**

- Implemented an early version of the `Player` class with:
    
    - **Position** (`Vector2`)
        
    - **Speed** (float)
        
    - **Texture** (`Texture2D`)
        
- Handles WASD keyboard input for movement.
    
- Implements:
    
    - `Update()` → movement logic
        
    - `Draw()` → rendering using **SpriteBatch**
        
- Integrated into `Game1` with:
    
    - Direct texture loading via `Content.Load<Texture2D>()`
        
    - Direct rendering via `spriteBatch.Draw()`
        

This is the first fully functional interactive object in the engine.

---

## **2. Purpose / Motivation**

The Player in Sprint 1 exists to:

- Test the **InputManager**
    
- Validate **EngineTime.DeltaTime**
    
- Confirm the fundamental game loop works
    
- Provide a starting point before building the Entity system in Sprint 2
    

This implementation is intentionally simple — MVP level.

---

## **3. Current Design Notes**

- **No AssetLoader is used**, because it did not exist yet.  
    Textures are loaded like this:
```csharp
texture = Content.Load<Texture2D>("player");
```

SpriteBatch is used directly in the Player’s `Draw()`:

```csharp
spriteBatch.Draw(Texture, Position, Color.White);
```

Movement logic:

```csharp
if (InputManager.IsKeyDown(Keys.W))
    Position.Y -= Speed * EngineTime.DeltaTime;
```

- Player is fully self-contained and independent of any future engine systems.
    

---

## **4. Integration Notes**

Inside `Game1.LoadContent()`:

```csharp
_playerTexture = Content.Load<Texture2D>("player");
_player = new Player(_playerTexture, new Vector2(100, 100));
```

Inside `Game1.Update()`:

```csharp
_player.Update();
```

Inside `Game1.Draw()`:

```csharp
spriteBatch.Begin();
_player.Draw(spriteBatch);
spriteBatch.End();
```

No asset caching, no entity lists, no scene architecture at this stage.

---

## **5. Decisions Made**

- Kept Player simple to validate input and timing.
    
- Used **direct Content loading** (since AssetLoader wasn’t created yet).
    
- Used basic SpriteBatch draw calls without transformations.
    
- Added speed and movement to verify DeltaTime correctness.
    

These decisions allow rapid prototyping in Sprint 1.

---

## **6. Known Limitations / To-Do**

- ❗ Player does **not** inherit from `Entity` (not implemented yet).
    
- ❗ No boundary checks — Player can move outside the screen.
    
- ❗ No animation or sprite flipping.
    
- ❗ Logic + rendering + input coupling (will be separated later).
    
- ❗ Must be integrated into EntityManager in Sprint 2.
    
- ❗ Must transition to using AssetLoader once available.