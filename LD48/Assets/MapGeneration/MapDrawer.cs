using Assets.Ressources;
using MeshSurface2DPresentation;
using System.Collections.Generic;
using UnityEngine;

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

            // Needed ?
            Mesh.RecalculateBounds();
        }

        private Mesh GenerateMesh(int fromY, int toY)
        {
            var height = toY - fromY;
            var surface = new MeshSurface2D(map.Configuration.width, height)
            {
                MeshSizeScale = 1,
            };

            var texture2D = GetMapPageAsTexture2D(fromY, toY, height);
            surface.PrintToMesh(texture2D);
            return surface.Mesh;
        }

        private Texture2D GetMapPageAsTexture2D(int fromY, int toY, int height)
        {
            var texture2D = new Texture2D(map.Configuration.width, height);
            for (int x = 0; x < map.Configuration.width; x++)
            {
                for (int y = fromY; y < toY; y++)
                {
                    var textY = y - fromY;
                    var topDownRevertedTextY = height - textY;
                    if (map.IsEmpty(x, y))
                    {
                        texture2D.SetPixel(x, topDownRevertedTextY, new Color(0, 0, 0));
                    }
                    else
                    {
                        texture2D.SetPixel(x, topDownRevertedTextY, new Color(1, 1, 1));
                    }
                }
            }

            return texture2D;
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
