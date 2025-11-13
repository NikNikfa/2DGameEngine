using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MyEngine.Core
{
    public class EngineTime
    {
        public static float DeltaTime { get; private set; }
        public static float TotalTime { get; private set; }

        public static void Update(GameTime gameTime)
        {
            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            TotalTime += DeltaTime;

        }
    }
}

