using Assets.Map.Application;
using Assets.Map.Infrastructure;
using Assets.Ressources;
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

        public MapController(MapConfig mapConfiguration, int mapPageSize)
        {
            map = new Domain.Map(mapConfiguration);
            mapMeshDrawer = new MapMeshDrawer(map);
            mapPaging = new MapPaging(mapPageSize);
            mapPaging.PageChanged += MapPaging_PageChanged;
        }

        public void Update(Vector3 playerPosition)
        {
            mapPaging.UpdatePlayerPosition(playerPosition);
        }

        private void MapPaging_PageChanged(MapPaging.MapPageInfo pageInfo)
        {
            map.GenerateRegion(pageInfo.FromPoint(), pageInfo.ToPoint());
            mapMeshDrawer.RedrawRegion(pageInfo.FromPoint(), pageInfo.ToPoint());

            MapDisplayedMeshChanged?.Invoke(mapMeshDrawer.Mesh);
        }

        public void RemoveRessource(IRessource ressource)
        {
            map.RemoveRessource(ressource);
        }
    }
}
