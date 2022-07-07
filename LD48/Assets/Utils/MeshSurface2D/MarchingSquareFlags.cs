namespace MeshSurface2D
{
    public enum MarchingSquareFlags
    {
        // Flags
        Empty = 0,
        TopLeft = 1,
        TopRight = 2,
        BottomLeft = 4,
        BottomRight = 8,
        // Combinations of flags
        Top = 3,
        Left = 5,
        BottomLeftToTopRight = 6,
        AllButBottomRight = 7,
        TopLeftToBottomRight = 9,
        Right = 10,
        AllButBottomLeft = 11,
        Bottom = 12,
        AllButTopRight = 13,
        AllButTopLeft = 14,
        Full = 15,
    }
}
