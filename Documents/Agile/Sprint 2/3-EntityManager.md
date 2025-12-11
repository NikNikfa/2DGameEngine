
## 1. Purpose of the Task

The purpose of this task was to introduce a centralized system responsible for managing all active game entities.  
By creating `EntityManager`, the engine gains:

- A unified way to store and track entities
    
- Consistent Update and Draw processing
    
- A scalable architecture that supports multiple entity types
    
- Cleaner separation of concerns in `Game1`
    

This is a critical foundational piece for any game engine, enabling the transition from a “single Player object” to a fully entity-based design.

---

## 2. Problem Before Implementation

Before this task:

- `Game1` manually updated and drew the `Player` object.
    
- Adding new entities (Enemy, Coin, Projectile) would require manual calls inside `Game1`.
    
- No scalable system existed for handling multiple entities.
    
- Removing or deactivating entities would be difficult to manage.
    
- The update and rendering pipelines were not centralized.
    

This approach does not scale and violates SOLID principles.

---

## 3. Implemented Solution: EntityManager Class

A new class, `EntityManager`, was created to:

- Maintain a list of all active entities
    
- Update all entities each frame
    
- Draw all entities each frame
    
- Provide Add and Remove operations
    
- Keep `Game1` clean and focused on high-level coordination
    

This structure also prepares the engine for systems such as:

- Scene management
    
- Collision systems
    
- AI systems
    
- Physics
    
- Components and ECS patterns
    

---

## 4. Final Implementation – EntityManager.cs

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyEngine.Entities;

namespace MyEngine.Core
{
    public class EntityManager
    {
        private readonly List<Entity> _entities = new List<Entity>();

        public void Add(Entity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _entities.Add(entity);
        }

        public void Remove(Entity entity)
        {
            if (entity == null)
                return;

            _entities.Remove(entity);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var entity in _entities.ToList())
            {
                if (!entity.IsActive)
                    continue;

                entity.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in _entities)
            {
                if (!entity.IsActive)
                    continue;

                entity.Draw(spriteBatch);
            }
        }
    }
}
```

## 5. Integration into Game1

`Game1` now delegates all entity operations to the manager.

### Initialize

```csharp
_entityManager = new EntityManager();
```

### LoadContent

```csharp
var playerTexture = AssetLoader.LoadTexture("Player");
_player = new Player(playerTexture, new Vector2(200, 200));
_entityManager.Add(_player);
```

### Update

```csharp
EngineTime.Update(gameTime);
InputManager.Update();

if (InputManager.IsKeyPressed(Keys.Escape))
    Exit();

_entityManager.Update(gameTime);
```

### Draw

```csharp
_spriteBatch.Begin();
_entityManager.Draw(_spriteBatch);
_spriteBatch.End();
```

The update/draw loop in `Game1` no longer needs to know how many entities exist or which ones they are.

---

## 6. Architectural Impact

### 6.1. Single Responsibility Principle (SRP)

- `EntityManager` only manages entities.
    
- `Game1` no longer handles per-entity logic.
    
- Update and Draw responsibilities are centralized.
    

### 6.2. Open/Closed Principle (OCP)

To add new types of entities (Enemy, Item, Projectile), no existing code needs modification.  
They simply inherit from `Entity` and are added via:

```csharp
_entityManager.Add(new Enemy(...));
```

### 6.3. Liskov Substitution Principle (LSP)

`EntityManager` operates on the `Entity` type.  
Any subclass (Player, Enemy, Coin) is handled consistently.

### 6.4. Engine Scalability

This design scales naturally for:

- Hundreds of entities
    
- Multiple scenes
    
- Object pooling
    
- Advanced systems (AI, physics, ECS)
    

The manager can later support:

- Sorted rendering
    
- Spatial partitioning
    
- Entity lifecycle events
    
- Deferred addition/removal
    

---

## 7. Implementation Notes and Decisions

- `ToList()` is used during Update to avoid iterator modification issues if an entity removes itself during Update.
    
- `IsActive` allows temporary deactivation without removal.
    
- No update order or rendering order system is implemented yet. These can be added later if needed.
    

---

## 8. Future Extensions

Possible improvements:

- Layer-based rendering
    
- Tag or group systems
    
- Entity filtering (e.g., all enemies, all collidable entities)
    
- Deferred removal queues
    
- Sorting entities by Z-index
    
- Scene-specific EntityManagers
    
- Support for coroutines or scheduled events
    

These can be introduced without modifying existing systems.

---

## 9. Summary

Task 3 successfully introduced the `EntityManager`, a cornerstone of the engine's architecture.  
The engine now has a unified and scalable mechanism for managing all game entities, reducing complexity in `Game1` and supporting future growth toward a full game engine.