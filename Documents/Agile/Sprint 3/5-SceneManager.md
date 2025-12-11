
## 1. Purpose of the Task

The purpose of Task 5 was to introduce a **central SceneManager** responsible for:

- Keeping track of the **currently active Scene**
    
- Handling **scene switching** in a controlled way
    
- Automatically calling `Load()` and `Unload()` on scenes
    
- Forwarding `Update` and `Draw` calls to the active scene
    

This is a key step in moving all game-state logic out of `Game1` and into concrete Scene classes like `GameScene` and `MenuScene`.

---

## 2. Problem Before Implementation

Before Task 5:

- There was a `Scene` base class (Task 4), but no system managing which scene is active.
    
- `Game1` was still conceptually responsible for the entire game flow.
    
- There was no unified mechanism to:
    
    - Switch from one scene to another
        
    - Ensure `Unload()` is called on the old scene
        
    - Ensure `Load()` is called on the new scene
        

Without a SceneManager:

- Scene switching logic would become ad-hoc and scattered.
    
- Each scene might be manually created/loaded in different places.
    
- The architecture would not scale for multiple states (menu, gameplay, pause, etc.).
    

---

## 3. Implemented Solution: Static SceneManager

A new static class `SceneManager` was created to act as a global scene controller.

### 3.1 Final Implementation – `SceneManager.cs`

```csharp
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyEngine.Core
{
    public static class SceneManager
    {
        private static Scene _currentScene;

        public static Scene CurrentScene => _currentScene;

        public static void ChangeScene(Scene newScene)
        {
            if (newScene == null)
                throw new ArgumentNullException(nameof(newScene));

            // Unload the previous scene, if there is one
            _currentScene?.Unload();

            // Switch to the new scene
            _currentScene = newScene;

            // Load the new scene
            _currentScene.Load();
        }

        public static void Update(GameTime gameTime)
        {
            _currentScene?.Update(gameTime);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            _currentScene?.Draw(spriteBatch);
        }
    }
}
```

---

## 4. Design Decisions

### 4.1 Static Class

`SceneManager` is `static` because:

- There is exactly one active scene at a time.
    
- Scene management is a **global engine concern**, similar to `Renderer`, `EngineTime`, and `InputManager`.
    
- It simplifies access from `Game1` and other engine code.
    

This fits the current architecture style of the engine.

---

### 4.2 Single Active Scene

The manager stores:

```csharp
private static Scene _currentScene;
public static Scene CurrentScene => _currentScene;
```

This enforces the invariant:

- At most one Scene is active at any given time.
    
- `CurrentScene` exposes the active scene as read-only (no external setter).
    

---

### 4.3 ChangeScene API

```csharp
public static void ChangeScene(Scene newScene)
{
    if (newScene == null)
        throw new ArgumentNullException(nameof(newScene));

    _currentScene?.Unload();
    _currentScene = newScene;
    _currentScene.Load();
}
```

The responsibilities of `ChangeScene` are:

1. Validate that `newScene` is not null.
    
2. Call `Unload()` on the current scene, if it exists.
    
3. Assign `_currentScene = newScene`.
    
4. Call `Load()` on the new scene.
    

This ensures that:

- Scene transitions are consistent and safe.
    
- Resource loading/unloading is handled in a standardized way.
    
- The lifecycle contract defined in `Scene` (Task 4) is respected.
    

---

### 4.4 Update and Draw Delegation

```csharp
public static void Update(GameTime gameTime)
{
    _currentScene?.Update(gameTime);
}

public static void Draw(SpriteBatch spriteBatch)
{
    _currentScene?.Draw(spriteBatch);
}
```

These methods delegate the engine’s per-frame work to whatever scene is currently active:

- No scene → nothing happens.
    
- Active scene → its `Update` and `Draw` methods are called each frame.
    

This is how `Game1` will eventually stop knowing about gameplay details and simply route work to `SceneManager`.

---

## 5. Architectural Impact

### 5.1 Separation of Concerns

- `Scene` defines what a scene is (base abstraction).
    
- `SceneManager` manages which scene is active and handles transitions.
    
- `Game1` will only:
    
    - Initialize the engine systems (Renderer, Input, Time, Camera).
        
    - Delegate update/draw to `SceneManager`.
        

### 5.2 Extensibility

- New scenes (MenuScene, PauseScene, etc.) can be added without modifying SceneManager.
    
- SceneManager operates on the abstraction `Scene`, not specific concrete classes.
    
- The API is stable and reusable across the whole game.
    

### 5.3 SOLID Principles

- **SRP:** SceneManager has exactly one responsibility: manage scenes.
    
- **OCP:** New scene types do not require changes in SceneManager.
    
- **LSP:** Any `Scene` subclass can be used as `_currentScene`.
    
- **DIP:** High-level flow depends on `Scene` abstraction, not specific implementations.
    

---

## 6. Limitations and Future Work

SceneManager is intentionally minimal at this stage:

- No scene stack (e.g., pushing/popping scenes for pause screens).
    
- No transitions (fade in/out, cross-fade, loading bars).
    
- No error handling for failed scene loads.
    

These features can be added later if needed, but the current design is sufficient for:

- A GameScene
    
- A MenuScene
    
- Simple switching between them via `ChangeScene`.
    

---

## 7. Summary

Task 5 introduced a `SceneManager` that:

- Stores the active `Scene`.
    
- Handles scene transitions with `ChangeScene`.
    
- Delegates `Update` and `Draw` to the active scene.
    
- Integrates with the existing `Scene` abstraction created in Task 4.
    

This completes the core of the scene management architecture and prepares the engine for:

- Integrating SceneManager into `Game1` (next task).
    
- Implementing concrete scenes such as `GameScene` and `MenuScene`.
