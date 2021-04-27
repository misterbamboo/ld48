using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.MapGeneration
{
    public class MapShapeDetector
    {
        private Map map;

        private int nextNewShapeId;
        private List<int[]> redrawShapes;
        private List<int[]> redrawEdges;
        private Dictionary<int, List<Vector2>> redrawEdgesVectors;

        public MapShapeDetector(Map map)
        {
            this.map = map;
        }

        public IEnumerable<MapShape> GetShapes(int fromY, int toY)
        {
            fromY = fromY < 0 ? 0 : fromY;

            InitRedrawShapes(fromY, toY);
            ScanShapes(fromY, toY);
            ConvertShapesToEdges(fromY);
            //FlushToFile();
            return ConvertShapeEdgesToShapes();
        }

        private void InitRedrawShapes(int fromY, int toY)
        {
            nextNewShapeId = 0;
            redrawShapes = new List<int[]>();
            redrawEdges = new List<int[]>();
            redrawEdgesVectors = new Dictionary<int, List<Vector2>>();
            var height = toY - fromY;
            for (int x = 0; x < height; x++)
            {
                redrawShapes.Add(new int[height]);
                redrawEdges.Add(new int[height]);
            }
        }

        private void FlushToFile()
        {
            var fileName = @"C:\Users\NicolasCharette-Naud\Desktop\result.txt";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < redrawEdges[0].Length; y++)
            {
                for (int x = 0; x < redrawEdges.Count; x++)
                {
                    var shapeId = redrawEdges[x][y];
                    if (shapeId > 0)
                    {
                        sb.Append($"[{shapeId:##}]");
                    }
                    else
                    {
                        sb.Append("    ");
                    }
                }
                sb.AppendLine();
            }

            File.WriteAllText(fileName, sb.ToString());
        }

        private void ScanShapes(int fromY, int toY)
        {
            var height = toY - fromY;
            for (int y = 0; y < height; y++)
            {
                int mapY = fromY + y;
                for (int x = 0; x < map.Configuration.width; x++)
                {
                    if (!map.IsEmpty(x, mapY))
                    {
                        redrawShapes[x][y] = GetShapeId(x, y);
                        CheckMergeShapes(x, y, redrawShapes[x][y]);
                        FillHoles(x, y, redrawShapes[x][y]);
                    }
                }
            }
        }

        private int GetShapeId(int x, int y)
        {
            int shapeId = GetLeftShapeId(x, y);
            if (shapeId > 0) return shapeId;

            shapeId = GetTopLeftShapeId(x, y);
            if (shapeId > 0) return shapeId;

            shapeId = GetTopShapeId(x, y);
            if (shapeId > 0) return shapeId;

            shapeId = GetTopRightShapeId(x, y);
            if (shapeId > 0) return shapeId;

            nextNewShapeId++;
            return nextNewShapeId;
        }

        private void CheckMergeShapes(int x, int y, int shapeId)
        {
            int otherShapeId = GetTopRightShapeId(x, y);
            if (otherShapeId != 0 && otherShapeId != shapeId)
            {
                MergeShapes(shapeId, otherShapeId, x, y);
            }

            otherShapeId = GetTopShapeId(x, y);
            if (otherShapeId != 0 && otherShapeId != shapeId)
            {
                MergeShapes(shapeId, otherShapeId, x, y);
            }

            otherShapeId = GetTopLeftShapeId(x, y);
            if (otherShapeId != 0 && otherShapeId != shapeId)
            {
                MergeShapes(shapeId, otherShapeId, x, y);
            }
        }

        private void FillHoles(int x, int y, int shapeId)
        {
            int otherShapeId = GetLeftShapeId(x - 1, y);
            if (otherShapeId != 0 && otherShapeId == shapeId)
            {
                redrawShapes[x - 1][y] = shapeId;
            }

            otherShapeId = GetTopLeftShapeId(x - 1, y - 1);
            if (otherShapeId != 0 && otherShapeId == shapeId)
            {
                redrawShapes[x - 1][y - 1] = shapeId;
            }

            otherShapeId = GetTopShapeId(x, y - 1);
            if (otherShapeId != 0 && otherShapeId == shapeId)
            {
                redrawShapes[x][y - 1] = shapeId;
            }

            otherShapeId = GetTopRightShapeId(x + 1, y - 1);
            if (otherShapeId != 0 && otherShapeId == shapeId)
            {
                redrawShapes[x + 1][y - 1] = shapeId;
            }
        }

        private void MergeShapes(int shapeId, int otherShapeId, int untilsX, int untilsY)
        {
            int targetShapeId = shapeId > otherShapeId ? shapeId : otherShapeId;
            int becomeShapeId = shapeId <= otherShapeId ? shapeId : otherShapeId;

            for (int y = 0; y <= untilsY; y++)
            {
                for (int x = 0; x < map.Configuration.width; x++)
                {
                    if (redrawShapes[x][y] == targetShapeId)
                    {
                        redrawShapes[x][y] = becomeShapeId;
                    }
                    if (y == untilsY && x == untilsX) break;
                }
            }
        }

        private int GetLeftShapeId(int x, int y)
        {
            var leftX = x - 1;
            if (leftX < 0) return 0;
            if (y < 0) return 0;
            if (y >= redrawShapes[leftX].Length) return 0;
            return redrawShapes[leftX][y];
        }

        private int GetTopLeftShapeId(int x, int y)
        {
            var leftX = x - 1;
            if (leftX < 0) return 0;
            var topY = y - 1;
            if (topY < 0) return 0;
            if (leftX >= redrawShapes.Count) return 0;
            if (topY >= redrawShapes[leftX].Length) return 0;
            return redrawShapes[leftX][topY];
        }

        private int GetTopShapeId(int x, int y)
        {
            var topY = y - 1;
            if (topY < 0) return 0;
            return redrawShapes[x][topY];
        }

        private int GetTopRightShapeId(int x, int y)
        {
            var rightX = x + 1;
            if (rightX >= redrawShapes.Count) return 0;
            var topY = y - 1;
            if (topY < 0) return 0;
            return redrawShapes[rightX][topY];
        }

        private void ConvertShapesToEdges(int fromY)
        {
            for (int y = 0; y < redrawShapes[0].Length; y++)
            {
                for (int x = 0; x < redrawShapes.Count; x++)
                {
                    if (redrawShapes[x][y] != 0 && IsEdge(x, y))
                    {
                        redrawEdges[x][y] = redrawShapes[x][y];
                        CreateEdgeVector(x, y, fromY, redrawShapes[x][y]);
                    }
                }
            }
        }

        private void CreateEdgeVector(int x, int y, int mapFromY, int shapeId)
        {
            if (!redrawEdgesVectors.ContainsKey(shapeId))
            {
                redrawEdgesVectors[shapeId] = new List<Vector2>();
            }
            redrawEdgesVectors[shapeId].Add(new Vector2(x, -(mapFromY + y)));
        }

        private bool IsEdge(int x, int y)
        {
            if (x == 0) return true;
            if (y == 0) return true;
            if (x == redrawShapes.Count - 1) return true;
            if (y == redrawShapes[0].Length - 1) return true;

            if (x > 0 && redrawShapes[x - 1][y] == 0) return true;
            if (y > 0 && redrawShapes[x][y - 1] == 0) return true;
            if (x < redrawShapes.Count - 1 && redrawShapes[x + 1][y] == 0) return true;
            if (y < redrawShapes[0].Length - 1 && redrawShapes[x][y + 1] == 0) return true;
            return false;
        }

        private IEnumerable<MapShape> ConvertShapeEdgesToShapes()
        {
            List<MapShape> mapShapes = new List<MapShape>();
            foreach (var key in redrawEdgesVectors.Keys)
            {
                mapShapes.Add(new MapShape(key, redrawEdgesVectors[key]));
            }
            return mapShapes;
        }
    }
}
