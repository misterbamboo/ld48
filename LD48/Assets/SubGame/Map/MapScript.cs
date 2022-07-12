using Assets.Map.Infrastructure;
using Assets.Map.Presentation;
using UnityEngine;

namespace Assets.Map
{
    public interface IMap
    {
        float MapSizeBoundries();

        void RemoveRessource(int x, int y);
    }


    public class MapScript : MonoBehaviour, IMap
    {
        public static IMap Instance { get; private set; }

        [SerializeField] private MapConfig mapConfiguration;

        [SerializeField] private int mapPageSize = 100;
        [SerializeField] private Transform playerPosition;

        [SerializeField] private GameObject mapShapeMeshPrefab;

        private GameObject mapShapeGameObject;
        private MapController mapController;

        public float MapSizeBoundries()
        {
            return mapPageSize;
        }

        public void RemoveRessource(int x, int y)
        {
            mapController.RemoveRessourceAt(x, y);
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            mapController = new MapController(mapConfiguration, mapPageSize);
            mapController.MapDisplayedMeshChanged += MapController_MapDisplayedMeshChanged1;
        }

        private void Update()
        {
            mapController.Update(playerPosition.position);
        }

        private void MapController_MapDisplayedMeshChanged1(Mesh newMesh)
        {
            ReplaceMapShapeGameObject(newMesh);
        }

        private void ReplaceMapShapeGameObject(Mesh newMesh)
        {
            if (mapShapeGameObject != null)
            {
                Destroy(mapShapeGameObject);
            }

            mapShapeGameObject = Instantiate(mapShapeMeshPrefab);
            mapShapeGameObject.transform.position = new Vector3(0, 0, 0);
            mapShapeGameObject.GetComponent<MeshFilter>().sharedMesh = newMesh;
            mapShapeGameObject.GetComponent<MeshCollider>().sharedMesh = newMesh;
        }


    }
}