using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.MapGeneration
{
    public class Path
    {
        private int height;
        private int width;

        private List<int> path;
        private List<int> pathSize;
        private int minPathSize;
        private int maxPathSize;
        private int currentGenPathSize;
        private int pathGenBufferBeforeReducing = 10;

        public Path(int initialHeight, int width, int minPathSize, int maxPathSize)
        {
            height = initialHeight;
            this.width = width;
            currentGenPathSize = width;

            this.minPathSize = minPathSize;
            this.maxPathSize = maxPathSize;

            path = new List<int>(height);
            pathSize = new List<int>(height);
        }

        public void Generate()
        {
            int pathX = Random.Range(0, width - currentGenPathSize);
            for (int y = 0; y < height; y++)
            {
                path.Add(pathX);
                pathSize.Add(currentGenPathSize);

                AdjustPathGenSize();
                MovePathX(ref pathX);
            }
        }

        public bool Overlap(int x, int y)
        {
            if (x < 0 || y < 0) return false;
            if (x >= width || y >= height) return false;

            int leftPathX = path[y];
            int rightPathX = leftPathX + pathSize[y];
            return x >= leftPathX && x <= rightPathX;
        }

        private void MovePathX(ref int pathX)
        {
            int maxPathX = width - currentGenPathSize;

            int move = Random.Range(-2, 3); // -2 to 2 (inclusive)
            if (move < 0 && pathX <= 0)
            {
                move = 0;
            }
            else if (move > 0 && pathX >= maxPathX)
            {
                move = 0;
            }

            pathX += move;
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
