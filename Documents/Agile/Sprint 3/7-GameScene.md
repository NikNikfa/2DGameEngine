
## 1. Purpose of the Task

The goal of Task 7 was to implement the engine’s first **fully operational gameplay scene**, named `GameScene`.  
This scene serves as the primary example of how game logic is meant to be structured inside the new scene-based architecture introduced in Sprint 3.

GameScene demonstrates:

- Scene lifecycle (`Load`, `Unload`, `Update`, `Draw`)
    
- Ownership and orchestration of core gameplay systems
    
- Integration of the camera system at the scene level
    
- Decoupling of gameplay logic from `Game1`
    
- Use of `EntityManager` and `Player` inside a Scene
    

This task completes the transition to a modular, scene-driven engine.

---

## 2. Requirements

**Functional Requirements:**

1. The scene must load all gameplay assets including textures for the player.
    
2. The Player entity must be instantiated and managed through `EntityManager`.
    
3. The Camera2D must track the Player during movement.
    
4. The scene must implement all Scene lifecycle methods:
    
    - `Load`
        
    - `Unload`
        
    - `Update`
        
    - `Draw`
        
    - `GetViewMatrix`
        
5. The scene must be compatible with `SceneManager` and `Game1`.
    

**Non-Functional Requirements:**

- The implementation must follow SOLID principles.
    
- Game1 must not contain gameplay logic.
    
- No direct coupling between GameScene and engine internals such as SpriteBatch initialization.
    

---

## 3. Implementation Summary

A new class `GameScene` was created and added to the `Scenes` folder.  
This class inherits from `Scene` and overrides all relevant methods.

### Key Internal Components:

- **EntityManager**: Manages all entities in the scene.
    
- **Player**: The main controllable entity.
    
- **Camera2D**: Provides camera-follow functionality.
    

### High-Level Behavior:

- On `Load()`, the Player is created, added to EntityManager, and the camera is initialized.
    
- On `Update()`, all entities are updated and the camera centers on the Player.
    
- On `Draw()`, the EntityManager draws the Player and future entities.
    
- `GetViewMatrix()` returns the camera’s view matrix so the world renders with scrolling.
    

---

## 4. Final Code Implementation: `GameScene.cs`

```csharp
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyEngine.Core;
using MyEngine.Entities;

namespace MyGame.Scenes
{
    public class GameScene : Scene
    {
        private EntityManager _entityManager;
        private Player _player;
        private Camera2D _camera;

        public override void Load()
        {
            _entityManager = new EntityManager();
            _camera = new Camera2D();

            // Load player texture via AssetLoader
            var playerTexture = AssetLoader.LoadTexture("Player");

            // Initial player position in world space
            _player = new Player(playerTexture, new Vector2(200, 200));

            _entityManager.Add(_player);
        }

        public override void Unload()
        {
            _entityManager = null;
            _player = null;
            _camera = null;
        }

        public override void Update(GameTime gameTime)
        {
            _entityManager?.Update(gameTime);

            // Camera follows the player
            if (_player != null && _camera != null)
            {
                _camera.Position = _player.Position;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _entityManager?.Draw(spriteBatch);
        }

        public override Matrix GetViewMatrix(Viewport viewport)
        {
            if (_camera == null)
                return Matrix.Identity;

            return _camera.GetViewMatrix(viewport);
        }
    }
}
```

## 5. Integration with the Engine

To activate the GameScene, the following line was added to `Game1.LoadContent`:

```csharp
SceneManager.ChangeScene(new GameScene());
```

This triggers:

- `GameScene.Load()`
    
- Proper delegation of update/draw via SceneManager
    
- Camera integration via `GetViewMatrix`
    

`Game1` now functions strictly as an orchestrator and no longer contains gameplay logic.

---

## 6. Outcomes

After implementing GameScene:

1. The engine now has a **fully functional gameplay screen**, not hardcoded into Game1.
    
2. Camera-follow is working as part of the Scene itself.
    
3. Player movement and entity updating are entirely Scene-managed.
    
4. Scene-based architecture is now validated and operational.
    
5. Game1 contains no gameplay-specific references, improving modularity.
    

---

## 7. Future Improvements (Optional)

- Add smoothing/damping to camera movement.
    
- Add tilemap or level rendering to validate scrolling visually.
    
- Introduce parallax backgrounds.
    
- Add UI rendering (health bar, debug overlay) independent of camera (using identity matrix).