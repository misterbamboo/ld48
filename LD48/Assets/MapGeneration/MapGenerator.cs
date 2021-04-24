using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Assets.MapGeneration
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private MapConfig mapConfiguration;
        [SerializeField] private int numberOfPaths = 5;
        [SerializeField] private int minPathSize = 5;
        [SerializeField] private int maxPathSize = 20;

        [SerializeField] private Transform playerPosition;
        [SerializeField] private int playerViewBuffer = 100;

        private Mesh mesh;

        private List<Vector3> vertices = new List<Vector3>();

        private List<int> triangles = new List<int>();

        private Map map;

        private Path[] paths;

        private int lastPlayerPageIndex;

        private void Awake()
        {
            mesh = new Mesh();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            GetComponent<MeshFilter>().mesh = mesh;
        }

        void Start()
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

            GenerateNewPage(playerViewBuffer);
        }

        private void Update()
        {
            if (ShouldGenerateNewPage())
            {
                GenerateNewPage(playerViewBuffer);
            }

            if (IsPlayerChangedViewPage())
            {
                UpdatePageView();
            }
        }

        private void UpdatePageView()
        {
            lastPlayerPageIndex = GetCurrentPlayerPageIndex();
            GenerateMeshFromMapView();
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

        private int GetCurrentPlayerPageIndex()
        {
            var pos = GetPlayerPosition();
            int pageIndex = pos / playerViewBuffer;
            return pageIndex;
        }

        private void GenerateNewPage(int pageSize)
        {
            foreach (var path in paths)
            {
                path.Generate(pageSize);
            }
            map.Generate(pageSize);
            GenerateMeshFromMapView();
        }

        private bool ShouldGenerateNewPage()
        {
            int generationPosition = GetGenerationPosition();
            return map.Height < generationPosition;
        }

        private int GetGenerationPosition()
        {
            int position = GetPlayerPosition();
            int generationPosition = position + playerViewBuffer;
            return generationPosition;
        }

        private int GetPlayerPosition()
        {
            // Invert Y, because in Unity world, the player is going deeper and deeper
            // so the Y position will become more and more negative
            // but in MapGeneration context, the Y position is always positive
            return (int)-playerPosition.position.y;
        }

        private void GenerateMeshFromMapView()
        {
            vertices.Clear();
            triangles.Clear();

            int index = 0;
            for (int x = 0; x < mapConfiguration.width; x++)
            {
                var min = (GetCurrentPlayerPageIndex() - 1) * playerViewBuffer;
                var max = (GetCurrentPlayerPageIndex() + 2) * playerViewBuffer;

                for (int y = min; y < max; y++)
                {
                    if (!map.IsEmpty(x, y))
                    {
                        DrawMap(x, -y, ref index);
                    }
                }
            }

            mesh.Clear();
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.RecalculateNormals();
        }

        private void DrawMap(int x, int y, ref int triangleIndex)
        {
            vertices.Add(new Vector2(x, y));
            vertices.Add(new Vector2(x + 1, y));
            vertices.Add(new Vector2(x + 1, y - 1));
            vertices.Add(new Vector2(x, y - 1));

            triangles.Add(triangleIndex);
            triangles.Add(triangleIndex + 1);
            triangles.Add(triangleIndex + 3);
            triangles.Add(triangleIndex + 1);
            triangles.Add(triangleIndex + 2);
            triangles.Add(triangleIndex + 3);
            triangleIndex += 4;
        }
    }
}