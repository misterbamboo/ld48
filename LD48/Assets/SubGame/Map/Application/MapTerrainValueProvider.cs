using Assets.Map.Infrastructure;
using MeshSurface2D;
using UnityEngine;

namespace Assets.Map.Domain
{
    public class MapTerrainValueProvider : IMeshPointValueProvider
    {
        private MapConfig config;

        public MapTerrainValueProvider(MapConfig config, Vector2 from, Vector2 to)
        {
            this.config = config;
            Right = Mathf.Max(from.x, to.x);
            Left = Mathf.Min(from.x, to.x);
            Top = Mathf.Max(from.y, to.y);
            Bottom = Mathf.Min(from.y, to.y);
            Width = Right - Left;
            Height = Top - Bottom;
        }

        public float Left { get; }

        public float Right { get; }

        public float Top { get; }

        public float Bottom { get; }

        public float Width { get; }

        public float Height { get; }

        public float ValueAt(float x, float y)
        {
            return GetTerrainNoiseValue(config.width, x, y);
        }

        private float GetTerrainNoiseValue(float mapSize, float x, float y)
        {
            var deepness = -y;
            float xRatio = x * config.baseNoiseSize / mapSize;
            float yRatio = deepness * config.baseNoiseSize / mapSize;

            var probability = 1f - Mathf.Pow((float)System.Math.E, -config.deepnessK * deepness);
            var value = Mathf.PerlinNoise(xRatio, yRatio) * config.deepnessWeight;
            return probability * value;
        }
    }
}
