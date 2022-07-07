using Assets.Map.Infrastructure;
using Assets.Ressources;
using MeshSurface2D;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Map.Domain
{
    public class Map
    {
        public MapConfig Configuration => config;
        private MapConfig config;

        private Dictionary<Tuple<int, int>, MapCell> mapCells;

        public IMeshPointValueProvider GetTerrainValueProvider(Vector2 from, Vector2 to)
        {
            return new MapTerrainValueProvider(config, from, to);
        }

        public Map(MapConfig config)
        {
            this.config = config;
            mapCells = new Dictionary<Tuple<int, int>, MapCell>();
        }

        public bool IsEmpty(int x, int y)
        {
            var coord = new Tuple<int, int>(x, y);
            if (!mapCells.ContainsKey(coord))
            {
                return true;
            }

            return mapCells[coord].mapCellType == MapCellType.Empty;
        }

        public void GenerateRegion(Vector2 from, Vector2 to)
        {
            mapCells.Clear();

            var terrainValueProvider = GetTerrainValueProvider(from, to);
            for (float x = terrainValueProvider.Left; x < terrainValueProvider.Width; x++)
            {
                for (float y = terrainValueProvider.Bottom; y < terrainValueProvider.Height; y++)
                {
                    GenerateCell(terrainValueProvider, x, y);
                }
            }
        }

        private void GenerateCell(IMeshPointValueProvider terrainValueProvider, float x, float y)
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

        private void PlaceRessource(float x, float y)
        {
            IEnumerable<MapCellType> possibleRessources = RessourceChances.GetPossibilities((int)y);
            float count = possibleRessources.Count();
            float value = GetRessourceNoiseValue(config.width, x, y);
            var coord = new Tuple<int, int>((int)x, (int)y);
            var previousValue = mapCells[coord].value;
            mapCells[coord] = new MapCell
            {
                mapCellType = possibleRessources.ElementAt((int)(value * count)),
                value = previousValue
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
            var coord = new Tuple<int, int>(ressource.SpawnX, ressource.SpawnY);
            var previousValue = mapCells[coord].value;
            mapCells[coord] = new MapCell
            {
                mapCellType = MapCellType.Terrain,
                value = previousValue
            };
        }

        private void PlaceTerrain(int x, int y, float value)
        {
            PlaceCellType(x, y, MapCellType.Terrain, value);
        }

        private void PlaceCellType(int x, int y, MapCellType mapCellType, float value)
        {
            mapCells[new Tuple<int, int>(x, y)] = new MapCell
            {
                mapCellType = mapCellType,
                value = value,
            };
        }

        public bool IsRessource(int x, int y)
        {
            var coord = new Tuple<int, int>(x, y);
            return
                mapCells[coord].mapCellType == MapCellType.Iron ? true :
                mapCells[coord].mapCellType == MapCellType.Copper ? true :
                mapCells[coord].mapCellType == MapCellType.Gold ? true :
                mapCells[coord].mapCellType == MapCellType.Platinum ? true :
                mapCells[coord].mapCellType == MapCellType.Diamond ? true :
                false;
        }

        public MapCell GetCell(int x, int y)
        {
            var coord = new Tuple<int, int>(x, y);
            return mapCells[coord];
        }
    }
}
