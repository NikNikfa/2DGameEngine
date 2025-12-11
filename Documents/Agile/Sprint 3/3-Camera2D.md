
## 1. Purpose of the Task

The purpose of this task was to introduce a simple but robust **2D camera system** that:

- Follows the player
    
- Supports zoom and (optionally) rotation
    
- Integrates cleanly into the existing rendering pipeline (`Renderer`)
    
- Uses a proper view matrix to transform world coordinates into screen coordinates
    

This moves the engine from a static view to a fully scrollable camera-based world.

---

## 2. Problem Before Implementation

Before Task 3:

- All entities were drawn directly in screen space (no camera).
    
- The Player moved, but the world did not scroll.
    
- There was no way to zoom or rotate the view.
    
- The rendering pipeline was not aware of any camera transformation.
    

As a result, the engine could not support:

- Large scrolling worlds
    
- Player-centered views
    
- Zooming in/out
    
- Camera effects
    

A dedicated camera abstraction was required.

---

## 3. Implemented Solution Overview

The solution consists of three main parts:

1. A `Camera2D` class encapsulating camera properties and generating a view matrix.
    
2. An update to `Renderer` to accept an optional transform matrix in `Begin`.
    
3. Changes in `Game1` to own a `Camera2D`, follow the player, and pass the camera matrix to the renderer.
    

Together, these steps implement a minimal but correct 2D camera.

---

## 4. Camera2D Implementation (Core/Camera2D.cs)

```csharp
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyEngine.Core
{
    public class Camera2D
    {
        public Vector2 Position { get; set; } = Vector2.Zero;

        private float _zoom = 1f;
        public float Zoom
        {
            get => _zoom;
            set
            {
                // Prevent zero or negative zoom, which would break rendering
                _zoom = MathHelper.Max(0.1f, value);
            }
        }

        public float Rotation { get; set; } = 0f;

        public Matrix GetViewMatrix(Viewport viewport)
        {
            var origin = new Vector2(viewport.Width * 0.5f, viewport.Height * 0.5f);

            return
                Matrix.CreateTranslation(new Vector3(-Position, 0f)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(Zoom, Zoom, 1f) *
                Matrix.CreateTranslation(new Vector3(origin, 0f));
        }
    }
}
```

### Key Design Points

- `Position` represents the camera position in world space.
    
- `Zoom` controls how close/far the view is; clamped to `>= 0.1f` to avoid invalid transforms.
    
- `Rotation` allows future camera rotation effects (default 0).
    
- `GetViewMatrix(Viewport)` builds the view matrix using:
    
    - Translation by `-Position` (move world opposite to camera).
        
    - Rotation around Z.
        
    - Scaling by `Zoom`.
        
    - Final translation to center the view in the middle of the screen (viewport center).
        

The result is a standard 2D view matrix used in SpriteBatch.

---

## 5. Renderer Updated to Support Transform Matrix (Core/Renderer.cs)

`Renderer.Begin` was extended to accept an optional camera transform:

```csharp
public static void Begin(Matrix? transform = null)
{
    if (transform.HasValue)
        SpriteBatch.Begin(transformMatrix: transform.Value);
    else
        SpriteBatch.Begin();
}
```

This allows two modes:

1. `Renderer.Begin(viewMatrix)` – used for world rendering with camera.
    
2. `Renderer.Begin()` – used for UI rendering without camera (in future tasks).
    

The rest of `Renderer` remains the same:

- `Initialize(GraphicsDevice graphicsDevice)`
    
- `End()`
    
- `Clear(Color color)`
    

---

## 6. Game1 Integration

### 6.1 Camera Field and Initialization

In `Game1.cs`:

```csharp
private Camera2D _camera;

protected override void Initialize()
{
    _entityManager = new EntityManager();
    _camera = new Camera2D();

    base.Initialize();
}
```

The game now owns a single camera instance.

---

### 6.2 Camera Following the Player

After entities are updated, the camera is set to follow the player:

```csharp
protected override void Update(GameTime gameTime)
{
    EngineTime.Update(gameTime);
    InputManager.Update();

    if (InputManager.IsKeyPressed(Keys.Escape))
        Exit();

    _entityManager.Update(gameTime);

    // Simple follow: center camera on the player
    _camera.Position = _player.Position;

    base.Update(gameTime);
}
```

This is a direct, “hard-follow” camera: the camera’s position equals the player’s position.  
Smoothing can be added later if desired.

---

### 6.3 Applying the Camera in Draw

```csharp
protected override void Draw(GameTime gameTime)
{
    Renderer.Clear(Color.CornflowerBlue);

    var viewMatrix = _camera.GetViewMatrix(GraphicsDevice.Viewport);

    Renderer.Begin(viewMatrix);
    _entityManager.Draw(Renderer.SpriteBatch);
    Renderer.End();

    base.Draw(gameTime);
}
```

Key details:

- `viewMatrix` is obtained from the camera using the current viewport.
    
- `Renderer.Begin(viewMatrix)` ensures all entities are rendered through the camera transform.
    
- `EntityManager.Draw` remains unchanged, but its draw calls are now affected by the camera.
    

From this point on, all entity positions are interpreted as **world coordinates**, not screen coordinates.

---

## 7. Behavioral Result

With this implementation:

- The player still moves via `Player.Update`.
    
- The camera position is set equal to the player’s position.
    
- The `Camera2D` view matrix translates the entire world in the opposite direction.
    
- The player appears centered on screen (once the world is larger than the viewport).
    
- The environment scrolls as the player moves.
    
- The zoom and rotation properties can be used to zoom in/out and rotate the entire scene.
    

The camera does not modify world data; it only transforms how the world is drawn.

---

## 8. Architectural Impact

### 8.1 Separation of Concerns

- `Camera2D` is responsible for view matrices and camera-related properties.
    
- `Renderer` is responsible for SpriteBatch configuration and draw passes.
    
- `Game1` is responsible for wiring the camera and renderer together.
    
- Entities and `EntityManager` remain unaware of the camera, which is good for decoupling.
    

### 8.2 Extensibility

The system now supports:

- Future camera smoothing (lerp, damping).
    
- Camera bounds (clamping camera position to level size).
    
- Multiple cameras (e.g., split-screen) if needed later.
    
- Independent UI rendering (without camera) via `Renderer.Begin()`.
    

---

## 9. Limitations and Future Work

Current camera is intentionally minimal:

- No smoothing or lag; camera snaps directly to the player.
    
- No clamping to world bounds.
    
- UI and HUD still share the same draw call (camera-based) for now.
    

These enhancements are potential tasks for later in Sprint 3 or Sprint 4:

- Camera smoothing (interpolating toward player position).
    
- Camera bounds to avoid showing outside the level.
    
- Separate draw passes for UI (without the view matrix).
    
- Camera shake effects, scripted camera paths, etc.
    

---

## 10. Summary

Sprint 3 – Task 3 successfully introduced a functional 2D camera system:

- `Camera2D` encapsulates position, zoom, and rotation.
    
- `Renderer` can now apply a view matrix to all drawing operations.
    
- `Game1` uses the camera to follow the player and render the scene accordingly.
    

The engine now supports a scrolling world and proper camera-based rendering, which is foundational for any non-trivial game.