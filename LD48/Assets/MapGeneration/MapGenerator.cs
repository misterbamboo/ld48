using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Assets.MapGeneration
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private int numberofpaths = 3;
        [SerializeField] private int minPathSize = 2;
        [SerializeField] private int maxPathSize = 15;

        [SerializeField] private Transform playerPosition;
        [SerializeField] private int underPlayerGenerationBuffer = 25;

        private Mesh mesh;

        private List<Vector3> vertices = new List<Vector3>();

        private List<int> triangles = new List<int>();

        private Map map;

        private Path[] paths;

        private int mapWidth = 100;
        private int mapInitialHeight = 50;

        void Start()
        {
            Init();
            GeneratePoints(1000);

            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;

            ConvertMapToMesh();

            mesh.Clear();
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.RecalculateNormals();
        }

        private void Init()
        {
            map = new Map(mapWidth, mapInitialHeight);
            paths = new Path[numberofpaths];
            for (int i = 0; i < numberofpaths; i++)
            {
                paths[i] = new Path(mapInitialHeight, mapWidth, minPathSize, maxPathSize);
                paths[i].Generate();
            }
        }

        private void GeneratePoints(int numberOfPoints)
        {
            for (int i = 0; i < numberOfPoints; i++)
            {
                int x = Random.Range(0, mapWidth);
                int y = Random.Range(0, mapInitialHeight);
                int radius = Random.Range(3, 10);
                PlaceCircle(x, y, radius);
            }
        }

        private void PlaceCircle(int x, int y, int radius)
        {
            for (int r = 0; r <= radius; r++)
            {
                float fullAngle = Mathf.PI * 2;
                for (float a = 0; a < fullAngle; a += fullAngle / 100f)
                {
                    int xDiff = (int)(r * Mathf.Cos(a));
                    int yDiff = (int)(r * Mathf.Sin(a));
                    PlacePoint(x + xDiff, y + yDiff);
                }
            }
        }

        private void PlacePoint(int x, int y)
        {
            if (OverlapPath(x, y)) return;
            map.Place(x, y);
        }

        private bool OverlapPath(int x, int y)
        {
            return paths.Any(p => p.Overlap(x, y));
        }

        private void Update()
        {
            int position = GetPlayerPosition();
            int generationPosition = position + underPlayerGenerationBuffer;

            if(map.Height < generationPosition)
            {

            }
        }

        private int GetPlayerPosition()
        {
            // Invert Y, because in Unity world, the player is going deeper and deeper
            // so the Y position will become more and more negative
            // but in MapGeneration context, the Y position is always positive
            return (int)-playerPosition.position.y;
        }

        private void ConvertMapToMesh()
        {
            int index = 0;
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapInitialHeight; y++)
                {
                    if (!map.IsEmpty(x, y))
                    {
                        DrawMap(x, -y, ref index);
                    }
                }
            }
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