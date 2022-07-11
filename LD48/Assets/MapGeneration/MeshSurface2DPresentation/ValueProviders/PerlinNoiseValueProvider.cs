using UnityEngine;

namespace MeshSurface2DPresentation.ValueProviders
{
    public class PerlinNoiseValueProvider : IMeshPointValueProvider
    {
        public float Width { get; set; }

        public float Height { get; set; }

        public float XRepeat { get; set; }
        public float YRepeat { get; set; }
        public float Weight { get; set; }

        public PerlinNoiseValueProvider(float width, float height)
        {
            Width = width;
            Height = height;
        }

        public float ValueAt(float x, float y)
        {
            return Mathf.PerlinNoise(x / Width / XRepeat, y / Height / YRepeat) * Weight;
        }
    }
}
