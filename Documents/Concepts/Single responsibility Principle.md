
## 🧩 What SRP Means

**Definition:**

> A class should have only **one reason to change** — that is, it should have **only one responsibility**.

In simpler words:  
🎯 **Each class, module, or function should do one thing, and do it well.**

---

## 💡 Why It Matters

When a class does too many things:

- It becomes **hard to understand** 
    
- A change in one area can **break another** 
    
- It’s **hard to test** because it has too many dependencies
    
- It **violates modularity**, making refactoring painful later
    

SRP encourages you to **separate concerns** — so every piece of your code focuses on a single job.

---

## ⚙️ Example (without SRP ❌)

Imagine this Unity-style `Player` class:

```csharp
public class Player : MonoBehaviour
{
    void Update()
    {
        Move();
        Jump();
        PlaySound();
        SaveData();
    }

    void Move() { /* movement logic */ }
    void Jump() { /* jumping logic */ }
    void PlaySound() { /* audio logic */ }
    void SaveData() { /* save player data */ }
}
```

🔴 This `Player` class has **multiple responsibilities**:

- movement logic
    
- audio control
    
- saving progress
    

So if the sound system changes, or saving logic changes — you have to modify this same class for unrelated reasons.  
That’s exactly what SRP warns against.

---

## ✅ Example (with SRP)

Split each responsibility into its own class:

```csharp
public class PlayerMovement : MonoBehaviour
{
    public void Move() { /* movement logic */ }
    public void Jump() { /* jumping logic */ }
}

public class PlayerAudio : MonoBehaviour
{
    public void PlaySound() { /* audio logic */ }
}

public class PlayerSaveSystem : MonoBehaviour
{
    public void SaveData() { /* saving logic */ }
}
```

Then, have a lightweight `Player` controller coordinate them:

```csharp
[RequireComponent(typeof(PlayerMovement), typeof(PlayerAudio), typeof(PlayerSaveSystem))]
public class Player : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerAudio audioSystem;
    private PlayerSaveSystem saveSystem;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        audioSystem = GetComponent<PlayerAudio>();
        saveSystem = GetComponent<PlayerSaveSystem>();
    }
}
```

✅ Now:

- If you change saving logic → only update `PlayerSaveSystem`
    
- If you change sound logic → only update `PlayerAudio`
    
- Each class has **one reason to change**
    

---

## 🧠 In a Game Engine Context

In your 2D game engine, SRP applies everywhere:

|Module|Example of “Single Responsibility”|
|---|---|
|`EngineTime`|Only tracks time and deltaTime — not physics or input|
|`InputManager`|Only gathers user input — not decide what to do with it|
|`PhysicsSystem`|Only calculates collisions and forces — not rendering|
|`RenderManager`|Only draws things — not manages game state|
|`AudioManager`|Only handles sound — not gameplay logic|

This separation lets you **swap**, **extend**, and **test** systems independently.


> `Source`: Level up your code with Game Programming Pattern