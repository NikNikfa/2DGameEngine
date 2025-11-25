
## 📘 Overview

The `EngineTime` class is one of the core classes of the game engine, responsible for managing time during the game's execution.  
This class keeps track of `DeltaTime` and `TotalTime` values and provides them to other engine systems (such as Physics, Animation, and Game Logic).

---

## ⚙️ Responsibilities

- **DeltaTime** property: the duration between two consecutive frames (in seconds)  
- **TotalTime** property: the total elapsed time from the start of the game to the current frame  
- Updating time values in each `Update()` cycle  
- Providing centralized access to time information for all systems

---

## 🧩 Structure

Using this class:

- The `EngineTime` class is **not dependent on MonoGame** (dependency has been decoupled)
    
- If we ever want to separate the engine from MonoGame or use it in another environment, we just need to create and pass `gameTime` to the `Update` method of this class
    
- Meanwhile, any other system in the project (like Input, Physics, Renderer...) can access the engine's global time via `EngineTime.DeltaTime`

## Simplified relationship between the [[Game1]] class and EngineTime:

```csharp
// Game1.cs
protected override void Update(GameTime gameTime)
{
    EngineTime.Update(gameTime);  // <-- this line establishes the connection
    base.Update(gameTime);
}
```

```csharp
// EngineTime.cs
public static void Update(GameTime gameTime)
{
    DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
    TotalTime += DeltaTime;
}
```

Here, it's just a **parameter passing**.  
`EngineTime` does **not need to know** where `GameTime` comes from;  
it only needs to receive the new data whenever `Game1.Update()` is called.

## 🔁 How It Works

1. In each cycle of the game loop (`Game.Update()`):
    
    - The current time (`TotalGameTime`) is retrieved from the system.
        
2. The difference from the previous frame is calculated and stored in `DeltaTime`.
    
3. The total elapsed time since the start of the game is updated in `TotalTime`.
    
4. Other systems (like Physics or Animation) use `EngineTime.DeltaTime` to control speed and movement.
    

### We can visualize it like this:

```mathematica
MonoGame Engine
   ↓
Game1.Update(gameTime)
   ↓
EngineTime.Update(gameTime)
   ↓
EngineTime.DeltaTime / .TotalTime
```

## 🧠 Design Notes

- Implementation is completely independent of the game logic.
    
- Instead of directly checking input in `Game.Update()`, this class is used to keep the code cleaner and more maintainable.
    
- The **[[Dependency inversion Principle]]** principle is respected: other systems only use the public methods of `InputManager`.