using Assets.MapGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.WaterSurfaceManagement
{
    public class WaterSurface : MonoBehaviour
    {
        [SerializeField] WaterSurfaceDuplication basicWaterSurface;

        [SerializeField] private int followDistance = 30;

        private WaterSurfaceDrawer waterSurfaceDrawer;

        private void Update()
        {
            if (waterSurfaceDrawer == null)
            {
                Init();
            }

            FollowPlayer();

            waterSurfaceDrawer.MoveSurface();
        }

        private void FollowPlayer()
        {
            if (Submarine.Instance.Deepness > followDistance)
            {
                var pos = transform.position;
                pos.y = -Submarine.Instance.Deepness + followDistance;
                transform.position = pos;
            }
            else
            {
                var pos = transform.position;
                pos.y = 0;
                transform.position = pos;
            }
        }

        private void Init()
        {
            transform.position = new Vector3(-Map.Instance.Configuration.width, transform.position.y, transform.position.z);

            // X3 because : 1 widht left, actual width and 1 width right
            waterSurfaceDrawer = new WaterSurfaceDrawer(Map.Instance.Configuration.width * 3, followDistance * 2, 0.1f);
            waterSurfaceDrawer.Init();
            GetComponent<MeshFilter>().sharedMesh = waterSurfaceDrawer.Mesh;
            
            basicWaterSurface.Init(waterSurfaceDrawer.Mesh);
        }
    }
}
