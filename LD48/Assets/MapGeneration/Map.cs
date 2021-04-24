using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.MapGeneration
{
    public class Map
    {
        private List<bool[]> mapRows;
        private int width;

        public int Height => mapRows.Count;

        public Map(int width, int initialHeight)
        {
            this.width = width;
            mapRows = new List<bool[]>(initialHeight);
            for (int i = 0; i < initialHeight; i++)
            {
                mapRows.Add(new bool[width]);
            }
        }

        public void Place(int x, int y)
        {
            if (x < 0 || y < 0) return;
            if (x >= width || y >= Height) return;
            mapRows[y][x] = true;
        }

        public bool IsEmpty(int x, int y)
        {
            if (x < 0 || y < 0) return true;
            if (x >= width || y >= Height) return true;
            return !mapRows[y][x];
        }
    }
}
