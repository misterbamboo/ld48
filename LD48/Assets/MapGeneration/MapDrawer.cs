using Assets.MapGeneration.Utils;
using Assets.Ressources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.MapGeneration
{
    public class MapDrawer
    {
        public Mesh[] Mesh { get; private set; }
        public MapShape[] MapShapes { get; private set; }

        private List<Vector3> vertices = new List<Vector3>();

        private List<int> triangles = new List<int>();

        private Map map;

        public MapDrawer(Map map)
        {
            this.map = map;
        }

        private bool drawShapeLines = true;
        private bool doOldGeneration = false;

        public void Redraw(int fromY, int toY)
        {
            vertices.Clear();
            triangles.Clear();

            // old
            if (doOldGeneration)
            {
                OldGeneration(fromY, toY);
                var mesh = new Mesh();
                mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
                mesh.vertices = vertices.ToArray();

                mesh.triangles = triangles.ToArray();
                mesh.RecalculateNormals();
                mesh.RecalculateBounds();
                Mesh = new[] { mesh };
            }
            else
            {
                // new
                var shapeDetector = new MapShapeDetector(map);
                var shapes = shapeDetector.GetShapes(fromY, toY);

                if (drawShapeLines)
                {
                    DrawShapeLines(shapes);
                }

                TriangulateShapes(shapes);

            }
        }

        private void TriangulateShapes(IEnumerable<MapShape> shapes)
        {
            List<Mesh> meches = new List<Mesh>();
            List<MapShape> mapShapes = new List<MapShape>();
            foreach (var shape in shapes)
            {
                var vertices2D = shape.OrderedVectors.ToArray();
                var tr = new Triangulator(vertices2D);
                int[] indices = tr.Triangulate();

                // Create the Vector3 vertices
                Vector3[] vertices3D = new Vector3[vertices2D.Length];
                for (int i = 0; i < vertices3D.Length; i++)
                {
                    vertices3D[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, 0);
                }

                var mesh = new Mesh();
                mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
                mesh.vertices = vertices3D;
                mesh.triangles = indices.Reverse().ToArray();
                mesh.RecalculateNormals();
                mesh.RecalculateBounds();
                meches.Add(mesh);
                mapShapes.Add(shape);
            }
            Mesh = meches.ToArray();
            MapShapes = mapShapes.ToArray();
        }

        private static void DrawShapeLines(IEnumerable<MapShape> shapes)
        {
            foreach (var shape in shapes)
            {
                Vector2 first = shape.OrderedVectors.FirstOrDefault();
                if (first == default(Vector2)) continue;

                Vector2 last = first;
                foreach (var vector in shape.OrderedVectors)
                {
                    // skip first
                    if (vector == first) continue;
                    Debug.DrawLine(last, vector, Color.red, 60000);
                    last = vector;
                }
                Debug.DrawLine(last, first, Color.red, 60000);
            }
        }

        private void OldGeneration(int fromY, int toY)
        {
            int index = 0;
            for (int x = 0; x < map.Configuration.width; x++)
            {
                for (int y = fromY; y < toY; y++)
                {
                    if (!map.IsEmpty(x, y))
                    {
                        DrawMap(x, y, ref index);
                    }
                }
            }
        }



        private void DrawMap(int x, int y, ref int triangleIndex)
        {
            int unityTranslatedY = -y;

            vertices.Add(new Vector2(x, unityTranslatedY));
            vertices.Add(new Vector2(x + 1, unityTranslatedY));
            vertices.Add(new Vector2(x + 1, unityTranslatedY - 1));
            vertices.Add(new Vector2(x, unityTranslatedY - 1));

            triangles.Add(triangleIndex);
            triangles.Add(triangleIndex + 1);
            triangles.Add(triangleIndex + 3);
            triangles.Add(triangleIndex + 1);
            triangles.Add(triangleIndex + 2);
            triangles.Add(triangleIndex + 3);
            triangleIndex += 4;
        }

        public void ReplaceRessources(int fromY, int toY)
        {
            RessourcePooler.Instance.RecycleOutOfRange(fromY, toY);

            for (int x = 0; x < map.Configuration.width; x++)
            {
                for (int y = fromY; y < toY; y++)
                {
                    if (!map.IsEmpty(x, y))
                    {
                        DrawRessource(x, y);
                    }
                }
            }
        }

        private void DrawRessource(int x, int y)
        {
            if (map.IsRessource(x, y))
            {
                int unityTranslatedY = -y;

                var ressource = RessourcePooler.Instance.GetOne(map.GetCellType(x, y));

                var ressourceDef = ressource.GetComponent<IRessource>();
                ressourceDef.SpawnX = x;
                ressourceDef.SpawnY = y;
                var randomAngle = UnityEngine.Random.Range(1, 360);

                var offsetX = UnityEngine.Random.Range(0f, 1f);
                var offsety = UnityEngine.Random.Range(0f, 1f);

                ressource.transform.position = new Vector3(x + offsetX, unityTranslatedY - offsety, 0);
                ressource.transform.Rotate(new Vector3(0, 0, randomAngle));
            }
        }
    }
}
