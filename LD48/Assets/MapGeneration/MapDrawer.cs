using Assets.Ressources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.MapGeneration
{
    public class MapDrawer
    {
        public Mesh Mesh => mesh;

        private Mesh mesh;

        private List<Vector3> vertices = new List<Vector3>();

        private List<int> triangles = new List<int>();

        private Map map;

        public MapDrawer(Map map)
        {
            this.map = map;

            mesh = new Mesh();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        }

        public void Redraw(int fromY, int toY)
        {
            RessourcePooler.Instance.RecycleOutOfRange(fromY, toY);

            vertices.Clear();
            triangles.Clear();

            int index = 0;
            for (int x = 0; x < map.Configuration.width; x++)
            {
                for (int y = fromY; y < toY; y++)
                {
                    if (!map.IsEmpty(x, y))
                    {
                        DrawMap(x, y, ref index);
                        DrawRessource(x, y);
                    }
                }
            }

            mesh.Clear();
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.RecalculateNormals();
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

        private void DrawRessource(int x, int y)
        {
            if (map.IsRessource(x, y, MapCellType.Copper))
            {
                int unityTranslatedY = -y;

                var copper = RessourcePooler.Instance.GetOne(MapCellType.Copper);
                copper.transform.position = new Vector3(x, unityTranslatedY, 0);
            }
        }
    }
}
