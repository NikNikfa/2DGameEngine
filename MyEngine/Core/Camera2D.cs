using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyEngine.Core
{
    public class Camera2D
    {
        public Vector2 Position { get; set; } = Vector2.Zero;

        private float _zoom = 1f;
        public float Zoom
        {
            get => _zoom;
            set
            {
                // Prevent negative / zero zoom
                _zoom = MathHelper.Max(0.1f, value);
            }
        }

        public float Rotation { get; set; } = 0f;

        public Matrix GetViewMatrix(Viewport viewport)
        {
            var origin = new Vector2(viewport.Width * 0.5f, viewport.Height * 0.5f);

            return
                Matrix.CreateTranslation(new Vector3(-Position, 0f)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(Zoom, Zoom, 1f) *
                Matrix.CreateTranslation(new Vector3(origin, 0f));
        }
    }
}
