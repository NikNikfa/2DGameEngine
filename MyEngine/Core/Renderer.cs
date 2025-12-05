using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyEngine.Core
{
    public static class Renderer
    {
        private static GraphicsDevice _graphicsDevice;
        public static SpriteBatch SpriteBatch { get; private set; }

        public static void Initialize(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice ?? throw new ArgumentNullException(nameof(graphicsDevice));
            SpriteBatch = new SpriteBatch(_graphicsDevice);
        }

        public static void Begin(Matrix? transform = null)
        {
            if (transform.HasValue)
                SpriteBatch.Begin(transformMatrix: transform.Value);
            else
                SpriteBatch.Begin();
        }

        public static void End()
        {
            SpriteBatch.End();
        }

        public static void Clear(Color color)
        {
            _graphicsDevice.Clear(color);
        }
    }
}
