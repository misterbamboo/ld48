using MeshSurface2D;
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
    }
}
