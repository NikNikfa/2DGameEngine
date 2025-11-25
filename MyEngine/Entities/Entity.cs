using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyEngine.Entities
{
    public abstract class Entity
    {
        public Vector2 Position { get; set; }

        protected Texture2D Texture { get; private set; }

        public bool IsActive { get; set; } = true;

        protected Entity(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
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
