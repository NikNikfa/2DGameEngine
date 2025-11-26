
## **1. Summary of Implementation**

- A dedicated `EngineTime` class was implemented for handling game timing.
    
- Computes **DeltaTime** each frame (time between the current and previous frame).
    
- Maintains **TotalTime** since the game started.
    
- Exposes clean methods/properties to access:
    
    - `DeltaTime`
        
    - `TotalTime`
        
    - `ElapsedGameTime`
        
- Integrated directly into the `Game1.Update()` loop.
    

---

## **2. Purpose / Motivation**

The goal of `EngineTime` is to provide a **central and consistent timing system** independent of MonoGame’s raw `GameTime`.  
This ensures:

- Frame-rate–independent movement
    
- Stable animations
    
- Predictable physics behavior
    
- Cleaner usage across engine subsystems (Input, Entity updates, etc.)
    

This abstraction prepares the engine for future systems (Scene, Physics, Animation).

---

## **3. Current Design Notes**

- Uses MonoGame’s `gameTime.ElapsedGameTime.TotalSeconds` internally.
    
- Converts values into floats for easier use in gameplay logic.
    
- Designed as a simple static class for now (acceptable for early engine stages).
    
- Called once per frame at the top of `Game1.Update()`.
    

This design keeps responsibilities clear and aligns with SRP.

---

## **4. Integration Notes**

- `EngineTime.Update(gameTime)` is called before all gameplay or input logic.
    
- Ensures all subsystems use the same DeltaTime for consistent behavior.
    
- Currently used by Player movement and safe to integrate into future systems like:
    
    - EntityManager
        
    - Animation
        
    - Physics
        
    - Scene transitions
        

---

## **5. Decisions Made**

- Chosen as a **static class** to avoid instantiation overhead or dependency injection at this stage.
    
- DeltaTime stored as a `float` for performance and ease of use in arithmetic operations.
    
- No separate pause-time logic yet—planned for future sprints.
    

---

## **6. Known Limitations / To-Do**

- ❗ No support for **pausing** the time (pause menu will require freezing DeltaTime).
    
- ❗ No built-in **time scaling** (slow motion, fast forward).
    
- ❗ No support for **fixed timestep** physics (may be added in Sprint 3 or 4).
    
- ❗ Relies on being updated correctly by `Game1`; moving to a future Scene system may require refactoring.