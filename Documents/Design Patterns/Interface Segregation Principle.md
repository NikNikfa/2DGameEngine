
## 1. Definition

> **Clients should not be forced to depend on interfaces they do not use.**

In other words:

- Interfaces should be **small and specific**, not large and general.
    
- A class should not be required to implement methods that are irrelevant to its role.
    
- Instead of one “god interface”, we provide multiple focused interfaces.
    

Applied correctly, ISP keeps the design clean, modular, and easier to maintain.

---

## 2. Why ISP Matters in Our 2D Game Engine

Without ISP, it is easy to design interfaces like:

```csharp
public interface IGameObject
{
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
    void OnCollision(Entity other);
    void OnInput(InputManager input);
}
```

This forces every implementing class to:

- Deal with Update, Draw, Collision, and Input
    
- Even if the object does not need some of those responsibilities
    

For example:

- A UI element might not need collision.
    
- A non-visual entity might not need Draw.
    
- A pure logic controller might not need collision or drawing.
    

ISP encourages us to create **separate, focused interfaces**, such as:

- `IUpdatable`
    
- `IRenderable`
    
- `ICollidable`
    
- `IInputHandler`
    

This directly supports a modular and scalable engine.

---

## 3. Bad Example (Violating ISP)

Example of a “fat” interface in the engine context:

```csharp
public interface IGameEntity
{
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
    void OnCollision(Entity other);
    void HandleInput(InputManager input);
}
```

Problems:

- A background decoration object must implement `OnCollision` and `HandleInput` even if it never collides or uses input.
    
- A logic-only system object must implement `Draw` even though it does not render anything.
    
- Changes in one concern (e.g. input) may force recompilation of all implementers.
    

This leads to:

- Unnecessary methods with empty bodies
    
- Tight coupling between unrelated concerns
    
- A design that is harder to evolve or refactor
    

---

## 4. Good Example (Applying ISP)

Instead, we define small, focused interfaces:

```csharp
public interface IUpdatable
{
    void Update(GameTime gameTime);
}

public interface IRenderable
{
    void Draw(SpriteBatch spriteBatch);
}

public interface ICollidable
{
    void OnCollision(Entity other);
}

public interface IInputHandler
{
    void HandleInput();
}
```

Now each class implements only what it needs:

```csharp
public class Player : Entity, IUpdatable, IRenderable, ICollidable, IInputHandler
{
    public void Update(GameTime gameTime) { /* movement, logic */ }
    public void Draw(SpriteBatch spriteBatch) { /* rendering */ }
    public void OnCollision(Entity other) { /* collision handling */ }
    public void HandleInput() { /* read InputManager, react to controls */ }
}

public class StaticDecoration : Entity, IRenderable
{
    public void Draw(SpriteBatch spriteBatch) { /* draw tree, rock, etc. */ }
    // No Update, no collision, no input
}

public class LogicController : IUpdatable
{
    public void Update(GameTime gameTime) { /* global logic, timers, etc. */ }
    // No Draw, no collision, no input
}
```

Engine systems can then work with just the interfaces they care about:

```csharp
public class UpdateSystem
{
    private readonly List<IUpdatable> _updatables = new();

    public void UpdateAll(GameTime gameTime)
    {
        foreach (var u in _updatables)
            u.Update(gameTime);
    }
}

public class RenderSystem
{
    private readonly List<IRenderable> _renderables = new();

    public void DrawAll(SpriteBatch spriteBatch)
    {
        foreach (var r in _renderables)
            r.Draw(spriteBatch);
    }
}
```

This respects ISP and improves flexibility.

---

## 5. ISP and Engine Architecture

### 5.1 Separation of Responsibilities via Interfaces

Possible interfaces in the engine:

- `IUpdatable` — objects updated each frame
    
- `IRenderable` — objects that draw themselves
    
- `ICollidable` — objects participating in collision detection
    
- `IHasDebugInfo` — objects that can expose debug data
    
- `IInputHandler` — objects that react to input
    

Not every entity needs all of these.  
ISP lets us wire entities only to what they require.

### 5.2 Interaction with EntityManager

Instead of `EntityManager` handling one monolithic type, it can collaborate with multiple systems:

- `EntityManager` might maintain a general list of `Entity` objects.
    
- `UpdateSystem` might keep a list of `IUpdatable` references.
    
- `RenderSystem` might work purely with `IRenderable`.
    

This prevents systems from depending on capabilities they do not need.

---

## 6. Practical Guidelines for ISP in This Project

When designing interfaces for the engine:

1. **Prefer small, focused interfaces.**
    
    - One concept per interface (update, render, collide, etc.).
        
2. **Avoid “god interfaces”.**
    
    - If an interface starts growing large (many methods or responsibilities), consider splitting it.
        
3. **Design for the consumer.**
    
    - Think: “What does this system actually need?”
        
    - An update loop only needs `Update(GameTime)`, not Draw or Input.
        
4. **Use composition of interfaces.**
    
    - A complex object (like Player) can implement multiple small interfaces.
        
    - This is more flexible than one large interface.
        
5. **Keep interfaces stable but minimal.**
    
    - Changing an interface affects all implementers.
        
    - Smaller interfaces reduce the impact of change.
        

---

## 7. Checklist for Applying ISP

When you add or modify an interface, ask:

1. **Does this interface represent a single, clear responsibility?**
    
2. **Are there methods that some implementers would not reasonably use?**
    
3. **Do any classes implement this interface but leave methods empty or throw `NotImplementedException`?**
    
    - If yes, it is a sign that the interface is too broad.
        
4. **Can the interface be split into two or more smaller, more cohesive interfaces?**
    
5. **Does each system (Update, Render, Collision, Input, etc.) depend only on the methods it truly needs?**
    

If you find violations, refactor toward smaller, focused interfaces.

---

## 8. Summary

- The **Interface Segregation Principle (ISP)** states that clients should not be forced to depend on methods they do not use.
    
- In a 2D game engine, this means:
    
    - Splitting large generic interfaces into small, capability-specific ones.
        
    - Ensuring each system (update, rendering, collision, input) only depends on what it needs.
        
- ISP improves:
    
    - Modularity
        
    - Testability
        
    - Flexibility
        
    - Long-term maintainability
        

Used together with SRP, OCP, and LSP, ISP helps keep the engine architecture clean and robust as it grows.
