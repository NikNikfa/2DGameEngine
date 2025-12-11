
## 1. Purpose of the Task

The goal of Task 1 in Sprint 3 was to:

- Centralize all rendering responsibilities into a dedicated `Renderer` class.
    
- Remove direct `SpriteBatch` management from `Game1`.
    
- Provide a single place where rendering configuration and pipeline behavior can evolve (layers, camera, transforms, etc.).
    

This is an important architectural step towards:

- A proper rendering pipeline
    
- Camera integration
    
- Scene-based rendering
    

---

## 2. Problem Before Implementation

Before Task 1:

- `Game1` was creating and owning the `SpriteBatch`.
    
- The rendering sequence was hard-coded inside `Game1.Draw`:

```csharp
GraphicsDevice.Clear(Color.CornflowerBlue);

_spriteBatch.Begin();
_entityManager.Draw(_spriteBatch);
_spriteBatch.End();
```

Issues:

1. **Game1 was responsible for low-level rendering details**, such as:
    
    - `SpriteBatch` creation
        
    - `Begin` / `End` calls
        
    - Direct use of `GraphicsDevice.Clear`
        
2. **No centralized rendering service existed**, making it harder to:
    
    - Add cameras
        
    - Introduce render layers
        
    - Apply global effects or transformations
        
3. The architecture did not clearly separate:
    
    - Engine-level rendering pipeline
        
    - Game-level logic
        

---

## 3. Implemented Solution: Static Renderer Service

A new static class `Renderer` was introduced in `Core`.

Responsibilities:

- Own the `GraphicsDevice` reference
    
- Own the shared `SpriteBatch` instance
    
- Provide simple APIs for:
    
    - Clearing the screen
        
    - Beginning a draw pass
        
    - Ending a draw pass
        
    - Exposing the `SpriteBatch` for now
        

This decouples rendering setup from `Game1` and prepares the system for later extension.

---

## 4. Final Implementation – Renderer.cs

```csharp
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyEngine.Core
{
    public static class Renderer
    {
        private static GraphicsDevice _graphicsDevice;
        public static SpriteBatch SpriteBatch { get; private set; }

        public static void Initialize(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice ?? throw new ArgumentNullException(nameof(graphicsDevice));
            SpriteBatch = new SpriteBatch(_graphicsDevice);
        }

        public static void Begin()
        {
            SpriteBatch.Begin();
        }

        public static void End()
        {
            SpriteBatch.End();
        }

        public static void Clear(Color color)
        {
            _graphicsDevice.Clear(color);
        }
    }
}
```

Key points:

- `Initialize(GraphicsDevice)` must be called once at startup.
    
- `Renderer` is static, because:
    
    - There is exactly one `GraphicsDevice` per game.
        
    - Only one rendering pipeline is needed at this stage.
        
- For now, `SpriteBatch` is still passed down to entities.  
    This will potentially be refactored in later tasks when layers and cameras are introduced.
    

---

## 5. Changes to Game1

### 5.1 Removal of SpriteBatch Field

Previously:

```csharp
private SpriteBatch _spriteBatch;
```

This field has been removed. `Game1` no longer manages a `SpriteBatch`.

---

### 5.2 LoadContent – Renderer Initialization

Previously:

```csharp
protected override void LoadContent()
{
    _spriteBatch = new SpriteBatch(GraphicsDevice);

    AssetLoader.Initialize(Content);

    var playerTexture = AssetLoader.LoadTexture("Player");
    _player = new Player(playerTexture, new Vector2(200, 200));

    _entityManager.Add(_player);
}
```

Now:

```csharp
using MyEngine.Core;
using MyEngine.Entities;

// ...

protected override void LoadContent()
{
    Renderer.Initialize(GraphicsDevice);
    AssetLoader.Initialize(Content);

    var playerTexture = AssetLoader.LoadTexture("Player");
    _player = new Player(playerTexture, new Vector2(200, 200));

    _entityManager.Add(_player);
}

```

`Renderer.Initialize(GraphicsDevice)` replaces the explicit `SpriteBatch` creation.

---

### 5.3 Draw – Using Renderer Instead of SpriteBatch

Previously:

```csharp
protected override void Draw(GameTime gameTime)
{
    GraphicsDevice.Clear(Color.CornflowerBlue);

    _spriteBatch.Begin();
    _entityManager.Draw(_spriteBatch);
    _spriteBatch.End();

    base.Draw(gameTime);
}
```

Now:

```csharp
using MyEngine.Core;

// ...

protected override void Draw(GameTime gameTime)
{
    Renderer.Clear(Color.CornflowerBlue);

    Renderer.Begin();
    _entityManager.Draw(Renderer.SpriteBatch);
    Renderer.End();

    base.Draw(gameTime);
}
```

Effects:

- Clearing the screen is now done via `Renderer.Clear`.
    
- Begin/End of the draw pass are controlled by `Renderer`.
    
- `EntityManager` still receives a `SpriteBatch`, but it is now provided by `Renderer.SpriteBatch`.
    

---

## 6. Architectural Impact

### 6.1 Single Responsibility Principle (SRP)

- `Renderer` now has a clear single responsibility: manage rendering pipeline state.
    
- `Game1` now focuses on:
    
    - Engine system orchestration (`EngineTime`, `InputManager`, `SceneManager` later)
        
    - Delegating `Update` and `Draw`, not drawing directly.
        

### 6.2 Open/Closed Principle (OCP)

- Renderer can be extended in future tasks without modifying Game1:
    
    - Render layers
        
    - Camera support
        
    - Post-processing
        
    - Custom `Begin` configurations
        

### 6.3 Dependency Management

- Rendering dependencies are now localized in `Renderer`.
    
- Systems that need to draw can be gradually moved to depend on higher-level abstractions instead of raw `SpriteBatch`.


## 7. Summary

Task 1 of Sprint 3 introduced a central `Renderer` class responsible for:

- Managing `GraphicsDevice` and `SpriteBatch`
    
- Providing unified `Clear`, `Begin`, and `End` operations
    
- Decoupling low-level rendering details from `Game1`
    

The engine is now structurally prepared for more advanced rendering features such as cameras and layers, which will be introduced in the next tasks of Sprint 3.

