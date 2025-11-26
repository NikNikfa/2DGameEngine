## 1. What is the Single Responsibility Principle?

> **A class should have one and only one reason to change.**

This means:

- Each class does **one thing**, and does it well.
    
- A class should not mix multiple responsibilities.
    
- If a class has more than one reason to change in the future → it is violating SRP.
    

SRP helps keep the engine modular, easier to maintain, and easier to extend.

---

## 2. Why SRP matters in our 2D Game Engine

Game engines can quickly become unmanageable when one class tries to "do everything":

- `Game1` handling input + logic + asset loading → wrong
    
- `Player` loading textures + controlling movement + rendering → wrong
    
- `EntityManager` updating physics + checking collisions → wrong
    

SRP ensures:

- Each subsystem is small
    
- Debugging is easier
    
- Future sprints can add new features without breaking existing ones
    

Following SRP is one of the main reasons we already created classes like:

- `EngineTime`
    
- `InputManager`
    
- `AssetLoader`
    
- `Entity`
    
- `EntityManager`
    

Each one handles **one core job**.

---

## 3. SRP in the Current Engine Architecture

### 3.1 `EngineTime`

**Responsibility:** Track DeltaTime and TotalTime  
**Reason to change:** If timing logic changes

_It does not: handle input, rendering, physics._

---

### 3.2 `InputManager`

**Responsibility:** Collect input states each frame  
**Reason to change:** If we change input handling or add new devices

_It does not: move the player, update game logic, or draw anything._

---

### 3.3 `AssetLoader`

**Responsibility:** Load and cache assets  
**Reason to change:** Asset pipelines or loading rules

_It does not: update entities, draw textures, or manage scenes._

---

### 3.4 `Entity`

**Responsibility:** Represent a drawable/updatable game object  
**Reason to change:** Entity behavior or rendering rules

_It does not: manage a list of entities or handle collisions._

---

### 3.5 `EntityManager`

**Responsibility:** Store, update, remove, and draw all entities  
**Reason to change:** Entity management rules

_It does not: load assets, interpret input, or implement game logic._

---

## 4. Examples of Good vs Bad SRP (Engine Context)

### 4.1 ❌ Bad Example — SRP Violation

```csharp
public class Player
{
    private Texture2D _texture;

    public void LoadTexture(ContentManager content)
    {
        _texture = content.Load<Texture2D>("player");  // asset loading
    }

    public void HandleInput()
    {
        // input logic
    }

    public void Update()
    {
        HandleInput(); // gameplay + input + asset loading
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, ...);
    }
}
```

**What’s wrong here?**

- Player loads assets
    
- Player handles input
    
- Player updates game logic
    
- Player draws itself
    

→ This class has **too many reasons to change**.

---

### 4.2 ✔ Good Example — SRP Applied

```csharp
public class Player : Entity
{
    public override void Update(GameTime gameTime)
    {
        HandleMovement();       // Gameplay logic only
    }

    private void HandleMovement()
    {
        if (InputManager.IsKeyDown(Keys.W))
            Position.Y -= 1;
    }
}
```

`Player` now depends on other systems instead of performing their work:

- Texture is loaded by **AssetLoader**
    
- Input is read by **InputManager**
    
- Entity is drawn by **Entity.Draw()**
    
- Player only **updates its own behavior**
    

This is correct SRP usage.

---

## 5. How SRP Shapes the Engine Architecture

Thanks to SRP, our engine follows a **clean separation of responsibilities**:

### Core Classes

|Class|Responsibility|
|---|---|
|`EngineTime`|Timing system|
|`InputManager`|Input system|
|`AssetLoader`|Asset loading & caching|
|`Entity`|Base object representation|
|`EntityManager`|Object management|

### Game Classes

|Class|Responsibility|
|---|---|
|`Player`|Player-specific behavior|
|`Enemy` (future)|Enemy-specific behavior|
|`Collectible` (future)|Collectible logic|

### Game1

|Responsibility|
|---|
|Orchestrate systems (call Update/Draw)|
|Create initial game state|
|Manage high-level game flow|

Thanks to SRP:

- Game1 stays clean
    
- Entities stay simple
    
- Subsystems remain reusable
    
- The engine becomes scalable
    

---

## 6. Checklist for SRP in the Project

When creating or modifying a class, ask:

1. **What is the single responsibility of this class?**
    
2. **Does this class have more than one reason to change?**
    
3. **Can I split this class into two smaller ones?**
    
4. **Is this class doing work that belongs in another subsystem?**
    
5. **Can I describe the class in one short sentence?**
    
    - If not → it’s doing too much.
        

If your answer catches multiple responsibilities → refactor.

---

## 7. Summary (Quick Reference)

- SRP = **one class, one responsibility**
    
- Each class should have **one reason to change**
    
- Helps create a modular, maintainable engine
    
- Avoids “god classes” like a 3000-line Player or Game1
    

**In our engine, SRP is already guiding the architecture**, and future systems will continue to follow this principle.