
## 📘 Overview
`Player` represents the controllable character in the game.  
This class manages player movement, state, and interactions with the game world.  
It receives input through `InputManager` and updates position and actions accordingly.

---

## ⚙️ Responsibilities
- Store player properties such as **position**, **velocity**, and **state**  
- Handle **movement** based on input  
- Interact with game **physics** and boundaries  
- Update the player's visual representation (sprite/animations)  
- Provide a clean interface for the **Game1** class to call in the game loop

---

## 🧩 Structure

```csharp
    public class Player
    {
        private Texture2D _texture;
        private Vector2 _position;
        private float _speed = 200f; // PPS

        public Player(Texture2D texture, Vector2 startPosition)
        {
            _texture = texture;
            _position = startPosition;
        }

        public void Update()
        {
            Vector2 direction = Vector2.Zero;

            if (InputManager.IsKeyDown(Keys.Up))
                direction.Y -= 1;
            if (InputManager.IsKeyDown(Keys.Down))
                direction.Y += 1;
            if (InputManager.IsKeyDown(Keys.Left))
                direction.X -= 1;
            if (InputManager.IsKeyDown(Keys.Right))
                direction.X += 1;

            if (direction != Vector2.Zero)
                direction.Normalize(); // 

            _position += direction * _speed * EngineTime.DeltaTime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }

    }
```

And in Game1.cs we have:

```csharp
//Game1.cs
private GraphicsDeviceManager _graphics;
private SpriteBatch _spriteBatch;

private Player _player;
private Texture2D _playerTexture;

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Building simple texture for player
            _playerTexture = new Texture2D(GraphicsDevice, 50, 50);
            Color[] data = new Color[50 * 50];
            for (int i = 0; i < data.Length; i++) data[i] = Color.Red;
            _playerTexture.SetData(data);

            _player = new Player(_playerTexture, new Vector2(200, 200));
        }
        
        protected override void Update(GameTime gameTime)
       {

           EngineTime.Update(gameTime); //Update Time

           InputManager.Update(); //Update Inputs

           //using InputManager instead of Keyboard.GetState()
           if (InputManager.IsKeyPressed(Keys.Escape))
               Exit();

           _player.Update();


           base.Update(gameTime);
       }

       protected override void Draw(GameTime gameTime)
       {
           GraphicsDevice.Clear(Color.CornflowerBlue);

           _spriteBatch.Begin();
           _player.Draw(_spriteBatch);
           _spriteBatch.End();

           base.Draw(gameTime);
       }
```

## 🔁 How It Works

1. **Initialization:**  
	
    `Player` is initialized with a reference to the `Texture2D` and a starting position in `LoadContent()` method in `Game1.cs`
    
2. **Update Loop:**
	    
    - Checks input using `_inputManager`.
        
    - Calculates movement vector (`Velocity`) and normalizes it.
        
    - Updates `Position` based on `Velocity`, `Speed`, and `DeltaTime`.
        
3. **Rendering:**
	    
    - Draws the player sprite at the current `Position` using [[SpriteBatch]].
        
4. **Integration with Game1.cs:**
	- `Player(Texture2D texture, Vector2 startPosition)` is called inside `Game1.Update()`.
		
    - `Player.Update(gameTime)` is called inside `Game1.Update()`.
        
    - `Player.Draw(spriteBatch, texture)` is called inside `Game1.Draw()`.


## 🧠 Design Notes

- Player logic is **[[Decoupled]]** from `Game1` to keep update/render separate.
    
- Movement logic uses normalized velocity to prevent diagonal speed issues.
    
- Single Responsibility: Player only handles **movement and rendering**, not input or physics.