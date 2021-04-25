using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Ressources;

namespace Assets.MapGeneration
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private MapConfig mapConfiguration;
        [SerializeField] private int numberOfPaths = 5;
        [SerializeField] private int minPathSize = 5;
        [SerializeField] private int maxPathSize = 20;

        [SerializeField] private int playerViewBuffer = 100;
        [SerializeField] private Transform playerPosition;

        // Invert Y, because in Unity world, the player is going deeper and deeper
        // so the Y position will become more and more negative
        // but in MapGeneration context, the Y position is always positive
        private int TranslatedPlayerPosition => (int)-playerPosition.position.y;

        private Map map;

        private Path[] paths;

        private MapDrawer mapDrawer;

        private MapCollider mapCollider;

        private int lastPlayerPageIndex = int.MinValue;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            paths = new Path[numberOfPaths];
            for (int i = 0; i < numberOfPaths; i++)
            {
                paths[i] = new Path(mapConfiguration.width, minPathSize, maxPathSize);
            }
            map = new Map(mapConfiguration, paths);
            mapDrawer = new MapDrawer(map);
            mapCollider = new MapCollider(map);

            GenerateNewPage(playerViewBuffer);
            UpdatePageView();
        }

        private void Update()
        {
            if (ShouldGenerateNewPage())
            {
                GenerateNewPage(playerViewBuffer);
                UpdatePageView();
            }
            else if (IsPlayerChangedViewPage())
            {
                UpdatePageView();
            }
        }

        private bool ShouldGenerateNewPage()
        {
            int generationPosition = GetGenerationPosition();
            return map.Height < generationPosition;
        }

        private void GenerateNewPage(int pageSize)
        {
            foreach (var path in paths)
            {
                path.Generate(pageSize);
            }
            map.Generate(pageSize);
        }

        private bool IsPlayerChangedViewPage()
        {
            int pageIndex = GetCurrentPlayerPageIndex();
            if (lastPlayerPageIndex != pageIndex)
            {
                return true;
            }
            return false;
        }

        private void UpdatePageView()
        {
            lastPlayerPageIndex = GetCurrentPlayerPageIndex();

            var fromY = (lastPlayerPageIndex - 1) * playerViewBuffer;
            var toY = (lastPlayerPageIndex + 2) * playerViewBuffer;

            mapDrawer.Redraw(fromY, toY);
            GetComponent<MeshFilter>().sharedMesh = mapDrawer.Mesh;

            var currentPageFromY = (lastPlayerPageIndex) * playerViewBuffer;
            var currentPageToY = (lastPlayerPageIndex + 1) * playerViewBuffer;

            mapDrawer.ReplaceRessources(currentPageFromY, currentPageToY);

            BoxCollider2DPooler.Instance.RecycleOutOfRange(currentPageFromY, currentPageToY);

            var poligons = mapCollider.GetCollisionPoints(currentPageFromY, currentPageToY);
            for (int i = 0; i < poligons.Count(); i++)
            {
                var polygon = poligons.ElementAt(i);
                var boxCollider = BoxCollider2DPooler.Instance.GetOne();
                boxCollider.transform.position = polygon[0];
            }
        }

        private int GetCurrentPlayerPageIndex()
        {
            return TranslatedPlayerPosition / playerViewBuffer;
        }

        private int GetGenerationPosition()
        {
            return TranslatedPlayerPosition + playerViewBuffer;
        }
    }
}