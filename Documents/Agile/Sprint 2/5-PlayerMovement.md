
## 1. Purpose of the Task

The objective of Task 5 was to implement real-time, frame-rate-independent movement for the Player entity.  
This task builds directly on the integration completed in Task 4, which introduced:

- EngineTime (DeltaTime and TotalTime)
    
- InputManager (keyboard input state)
    

The goal here is to give the Player:

- Smooth movement
    
- Proper responsiveness to WASD and Arrow keys
    
- Consistency regardless of frame rate
    

Movement logic must be contained **inside the Player class** to maintain clean separation of responsibilities.

---

## 2. Problem Before Implementation

Before Task 5:

- Player.Update() read keyboard state but movement was not implemented.
    
- Player did not respond to input.
    
- There was no use of EngineTime.DeltaTime to maintain consistent movement speed.
    
- Directional input logic existed but did not result in position changes.
    

This meant the Player class was incomplete from a gameplay perspective.

---

## 3. Implemented Solution: Movement Logic in Player.Update()

Movement was implemented directly inside the `Player` class.  
The solution includes:

- A direction vector to accumulate movement input
    
- Normalization to avoid faster diagonal speed
    
- Application of movement speed multiplied by EngineTime.DeltaTime
    

This ensures consistent, predictable motion.

---

## 4. Final Implementation – Player.cs

```csharp
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyEngine.Core;

namespace MyEngine.Entities
{
    public class Player : Entity
    {
        private float _speed = 200f; // units per second

        public Player(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 direction = Vector2.Zero;

            // Horizontal input
            if (InputManager.IsKeyDown(Keys.A) || InputManager.IsKeyDown(Keys.Left))
                direction.X -= 1f;

            if (InputManager.IsKeyDown(Keys.D) || InputManager.IsKeyDown(Keys.Right))
                direction.X += 1f;

            // Vertical input
            if (InputManager.IsKeyDown(Keys.W) || InputManager.IsKeyDown(Keys.Up))
                direction.Y -= 1f;

            if (InputManager.IsKeyDown(Keys.S) || InputManager.IsKeyDown(Keys.Down))
                direction.Y += 1f;

            // Normalize diagonal movement and apply movement
            if (direction != Vector2.Zero)
            {
                direction.Normalize();
                Position += direction * _speed * EngineTime.DeltaTime;
            }
        }
    }
}
```

Notes:

- `_speed` is defined in units per second and can be tuned later.
    
- Normalization prevents diagonal speed from being mathematically larger than straight movement.
    
- Movement is applied using `EngineTime.DeltaTime` to ensure frame-rate independence.
    

---

## 5. Architectural Impact

### Single Responsibility Principle (SRP)

Player is solely responsible for handling its own update logic, including movement.  
Game1 no longer knows anything about how Player moves.

### Open-Closed Principle (OCP)

Movement speed, input configurations, or control schemes can be modified without touching other systems.

### Dependency Inversion Principle (DIP)

Player depends on high-level abstractions (EngineTime, InputManager), not the MonoGame API directly.

### Alignment with Engine Loop (Task 4)

Player movement is executed after:

1. EngineTime.Update()
    
2. InputManager.Update()
    

ensuring correct timing and correct input state each frame.

---

## 6. Excluded Features (Intentional Omissions)

The following items were intentionally excluded in this task:

- World boundary clamping
    
- Collision interaction with world tiles
    
- Camera following
    
- Sprite orientation or animation based on movement
    

These features will be assigned to later tasks to maintain a focused and incremental development approach.

---

## 7. Summary

Task 5 successfully introduced player movement using an integrated engine approach.  
Player movement is now smooth, frame-rate independent, and responsive to both WASD and Arrow keys.  
The Player class is now a functional entity that participates properly in the engine’s update pipeline.

This completes the initial implementation of Player behavior for Sprint 2.