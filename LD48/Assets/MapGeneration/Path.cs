using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.MapGeneration
{
    public class Path
    {
        private int Height => path.Count;
        private int width;

        private List<int> path;
        private List<int> pathSize;
        private int minPathSize;
        private int maxPathSize;
        private int pathGenBufferBeforeReducing = 10;

        private int currentPathX;
        private int currentGenPathSize;

        public Path(int width, int minPathSize, int maxPathSize)
        {
            this.width = width;
            currentGenPathSize = width;
            currentPathX = 0;

            this.minPathSize = minPathSize;
            this.maxPathSize = maxPathSize;

            path = new List<int>();
            pathSize = new List<int>();
        }

        public void Generate(int count)
        {
            for (int c = 0; c < count; c++)
            {
                path.Add(currentPathX);
                pathSize.Add(currentGenPathSize);

                AdjustPathGenSize();
                MovePathX();
            }
        }

        public bool Overlap(int x, int y)
        {
            if (x < 0 || y < 0) return false;
            if (x >= width || y >= Height) return false;

            int leftPathX = path[y];
            int rightPathX = leftPathX + pathSize[y];
            return x >= leftPathX && x <= rightPathX;
        }

        private void MovePathX()
        {
            int maxPathX = width - currentGenPathSize;

            int move = Random.Range(-2, 3); // -2 to 2 (inclusive)
            if (move < 0 && currentPathX <= 0)
            {
                move = 0;
            }
            else if (move > 0 && currentPathX >= maxPathX)
            {
                move = 0;
            }

            currentPathX += move;
        }

        private void AdjustPathGenSize()
        {
            pathGenBufferBeforeReducing--;
            if (pathGenBufferBeforeReducing > 0) return;

            if (currentGenPathSize >= maxPathSize)
            {
                currentGenPathSize--;
            }
            else if (currentGenPathSize <= minPathSize)
            {
                currentGenPathSize++;
            }
            else
            {
                currentGenPathSize += (int)Random.Range(-1f, 1f);
            }
        }
    }
}
