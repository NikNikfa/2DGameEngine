using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyEngine.Core;
using MyEngine.Entities;

namespace MyGame.Scenes
{
    public class GameScene : Scene
    {
        private EntityManager _entityManager;
        private Player _player;
        private Camera2D _camera;

        public override void Load()
        {
            _entityManager = new EntityManager();
            _camera = new Camera2D();

            // Load player texture via AssetLoader (centralized content access)
            var playerTexture = AssetLoader.LoadTexture("Player");

            // Initial player position in world space
            _player = new Player(playerTexture, new Vector2(200, 200));

            _entityManager.Add(_player);
        }

        public override void Unload()
        {
            // For now just drop references; more complex scenes could dispose extra resources here
            _entityManager = null;
            _player = null;
            _camera = null;
        }

        public override void Update(GameTime gameTime)
        {
            // Update all entities (Player included)
            _entityManager?.Update(gameTime);

            // Simple camera follow: lock camera to player position
            if (_player != null && _camera != null)
            {
                _camera.Position = _player.Position;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw all entities
            _entityManager?.Draw(spriteBatch);
        }

        public override Matrix GetViewMatrix(Viewport viewport)
        {
            // Use the scene-local camera; fall back to identity if something is missing
            if (_camera == null)
                return Matrix.Identity;

            return _camera.GetViewMatrix(viewport);
        }
    }
}

