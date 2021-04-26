using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.WaterSurfaceManagement
{
    public class WaterSurfaceDrawer
    {
        public Mesh Mesh => mesh;

        private Mesh mesh;

        private List<float> perWaveHeight = new List<float>();
        private List<Vector3> vertices = new List<Vector3>();
        private Vector3[] meshVertices;

        private List<int> triangles = new List<int>();

        private int width;

        private float steps;

        private float height;

        public WaterSurfaceDrawer(int width, int height, float steps)
        {
            this.width = width;
            this.steps = steps;
            this.height = height;

            mesh = new Mesh();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        }

        private float timeCursor;
        public void MoveSurface()
        {
            timeCursor += Time.deltaTime * 2;

            for (float i = 0; i < meshVertices.Length / 4; i++)
            {
                var leftHeight = perWaveHeight[(int)i];
                var rightHeight = perWaveHeight[(int)i];

                int leftWave = (int)(i * 4);
                int rightWave = (int)((i * 4) + 1);
                meshVertices[leftWave] = new Vector2(meshVertices[leftWave].x, Mathf.Sin(timeCursor + i / 5f) * leftHeight);
                meshVertices[rightWave] = new Vector2(meshVertices[rightWave].x, Mathf.Sin(timeCursor + i / 5f) * rightHeight);
            }

            mesh.vertices = meshVertices;
        }

        public void Init()
        {
            vertices.Clear();
            triangles.Clear();

            perWaveHeight = new List<float>();
            float waveHeight = 0.15f;
            int index = 0;
            for (float x = 0; x <= width; x += steps)
            {
                DrawWave(x, ref index);

                waveHeight += Random.Range(-0.02f, 0.02f);
                waveHeight = Mathf.Clamp(waveHeight, 0.02f, 0.25f);
                perWaveHeight.Add(waveHeight);
            }

            mesh.Clear();
            mesh.vertices = meshVertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.RecalculateNormals();
        }

        private void DrawWave(float x, ref int triangleIndex)
        {
            vertices.Add(new Vector2(x, 0)); // surface left
            vertices.Add(new Vector2(x + steps, 0)); // surface right
            vertices.Add(new Vector2(x, -height)); // deep water left
            vertices.Add(new Vector2(x + steps, -height)); // deep water right

            triangles.Add(triangleIndex);
            triangles.Add(triangleIndex + 3);
            triangles.Add(triangleIndex + 2);
            triangles.Add(triangleIndex);
            triangles.Add(triangleIndex + 1);
            triangles.Add(triangleIndex + 3);
            triangleIndex += 4;
        }
    }
}