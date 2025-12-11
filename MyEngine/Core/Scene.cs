using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyEngine.Core
{
    public abstract class Scene
    {
        protected Scene() { }

        public virtual void Load() { }
        public virtual void Unload() { }

        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }

        public virtual Matrix GetViewMatrix(Viewport viewport)
        {
            // Default: no camera transform (identity)
            return Matrix.Identity;
        }
    }
}

