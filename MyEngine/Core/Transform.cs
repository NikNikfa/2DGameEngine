using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MyEngine.Core
{
    public class Transform
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Origin { get; set; } = Vector2.Zero;

        // Collision can be smaller than visual
        public Vector2 CollisionSize { get; set; }
        public Vector2 CollisionOffset { get; set; } = Vector2.Zero;


        public Transform(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
            CollisionSize = size; // default: same as sprite
        }

        // Visual bounds (used for drawing if you want rectangle drawing)
        public Rectangle VisualBounds =>
            new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

        // Collision bounds (used for collision checks)
        public Rectangle CollisionBounds =>
            new Rectangle(
                (int)(Position.X + CollisionOffset.X),
                (int)(Position.Y + CollisionOffset.Y),
                (int)CollisionSize.X,
                (int)CollisionSize.Y
            );
    }
}
