using MeshSurface2D;
using Assets.Ressources;
using UnityEngine;

namespace Assets.Map.Presentation
{
    public class MapMeshDrawer
    {
        public Mesh Mesh { get; private set; }

        private Domain.Map map;

        public MapMeshDrawer(Domain.Map map)
        {
            this.map = map;
        }

        public void RedrawRegion(Vector2 from, Vector2 to)
        {
            Mesh = GenerateMesh(from, to);
        }

        private Mesh GenerateMesh(Vector2 from, Vector2 to)
        {
            var surface = new LerpMeshSurface2D(map.GetTerrainValueProvider(from, to));
            surface.ChangeScale(1);
            return surface.CreateMesh();
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

                var ressource = RessourcePooler.Instance.GetOne(map.GetCell(x, y).mapCellType);

                var ressourceDef = ressource.GetComponent<IRessource>();
                ressourceDef.SpawnX = x;
                ressourceDef.SpawnY = y;
                var randomAngle = Random.Range(1, 360);

                var offsetX = Random.Range(0f, 1f);
                var offsety = Random.Range(0f, 1f);

                ressource.transform.position = new Vector3(x + offsetX, unityTranslatedY - offsety, 0);
                ressource.transform.Rotate(new Vector3(0, 0, randomAngle));
            }
        }
    }
}
