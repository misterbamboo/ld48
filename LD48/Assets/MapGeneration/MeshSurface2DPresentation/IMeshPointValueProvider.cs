namespace MeshSurface2DPresentation
{
    public interface IMeshPointValueProvider
    {
        float Width { get; }

        float Height { get; }

        float ValueAt(float x, float y);
    }
}
