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

        public Player(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
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
                Position += direction * _speed * EngineTime.DeltaTime;
            }

        }

    /
}
}
