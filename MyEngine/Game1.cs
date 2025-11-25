using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyEngine.Core;
using MyEngine.Entities;

namespace MyEngine
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private EntityManager _entityManager;

        private Player _player;
     

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _entityManager = new EntityManager();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            AssetLoader.Initialize(Content);
            var playerTexture = AssetLoader.LoadTexture("Player");
            _player = new Player(playerTexture, new Vector2(200, 200));

            _entityManager.Add(_player);
        }

        protected override void Update(GameTime gameTime)
        {
            // 1. Update engine time
            EngineTime.Update(gameTime);

            // 2. Update input system
            InputManager.Update();

            // 3. Global shortcuts
            if (InputManager.IsKeyPressed(Keys.Escape))
                Exit();

            // 4. Update all entities (Player, Enemy, NPCs, etc.)
            _entityManager.Update(gameTime);

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
    }
}
