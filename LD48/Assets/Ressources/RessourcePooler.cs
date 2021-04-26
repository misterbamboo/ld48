using Assets.MapGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Ressources
{
    public interface IRessourcePooler
    {
        GameObject GetOne(MapCellType mapCellType);
        void RecycleOutOfRange(int fromY, int toY);
    }

    public class RessourcePooler : MonoBehaviour, IRessourcePooler
    {
        public static IRessourcePooler Instance { get; private set; }

        [Header("Prefabs")]
        [SerializeField] private GameObject IronPrefab;
        [SerializeField] private GameObject CopperPrefab;
        [SerializeField] private GameObject GoldPrefab;
        [SerializeField] private GameObject PlatinumPrefab;
        [SerializeField] private GameObject DiamondPrefab;

        private Dictionary<MapCellType, GameObject> prefabPerRessourceType;

        [SerializeField] private int PoolerSize = 100;

        private Dictionary<MapCellType, List<GameObject>> generated;

        private void Awake()
        {
            prefabPerRessourceType = new Dictionary<MapCellType, GameObject>()
            {
                { MapCellType.Iron, IronPrefab },
                { MapCellType.Copper, CopperPrefab },
                { MapCellType.Gold, GoldPrefab },
                { MapCellType.Platinum, PlatinumPrefab },
                { MapCellType.Diamond, DiamondPrefab },
            };

            generated = new Dictionary<MapCellType, List<GameObject>>();
            for (int i = 0; i < PoolerSize; i++)
            {
                GetOne(MapCellType.Iron).SetActive(false);
                GetOne(MapCellType.Copper).SetActive(false);
                GetOne(MapCellType.Gold).SetActive(false);
                GetOne(MapCellType.Platinum).SetActive(false);
                GetOne(MapCellType.Diamond).SetActive(false);
            }

            Instance = this;
        }

        public GameObject GetOne(MapCellType mapCellType)
        {
            switch (mapCellType)
            {
                case MapCellType.Iron:
                case MapCellType.Copper:
                case MapCellType.Gold:
                case MapCellType.Platinum:
                case MapCellType.Diamond:
                    return GetGameObjectOf(mapCellType);
                default:
                    throw new NotImplementedException("Ressource prefab unknowned");
            }
        }

        private GameObject GetGameObjectOf(MapCellType ressourceType)
        {
            var generatedCandidates = GetGeneratedGameObjects(ressourceType);
            var inactiveGameObject = generatedCandidates.FirstOrDefault(g => !g.activeInHierarchy);

            if (inactiveGameObject != null)
            {
                inactiveGameObject.SetActive(true);
                return inactiveGameObject;
            }
            else
            {
                var prefab = prefabPerRessourceType[ressourceType];
                var newInstance = Instantiate(prefab, transform);
                generatedCandidates.Add(newInstance);
                return newInstance;
            }
        }

        private List<GameObject> GetGeneratedGameObjects(MapCellType mapCellType)
        {
            if (!generated.ContainsKey(mapCellType))
            {
                generated[mapCellType] = new List<GameObject>();
            }
            return generated[mapCellType];
        }

        public void RecycleOutOfRange(int fromY, int toY)
        {
            foreach (var generatedCandidates in generated.Values)
            {
                foreach (var generatedCandidate in generatedCandidates)
                {
                    if (generatedCandidate.transform.position.y < fromY || generatedCandidate.transform.position.y > toY)
                    {
                        generatedCandidate.SetActive(false);
                    }
                }
            }
        }
    }
}
