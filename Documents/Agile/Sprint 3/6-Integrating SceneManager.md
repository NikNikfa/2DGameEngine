
## 1. Purpose of the Task

The purpose of Task 6 was to:

- Connect the existing **Scene** and **SceneManager** abstractions to the actual MonoGame loop (`Game1`).
    
- Ensure that `Game1` no longer contains game-specific logic (Player, EntityManager, etc.) but instead delegates work to the active Scene via `SceneManager`.
    
- Introduce a basic mechanism so each Scene can provide its own **camera/view matrix**.
    

This task transforms `Game1` from a “God class” into a **composition root** that wires engine services together.

---

## 2. Problem Before Implementation

Before this task:

- `Scene` and `SceneManager` existed but were not used by `Game1`.
    
- `Game1` was still conceptually responsible for the update/draw flow.
    
- There was no connection between:
    
    - The camera system
        
    - The rendering pipeline
        
    - The scene architecture
        

Engine structure was still incomplete: the scene system was defined but not integrated.

---

## 3. Changes to Scene Base Class

To support camera-aware rendering, the `Scene` base class was extended with a method that returns a **view matrix**.

### 3.1 Updated Scene.cs

```csharp
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyEngine.Core
{
    public abstract class Scene
    {
        protected Scene()
        {
        }

        public virtual void Load()
        {
        }

        public virtual void Unload()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        // New: scenes can define their own camera/view transform.
        public virtual Matrix GetViewMatrix(Viewport viewport)
        {
            // Default behavior: no camera transform (identity matrix)
            return Matrix.Identity;
        }
    }
}
```

### Design Rationale

- Not all scenes require a camera (e.g., MenuScene, PauseScene).
    
- Gameplay scenes can override `GetViewMatrix` to use `Camera2D`.
    
- `Game1` no longer needs to know how the camera is implemented; it just asks the current Scene for its view matrix.
    

This follows the **Dependency Inversion Principle (DIP)**: `Game1` depends on the abstraction (`Scene`) rather than concrete camera or scene types.

---

## 4. Changes to Game1 – Delegating to SceneManager

`Game1` was refactored to:

- Initialize engine core systems.
    
- Delegate `Update` and `Draw` to `SceneManager`.
    
- Ask the current Scene for its view matrix and apply it through `Renderer`.
    

### 4.1 Simplified Game1.cs Core Structure

```csharp
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyEngine.Core;

namespace MyGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Renderer.Initialize(GraphicsDevice);
            AssetLoader.Initialize(Content);

            // Initial scene will be set in the next task (e.g., GameScene):
            // SceneManager.ChangeScene(new GameScene());
        }

        protected override void Update(GameTime gameTime)
        {
            EngineTime.Update(gameTime);
            InputManager.Update();

            if (InputManager.IsKeyPressed(Keys.Escape))
                Exit();

            SceneManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Renderer.Clear(Color.CornflowerBlue);

            Matrix viewMatrix = Matrix.Identity;

            if (SceneManager.CurrentScene != null)
            {
                viewMatrix = SceneManager.CurrentScene.GetViewMatrix(GraphicsDevice.Viewport);
            }

            Renderer.Begin(viewMatrix);
            SceneManager.Draw(Renderer.SpriteBatch);
            Renderer.End();

            base.Draw(gameTime);
        }
    }
}
```

### Key Points

- `Game1` no longer directly updates entities or draws them.
    
- `SceneManager.Update` and `SceneManager.Draw` are the single entry points for scene logic.
    
- `viewMatrix` is obtained from the active Scene via `GetViewMatrix(viewport)`.
    
- If there is no active scene, `Matrix.Identity` is used (no transform).
    

---

## 5. Resulting Engine Flow

After Task 6, the core per-frame flow is:

1. `Game1.Update`
    
    - Updates `EngineTime` and `InputManager`.
        
    - Calls `SceneManager.Update(gameTime)` → delegates to `CurrentScene.Update`.
        
2. `Game1.Draw`
    
    - Clears screen via `Renderer.Clear`.
        
    - Asks `CurrentScene` for view matrix via `GetViewMatrix(viewport)`.
        
    - Calls `Renderer.Begin(viewMatrix)`.
        
    - Calls `SceneManager.Draw(Renderer.SpriteBatch)` → delegates to `CurrentScene.Draw`.
        
    - Calls `Renderer.End()`.
        

This is now a **scene-driven architecture**, where `Game1` only orchestrates systems, not game rules.

---

## 6. Architectural Impact

### 6.1 Separation of Concerns

- `Game1` is responsible for:
    
    - Engine startup
        
    - Core systems initialization
        
    - Delegating to `SceneManager`
        
- `SceneManager` is responsible for:
    
    - Choosing which Scene is active
        
    - Handling scene switching logic
        
- Each `Scene` is responsible for:
    
    - Its own content loading/unloading
        
    - Its own Update/Draw logic
        
    - Its own camera behavior via `GetViewMatrix`
        

### 6.2 Extensibility

- New scenes can easily define custom camera logic without touching `Game1`.
    
- Menu, pause, and gameplay scenes can coexist cleanly.
    
- Scene-specific rendering (with or without camera) is fully supported.
    

---

## 7. Limitations and Next Steps

At the end of Task 6:

- The engine is wired for Scene-based architecture.
    
- However, there is **no concrete Scene** yet using `Scene` and `SceneManager`.
    
- The next step is to implement **GameScene** (Task 7), where:
    
    - `GameScene` will own `EntityManager`, `Player`, and `Camera2D`.
        
    - `GameScene.Load` will set up entities and content.
        
    - `GameScene.Update` will move the player and update the camera.
        
    - `GameScene.Draw` will render the world via `EntityManager`.
        
    - `GameScene.GetViewMatrix` will return `Camera2D.GetViewMatrix(viewport)`.
        

---

## 8. Summary

Task 6 successfully:

- Extended `Scene` with `GetViewMatrix(Viewport)` for camera integration.
    
- Refactored `Game1` to delegate `Update` and `Draw` to `SceneManager`.
    
- Connected the scene architecture, camera system, and renderer into a coherent loop.
    

The engine is now ready for actual scenes to be created and plugged in, starting with `GameScene` in the next task.