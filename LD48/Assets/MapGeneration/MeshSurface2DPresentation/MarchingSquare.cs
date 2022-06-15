using Assets.MeshSurface2DPresentation;
using UnityEngine;

namespace MeshSurface2DPresentation
{
    public class MarchingSquare
    {
        private float[,] gridScales;
        private int x;
        private int y;

        private MarchingSquareFlags mode;
        private float fillMinGrayScale;

        public MarchingSquare(float[,] gridScales, int x, int y)
        {
            this.gridScales = gridScales;
            this.x = x;
            this.y = y;
        }

        public void Refresh(float fillMinGrayScale)
        {
            var mode = MarchingSquareFlags.Empty;
            mode |= gridScales[x, y] > fillMinGrayScale ? MarchingSquareFlags.BottomLeft : MarchingSquareFlags.Empty;
            mode |= gridScales[x + 1, y] > fillMinGrayScale ? MarchingSquareFlags.BottomRight : MarchingSquareFlags.Empty;
            mode |= gridScales[x, y + 1] > fillMinGrayScale ? MarchingSquareFlags.TopLeft : MarchingSquareFlags.Empty;
            mode |= gridScales[x + 1, y + 1] > fillMinGrayScale ? MarchingSquareFlags.TopRight : MarchingSquareFlags.Empty;
            this.mode = mode;
            this.fillMinGrayScale = fillMinGrayScale;
        }

        public MeshTriangle[] GetTriangles()
        {
            switch (mode)
            {
                case MarchingSquareFlags.TopLeft:
                    return TopLeftTriangles();
                case MarchingSquareFlags.TopRight:
                    return TopRightTriangles();
                case MarchingSquareFlags.Top:
                    return TopTriangles();
                case MarchingSquareFlags.BottomLeft:
                    return BottomLeftTriangles();
                case MarchingSquareFlags.Left:
                    return LeftTriangles();
                case MarchingSquareFlags.BottomLeftToTopRight:
                    return BottomLeftToTopRightTriangles();
                case MarchingSquareFlags.AllButBottomRight:
                    return AllButBottomRightTriangles();
                case MarchingSquareFlags.BottomRight:
                    return BottomRightTriangles();
                case MarchingSquareFlags.TopLeftToBottomRight:
                    return TopLeftToBottomRightTriangles();
                case MarchingSquareFlags.Right:
                    return RightTriangles();
                case MarchingSquareFlags.AllButBottomLeft:
                    return AllButBottomLeftTriangles();
                case MarchingSquareFlags.Bottom:
                    return BottomTriangles();
                case MarchingSquareFlags.AllButTopRight:
                    return AllButTopRightTriangles();
                case MarchingSquareFlags.AllButTopLeft:
                    return AllButTopLeftTriangles();
                case MarchingSquareFlags.Full:
                    return FullSquareTriangles();
                case MarchingSquareFlags.Empty:
                default:
                    return new MeshTriangle[0];
            }
        }

        private MeshTriangle[] TopLeftTriangles()
        {
            return new MeshTriangle[]
            {
                new MeshTriangle
                {
                    Point1 = TLPos(),
                    Point2 = BPos(),
                    Point3 = APos(),
                },
            };
        }

        private MeshTriangle[] TopRightTriangles()
        {
            return new MeshTriangle[]
            {
                new MeshTriangle
                {
                    Point1 = BPos(),
                    Point2 = TRPos(),
                    Point3 = CPos(),
                },
            };
        }

        private MeshTriangle[] TopTriangles()
        {
            var tl = TLPos();
            var tr = TRPos();
            var a = APos();
            var c = CPos();
            return new MeshTriangle[]
            {
                new MeshTriangle
                {
                    Point1 = tl,
                    Point2 = tr,
                    Point3 = a,
                },
                new MeshTriangle
                {
                    Point1 = tr,
                    Point2 = c,
                    Point3 = a,
                },
            };
        }

        private MeshTriangle[] BottomLeftTriangles()
        {
            return new MeshTriangle[]
            {
                new MeshTriangle
                {
                    Point1 = BLPos(),
                    Point2 = APos(),
                    Point3 = DPos(),
                },
            };
        }

        private MeshTriangle[] LeftTriangles()
        {
            var tl = TLPos();
            var b = BPos();
            var bl = BLPos();
            var d = DPos();
            return new MeshTriangle[]
            {
                new MeshTriangle
                {
                    Point1 = bl,
                    Point2 = tl,
                    Point3 = b,
                },
                new MeshTriangle
                {
                    Point1 = bl,
                    Point2 = b,
                    Point3 = d,
                },
            };
        }

        private MeshTriangle[] BottomLeftToTopRightTriangles()
        {
            var a = APos();
            var b = BPos();
            var tr = TRPos();
            var c = CPos();
            var d = DPos();
            var bl = BLPos();
            return new MeshTriangle[]
            {
                new MeshTriangle // BottomLeft triangle
                {
                    Point1 = bl,
                    Point2 = a,
                    Point3 = d,
                },
                new MeshTriangle
                {
                    Point1 = a,
                    Point2 = b,
                    Point3 = tr,
                },
                new MeshTriangle
                {
                    Point1 = a,
                    Point2 = tr,
                    Point3 = d,
                },
                new MeshTriangle
                {
                    Point1 = d,
                    Point2 = tr,
                    Point3 = c,
                },
            };
        }

        private MeshTriangle[] AllButBottomRightTriangles()
        {
            var tl = TLPos();
            var tr = TRPos();
            var bl = BLPos();
            var c = CPos();
            var d = DPos();
            return new MeshTriangle[]
            {
                new MeshTriangle
                {
                    Point1 = tl,
                    Point2 = tr,
                    Point3 = c,
                },
                new MeshTriangle
                {
                    Point1 = tl,
                    Point2 = c,
                    Point3 = d,
                },
                new MeshTriangle
                {
                    Point1 = tl,
                    Point2 = d,
                    Point3 = bl,
                },
            };
        }

        private MeshTriangle[] BottomRightTriangles()
        {
            return new MeshTriangle[]
            {
                new MeshTriangle
                {
                    Point1 = DPos(),
                    Point2 = CPos(),
                    Point3 = BRPos(),
                },
            };
        }

        private MeshTriangle[] TopLeftToBottomRightTriangles()
        {
            var tl = TLPos();
            var b = BPos();
            var c = CPos();
            var br = BRPos();
            var d = DPos();
            var a = APos();
            return new MeshTriangle[]
            {
                new MeshTriangle
                {
                    Point1 = tl,
                    Point2 = b,
                    Point3 = c,
                },
                new MeshTriangle
                {
                    Point1 = tl,
                    Point2 = c,
                    Point3 = d,
                },
                new MeshTriangle
                {
                    Point1 = tl,
                    Point2 = d,
                    Point3 = a,
                },
                new MeshTriangle
                {
                    Point1 = d,
                    Point2 = c,
                    Point3 = br,
                },
            };
        }

        private MeshTriangle[] RightTriangles()
        {
            var b = BPos();
            var tr = TRPos();
            var br = BRPos();
            var d = DPos();
            return new MeshTriangle[]
            {
                new MeshTriangle
                {
                    Point1 = b,
                    Point2 = tr,
                    Point3 = br
                },
                new MeshTriangle
                {
                    Point1 = b,
                    Point2 = br,
                    Point3 = d,
                },
            };
        }

        private MeshTriangle[] AllButBottomLeftTriangles()
        {
            var tl = TLPos();
            var tr = TRPos();
            var br = BRPos();
            var d = DPos();
            var a = APos();
            return new MeshTriangle[]
            {
                new MeshTriangle
                {
                    Point1 = tl,
                    Point2 = tr,
                    Point3 = a,
                },
                new MeshTriangle
                {
                    Point1 = a,
                    Point2 = tr,
                    Point3 = d,
                },
                new MeshTriangle
                {
                    Point1 = d,
                    Point2 = tr,
                    Point3 = br,
                },
            };
        }

        private MeshTriangle[] BottomTriangles()
        {
            var a = APos();
            var c = CPos();
            var br = BRPos();
            var bl = BLPos();
            return new MeshTriangle[]
            {
                new MeshTriangle
                {
                    Point1 = bl,
                    Point2 = a,
                    Point3 = c,
                },
                new MeshTriangle
                {
                    Point1 = bl,
                    Point2 = c,
                    Point3 = br,
                },
            };
        }

        private MeshTriangle[] AllButTopRightTriangles()
        {
            var tl = TLPos();
            var b = BPos();
            var c = CPos();
            var br = BRPos();
            var bl = BLPos();
            return new MeshTriangle[]
            {
                new MeshTriangle
                {
                    Point1 = bl,
                    Point2 = tl,
                    Point3 = b,
                },
                new MeshTriangle
                {
                    Point1 = bl,
                    Point2 = b,
                    Point3 = c,
                },
                new MeshTriangle
                {
                    Point1 = bl,
                    Point2 = c,
                    Point3 = br,
                },
            };
        }

        private MeshTriangle[] AllButTopLeftTriangles()
        {
            var a = APos();
            var b = BPos();
            var tr = TRPos();
            var br = BRPos();
            var bl = BLPos();
            return new MeshTriangle[]
            {
                new MeshTriangle
                {
                    Point1 = bl,
                    Point2 = a,
                    Point3 = br,
                },
                new MeshTriangle
                {
                    Point1 = a,
                    Point2 = b,
                    Point3 = br,
                },
                new MeshTriangle
                {
                    Point1 = b,
                    Point2 = tr,
                    Point3 = br,
                },
            };
        }

        private MeshTriangle[] FullSquareTriangles()
        {
            var tl = TLPos();
            var tr = TRPos();
            var bl = BLPos();
            var br = BRPos();
            return new MeshTriangle[]
            {
                new MeshTriangle
                {
                    Point1 = bl,
                    Point2 = tl,
                    Point3 = br,
                },
                new MeshTriangle
                {
                    Point1 = tl,
                    Point2 = tr,
                    Point3 = br,
                },
            };
        }

        // Point between BottomLeft and TopLeft
        private Vector2 APos()
        {
            return GetInterpolatePoint(BLVal(), TLVal(), BLPos(), TLPos());
        }

        // Point between TopLeft and TopRight
        private Vector2 BPos()
        {
            return GetInterpolatePoint(TLVal(), TRVal(), TLPos(), TRPos());
        }

        // Point between TopRight and BottomRight
        private Vector2 CPos()
        {
            return GetInterpolatePoint(BRVal(), TRVal(), BRPos(), TRPos());
        }

        // Point between BottomRight and BottomLeft
        private Vector2 DPos()
        {
            return GetInterpolatePoint(BLVal(), BRVal(), BLPos(), BRPos());
        }

        // TopLeft pos
        private Vector2 TLPos()
        {
            return new(x, y + 1);
        }

        // TopRight pos
        private Vector2 TRPos()
        {
            return new(x + 1, y + 1);
        }

        // BottomLeft pos
        private Vector2 BLPos()
        {
            return new(x, y);
        }

        // BottomRight pos
        private Vector2 BRPos()
        {
            return new(x + 1, y);
        }

        // TopLeft value
        private float TLVal()
        {
            return GetValue(x, y + 1);
        }

        // TopRight value
        private float TRVal()
        {
            return GetValue(x + 1, y + 1);
        }

        // BottomLeft value
        private float BLVal()
        {
            return GetValue(x, y);
        }

        // BottomRight value
        private float BRVal()
        {
            return GetValue(x + 1, y);
        }

        private float GetValue(int x, int y)
        {
            return gridScales[x, y];
        }

        private Vector2 GetInterpolatePoint(float srcVal, float dstVal, Vector2 srcPos, Vector2 dstPos)
        {
            return Vector2.Lerp(srcPos, dstPos, 0.5f);
        }
    }
}
