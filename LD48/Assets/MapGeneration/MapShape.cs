using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.MapGeneration
{
    public class MapShape
    {
        public int Key { get; private set; }
        List<Vector2> orderedVectors = new List<Vector2>();

        public IEnumerable<Vector2> OrderedVectors => orderedVectors;

        public MapShape(int key, IEnumerable<Vector2> vectors)
        {
            Key = key;
            AddVectors(vectors);
        }

        private void AddVectors(IEnumerable<Vector2> vectors)
        {
            var pool = vectors.ToList();
            var next = pool.FirstOrDefault();
            Vector2 direction = new Vector2(1, 0); // right for start
            while (pool.Any())
            {
                var current = next;

                orderedVectors.Add(current);
                pool.Remove(current);

                if (!pool.Any())
                {
                    break;
                }

                var neer = Nearest(pool, current, direction);
                if (!neer.HasValue)
                {
                    break;
                }

                direction = (next - current).normalized;
                next = neer.Value;
            }

            if (next != null)
            {
                orderedVectors.Add(next);
            }
        }

        private Vector2? Nearest(IEnumerable<Vector2> poolVectors, Vector2 vector, Vector2 direction)
        {
            // default (right) = direction.x > 0
            var checkOrder = new[]
            {
               new Func<IEnumerable<Vector2>, Vector2, int, Vector2>((pv, v, d) => CheckRightNearest(pv, v, d)),
               new Func<IEnumerable<Vector2>, Vector2, int, Vector2>((pv, v, d) => CheckBottomNearest(pv, v, d)),
               new Func<IEnumerable<Vector2>, Vector2, int, Vector2>((pv, v, d) => CheckLeftNearest(pv, v, d)),
               new Func<IEnumerable<Vector2>, Vector2, int, Vector2>((pv, v, d) => CheckTopNearest(pv, v, d)),
            };

            // up
            if (direction.x == 0 && direction.y < 0)
            {
                checkOrder = new[]
                {
                   new Func<IEnumerable<Vector2>, Vector2, int, Vector2>((pv, v, d) => CheckTopNearest(pv, v, d)),
                   new Func<IEnumerable<Vector2>, Vector2, int, Vector2>((pv, v, d) => CheckRightNearest(pv, v, d)),
                   new Func<IEnumerable<Vector2>, Vector2, int, Vector2>((pv, v, d) => CheckBottomNearest(pv, v, d)),
                   new Func<IEnumerable<Vector2>, Vector2, int, Vector2>((pv, v, d) => CheckLeftNearest(pv, v, d)),
                };
            }

            // left
            if (direction.x < 0)
            {
                checkOrder = new[]
                {
                   new Func<IEnumerable<Vector2>, Vector2, int, Vector2>((pv, v, d) => CheckLeftNearest(pv, v, d)),
                   new Func<IEnumerable<Vector2>, Vector2, int, Vector2>((pv, v, d) => CheckTopNearest(pv, v, d)),
                   new Func<IEnumerable<Vector2>, Vector2, int, Vector2>((pv, v, d) => CheckRightNearest(pv, v, d)),
                   new Func<IEnumerable<Vector2>, Vector2, int, Vector2>((pv, v, d) => CheckBottomNearest(pv, v, d)),
                };
            }

            // bottom
            if (direction.x == 0 && direction.y > 0)
            {
                checkOrder = new[]
                {
                   new Func<IEnumerable<Vector2>, Vector2, int, Vector2>((pv, v, d) => CheckBottomNearest(pv, v, d)),
                   new Func<IEnumerable<Vector2>, Vector2, int, Vector2>((pv, v, d) => CheckLeftNearest(pv, v, d)),
                   new Func<IEnumerable<Vector2>, Vector2, int, Vector2>((pv, v, d) => CheckTopNearest(pv, v, d)),
                   new Func<IEnumerable<Vector2>, Vector2, int, Vector2>((pv, v, d) => CheckRightNearest(pv, v, d)),
                };
            }

            int checkDistance = 1;
            while (checkDistance < 5)
            {
                for (int i = 0; i < checkOrder.Length; i++)
                {
                    var found = checkOrder[i](poolVectors, vector, checkDistance);
                    if (found != Vector2.zero) return found;
                }

                checkDistance++;
            }

            return null;
        }

        private static Vector2 CheckTopNearest(IEnumerable<Vector2> poolVectors, Vector2 vector, int checkDistance)
        {
            for (int x = -checkDistance; x <= checkDistance; x++)
            {
                var found = poolVectors.FirstOrDefault(p => p == vector + new Vector2(x, -checkDistance));
                if (found != Vector2.zero)
                {
                    return found;
                }
            }
            return default;
        }

        private static Vector2 CheckLeftNearest(IEnumerable<Vector2> poolVectors, Vector2 vector, int checkDistance)
        {
            for (int y = checkDistance; y >= -checkDistance; y--)
            {
                var found = poolVectors.FirstOrDefault(p => p == vector + new Vector2(-checkDistance, y));
                if (found != Vector2.zero)
                {
                    return found;
                }
            }
            return default;
        }

        private static Vector2 CheckBottomNearest(IEnumerable<Vector2> poolVectors, Vector2 vector, int checkDistance)
        {
            for (int x = checkDistance; x >= -checkDistance; x--)
            {
                var found = poolVectors.FirstOrDefault(p => p == vector + new Vector2(x, checkDistance));
                if (found != Vector2.zero)
                {
                    return found;
                }
            }
            return default;
        }

        private static Vector2 CheckRightNearest(IEnumerable<Vector2> poolVectors, Vector2 vector, int checkDistance)
        {
            for (int y = -checkDistance; y <= checkDistance; y++)
            {
                var found = poolVectors.FirstOrDefault(p => p == vector + new Vector2(checkDistance, y));
                if (found != Vector2.zero)
                {
                    return found;
                }
            }
            return default;
        }
    }
}
