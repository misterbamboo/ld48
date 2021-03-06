using UnityEngine;

namespace Assets.MapGeneration
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private MapConfig mapConfiguration;

        [SerializeField] private int playerViewBuffer = 100;
        [SerializeField] private Transform playerPosition;

        [SerializeField] private GameObject mapShapeMeshPrefab;

        // Invert Y, because in Unity world, the player is going deeper and deeper
        // so the Y position will become more and more negative
        // but in MapGeneration context, the Y position is always positive
        private int TranslatedPlayerPosition => (int)-playerPosition.position.y;

        private Map map;

        private MapDrawer mapDrawer;

        private int lastPlayerPageIndex = int.MinValue;

        private GameObject mapShapeGameObject;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            map = new Map(mapConfiguration);
            mapDrawer = new MapDrawer(map);

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

            DrawPage(fromY, toY);

            var currentPageFromY = (lastPlayerPageIndex) * playerViewBuffer;
            var currentPageToY = (lastPlayerPageIndex + 1) * playerViewBuffer;

            mapDrawer.ReplaceRessources(currentPageFromY, currentPageToY);
        }

        private void DrawPage(int fromY, int toY)
        {
            mapDrawer.Redraw(fromY, toY);

            ReplaceMapShapeGameObject(-toY);
        }

        private void ReplaceMapShapeGameObject(int posY)
        {
            if (mapShapeGameObject != null)
            {
                Destroy(mapShapeGameObject);
            }

            mapShapeGameObject = Instantiate(mapShapeMeshPrefab);
            mapShapeGameObject.transform.position = new Vector3(0, posY, 0);
            mapShapeGameObject.GetComponent<MeshFilter>().sharedMesh = mapDrawer.Mesh;
            mapShapeGameObject.GetComponent<MeshCollider>().sharedMesh = mapDrawer.Mesh;
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