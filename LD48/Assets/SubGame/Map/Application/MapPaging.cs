using System;
using UnityEngine;

namespace Assets.Map.Application
{
    public class MapPaging
    {
        public event Action<MapPageInfo> PageChanged;

        private readonly int mapPageSize;
        private string currentPageKey;
        private Vector2 playerPosition;

        public MapPaging(int mapPageSize)
        {
            this.mapPageSize = mapPageSize;
            currentPageKey = GetPageKey();
        }

        public void UpdatePlayerPosition(Vector2 playerPosition)
        {
            this.playerPosition = playerPosition;
            CheckIfPageChanged();
        }

        private void CheckIfPageChanged()
        {
            if (ShouldUpdatePage())
            {
                UpdateMapPage();
                currentPageKey = GetPageKey();
            }
        }

        private bool ShouldUpdatePage()
        {
            string pageKey = GetPageKey();
            return pageKey != currentPageKey;
        }

        private string GetPageKey()
        {
            var pageInfo = GetPageInfo();
            return pageInfo.GetKey();
        }

        private void UpdateMapPage()
        {
            PageChanged?.Invoke(GetPageInfo());
        }

        public MapPageInfo GetCurrentPageInfo()
        {
            return GetPageInfo();
        }

        private MapPageInfo GetPageInfo()
        {
            var pagePos = playerPosition / mapPageSize;
            var pageInfo = new MapPageInfo(pagePos, mapPageSize);
            return pageInfo;
        }

        public class MapPageInfo
        {
            private int XIndex { get; }
            private int YIndex { get; }
            public int MapPageSize { get; }

            public MapPageInfo(Vector3 pagePos, int mapPageSize)
            {
                XIndex = (int)Math.Floor(pagePos.x);
                YIndex = (int)Math.Floor(pagePos.y);
                MapPageSize = mapPageSize;
            }

            public string GetKey()
            {
                return $"{XIndex},{YIndex}";
            }

            public Vector2 FromPoint()
            {
                var x = (XIndex - 1) * MapPageSize;
                var y = (YIndex - 1) * MapPageSize;
                return new Vector2(x, y);
            }

            public Vector2 ToPoint()
            {
                var x = (XIndex + 2) * MapPageSize;
                var y = (YIndex + 2) * MapPageSize;
                return new Vector2(x, y);
            }
        }
    }
}
