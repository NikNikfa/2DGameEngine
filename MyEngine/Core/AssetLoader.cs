using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyEngine.Core
{
    public static class AssetLoader
    {
        private static ContentManager _content;

        public static void Initialize(ContentManager content)
        {
            _content = content;
        }

        public static Texture2D LoadTexture(string assetName)
        {
            return _content.Load<Texture2D>(assetName);
        }
    }
}
