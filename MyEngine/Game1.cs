using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyEngine.Core;
using MyGame.Scenes;
using MyEngine.Entities;

namespace MyEngine
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;


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
            Renderer.Initialize(GraphicsDevice);
            AssetLoader.Initialize(Content);

            // Start with the main gameplay scene
            SceneManager.ChangeScene(new GameScene());
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

            SceneManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Renderer.Clear(Color.CornflowerBlue);

            // Ask the current scene for its view matrix.
            // If there is no scene yet, use identity.
            Matrix viewMatrix = Matrix.Identity;

            if (SceneManager.CurrentScene != null)
            {
                viewMatrix = SceneManager.CurrentScene.GetViewMatrix(GraphicsDevice.Viewport);
            }

            Renderer.Begin(viewMatrix);
            SceneManager.Draw(Renderer.SpriteBatch);
            Renderer.End();

            base.Draw(gameTime);
        }
    }
}
