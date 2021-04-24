using Assets.MapGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        [SerializeField] private GameObject CopperPrefab;

        private Dictionary<MapCellType, List<GameObject>> generated;

        private void Awake()
        {
            generated = new Dictionary<MapCellType, List<GameObject>>();
            Instance = this;
        }

        public GameObject GetOne(MapCellType mapCellType)
        {
            switch (mapCellType)
            {
                case MapCellType.Copper:
                    return GetGameObjectOf(mapCellType);
                default:
                    throw new NotImplementedException("Ressource prefab unknowned");
            }
        }

        private GameObject GetGameObjectOf(MapCellType mapCellType)
        {
            var generatedCandidates = GetGeneratedGameObjects(mapCellType);
            var inactiveGameObject = generatedCandidates.FirstOrDefault(g => !g.activeInHierarchy);

            if (inactiveGameObject != null)
            {
                inactiveGameObject.SetActive(true);
                return inactiveGameObject;
            }
            else
            {
                var newInstance = Instantiate(CopperPrefab, transform);
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
