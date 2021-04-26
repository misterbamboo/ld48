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

                ressource.transform.position = new Vector3(x, unityTranslatedY, 0);
            }
        }
    }
}
