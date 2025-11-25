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
        public Player(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
        }

        public override void Update(GameTime gameTime)
        {
            // اینجا بعداً InputManager را صدا می‌زنیم و حرکت Player را می‌نویسیم
        }

        // Draw لازم نیست اگر همان رفتار Entity کافی است
        // اگر خواستی افکت خاصی داشته باشی، می‌توانی override کنی
    }
}
