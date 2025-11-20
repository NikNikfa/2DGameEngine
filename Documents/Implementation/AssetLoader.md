
## 📘 Overview
`AssetLoader` is responsible for **loading and managing game assets** such as textures, sounds, and fonts.  
It provides a **centralized, reusable interface** for accessing assets in the engine, keeping the game code clean and decoupled from the MonoGame `ContentManager`.

---

## ⚙️ Responsibilities
- Load and store textures, sounds, and other assets from the **Content pipeline**  
- Provide **easy access** to assets through static methods  
- Prevent multiple loads of the same asset (caching)  
- Simplify content management for `Game1` and other systems

---

## 🧩 Structure

```csharp
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public static class AssetLoader
{
    private static ContentManager _content;
    private static Dictionary<string, Texture2D> _textures = new();

    // Initialize with the ContentManager from Game1
    public static void Initialize(ContentManager content)
    {
        _content = content;
    }

    // Load a texture by name (cached)
    public static Texture2D LoadTexture(string assetName)
    {
        if (!_textures.ContainsKey(assetName))
        {
            _textures[assetName] = _content.Load<Texture2D>(assetName);
        }
        return _textures[assetName];
    }

    // Optional: methods for sounds, fonts, etc.
}
```

## 🔁 How It Works

1. **Initialization:**  
    `AssetLoader.Initialize(Content)` is called in `Game1.LoadContent()`.
    
2. **Loading Assets:**  
    When an asset is requested via `LoadTexture("Player")`, the loader:
    
    - Checks if the asset is already loaded in the cache (`_textures`)
        
    - If not, loads it using MonoGame’s `ContentManager` and stores it in the cache
        
    - Returns the loaded texture
        
3. **Accessing Assets:**
    
    - Any system or entity can call `AssetLoader.LoadTexture("AssetName")` to get the texture without worrying about multiple loads.
        

---

## 🔗 Integration with Engine

- **Used by:**
    
    - `Game1.cs` → to load all game textures at startup
        
    - `Player.cs` → receives a texture reference from `AssetLoader`
        
    - Future game objects → can retrieve assets easily
        
- **Related Files:**
    
    - `Game1.cs` (LoadContent method)
        
    - `Player.cs`
        

---

## 🧠 Design Notes

- Implemented as a **static class** to ensure global accessibility.
    
- Caching prevents **redundant loads**, improving performance.
    
- Follows **Single Responsibility Principle**: only manages asset loading, not rendering or gameplay logic.
    

---

## 🧪 Example Usage

```csharp
// In Game1.LoadContent()
AssetLoader.Initialize(Content);
Texture2D playerTexture = AssetLoader.LoadTexture("Player");

// Pass texture to Player
_player = new Player(playerTexture, new Vector2(200, 200));

```

## ⚠️ Known Issues

- Currently only handles textures; sounds/fonts need to be added separately.
    
- Does not handle dynamic asset unloading (all assets remain in memory until the game exits).
    

---

## 🗒 Future Improvements

- Add support for **sound and font loading**
    
- Implement **automatic unloading** for unused assets
    
- Optionally allow **reloading assets at runtime** for hot-swapping textures