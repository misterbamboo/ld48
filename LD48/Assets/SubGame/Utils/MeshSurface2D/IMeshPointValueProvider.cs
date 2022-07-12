namespace MeshSurface2D
{
    public interface IMeshPointValueProvider
    {
        float Left { get; }

        float Right { get; }

        float Top { get; }

        float Bottom { get; }

        float Width { get; }

        float Height { get; }

        float ValueAt(float x, float y);
    }
}
