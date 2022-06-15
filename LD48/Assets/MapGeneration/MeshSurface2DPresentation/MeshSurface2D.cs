using Assets.MeshSurface2DPresentation;
using System.Collections.Generic;
using UnityEngine;

namespace MeshSurface2DPresentation
{
    public class MeshSurface2D
    {
        public float FillMinGrayScale { get; set; }
        public float MeshSizeScale { get; set; }

        public bool Inverted { get; set; }

        private int width;
        private int height;
        private Vector2 size;

        private float[,] gridScales;
        private MarchingSquare[,] marchingSquares;

        public Mesh Mesh => mesh;

        private Mesh mesh;
        private List<Vector3> vertices;
        private List<Vector2> uvs;
        private List<int> triangles;
        private int verticesIndex = 0;


        public MeshSurface2D(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.size = new Vector2(width, height);
            gridScales = new float[width, height];

            vertices = new List<Vector3>(width * height);
            uvs = new List<Vector2>(width * height);
            triangles = new List<int>(width * height);

            mesh = new Mesh
            {
                indexFormat = UnityEngine.Rendering.IndexFormat.UInt32
            };

            InitMarchingSquares();
        }

        private void InitMarchingSquares()
        {
            marchingSquares = new MarchingSquare[width - 1, height - 1];
            for (int x = 0; x < width - 1; x++)
            {
                for (int y = 0; y < height - 1; y++)
                {
                    marchingSquares[x, y] = new MarchingSquare(gridScales, x, y);
                }
            }
        }

        public void PrintToMesh(Texture2D texture)
        {
            UpdateGridSwitches(texture);
            RefreshMarchingSquares();
            BuildMeshData();
            UpdateMeshData();
        }

        private void UpdateGridSwitches(Texture2D texture)
        {
            float textWidth = texture.width;
            float textHeight = texture.height;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var textX = (float)x / width * textWidth;
                    var textY = (float)y / height * textHeight;

                    var grayscale = texture.GetPixel((int)textX, (int)textY).grayscale;
                    gridScales[x, y] = GetScaleValue(grayscale);
                }
            }
        }

        private float GetScaleValue(float grayscale)
        {
            return Inverted ? 1 - grayscale : grayscale;
        }

        private void RefreshMarchingSquares()
        {
            for (int x = 0; x < width - 1; x++)
            {
                for (int y = 0; y < height - 1; y++)
                {
                    marchingSquares[x, y].Refresh(FillMinGrayScale);
                }
            }
        }

        private void BuildMeshData()
        {
            vertices.Clear();
            uvs.Clear();
            triangles.Clear();
            verticesIndex = 0;

            for (int x = 0; x < width - 1; x++)
            {
                for (int y = 0; y < height - 1; y++)
                {
                    var squareTriangles = marchingSquares[x, y].GetTriangles();
                    AddTriangles(squareTriangles);
                }
            }
        }

        private void AddTriangles(MeshTriangle[] squareTriangles)
        {
            foreach (var triangle in squareTriangles)
            {
                vertices.Add(triangle.Point1 * MeshSizeScale);
                uvs.Add(triangle.Point1 / size);
                triangles.Add(verticesIndex);
                verticesIndex++;

                vertices.Add(triangle.Point2 * MeshSizeScale);
                uvs.Add(triangle.Point1 / size);
                triangles.Add(verticesIndex);
                verticesIndex++;

                vertices.Add(triangle.Point3 * MeshSizeScale);
                uvs.Add(triangle.Point1 / size);
                triangles.Add(verticesIndex);
                verticesIndex++;
            }
        }

        private void UpdateMeshData()
        {
            mesh.Clear();
            mesh.vertices = vertices.ToArray();
            mesh.uv = uvs.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.RecalculateNormals();
            mesh.Optimize();
        }
    }
}
