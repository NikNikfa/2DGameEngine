
## 1. Purpose of the Task

The goal of Task 2 was to introduce a **render layering system** so that entities are drawn in a predictable and controlled order. This is essential in any rendering pipeline because:

- UI must appear above the world
    
- Background elements must appear behind the Player
    
- Foreground overlays must appear above gameplay
    
- Menu scenes and HUDs typically exist in higher layers
    

Without render ordering, entities would be drawn in the order they were added to the `EntityManager`, which is unpredictable and not suitable for a real engine.

This task builds on Task 1 (centralized Renderer) by giving entities a structural way to express their rendering priority.

---

## 2. Problem Before Implementation

Before Task 2:

- All entities were drawn in a single flat list.
    
- Draw order depended on insertion order into `EntityManager`.
    
- No way existed to differentiate backgrounds, world objects, and UI.
    
- The rendering pipeline was not prepared for complex scenes.
    

As a result, any upcoming features (camera, UI, scene layering) required a proper layering system.

---

## 3. Implemented Solution Overview

Three key changes were made:

1. Introduced a **RenderLayer enum** describing common drawing layers.
    
2. Modified `Entity` to include a `Layer` property.
    
3. Updated `EntityManager.Draw` to **sort entities by their layer** before rendering.
    

The system is simple but flexible and can be expanded in later sprints.

---

## 4. Final Implementation Details

### 4.1 RenderLayer Enum (Core/RenderLayer.cs)

```csharp
namespace MyEngine.Core
{
    public enum RenderLayer
    {
        Background = 0,
        World = 100,
        Foreground = 200,
        UI = 300
    }
}
```

Notes:

- Numerical spacing allows new layers to be inserted without renumbering.
    
- The default layer for most gameplay entities is `World`.
    

---

### 4.2 Entity Class Updated with a Layer Property

```csharp
public RenderLayer Layer { get; set; }

protected Entity(Texture2D texture, Vector2 position)
{
    Texture = texture;
    Position = position;

    Layer = RenderLayer.World; // default for all game objects
}

```

This ensures every entity explicitly belongs to a rendering layer.

---

### 4.3 Updated EntityManager.Draw Sorting Logic

```csharp
public void Draw(SpriteBatch spriteBatch)
{
    foreach (var entity in _entities
        .Where(e => e.IsActive)
        .OrderBy(e => e.Layer))
    {
        entity.Draw(spriteBatch);
    }
}
```

Key Details:

- Entities are filtered to include only active ones.
    
- Entities are ordered by their `Layer` value.
    
- Drawing occurs in ascending order, meaning Background first, UI last.
    

Behavioral Result:

- Backgrounds → World → Foreground → UI
    
- Predictable and scalable drawing order
    
- Fully compatible with the existing Renderer and SpriteBatch usage
    

---

## 5. Architectural Impact

### 5.1 Rendering Consistency

Scenes can now control exactly what appears above or below other objects.

### 5.2 Extensibility

Future systems (camera, UI, particle systems, tilemaps) will rely on the existence of render layers.

### 5.3 Separation of Concerns

- Layering is handled at the entity level.
    
- Sorting is handled by the EntityManager.
    
- Rendering calls remain centralized in the Renderer.
    

This maintains SRP across components.

### 5.4 Future-Proofing

In later tasks, we will introduce:

- Z-sorting
    
- Depth buffers (optional)
    
- Layer-specific SpriteBatch Begin parameters
    
- Camera transforms applied only to certain layers
    
- UI layers drawn without camera transforms
    

This simple design makes all of that possible.

---

## 6. Dependencies and No-Change Areas

No changes were required in:

- `Game1`
    
- `Renderer`
    
- `Player`
    

The task was self-contained and fully backward-compatible.

---

## 7. Summary

Sprint 3 – Task 2 successfully introduced a flexible and minimal shading of the rendering system through:

- A shared `RenderLayer` enum
    
- A new `Layer` property on entities
    
- Sorted draw calls in `EntityManager`
    

This completes the layering foundation needed for the camera system and scene system in the upcoming tasks of Sprint 3.