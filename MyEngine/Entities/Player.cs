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
    public class Player
    {
        private Texture2D _texture;
        private Vector2 _position;
        private float _speed = 200f; // PPS

        public Player(Texture2D texture, Vector2 startPosition)
        {
            _texture = texture;
            _position = startPosition;
        }

        public void Update()
        {
            Vector2 direction = Vector2.Zero;

            if (InputManager.IsKeyDown(Keys.Up))
                direction.Y -= 1;
            if (InputManager.IsKeyDown(Keys.Down))
                direction.Y += 1;
            if (InputManager.IsKeyDown(Keys.Left))
                direction.X -= 1;
            if (InputManager.IsKeyDown(Keys.Right))
                direction.X += 1;

            if (direction != Vector2.Zero)
                direction.Normalize(); // 

            _position += direction * _speed * EngineTime.DeltaTime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }

    }
}
