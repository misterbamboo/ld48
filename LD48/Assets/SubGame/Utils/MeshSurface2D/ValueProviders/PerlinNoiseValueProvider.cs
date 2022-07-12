using UnityEngine;

namespace MeshSurface2D.ValueProviders
{
    public class PerlinNoiseValueProvider : IMeshPointValueProvider
    {
        public float Left { get; }

        public float Right { get; }

        public float Top { get; }

        public float Bottom { get; }

        public float Width { get; }

        public float Height { get; }

        public float XRepeat { get; set; }
        public float YRepeat { get; set; }
        public float Weight { get; set; }

        public PerlinNoiseValueProvider(Vector2 from, Vector2 to)
        {
            Right = Mathf.Max(from.x, to.x);
            Left = Mathf.Min(from.x, to.x);
            Top = Mathf.Max(from.y, to.y);
            Bottom = Mathf.Min(from.y, to.y);
            Width = Right - Left;
            Height = Top - Bottom;
        }

        public float ValueAt(float x, float y)
        {
            return Mathf.PerlinNoise(x / Width / XRepeat, y / Height / YRepeat) * Weight;
        }
    }
}
