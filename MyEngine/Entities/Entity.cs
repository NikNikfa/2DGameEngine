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
        public Transform Transform { get; protected set; }

        protected Texture2D Texture { get; private set; }

        public bool IsActive { get; set; } = true;

        public RenderLayer Layer { get; set; }

        public bool IsCollidable { get; set; } = true;

        public Rectangle Bounds => Transform.CollisionBounds;




        protected Entity(Texture2D texture, Vector2 position)
        {
            Texture = texture;

            // Create a Transform using texture size
            Transform = new Transform(
                position,
                new Vector2(texture.Width, texture.Height)
            );

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

            spriteBatch.Draw(Texture, Transform.Position, Color.White);
        }
    }
}
