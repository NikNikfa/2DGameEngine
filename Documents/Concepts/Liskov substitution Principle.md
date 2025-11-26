
## 1. What is the Liskov Substitution Principle?

> **Informal definition:**  
> If `B` inherits from `A`, then **everywhere** the code expects an `A`, you should be able to use a `B` **without breaking anything** or changing the expected behaviour.

In other words:

- A **derived class** must behave like its **base class** promises.
    
- Inheritance should model an **“is a”** relationship:
    
    - `Player` **is an** `Entity`
        
    - `Enemy` **is an** `Entity`
        
    - `UIElement` is **probably not** an `Entity` (so maybe shouldn’t inherit from it)
        

---

## 2. Why LSP matters in our 2D Game Engine

In our engine we already have:

- `Entity` (base type)
    
- `Player : Entity`
    
- `EntityManager` that works with `List<Entity>`
    

LSP ensures:

- `EntityManager` doesn’t need to know which specific entity type it’s dealing with.
    
- You can safely add new entity types (e.g. `Enemy`, `Collectible`) and everything that works with `Entity` keeps working.
    
- You avoid strange bugs where a subclass “breaks the engine contract”.
    

If LSP is respected:

- Code like this should always be safe:
```csharp
Entity entity = new Player(texture, position);
entity.Update(gameTime);
entity.Draw(spriteBatch);
```

---

## 3. Formal idea (in simple terms)

If a type `Base` has a public API, and another type `Derived` extends it:

- **Preconditions** (what must be true before a method call) in `Derived`  
    → cannot be **stronger** than in `Base`.
    
- **Postconditions** (what is guaranteed after a method call) in `Derived`  
    → cannot be **weaker** than in `Base`.
    

Example in our context:

- If `Entity.Update()` says:
    
    - “This method can be called **every frame** with any `GameTime`”
        
- Then `Player.Update()` must also accept being called every frame with any `GameTime`.  
    It must not add extra hidden requirements like:
    
    - “`Update` only works if this entity is a `Player` in a certain scene”
        
    - “`Update` throws if `Texture` is null but base class allowed that”
        

---

## 4. Bad vs Good Example (Game Engine Context)

### 4.1 Bad: breaking LSP with `Entity`

```csharp
public class Entity
{
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        // Base contract: Safe to call Draw at any time
        // If Texture is null, just do nothing.
    }
}

public class SpecialEntity : Entity
{
    public override void Draw(SpriteBatch spriteBatch)
    {
        // BAD: Violates LSP
        // Now Draw throws when Texture is null,
        // while base class allowed it.
        if (Texture == null)
            throw new InvalidOperationException("Texture must not be null!");

        // drawing logic...
    }
}

```

Problem:

- Any code that relied on the base guarantee **“Draw is always safe to call”**  
    can now suddenly crash if it happens to get a `SpecialEntity`.
    

---

### 4.2 Good: respecting LSP

```csharp
public class Entity
{
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        if (Texture == null)
            return;

        // default drawing...
    }
}

public class SpecialEntity : Entity
{
    public override void Draw(SpriteBatch spriteBatch)
    {
        // Keep the same basic safety:
        if (Texture == null)
            return; // still safe, just does nothing

        // extra behaviour, but same contract
        // e.g. custom shaders, effects, ...
    }
}
```

- The subclass **extends** behaviour but doesn’t break the base class guarantees.
    
- Anywhere an `Entity` is expected, a `SpecialEntity` can be used safely.
    

---

## 5. Applying LSP to our Engine Design

### 5.1 `Entity` and its subclasses

When designing classes like:

- `Player : Entity`
    
- `Enemy : Entity`
    
- `Collectible : Entity`
    

We should ensure:

- `Update(GameTime)` can be called every frame without special conditions.
    
- `Draw(SpriteBatch)` can be called whenever `EntityManager` or `Game1` decides.
    
- Flags like `IsActive` and `IsVisible` keep the same meaning in all subclasses.
    

**Don’t**:

- Change the meaning of `IsActive` only for one class (e.g. for one type `IsActive` means “dead”, for another means “paused”).
    
- Throw exceptions in `Update` or `Draw` under conditions where base `Entity` would behave gracefully.
    

---

### 5.2 `EntityManager` and LSP

`EntityManager` relies on LSP:

```csharp
foreach (var entity in _entities)
{
    if (entity.IsActive)
        entity.Update(gameTime);

    if (entity.IsVisible)
        entity.Draw(spriteBatch);
}
```

Here `EntityManager` doesn’t care whether `entity` is:

- `Player`
    
- `Enemy`
    
- `Collectible`
    
- `SomeNewType : Entity` created in Sprint 5
    

As long as all subclasses respect the **same contract** as `Entity`, this code continues to work.

---

## 6. Practical checklist for LSP in this project

When you create a new subclass (e.g. `Enemy : Entity`), ask:

1. **“Can I use this class wherever an `Entity` is expected?”**
    
    - In `EntityManager`
        
    - In lists of `Entity`
        
2. **“Did I change the meaning of any base property?”**
    
    - `IsActive`, `IsVisible`, `Position`, `Update`, `Draw`
        
3. **“Did I add any hidden extra requirements?”**
    
    - e.g. “This must only be updated after some other system ran”
        
4. **“Does my override still respect the base safety guarantees?”**
    
    - No new exceptions in normal scenarios
        
    - Same assumptions about `GameTime`, `SpriteBatch`, etc.
        

If the answer to all is “yes / safe”, then your subclass **respects LSP**.

---

## 7. Summary (for quick reference)

- LSP = **Subclasses must be usable anywhere the base class is expected**.
    
- In our engine:
    
    - Any `Entity` subclass must behave correctly when updated and drawn by `EntityManager` / `Game1`.
        
    - Subclasses may **extend** behaviour but must not **break** the base contract.
        
- Benefit:
    
    - We can add new entity types without rewriting engine code.
        
    - The engine stays more stable, testable, and easier to evolve.