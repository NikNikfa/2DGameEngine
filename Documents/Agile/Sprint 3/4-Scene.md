
## 1. Purpose of the Task

The purpose of this task was to introduce a **Scene abstraction** to structure the game into distinct screens or states, such as:

- Main gameplay scene
    
- Menu scene
    
- Pause scene
    
- Loading or intro scenes
    

Instead of keeping all logic inside `Game1`, we want each “screen” to be represented by its own Scene class, following a consistent lifecycle.

This Scene abstraction is the foundation for the **SceneManager** (Task 5) and is essential for a maintainable game architecture.

---

## 2. Problem Before Implementation

Before Task 4:

- All logic was conceptually inside `Game1` (even if temporarily structured).
    
- There was no formal separation between different game states (menu, gameplay, pause, etc.).
    
- Adding or switching between different screens would require ad-hoc logic inside `Game1`.
    
- There was no standard lifecycle (Load, Update, Draw, Unload) for “screens”.
    

This makes the project harder to extend, test, and reason about as it grows.

---

## 3. Implemented Solution: Abstract Scene Base Class

A new **abstract base class** `Scene` was introduced in `Core/Scene.cs`.

### 3.1 Final Implementation

```csharp
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyEngine.Core
{
    public abstract class Scene
    {
        // Constructor is protected to allow inheritance but prevent direct instantiation
        protected Scene()
        {
        }

        // Called once when the scene becomes active (e.g., when switching to this scene)
        public virtual void Load()
        {
        }

        // Called once when the scene is being removed or switched away from
        public virtual void Unload()
        {
        }

        // Called every frame to update scene logic
        public virtual void Update(GameTime gameTime)
        {
        }

        // Called every frame to draw the scene
        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
```

## 4. Design Decisions

### 4.1 Abstract Class

`Scene` is declared `abstract` because:

- It represents a **concept** (“a scene”) rather than a concrete implementation.
    
- We never want to instantiate `Scene` directly, only subclasses such as `GameScene`, `MenuScene`, etc.
    

This enforces correct usage and aligns with the engine’s design.

---

### 4.2 Protected Constructor

The constructor is:

```csharp
protected Scene() {}
```

protected Scene() {}
This means:

Only subclasses can call the constructor.

External code cannot instantiate Scene directly.

This prevents incorrect usage like:

```csharp
var s = new Scene(); // invalid
```

and enforces that scenes must be specific derived classes.

---

### 4.3 Lifecycle Methods

The base class defines four key virtual methods:

1. `Load()`
    
2. `Unload()`
    
3. `Update(GameTime gameTime)`
    
4. `Draw(SpriteBatch spriteBatch)`
    

#### Load()

- Called when the scene becomes active.
    
- Used to load content, set up entities, allocate resources, subscribe to events, etc.
    

#### Unload()

- Called when the scene is no longer needed.
    
- Used to free resources, stop music, unsubscribe events, and clean up.
    

#### Update()

- Called once per frame.
    
- Contains all logic for the scene: input handling, entity updates, timers, state transitions, etc.
    

#### Draw()

- Called once per frame after Update.
    
- Responsible for drawing the scene’s content using the provided `SpriteBatch`.
    
- In combination with `Renderer` and camera, this will draw world + UI.
    

The methods are `virtual` so that each specific Scene can override only what it needs.

---

## 5. Example Usage (Conceptual)

A typical gameplay scene using this base class might look like:

```csharp
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyEngine.Core;
using MyEngine.Entities;

namespace MyGame.Scenes
{
    public class GameScene : Scene
    {
        private EntityManager _entities;
        private Player _player;

        public override void Load()
        {
            _entities = new EntityManager();

            var texture = AssetLoader.LoadTexture("Player");
            _player = new Player(texture, new Vector2(200, 200));

            _entities.Add(_player);
        }

        public override void Update(GameTime gameTime)
        {
            _entities.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _entities.Draw(spriteBatch);
        }

        public override void Unload()
        {
            // Clean up if needed (e.g., clear entities)
        }
    }
}
```

This demonstrates how `Scene` acts as a container for:

- Entity manager
    
- Player and other entities
    
- Scene-specific logic
    

---

## 6. Architectural Impact

### 6.1 Separation of Concerns

- `Scene` encapsulates the logic and rendering of a single “screen”.
    
- `Game1` will later delegate to a `SceneManager` instead of knowing about gameplay details.
    
- Different scenes (menu, game, pause, etc.) become cleanly separated.
    

### 6.2 Extensibility

- New scenes (e.g., `MenuScene`, `PauseScene`, `SettingsScene`) can be added just by subclassing `Scene`.
    
- SceneManager (next task) can swap scenes without needing to know the internal structure of each scene.
    

### 6.3 Alignment with SOLID

- **SRP:** Each Scene encapsulates one mode/state of the game.
    
- **OCP:** New scenes can be added without modifying existing ones.
    
- **LSP:** All concrete scenes are substitutable as `Scene` in SceneManager.
    
- **DIP:** Higher-level code (SceneManager, Game1) depends on abstraction `Scene`, not concrete implementations.
    

---

## 7. Dependencies and Integration

At this stage:

- `Scene` does not yet integrate with Game1.
    
- Scene switching is not yet implemented.
    
- This is deliberate: SceneManager will handle that in Task 5.
    

This task is purely about defining the **abstraction** and **lifecycle contract**.

---

## 8. Summary

Task 4 introduced an abstract `Scene` base class with:

- Protected constructor (subclass-only instantiation)
    
- Lifecycle methods: `Load`, `Unload`, `Update`, `Draw`
    

This provides a clean, extensible foundation for managing different game states and prepares the engine for:

- SceneManager (Task 5)
    
- Multiple scenes (GameScene, MenuScene, etc.)
    
- Clean separation between engine-core and game-specific logic.