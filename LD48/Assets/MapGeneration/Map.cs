using Assets.Ressources;
using MeshSurface2DPresentation;
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

        private List<MapCell[]> mapRows;

        public int Height => mapRows.Count;

        public IMeshPointValueProvider GetTerrainValueProvider(int fromY, int toY)
        {
            return new MapTerrainValueProvider(config, fromY, toY);
        }

        public Map(MapConfig config)
        {
            this.config = config;
            mapRows = new List<MapCell[]>();

            Instance = this;
        }

        public bool IsEmpty(int x, int y)
        {
            if (x < 0 || y < 0) return true;
            if (x >= config.width || y >= Height) return true;
            return mapRows[y][x].mapCellType == MapCellType.Empty;
        }

        public void Generate(int count)
        {
            for (int c = 0; c < count; c++)
            {
                var row = new MapCell[config.width];
                mapRows.Add(row);
            }
            var topY = Height - count;
            var bottomY = Height;

            FillRows(topY, bottomY);
        }

        private void FillRows(int topY, int bottomY)
        {
            var terrainValueProvider = GetTerrainValueProvider(topY, bottomY);
            for (float x = 0; x < config.width; x++)
            {
                for (float y = topY; y < bottomY; y++)
                {
                    float value = terrainValueProvider.ValueAt(x, y);
                    if (ShouldPlaceTerrain(value))
                    {
                        PlaceTerrain((int)x, (int)y, value);

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
            mapRows[(int)y][(int)x] = new MapCell
            {
                mapCellType = possibleRessources.ElementAt((int)(value * count))
            };
        }

        private bool ShouldPlaceRessource(float x, float y)
        {
            float ressourceValue = GetRessourceNoiseValue(config.width, x, y);
            float triggerValue = GetRessourceTriggerValue(y);
            return ressourceValue < triggerValue;
        }

        private bool ShouldPlaceTerrain(float value)
        {
            return value > 1;
        }

        private float GetRessourceTriggerValue(float y)
        {
            // Get resource density 
            float divider = y * config.ressourceDensityValueMultiplicator;
            float dividedValue = 1 / divider;
            return dividedValue + config.ressourceDensityValueHeightShift;
        }

        private float GetRessourceNoiseValue(float mapSize, float x, float y)
        {
            float xRatio = x * config.ressourceNoiseSize / mapSize;
            float yRatio = y * config.ressourceNoiseSize / mapSize;
            return Mathf.PerlinNoise(xRatio, yRatio);
        }

        public void RemoveRessource(IRessource ressource)
        {
            mapRows[ressource.SpawnY][ressource.SpawnX] = new MapCell
            {
                mapCellType = MapCellType.Terrain
            };
        }

        private void PlaceTerrain(int x, int y, float value)
        {
            PlaceCellType(x, y, MapCellType.Terrain, value);
        }

        private void PlaceCellType(int x, int y, MapCellType mapCellType, float value)
        {
            if (x < 0 || y < 0) return;
            if (x >= config.width || y >= Height) return;
            mapRows[y][x] = new MapCell
            {
                mapCellType = mapCellType,
                value = value,
            };
        }

        public bool IsRessource(int x, int y)
        {
            return
                mapRows[y][x].mapCellType == MapCellType.Iron ? true :
                mapRows[y][x].mapCellType == MapCellType.Copper ? true :
                mapRows[y][x].mapCellType == MapCellType.Gold ? true :
                mapRows[y][x].mapCellType == MapCellType.Platinum ? true :
                mapRows[y][x].mapCellType == MapCellType.Diamond ? true :
                false;
        }

        public MapCell GetCell(int x, int y)
        {
            return mapRows[y][x];
        }
    }
}
