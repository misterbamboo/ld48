using Assets.Map.Infrastructure;
using MeshSurface2D;
using UnityEngine;

namespace Assets.Map.Presentation
{
    public class MapMeshDrawer
    {
        public Mesh Mesh { get; private set; }

        private Domain.Map map;
        private MapConfig mapConfig;

        public MapMeshDrawer(Domain.Map map, MapConfig mapConfig)
        {
            this.map = map;
            this.mapConfig = mapConfig;
        }

        public void RedrawRegion(Vector2 from, Vector2 to)
        {
            Mesh = GenerateMesh(from, to);
        }

        private Mesh GenerateMesh(Vector2 from, Vector2 to)
        {
            var surface = new LerpMeshSurface2D(map.GetTerrainValueProvider(from, to));
            surface.ChangeScale(mapConfig.mapDisplayScale);
            return surface.CreateMesh();
        }
    }
}
