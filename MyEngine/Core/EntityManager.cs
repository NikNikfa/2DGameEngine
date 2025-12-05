using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyEngine.Entities;

namespace MyEngine.Core
{
    public class EntityManager
    {
        private readonly List<Entity> _entities = new List<Entity>();

        public void Add(Entity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _entities.Add(entity);
        }

        public void Remove(Entity entity)
        {
            if (entity == null)
                return;

            _entities.Remove(entity);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var entity in _entities.ToList())
            {
                if (!entity.IsActive)
                    continue;

                entity.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in _entities
                .Where(e => e.IsActive)
                .OrderBy(e => e.Layer))
            {
                entity.Draw(spriteBatch);
            }
        }
    }
}