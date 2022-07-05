using MeshSurface2DPresentation;
using UnityEngine;

namespace Assets.MapGeneration
{
    public class MapTerrainValueProvider : IMeshPointValueProvider
    {
        private MapConfig config;

        public MapTerrainValueProvider(MapConfig config, int fromY, int toY)
        {
            this.config = config;
            Height = toY - fromY;
            FromY = fromY;
        }

        public float Width => config.width;

        public float Height { get; private set; }

        private int FromY { get; }

        public float ValueAt(float x, float y)
        {
            return GetTerrainNoiseValue(config.width, x, y);
        }

        private float GetTerrainNoiseValue(float mapSize, float x, float y)
        {
            var adjustedY = Height - y;
            var targetY = adjustedY + FromY;
            float xRatio = x * config.baseNoiseSize / mapSize;
            float yRatio = targetY * config.baseNoiseSize / mapSize;

            var probability = 1f - Mathf.Pow((float)System.Math.E, -config.deepnessK * targetY);
            var value = Mathf.PerlinNoise(xRatio, yRatio) * config.deepnessWeight;
            return probability * value;
        }
    }
}
