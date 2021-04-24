using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.MapGeneration
{
    public class Map
    {
        public MapConfig Configuration => config;
        private MapConfig config;

        private List<MapCellType[]> mapRows;
        private readonly IEnumerable<Path> paths;

        public int Height => mapRows.Count;

        public Map(MapConfig config, IEnumerable<Path> paths)
        {
            this.config = config;
            this.paths = paths;
            mapRows = new List<MapCellType[]>();
        }

        public bool IsEmpty(int x, int y)
        {
            if (x < 0 || y < 0) return true;
            if (x >= config.width || y >= Height) return true;
            return mapRows[y][x] == MapCellType.Empty;
        }

        public void Generate(int count)
        {
            for (int c = 0; c < count; c++)
            {
                mapRows.Add(new MapCellType[config.width]);
            }
            var topY = Height - count;
            var bottomY = Height;
            GenerateCircles(topY, bottomY);
            GenerateRessources(topY, bottomY);
        }

        private void GenerateCircles(int topY, int bottomY)
        {
            int radius = Random.Range(config.circleMinRadius, config.circleMaxRadius);
            int lastX = Random.Range(0, config.width);
            int lastY = Random.Range(topY + radius, bottomY - radius);

            for (int i = 0; i < config.circlesPerGeneration; i++)
            {
                radius = Random.Range(config.circleMinRadius, config.circleMaxRadius);

                int dispersion = Random.Range(config.circlesGenerationDispersionMin, config.circlesGenerationDispersionMax);
                int minX = GetCircleMinXRange(lastX, radius, dispersion);
                int maxX = GetCircleMaxXRange(lastX, radius, dispersion);
                int x = Random.Range(minX, maxX);

                int minY = GetCircleMinYRange(lastY, radius, topY, dispersion);
                int maxY = GetCircleMaxYRange(lastY, radius, bottomY, dispersion);
                int y = Random.Range(minY, maxY);

                PlaceCircle(x, y, radius);

                lastX = x;
                lastY = y;
            }
        }

        private int GetCircleMinXRange(int lastX, int radius, int dispersion)
        {
            var minX = lastX - radius * dispersion;
            if (minX < radius) return radius;
            return minX;
        }

        private int GetCircleMaxXRange(int lastX, int radius, int dispersion)
        {
            var maxX = lastX + radius * dispersion;
            if (maxX >= config.width - radius) return config.width - radius;
            return maxX;
        }

        private int GetCircleMinYRange(int lastY, int radius, int topY, int dispersion)
        {
            var minY = lastY - radius * dispersion;
            if (minY < topY) return topY;
            return minY;
        }

        private int GetCircleMaxYRange(int lastY, int radius, int bottomY, int dispersion)
        {
            var maxY = lastY + radius * dispersion;
            if (maxY >= bottomY - radius) return bottomY - radius;
            return maxY;
        }

        private void PlaceCircle(int x, int y, int radius)
        {
            float fullAngle = Mathf.PI * 2;
            for (float a = 0; a < fullAngle; a += fullAngle / 100f)
            {
                int radiusAddedNoise = Random.Range(0, config.circleRadiusAddedNoise + 1);
                for (int r = 0; r <= radius + radiusAddedNoise; r++)
                {
                    int xDiff = (int)(r * Mathf.Cos(a));
                    int yDiff = (int)(r * Mathf.Sin(a));
                    PlaceTerrain(x + xDiff, y + yDiff);
                }
            }
        }

        private void PlaceTerrain(int x, int y)
        {
            if (OverlapPath(x, y)) return;
            PlaceCellType(x, y, MapCellType.Terrain);
        }

        private bool OverlapPath(int x, int y)
        {
            return paths.Any(p => p.Overlap(x, y));
        }

        private void PlaceCellType(int x, int y, MapCellType mapCellType)
        {
            if (x < 0 || y < 0) return;
            if (x >= config.width || y >= Height) return;
            mapRows[y][x] = mapCellType;
        }

        private void GenerateRessources(int topY, int bottomY)
        {
            for (int x = 0; x < config.width; x++)
            {
                for (int y = topY; y < bottomY; y++)
                {
                    if (mapRows[y][x] == MapCellType.Terrain)
                    {
                        var randomValue = Random.value;
                        bool spawnCopper = randomValue < config.ressourceGenerationCopperPercent;
                        if (spawnCopper)
                        {
                            mapRows[y][x] = MapCellType.Copper;
                        }
                    }
                }
            }
        }

        public bool IsRessource(int x, int y, MapCellType mapCellType)
        {
            return mapRows[y][x] == mapCellType;
        }
    }
}
