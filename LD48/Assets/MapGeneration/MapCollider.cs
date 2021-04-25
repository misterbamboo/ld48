using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.MapGeneration
{
    public class MapCollider
    {
        private Map map;

        public MapCollider(Map map)
        {
            this.map = map;
        }

        public IEnumerable<Vector2[]> GetCollisionPoints(int fromY, int toY)
        {
            var colliderContext = new MapColliderContext(map.Configuration.width, toY - fromY, fromY);
            for (int y = fromY; y < toY; y++)
            {
                for (int x = 0; x < map.Configuration.width; x++)
                {
                    AddColliderIfNeeded(colliderContext, x, y);
                }
            }
            return colliderContext.GetCollisionPoints();
        }

        private void AddColliderIfNeeded(MapColliderContext colliderContext, int x, int y)
        {
            if (map.IsEmpty(x, y))
            {
                return;
            }

            AddColliderIfOnSide(colliderContext, x, y);
            colliderContext.Flush();
        }

        private void AddColliderIfOnSide(MapColliderContext colliderContext, int x, int y)
        {
            bool shouldAddCollider = false;
            shouldAddCollider |= map.IsEmpty(x - 1, y);
            shouldAddCollider |= map.IsEmpty(x + 1, y);
            shouldAddCollider |= map.IsEmpty(x, y - 1);
            shouldAddCollider |= map.IsEmpty(x, y + 1);

            if (shouldAddCollider)
            {
                colliderContext.AddSquareCollider(x, y);
            }
        }
    }

    public class MapColliderContext
    {
        private int width;
        private int height;
        private int offset;

        private MapColliderGroup _currentGroup;
        private MapColliderGroup CurrentGroup
        {
            get
            {
                if (_currentGroup == null)
                {
                    _currentGroup = new MapColliderGroup();
                }
                return _currentGroup;
            }
        }

        private List<MapColliderGroup> mapColliderGroups = new List<MapColliderGroup>();

        public MapColliderContext(int width, int height, int offset)
        {
            this.width = width;
            this.height = height;
            this.offset = offset;
        }

        public void AddSquareCollider(int x, int y)
        {
            CurrentGroup.Add(new Vector2(x, -y));
            CurrentGroup.Add(new Vector2(x + 1, -y));
            CurrentGroup.Add(new Vector2(x + 1, -y - 1));
            CurrentGroup.Add(new Vector2(x, -y - 1));
        }

        public void Flush()
        {
            if (_currentGroup != null)
            {
                mapColliderGroups.Add(_currentGroup);
            }
            _currentGroup = null;
        }

        public IEnumerable<Vector2[]> GetCollisionPoints()
        {
            return mapColliderGroups.Select(g => g.GetPoints()).ToList();
        }
    }

    public class MapColliderGroup
    {
        private List<Vector2> Vectors { get; } = new List<Vector2>();

        public void Add(Vector2 vector)
        {
            Vectors.Add(vector);
        }

        public bool Contains(Vector2 vector)
        {
            return Vectors.Any(v => v == vector);
        }

        public Vector2[] GetPoints()
        {
            return Vectors.ToArray();
        }

        public MapColliderGroupBounds GetBounds()
        {
            MapColliderGroupBounds bounds = null;
            foreach (var vector in Vectors)
            {
                if (bounds == null)
                {
                    bounds = new MapColliderGroupBounds();
                    bounds.MaxX = bounds.MinX = (int)vector.x;
                    bounds.MaxY = bounds.MinY = -(int)vector.y;
                }
                else
                {
                    if (vector.x < bounds.MinX) bounds.MinX = (int)vector.x;
                    if (vector.x > bounds.MaxX) bounds.MaxX = (int)vector.x;
                    if (vector.y < bounds.MinY) bounds.MinY = -(int)vector.y;
                    if (vector.y > bounds.MaxY) bounds.MaxY = -(int)vector.y;
                }
            }
            return bounds;
        }
    }

    public class MapColliderGroupBounds
    {
        public int MinX { get; set; }
        public int MaxX { get; set; }
        public int MinY { get; set; }
        public int MaxY { get; set; }
    }
}
