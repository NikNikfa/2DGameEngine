using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine.Entities
{
    public abstract class Entity
    {
        public Vector2 Position { get; set; }

        protected Texture2D Texture { get; private set; }

        public bool IsActive { get; set; } = true;

        public RenderLayer Layer { get; set; }


        protected Entity(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;

            // Default: most entities are in the world layer
            Layer = RenderLayer.World;
        }

        public virtual void Update(GameTime gameTime)
        {
            // default: doesn't do anything
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!IsActive)
                return;

            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
