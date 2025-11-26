
## 1. Purpose of the Task

The goal of this task was to introduce a foundational `Entity` class that represents all interactive objects in the game world (Player, Enemy, Collectible, Projectile, etc.).  
This establishes a consistent structure for any game object by providing shared properties and behaviors.

Key objectives:

- Remove duplication between different game objects.
    
- Introduce a unified abstraction for all entities.
    
- Prepare the engine for scalable entity management.
    
- Align the engine with SOLID design principles (especially SRP, OCP, LSP).
    

---

## 2. Problem Before Implementation

Before this task:

- Only the `Player` class existed.
    
- Any new game object (e.g., Enemy, Coin) would require fully separate code for Position, Texture, Update, and Draw.
    
- There was no shared abstraction for entities, making future systems (collision, physics, animations) harder to integrate.
    
- `EntityManager` could not manage all entities uniformly.
    

This structure did not scale well.

---

## 3. Implemented Solution: Abstract Entity Base Class

A new abstract class `Entity` was created to define:

- `Position` (location in the world)
    
- `Texture` (renderable sprite)
    
- `Update()` method (per-frame logic)
    
- `Draw()` method (rendering logic)
    
- `IsActive` flag for enabling/disabling entities
    

Any game object now derives from this class and automatically inherits essential functionality.

---

## 4. Final Implementation – Entity.cs

```csharp
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyEngine.Entities
{
    public abstract class Entity
    {
        public Vector2 Position { get; set; }

        protected Texture2D Texture { get; private set; }

        public bool IsActive { get; set; } = true;

        protected Entity(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!IsActive)
                return;

            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
```

## 5. Player Class Updated to Inherit from Entity

The `Player` class now extends `Entity`, inheriting all shared functionality while overriding behavior when needed.

### Updated Player.cs

```csharp
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyEngine.Entities
{
    public class Player : Entity
    {
        public Player(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
        }

        public override void Update(GameTime gameTime)
        {
            // Player-specific state updates, movement logic, etc.
        }
    }
}
```

Effects of inheritance:

- No duplicated Draw or property code.
    
- Player gains the shared structure of game objects.
    
- Player-specific features can be added through overrides.
    

---

## 6. Architectural Impact

### 6.1. Single Responsibility Principle (SRP)

`Entity` handles only common entity structure.  
`Player` handles only Player-specific logic.

### 6.2. Open/Closed Principle (OCP)

New entity types (Enemy, Coin, Projectile) can be added by extending `Entity` without modifying existing code.

### 6.3. Liskov Substitution Principle (LSP)

Any `Entity` subclass (Player, Enemy) can be passed safely to `EntityManager`, which expects `Entity`.

### 6.4. Engine Scalability

Future systems (collision, animation, components, physics) can operate on the `Entity` type without redesigning individual classes.

---

## 7. Interaction with EntityManager

This task enables [[S2-EntityManager]] to manage all entities generically:

```csharp
_entityManager.Add(_player);
```

All Update/Draw logic is now automatic through:

```csharp
_entityManager.Update(gameTime);
_entityManager.Draw(spriteBatch);
```

`EntityManager` no longer needs multiple lists or special handling for different entity types.

---

## 8. Future Extensions

The base class is intentionally minimal but can support:

- Velocity and movement handling
    
- Rotation and scale
    
- Bounding boxes
    
- Events (OnCreate, OnDestroy)
    
- Component or ECS-like systems
    
- Collision detection support
    
- Animation controllers
    

These features can be added later without modifying existing entities.

---

## 9. Summary

Task 2 successfully introduced a reusable `Entity` base class and refactored the `Player` to inherit from it.  
This establishes a unified framework for all game objects, reduces code duplication, and prepares the engine for significant future expansion.