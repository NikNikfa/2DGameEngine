
## 1. Overview

`Entity` is the **base class for all game objects** in the 2D Game Engine.  
It provides:

- A shared **transform** (position, rotation, scale)
    
- Basic **rendering support**
    
- A unified **Update / Draw API**
    

Purpose: remove duplicated code in classes like `Player`, improve maintainability, and prepare the engine for future systems (Scene, Physics, Components).

---

## 2. Responsibilities

`Entity` **is responsible for**:

- **Transform data**
    
    - `Position`
        
    - `Rotation`
        
    - `Scale`
        
    - `Origin`
        
- **Rendering properties**
    
    - `Texture`
        
    - `Color`
        
    - `LayerDepth`
        
- **State flags**
    
    - `IsActive` → should this entity update?
        
    - `IsVisible` → should this entity render?
        
- Providing **virtual methods**:
    
    - `Update(GameTime)`
        
    - `Draw(SpriteBatch)`
        

---

## 3. Non-Responsibilities

`Entity` does **NOT** handle:

- Input
    
- Physics logic (collision, velocity, forces)
    
- Scene or world management (adding/removing entities)
    

These belong to other systems.

---

## 4. Public API

### 4.1 Properties

```csharp
public class Entity
{
    // Transform
    public Vector2 Position { get; set; }
    public float Rotation { get; set; } = 0f;
    public Vector2 Scale { get; set; } = Vector2.One;
    public Vector2 Origin { get; set; } = Vector2.Zero;

    // Rendering
    public Texture2D Texture { get; protected set; }
    public Color Color { get; set; } = Color.White;
    public float LayerDepth { get; set; } = 0f;

    // State
    public bool IsActive { get; set; } = true;
    public bool IsVisible { get; set; } = true;

    // Optional identifier
    public string Name { get; set; }
}

```

### 4.2 Constructor
```csharp
public Entity(Texture2D texture, Vector2 position, string name = null)
{
    Texture = texture ?? throw new ArgumentNullException(nameof(texture));
    Position = position;
    Name = name;

    // Default origin = center of texture
    Origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
}

```
### 4.3 Virtual Methods
```csharp
public virtual void Update(GameTime gameTime)
{
    // Base implementation is empty.
}

public virtual void Draw(SpriteBatch spriteBatch)
{
    if (!IsVisible || Texture == null)
        return;

    spriteBatch.Draw(
        Texture,
        Position,
        sourceRectangle: null,
        color: Color,
        rotation: Rotation,
        origin: Origin,
        scale: Scale,
        effects: SpriteEffects.None,
        layerDepth: LayerDepth
    );
}

```

## 5. Integration in the Engine

### 5.1 Update Phase

Typical per-frame update:

```csharp
EngineTime.Update()
InputManager.Update()
entity.Update()
```

Only entities with `IsActive == true` are updated.

---

### 5.2 Draw Phase

Inside `Game1.Draw()` (or Scene.Draw in the future):
```csharp
if (entity.IsVisible)
    entity.Draw()
```

`Entity.Draw()` handles basic SpriteBatch drawing.

---

## 6. Example: Player Inheriting from Entity

```csharp
public class Player : Entity
{
    public float Speed { get; set; } = 200f;

    public Player(Texture2D texture, Vector2 position)
        : base(texture, position, "Player")
    {
    }

    public override void Update(GameTime gameTime)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        Vector2 direction = Vector2.Zero;

        if (InputManager.IsKeyDown(Keys.W))
            direction.Y -= 1;
        if (InputManager.IsKeyDown(Keys.S))
            direction.Y += 1;
        if (InputManager.IsKeyDown(Keys.A))
            direction.X -= 1;
        if (InputManager.IsKeyDown(Keys.D))
            direction.X += 1;

        if (direction != Vector2.Zero)
        {
            direction.Normalize();
            Position += direction * Speed * delta;
        }
    }
}

```
