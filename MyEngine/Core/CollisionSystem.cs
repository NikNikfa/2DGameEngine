using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MyEngine.Entities;

namespace MyEngine.Core
{
    public class CollisionSystem
    {
        // Returns all overlapping pairs found this frame
        public List<(Entity A, Entity B)> GetCollisions(IReadOnlyList<Entity> entities)
        {
            var collisions = new List<(Entity A, Entity B)>();

            if (entities == null || entities.Count < 2)
                return collisions;

            // Simple O(n^2) pair check (enough for this engine stage)
            for (int i = 0; i < entities.Count; i++)
            {
                var a = entities[i];
                if (a == null || !a.IsActive || !a.IsCollidable)
                    continue;

                Rectangle aBounds = a.Bounds;

                for (int j = i + 1; j < entities.Count; j++)
                {
                    var b = entities[j];
                    if (b == null || !b.IsActive || !b.IsCollidable)
                        continue;

                    if (aBounds.Intersects(b.Bounds))
                    {
                        collisions.Add((a, b));
                    }
                }
            }

            return collisions;
        }
    }
}

