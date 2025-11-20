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

        private Player _player;
        private Texture2D _playerTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            AssetLoader.Initialize(Content);
            _playerTexture = AssetLoader.LoadTexture("Player");

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
    }
}
