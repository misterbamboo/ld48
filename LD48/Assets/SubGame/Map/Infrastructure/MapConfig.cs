using System;

namespace Assets.Map.Infrastructure
{
    [Serializable]
    public class MapConfig
    {
        public MapConfig()
        {

        }

        public int width = 255;
        public int mapPageSize = 255;
        public float mapDisplayScale = 0.1f;

        // https://www.desmos.com/calculator/vdhgxngtsd
        public float baseNoiseSize = 10;
        public float deepnessK = 0.02f;
        public float deepnessWeight = 2;

        public float ressourceNoiseSize = 25;

        // https://www.desmos.com/calculator/8fbff0c9c8
        public float ressourceDensityValueMultiplicator = 0.1f;
        public float ressourceDensityValueHeightShift = 0.1f;
    }
}
