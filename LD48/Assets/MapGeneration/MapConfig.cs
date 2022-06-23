using System;

namespace Assets.MapGeneration
{
    [Serializable]
    public class MapConfig
    {
        public int width = 100;

        public float baseNoiseSize = 10;

        // https://www.desmos.com/calculator/dsqneioboj
        public float deepnessDensityValueMultiplicator = -0.002f;
        public float deepnessDensityValueSideShift = -1.1f;
        public float deepnessDensityValueHeightShift = 0.9f;

        public float ressourceNoiseSize = 25;

        // https://www.desmos.com/calculator/8fbff0c9c8
        public float ressourceDensityValueMultiplicator = 0.1f;
        public float ressourceDensityValueHeightShift = 0.1f;
    }
}
