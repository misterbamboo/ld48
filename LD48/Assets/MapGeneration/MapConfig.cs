using System;

namespace Assets.MapGeneration
{
    [Serializable]
    public class MapConfig
    {
        public int width = 100;

        public int circlesPerGeneration = 50;
        public int circlesGenerationDispersionMin = 5;
        public int circlesGenerationDispersionMax = 8;

        public int circleMinRadius = 5;
        public int circleMaxRadius = 12;
        public int circleRadiusAddedNoise = 1;
    }
}
