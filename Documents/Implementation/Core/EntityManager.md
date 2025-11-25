
## 1. Overview

`EntityManager` is a **simple central manager for all `Entity` instances** in the engine.

It provides:

- A single place to **register, update, and draw** entities
    
- Safe **add/remove** operations (no “collection modified” errors during iteration)
    
- A clean abstraction so `Game1` (or later `Scene`) does not directly manage lists
    

In the current project phase, `EntityManager` will be used by `Game1`.  
Later, it can be moved inside a `Scene` or `World` system without changing game logic.

> Assumption: we now have a base `Entity` class and classes like `Player` derive from it.

---

## 2. Responsibilities

`EntityManager` is responsible for:

- Storing **all active entities** in the current context (e.g. current “scene”)
    
- Providing methods to:
    
    - Add entities
        
    - Remove entities
        
    - Clear all entities
        
- Updating entities every frame:
    
    - Calls `Update(GameTime)` on each `Entity` where `IsActive == true`
        
- Drawing entities every frame:
    
    - Calls `Draw(SpriteBatch)` on each `Entity` where `IsVisible == true`
        
- Handling **safe addition/removal**:
    
    - Entities can be added/removed even during update, without breaking iteration
        

---

## 3. Non-Responsibilities

`EntityManager` does **not**:

- Handle **loading assets** (that’s `AssetLoader` / Content pipeline)
    
- Handle **input** (that’s `InputManager` / entities themselves)
    
- Implement **physics or collision systems**
    
- Handle **scene transitions** or global game states
    
- Decide **render order** based on camera or sorting logic beyond a simple iteration (for now)
    

In other words, `EntityManager` is a basic but robust container, not a full scene graph.

---

## 4. Public API

### 4.1 Fields / Properties

```csharp
public class EntityManager
{
    private readonly List<Entity> _entities = new();
    private readonly List<Entity> _entitiesToAdd = new();
    private readonly List<Entity> _entitiesToRemove = new();

    public IReadOnlyList<Entity> Entities => _entities;
}

```

- `_entities`  
    The main list of entities currently managed.
    
- `_entitiesToAdd` / `_entitiesToRemove`  
    Temporary lists used to safely modify entities between frames.
    
- `Entities`  
    Read-only view for debugging or external inspection if needed.
    

---

### 4.2 Adding Entities

```csharp
public void AddEntity(Entity entity)
{
    if (entity == null)
        throw new ArgumentNullException(nameof(entity));

    _entitiesToAdd.Add(entity);
}
```

- Entities are not added to `_entities` immediately.
    
- They are queued and applied at the **beginning or end of an update cycle** (see section 4.5).
    

---

### 4.3 Removing Entities

```csharp
public void RemoveEntity(Entity entity)
{
    if (entity == null)
        return;

    _entitiesToRemove.Add(entity);
}
```

- Removes are also queued to avoid modifying `_entities` while iterating.
    

Optionally, we can provide a “soft delete” via a flag on `Entity` (e.g. `IsActive = false`) and then detect it in `UpdateAll`, but for now we explicitly call `RemoveEntity`.

---

### 4.4 Clear All Entities

```csharp
public void Clear()
{
    _entities.Clear();
    _entitiesToAdd.Clear();
    _entitiesToRemove.Clear();
}
```
Used for example when switching scenes or restarting a level.

---

### 4.5 Applying Pending Changes

```csharp
private void ApplyPendingChanges()
{
    if (_entitiesToAdd.Count > 0)
    {
        _entities.AddRange(_entitiesToAdd);
        _entitiesToAdd.Clear();
    }

    if (_entitiesToRemove.Count > 0)
    {
        foreach (var entity in _entitiesToRemove)
            _entities.Remove(entity);

        _entitiesToRemove.Clear();
    }
}
```

This method is called once per frame (usually at the **beginning** or **end** of `UpdateAll`).

---

### 4.6 Updating Entities

```csharp
public void UpdateAll(GameTime gameTime)
{
    // First, apply any changes from previous frame
    ApplyPendingChanges();

    foreach (var entity in _entities)
    {
        if (entity.IsActive)
            entity.Update(gameTime);
    }

    // Optional: apply changes again if entities queued removals during this update
    ApplyPendingChanges();
}
```
Notes:

- Only entities with `IsActive == true` are updated
    
- Calling `ApplyPendingChanges()` before and after allows entities to schedule add/remove in their own `Update` methods safely
    

---

### 4.7 Drawing Entities

```csharp
public void DrawAll(SpriteBatch spriteBatch)
{
    foreach (var entity in _entities)
    {
        if (entity.IsVisible)
            entity.Draw(spriteBatch);
    }
}
```

Notes:

- Only entities with `IsVisible == true` are drawn
    
- More advanced sorting (e.g. by `LayerDepth`) can be added in future sprints:

```csharp
foreach (var entity in _entities.OrderBy(e => e.LayerDepth))
    ...
```


