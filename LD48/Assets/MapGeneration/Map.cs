using Assets.Ressources;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.MapGeneration
{
    public class Map
    {
        public static Map Instance { get; private set; }

        public MapConfig Configuration => config;
        private MapConfig config;

        private List<MapCellType[]> mapRows;

        public int Height => mapRows.Count;

        public Map(MapConfig config)
        {
            this.config = config;
            mapRows = new List<MapCellType[]>();

            Instance = this;
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
                var row = new MapCellType[config.width];
                mapRows.Add(row);
            }
            var topY = Height - count;
            var bottomY = Height;

            FillRows(topY, bottomY);
        }

        private void FillRows(int topY, int bottomY)
        {
            for (float x = 0; x < config.width; x++)
            {
                for (float y = topY; y < bottomY; y++)
                {
                    if (ShouldPlaceTerrain(x, y))
                    {
                        PlaceTerrain((int)x, (int)y);

                        if (ShouldPlaceRessource(x, y))
                        {
                            PlaceRessource(x, y);
                        }
                    }
                }
            }
        }

        private void PlaceRessource(float x, float y)
        {
            IEnumerable<MapCellType> possibleRessources = RessourceChances.GetPossibilities((int)y);
            float count = possibleRessources.Count();
            float value = GetRessourceNoiseValue(config.width, x, y);
            mapRows[(int)y][(int)x] = possibleRessources.ElementAt((int)(value * count));
        }

        private bool ShouldPlaceRessource(float x, float y)
        {
            float ressourceValue = GetRessourceNoiseValue(config.width, x, y);
            float triggerValue = GetRessourceTriggerValue(y);
            return ressourceValue < triggerValue;
        }

        private bool ShouldPlaceTerrain(float x, float y)
        {
            float value = GetTerrainNoiseValue(config.width, x, y);
            float triggerValue = GetTerrainTriggerValue(y);
            return value < triggerValue;
        }

        private float GetTerrainTriggerValue(float y)
        {
            // Get terrain density
            float divider = y * config.deepnessDensityValueMultiplicator;
            divider += config.deepnessDensityValueSideShift;
            float dividedValue = 1 / divider;
            return dividedValue + config.deepnessDensityValueHeightShift;
        }

        private float GetRessourceTriggerValue(float y)
        {
            // Get resource density 
            float divider = y * config.ressourceDensityValueMultiplicator;
            float dividedValue = 1 / divider;
            return dividedValue + config.ressourceDensityValueHeightShift;
        }

        private float GetTerrainNoiseValue(float mapSize, float x, float y)
        {
            float xRatio = x * config.baseNoiseSize / mapSize;
            float yRatio = y * config.baseNoiseSize / mapSize;
            return Mathf.PerlinNoise(xRatio, yRatio);
        }

        private float GetRessourceNoiseValue(float mapSize, float x, float y)
        {
            float xRatio = x * config.ressourceNoiseSize / mapSize;
            float yRatio = y * config.ressourceNoiseSize / mapSize;
            return Mathf.PerlinNoise(xRatio, yRatio);
        }

        public void RemoveRessource(IRessource ressource)
        {
            mapRows[ressource.SpawnY][ressource.SpawnX] = MapCellType.Terrain;
        }

        private void PlaceTerrain(int x, int y)
        {
            PlaceCellType(x, y, MapCellType.Terrain);
        }

        private void PlaceCellType(int x, int y, MapCellType mapCellType)
        {
            if (x < 0 || y < 0) return;
            if (x >= config.width || y >= Height) return;
            mapRows[y][x] = mapCellType;
        }

        public bool IsRessource(int x, int y)
        {
            return
                mapRows[y][x] == MapCellType.Iron ? true :
                mapRows[y][x] == MapCellType.Copper ? true :
                mapRows[y][x] == MapCellType.Gold ? true :
                mapRows[y][x] == MapCellType.Platinum ? true :
                mapRows[y][x] == MapCellType.Diamond ? true :
                false;
        }

        public MapCellType GetCellType(int x, int y)
        {
            return mapRows[y][x];
        }
    }
}
