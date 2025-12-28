
**Task Type:** Gameplay Foundation  
**Status:** Completed  
**Files Added / Modified:**

- `Core/Collision.cs` (new)
    
- `Entities/Entity.cs` (modified)
    
- `Core/EntityManager.cs` (modified)
    
- `Scenes/GameScene.cs` (modified – temporary collision test)


## 1. Purpose of the Task

The purpose of Task 2 was to introduce **basic collision detection** based on spatial overlap.

This task answers **only one question**:

> “Are two entities occupying the same space right now?”

No collision response, no blocking movement, and no physics behavior were intended or implemented in this task.

---

## 2. Problem Before Implementation

After Sprint 4 – Task 1:

- Entities had position, size, and bounds (`Transform.Bounds`).
    
- There was still no way to determine whether entities intersected.
    
- Gameplay systems had no notion of physical presence or overlap.
    

Collision detection was required as a prerequisite for any future gameplay constraints.

---

## 3. Implemented Solution

### 3.1 Collision Utility

A minimal static collision helper was introduced to centralize overlap logic.

**Key design choice:**

- Collision detection is based on **Axis-Aligned Bounding Boxes (AABB)**.
    
- Rectangular overlap is used for simplicity, performance, and predictability.
    

The collision utility only performs intersection checks and has no side effects.

---

### 3.2 Entity Collision Properties

Entities were extended with minimal collision-related data:

- `IsCollidable`
    
    - Allows entities to opt in or out of collision checks.
        
    - Defaults to `true`.
        
- `Bounds`
    
    - Exposes `Transform.Bounds` directly.
        
    - Provides a consistent way to access collision space.
        

These additions do not alter entity behavior and only expose spatial data.

---

## 4. EntityManager Update

`EntityManager` was extended with **read-only access** to its entities.

### Purpose:

- Allow systems (such as scenes or collision logic) to iterate over entities safely.
    
- Prevent external modification of the entity list.
    

### Design decision:

- Exposed entities as `IReadOnlyList<Entity>`.
    
- All mutations still occur exclusively through `Add` and `Remove`.
    

This preserves encapsulation while enabling collision checks.

---

## 5. Collision Test Integration (Temporary)

A temporary collision test was added in `GameScene.Update` to validate the system.

### Behavior:

- The Player entity is checked against all other active, collidable entities.
    
- When bounding boxes intersect, a debug message is printed.
    

### Observed behavior:

- Multiple `"Collision!"` messages appear while entities remain overlapping.
    
- This is expected, as collision is evaluated **every frame**.
    

This confirmed that:

- Bounds are calculated correctly.
    
- Collision detection logic works reliably.
    

---

## 6. Observations and Expected Limitations

### Continuous Collision Output

- Collision detection is frame-based.
    
- Overlapping entities trigger detection every frame.
    
- No collision state (enter/exit) tracking exists yet.
    

This behavior is correct for this task.

---

### Camera Interaction Side Effect

- Camera-relative movement caused visual confusion during collision testing.
    
- Input behavior was confirmed to be correct.
    
- The perceived “opposite movement” was due to the camera moving the world, not incorrect input logic.
    

This highlighted the importance of visual context, not a flaw in collision detection.

---

## 7. Scope Control

This task intentionally did **not** include:

- Collision response or resolution.
    
- Movement blocking.
    
- Physics simulation.
    
- Collision events or callbacks.
    
- State tracking (collision enter/exit).
    

Those behaviors depend on this task and will be addressed later.

---

## 8. Resulting System State

After completing Task 2:

- Entities have physical bounds.
    
- Overlap between entities can be detected reliably.
    
- Collision checks are centralized and consistent.
    
- The engine can now reason about spatial intersections.
    

This establishes the foundation for movement constraints and physics behavior.

---

## 9. Summary

Sprint 4 – Task 2 successfully introduced **basic AABB collision detection** using entity bounds derived from the Transform system.  
The task validated that entities can detect spatial overlap in real time, providing the necessary groundwork for collision response and gameplay rules in subsequent tasks.

No gameplay behavior was altered; this task strictly introduced detection infrastructure.