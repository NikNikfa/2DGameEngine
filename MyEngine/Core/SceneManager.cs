using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyEngine.Core
{
    public static class SceneManager
    {
        private static Scene _currentScene;

        public static Scene CurrentScene => _currentScene;

        public static void ChangeScene(Scene newScene)
        {
            if (newScene == null)
                throw new ArgumentNullException(nameof(newScene));

            // Unload the previous scene if there is one
            _currentScene?.Unload();

            // Switch
            _currentScene = newScene;

            // Load the new scene
            _currentScene.Load();
        }

        public static void Update(GameTime gameTime)
        {
            _currentScene?.Update(gameTime);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            _currentScene?.Draw(spriteBatch);
        }
    }
}
