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
    public class Obstacle : Entity
    {
        public Obstacle(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
            Layer = RenderLayer.World;
            IsCollidable = true;


            // Example: smaller collision box than sprite
            Transform.CollisionSize = new Vector2(texture.Width * 0.4f, texture.Height * 0.6f);

            // Center the collision box inside the sprite
            Transform.CollisionOffset = (Transform.Size - Transform.CollisionSize) * 0.5f;

        }
    }
}
