using MeshSurface2DPresentation;
using Assets.Ressources;
using System;
using UnityEngine;
using MeshSurface2DPresentation.ValueProviders;

namespace Assets.MapGeneration
{
    public class MapDrawer
    {
        public Mesh Mesh { get; private set; }

        private Map map;

        public MapDrawer(Map map)
        {
            this.map = map;
        }

        public void Redraw(int fromY, int toY)
        {
            Mesh = GenerateMesh(fromY, toY);
        }

        private Mesh GenerateMesh(int fromY, int toY)
        {
            var surface = new LerpMeshSurface2D(map.GetTerrainValueProvider(fromY, toY));
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

                var ressourceDef = ressource.GetComponent<Ore>();

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
