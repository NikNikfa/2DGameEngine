using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyEngine.Core;

namespace MyEngine.Entities
{
    public class Player : Entity
    {
        private float _speed = 200f; // units per second

        public Vector2 PreviousPosition { get; private set; }

        public Player(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
            Layer = RenderLayer.Foreground;

            // Example: smaller collision box than sprite
            Transform.CollisionSize = new Vector2(texture.Width * 0.4f, texture.Height * 0.6f);

            // Center the collision box inside the sprite
            Transform.CollisionOffset = (Transform.Size - Transform.CollisionSize) * 0.5f;
        }


        public override void Update(GameTime gameTime)
        {
            Vector2 direction = Vector2.Zero;

            // Horizontal input
            if (InputManager.IsKeyDown(Keys.A) || InputManager.IsKeyDown(Keys.Left))
                direction.X -= 1f;

            if (InputManager.IsKeyDown(Keys.D) || InputManager.IsKeyDown(Keys.Right))
                direction.X += 1f;

            // Vertical input
            if (InputManager.IsKeyDown(Keys.W) || InputManager.IsKeyDown(Keys.Up))
                direction.Y -= 1f;

            if (InputManager.IsKeyDown(Keys.S) || InputManager.IsKeyDown(Keys.Down))
                direction.Y += 1f;

            // Normalize to avoid faster diagonal movement
            if (direction != Vector2.Zero)
            {
                direction.Normalize();

                // Save position BEFORE moving (last safe position)
                PreviousPosition = Transform.Position;


                Transform.Position += direction * _speed * EngineTime.DeltaTime;
            }

        }

    
}
}
