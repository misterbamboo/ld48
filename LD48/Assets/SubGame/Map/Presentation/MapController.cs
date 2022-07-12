using Assets.Map.Application;
using Assets.Map.Infrastructure;
using System;
using UnityEngine;

namespace Assets.Map.Presentation
{
    public class MapController
    {
        public event Action<Mesh> MapDisplayedMeshChanged;

        private Domain.Map map;
        private MapMeshDrawer mapMeshDrawer;
        private MapPaging mapPaging;

        public MapController(MapConfig mapConfiguration)
        {
            map = new Domain.Map(mapConfiguration);
            mapMeshDrawer = new MapMeshDrawer(map, mapConfiguration);
            mapPaging = new MapPaging(mapConfiguration.mapPageSize);
            mapPaging.PageChanged += MapPaging_PageChanged;
        }

        public void Update(Vector3 playerPosition)
        {
            mapPaging.UpdatePlayerPosition(playerPosition);
        }

        private void MapPaging_PageChanged(MapPaging.MapPageInfo pageInfo)
        {
            var from = pageInfo.FromPoint();
            var to = pageInfo.ToPoint();
            map.GenerateRegion(from, to);
            mapMeshDrawer.RedrawRegion(from, to);

            MapDisplayedMeshChanged?.Invoke(mapMeshDrawer.Mesh);
        }

        public void RemoveRessourceAt(int x, int y)
        {
            map.RemoveRessourceAt(x, y);
        }
    }
}
