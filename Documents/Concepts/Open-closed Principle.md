
## 1. What is the Open/Closed Principle?

> **A class should be open for extension but closed for modification.**

This means:

- You **extend** a class by adding new behavior
    
- You do **not modify** the existing class every time a new feature is needed
    

A class following OCP remains stable, predictable, and safe from bugs introduced by changes.

---

## 2. Why OCP matters in our 2D Game Engine

Game engines grow fast. If every time we add:

- A new entity type
    
- A new rendering behavior
    
- A new input type
    
- A new Scene
    
- A new animation rule
    

…we modify the engine core (`Entity`, `EntityManager`, `Game1`, etc.), the code becomes fragile and unmaintainable.

OCP ensures that as the engine grows:

- Existing core classes stay **unchanged**
    
- New features are added via **extensions**, not edits
    
- Subclasses and composition become the main way to evolve the engine
    

This reduces bugs and keeps engine design stable across Sprints.

---

## 3. OCP in the Current Engine Architecture

### 3.1 `Entity` is _open_ for extension

You can create:

- `Player : Entity`
    
- `Enemy : Entity`
    
- `Collectible : Entity`
    
- `Projectile : Entity`
    

Each adds new logic by overriding `Update` or adding new behaviors.

But `Entity` itself rarely changes → it is **closed for modification**.

---

### 3.2 `EntityManager` is OCP-friendly

`EntityManager` updates and draws **all `Entity` subclasses** without knowing about specific types.

We can create dozens of new entity types:

- without editing EntityManager
    
- without touching Game1
    

This is exactly what OCP wants.

---

### 3.3 `InputManager` is closed, but extendable via usage

You can extend input behavior **in other classes**, e.g.:

```csharp
public override void Update(GameTime gameTime)
{
    if (InputManager.IsKeyDown(Keys.Space))
        Jump();
}
```

But `InputManager` itself doesn’t need modification when you add a new game mechanic.

---

### 3.4 `AssetLoader` is closed for modification

To add new assets, we do **not** modify `AssetLoader`.  
We simply call:

```csharp
var texture = AssetLoader.LoadTexture("enemy");
```

OCP is automatically enforced.

---

## 4. OCP Examples in Our Engine

---

### 4.1 ❌ Bad Example — Violating OCP

```csharp
public class EntityManager
{
    public void UpdateAll(GameTime gameTime)
    {
        foreach (var entity in _entities)
        {
            entity.Update(gameTime);

            // BAD:
            // Adding special behavior for specific types
            if (entity is Player)
                ((Player)entity).HandleSpecialInput();

            if (entity is Enemy)
                ((Enemy)entity).AIThink();
        }
    }
}
```

Why this is bad:

- Every time a new entity type is added, EntityManager must be modified
    
- It couples EntityManager to every subclass
    
- Violates OCP: `EntityManager` is not closed for modification
    

---

### 4.2 ✔ Good Example — Following OCP

```csharp
public class EntityManager
{
    public void UpdateAll(GameTime gameTime)
    {
        foreach (var entity in _entities)
        {
            if (entity.IsActive)
                entity.Update(gameTime);
        }
    }
}
```

All unique behavior is placed **in the subclass**, not in the manager.

Adding a new type:

```csharp
public class Enemy : Entity
{
    public override void Update(GameTime gameTime)
    {
        DoAI();
        base.Update(gameTime);
    }
}
```

→ EntityManager does not change  
→ Game1 does not change  
→ Engine core remains stable

This is perfect OCP.

## 5. How OCP Shapes Future Engine Architecture

### 5.1 New Scene Types

Instead of editing Game1 to support new scenes, we add:

```csharp
public class MenuScene : Scene { ... }
public class GameScene : Scene { ... }
public class PauseScene : Scene { ... }
```

SceneManager swaps scenes **without modifying engine internals**.

---

### 5.2 New Rendering Behaviors

Adding shaders or particle effects shouldn’t require editing `Entity`.  
Instead we create:

```csharp
public class ParticleEmitter : Entity { ... }
```

### 5.3 New Input Devices

If we add gamepad support:

- We extend `InputManager`
    
- We create `GamepadInputSource`
    
- We do not break keyboard logic
    

OCP ensures backward compatibility.

---

## 6. Practical Checklist for OCP in This Project

When adding a new feature, ask:

1. **Do I need to modify an existing class?**
    
    - If yes, can this logic be moved to a subclass or external component?
        
2. **Can I achieve the feature by extending behavior?**
    
    - Inheriting from a base class
        
    - Composing new components
        
    - Overriding virtual methods
        
3. **Am I adding “if type is X” logic somewhere?**
    
    - If yes → OCP violation
        
4. **Is the base class offering a clean API for extension?**
    
    - If not, add extension points (virtual methods, events, interfaces)
        
5. **Will this change force other systems to change later?**
    
    - If yes → redesign for OCP
        

---

## 7. Summary (Quick Reference)

- **Open for extension:** we can build new features on top
    
- **Closed for modification:** core classes aren’t constantly changed
    
- Prevents engine-wide rewrites when adding new entity types
    
- Makes the engine predictable and stable as it grows
    
- Helps define clean, maintainable subsystems
    

OCP ensures the **core engine** remains stable while the **game logic** grows freely and safely.