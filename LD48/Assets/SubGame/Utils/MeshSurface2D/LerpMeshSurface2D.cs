using System.Collections.Generic;
using UnityEngine;

namespace MeshSurface2D
{
    public class LerpMeshSurface2D
    {
        Mesh mesh;
        List<Vector3> vertices = new();
        List<Vector2> uvs = new();
        List<int> triangles = new();
        int verticesIndex = 0;

        float meshScale;
        bool inverted;

        private IMeshPointValueProvider ValueProvider { get; }

        public LerpMeshSurface2D(IMeshPointValueProvider valueProvider)
        {
            meshScale = 1f;
            mesh = new Mesh
            {
                indexFormat = UnityEngine.Rendering.IndexFormat.UInt32
            };
            ValueProvider = valueProvider;
        }

        public void ChangeScale(float scale)
        {
            meshScale = scale;
        }

        public void ChangeInverted(bool inverted)
        {
            this.inverted = inverted;
        }

        public Mesh CreateMesh()
        {
            vertices.Clear();
            uvs.Clear();
            triangles.Clear();
            verticesIndex = 0;

            for (int x = (int)ValueProvider.Left; x < (int)ValueProvider.Width - 1; x++)
            {
                for (int y = (int)ValueProvider.Bottom; y < (int)ValueProvider.Height - 1; y++)
                {
                    CreateTriangles(x, y);
                }
            }

            mesh.Clear();
            mesh.vertices = vertices.ToArray();
            mesh.uv = uvs.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.RecalculateNormals();
            mesh.Optimize();

            return mesh;
        }

        public void CreateTriangles(int x, int y)
        {
            var mode = MarchingSquareFlags.Empty;
            mode |= GetValue(x, y) > 1 ? MarchingSquareFlags.BottomLeft : MarchingSquareFlags.Empty;
            mode |= GetValue(x + 1, y) > 1 ? MarchingSquareFlags.BottomRight : MarchingSquareFlags.Empty;
            mode |= GetValue(x, y + 1) > 1 ? MarchingSquareFlags.TopLeft : MarchingSquareFlags.Empty;
            mode |= GetValue(x + 1, y + 1) > 1 ? MarchingSquareFlags.TopRight : MarchingSquareFlags.Empty;

            switch (mode)
            {
                case MarchingSquareFlags.TopLeft:
                    TopLeftTriangles(x, y);
                    break;
                case MarchingSquareFlags.TopRight:
                    TopRightTriangles(x, y);
                    break;
                case MarchingSquareFlags.Top:
                    TopTriangles(x, y);
                    break;
                case MarchingSquareFlags.BottomLeft:
                    BottomLeftTriangles(x, y);
                    break;
                case MarchingSquareFlags.Left:
                    LeftTriangles(x, y);
                    break;
                case MarchingSquareFlags.BottomLeftToTopRight:
                    BottomLeftToTopRightTriangles(x, y);
                    break;
                case MarchingSquareFlags.AllButBottomRight:
                    AllButBottomRightTriangles(x, y);
                    break;
                case MarchingSquareFlags.BottomRight:
                    BottomRightTriangles(x, y);
                    break;
                case MarchingSquareFlags.TopLeftToBottomRight:
                    TopLeftToBottomRightTriangles(x, y);
                    break;
                case MarchingSquareFlags.Right:
                    RightTriangles(x, y);
                    break;
                case MarchingSquareFlags.AllButBottomLeft:
                    AllButBottomLeftTriangles(x, y);
                    break;
                case MarchingSquareFlags.Bottom:
                    BottomTriangles(x, y);
                    break;
                case MarchingSquareFlags.AllButTopRight:
                    AllButTopRightTriangles(x, y);
                    break;
                case MarchingSquareFlags.AllButTopLeft:
                    AllButTopLeftTriangles(x, y);
                    break;
                case MarchingSquareFlags.Full:
                    FullSquareTriangles(x, y);
                    break;
                case MarchingSquareFlags.Empty:
                default:
                    break;
            }
        }

        private void TopLeftTriangles(int x, int y)
        {
            AddTrianglePoint(TLPos(x, y));
            AddTrianglePoint(BPos(x, y));
            AddTrianglePoint(APos(x, y));
        }

        private void TopRightTriangles(int x, int y)
        {
            AddTrianglePoint(BPos(x, y));
            AddTrianglePoint(TRPos(x, y));
            AddTrianglePoint(CPos(x, y));
        }

        private void TopTriangles(int x, int y)
        {
            var tl = TLPos(x, y);
            var tr = TRPos(x, y);
            var a = APos(x, y);
            var c = CPos(x, y);

            AddTrianglePoint(tl);
            AddTrianglePoint(tr);
            AddTrianglePoint(a);

            AddTrianglePoint(tr);
            AddTrianglePoint(c);
            AddTrianglePoint(a);
        }

        private void BottomLeftTriangles(int x, int y)
        {
            AddTrianglePoint(BLPos(x, y));
            AddTrianglePoint(APos(x, y));
            AddTrianglePoint(DPos(x, y));
        }

        private void LeftTriangles(int x, int y)
        {
            var tl = TLPos(x, y);
            var b = BPos(x, y);
            var bl = BLPos(x, y);
            var d = DPos(x, y);

            AddTrianglePoint(bl);
            AddTrianglePoint(tl);
            AddTrianglePoint(b);

            AddTrianglePoint(bl);
            AddTrianglePoint(b);
            AddTrianglePoint(d);
        }

        private void BottomLeftToTopRightTriangles(int x, int y)
        {
            var a = APos(x, y);
            var b = BPos(x, y);
            var tr = TRPos(x, y);
            var c = CPos(x, y);
            var d = DPos(x, y);
            var bl = BLPos(x, y);

            AddTrianglePoint(bl);
            AddTrianglePoint(a);
            AddTrianglePoint(d);

            AddTrianglePoint(a);
            AddTrianglePoint(b);
            AddTrianglePoint(tr);

            AddTrianglePoint(a);
            AddTrianglePoint(tr);
            AddTrianglePoint(d);

            AddTrianglePoint(d);
            AddTrianglePoint(tr);
            AddTrianglePoint(c);
        }

        private void AllButBottomRightTriangles(int x, int y)
        {
            var tl = TLPos(x, y);
            var tr = TRPos(x, y);
            var bl = BLPos(x, y);
            var c = CPos(x, y);
            var d = DPos(x, y);

            AddTrianglePoint(tl);
            AddTrianglePoint(tr);
            AddTrianglePoint(c);

            AddTrianglePoint(tl);
            AddTrianglePoint(c);
            AddTrianglePoint(d);

            AddTrianglePoint(tl);
            AddTrianglePoint(d);
            AddTrianglePoint(bl);
        }

        private void BottomRightTriangles(int x, int y)
        {
            AddTrianglePoint(DPos(x, y));
            AddTrianglePoint(CPos(x, y));
            AddTrianglePoint(BRPos(x, y));
        }

        private void TopLeftToBottomRightTriangles(int x, int y)
        {
            var tl = TLPos(x, y);
            var b = BPos(x, y);
            var c = CPos(x, y);
            var br = BRPos(x, y);
            var d = DPos(x, y);
            var a = APos(x, y);

            AddTrianglePoint(tl);
            AddTrianglePoint(b);
            AddTrianglePoint(c);

            AddTrianglePoint(tl);
            AddTrianglePoint(c);
            AddTrianglePoint(d);

            AddTrianglePoint(tl);
            AddTrianglePoint(d);
            AddTrianglePoint(a);

            AddTrianglePoint(d);
            AddTrianglePoint(c);
            AddTrianglePoint(br);
        }

        private void RightTriangles(int x, int y)
        {
            var b = BPos(x, y);
            var tr = TRPos(x, y);
            var br = BRPos(x, y);
            var d = DPos(x, y);

            AddTrianglePoint(b);
            AddTrianglePoint(tr);
            AddTrianglePoint(br);

            AddTrianglePoint(b);
            AddTrianglePoint(br);
            AddTrianglePoint(d);
        }

        private void AllButBottomLeftTriangles(int x, int y)
        {
            var tl = TLPos(x, y);
            var tr = TRPos(x, y);
            var br = BRPos(x, y);
            var d = DPos(x, y);
            var a = APos(x, y);

            AddTrianglePoint(tl);
            AddTrianglePoint(tr);
            AddTrianglePoint(a);

            AddTrianglePoint(a);
            AddTrianglePoint(tr);
            AddTrianglePoint(d);

            AddTrianglePoint(d);
            AddTrianglePoint(tr);
            AddTrianglePoint(br);
        }

        private void BottomTriangles(int x, int y)
        {
            var a = APos(x, y);
            var c = CPos(x, y);
            var br = BRPos(x, y);
            var bl = BLPos(x, y);

            AddTrianglePoint(bl);
            AddTrianglePoint(a);
            AddTrianglePoint(c);

            AddTrianglePoint(bl);
            AddTrianglePoint(c);
            AddTrianglePoint(br);
        }

        private void AllButTopRightTriangles(int x, int y)
        {
            var tl = TLPos(x, y);
            var b = BPos(x, y);
            var c = CPos(x, y);
            var br = BRPos(x, y);
            var bl = BLPos(x, y);

            AddTrianglePoint(bl);
            AddTrianglePoint(tl);
            AddTrianglePoint(b);

            AddTrianglePoint(bl);
            AddTrianglePoint(b);
            AddTrianglePoint(c);

            AddTrianglePoint(bl);
            AddTrianglePoint(c);
            AddTrianglePoint(br);
        }

        private void AllButTopLeftTriangles(int x, int y)
        {
            var a = APos(x, y);
            var b = BPos(x, y);
            var tr = TRPos(x, y);
            var br = BRPos(x, y);
            var bl = BLPos(x, y);

            AddTrianglePoint(bl);
            AddTrianglePoint(a);
            AddTrianglePoint(br);

            AddTrianglePoint(a);
            AddTrianglePoint(b);
            AddTrianglePoint(br);

            AddTrianglePoint(b);
            AddTrianglePoint(tr);
            AddTrianglePoint(br);
        }

        private void FullSquareTriangles(int x, int y)
        {
            var tl = TLPos(x, y);
            var tr = TRPos(x, y);
            var bl = BLPos(x, y);
            var br = BRPos(x, y);

            AddTrianglePoint(bl);
            AddTrianglePoint(tl);
            AddTrianglePoint(br);

            AddTrianglePoint(tl);
            AddTrianglePoint(tr);
            AddTrianglePoint(br);
        }

        private void AddTrianglePoint(Vector3 point)
        {
            vertices.Add(point * meshScale);
            uvs.Add(Vector2.zero);
            triangles.Add(verticesIndex);
            verticesIndex++;
        }

        // Point between BottomLeft and TopLeft
        private Vector2 APos(int x, int y)
        {
            return GetInterpolatePoint(BLVal(x, y), TLVal(x, y), BLPos(x, y), TLPos(x, y));
        }

        // Point between TopLeft and TopRight
        private Vector2 BPos(int x, int y)
        {
            return GetInterpolatePoint(TLVal(x, y), TRVal(x, y), TLPos(x, y), TRPos(x, y));
        }

        // Point between TopRight and BottomRight
        private Vector2 CPos(int x, int y)
        {
            return GetInterpolatePoint(BRVal(x, y), TRVal(x, y), BRPos(x, y), TRPos(x, y));
        }

        // Point between BottomRight and BottomLeft
        private Vector2 DPos(int x, int y)
        {
            return GetInterpolatePoint(BLVal(x, y), BRVal(x, y), BLPos(x, y), BRPos(x, y));
        }

        // TopLeft pos
        private Vector2 TLPos(int x, int y)
        {
            return new(x, y + 1);
        }

        // TopRight pos
        private Vector2 TRPos(int x, int y)
        {
            return new(x + 1, y + 1);
        }

        // BottomLeft pos
        private Vector2 BLPos(int x, int y)
        {
            return new(x, y);
        }

        // BottomRight pos
        private Vector2 BRPos(int x, int y)
        {
            return new(x + 1, y);
        }

        // TopLeft value
        private float TLVal(int x, int y)
        {
            return GetValue(x, y + 1);
        }

        // TopRight value
        private float TRVal(int x, int y)
        {
            return GetValue(x + 1, y + 1);
        }

        // BottomLeft value
        private float BLVal(int x, int y)
        {
            return GetValue(x, y);
        }

        // BottomRight value
        private float BRVal(int x, int y)
        {
            return GetValue(x + 1, y);
        }

        private float GetValue(int x, int y)
        {
            var value = ValueProvider.ValueAt(x, y);
            if (inverted)
            {
                value = ((value - 1) * -1) + 1;
            }
            return value;
        }

        public Vector2 GetInterpolatePoint(float srcVal, float dstVal, Vector2 srcPos, Vector2 dstPos)
        {
            var numerator = 1f - srcVal;
            var denominator = dstVal - srcVal;
            var t = numerator / denominator;
            return Vector2.Lerp(srcPos, dstPos, t);
        }
    }
}
