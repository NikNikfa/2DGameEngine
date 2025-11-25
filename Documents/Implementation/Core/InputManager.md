## 📘 Overview

The `InputManager` class is responsible for receiving, processing, and managing all user inputs in the game engine.  
This class acts as an interface between **input hardware** (such as Keyboard and GamePad) and the game systems.  

---

## ⚙️ Responsibilities
- Detecting key presses and releases (Keyboard)  
- Checking the status of GamePad buttons  
- Providing simple methods for checking inputs in game logic  
- Completely separating input logic from other parts (following the Single Responsibility principle)  

---

## 🧩 Structure

```csharp
// InputManager.cs
using Microsoft.Xna.Framework.Input;

public class InputManager
{
    private KeyboardState _currentKeyboardState;
    private KeyboardState _previousKeyboardState;
    
    public void Update()
    {
        _previousKeyboardState = _currentKeyboardState;
        _currentKeyboardState = Keyboard.GetState();
    }

    // key pressed?
    public static bool IsKeyDown(Keys key)
    {
        return _currentKeyboardState.IsKeyDown(key);
    }

    // has key been pressed again in the same frame?
    public static bool IsKeyPressed(Keys key)
    {
        return _currentKeyboardState.IsKeyDown(key) && !_previousKeyboardState.IsKeyDown(key);
    }

    // key released?
    public static bool IsKeyReleased(Keys key)
    {
        return !_currentKeyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyDown(key);
    }

}
```

```csharp
// Game1.cs
protected override void Update(GameTime gameTime)
{
    EngineTime.Update(gameTime); // Update Time

    InputManager.Update(); // Update Inputs

    // using InputManager instead of Keyboard.GetState()
    if (InputManager.IsKeyPressed(Keys.Escape))
        Exit();
    
    base.Update(gameTime);
}
```

## 🔁 How It Works

1. In each cycle of `Game.Update()`, the `Update()` method in `InputManager` is called.
    
2. The current and previous states of the keyboard and GamePad are stored.
    
3. By comparing the two consecutive states, it is possible to detect which key has just been pressed or released.
    
4. Other systems (such as `PlayerController` or `UIManager`) check inputs using methods like `IsKeyPressed` or `IsGamePadButtonPressed`.