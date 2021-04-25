using Assets.MapGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Ressources
{
    public interface IBoxCollider2DPooler
    {
        GameObject GetOne();
        void RecycleOutOfRange(int fromY, int toY);
    }

    public class BoxCollider2DPooler : MonoBehaviour, IBoxCollider2DPooler
    {
        public static IBoxCollider2DPooler Instance { get; private set; }

        [SerializeField] private int PoolerSize = 400;

        private List<GameObject> generated;

        private void Awake()
        {
            generated = new List<GameObject>();
            for (int i = 0; i < PoolerSize; i++)
            {
                GetOne().SetActive(false);
            }
            Instance = this;
        }

        public GameObject GetOne()
        {
            var inactiveGameObject = generated.FirstOrDefault(g => !g.activeInHierarchy);
            if (inactiveGameObject != null)
            {
                inactiveGameObject.SetActive(true);
                return inactiveGameObject;
            }
            else
            {
                var go = new GameObject();
                go.transform.parent = transform;
                var collider = go.AddComponent<BoxCollider2D>();
                collider.offset = new Vector2(0.5f, -0.5f);
                generated.Add(go);
                return go;
            }
        }

        public void RecycleOutOfRange(int fromY, int toY)
        {
            foreach (var go in generated)
            {
                if (go.transform.position.y > -fromY || go.transform.position.y < -toY)
                {
                    go.SetActive(false);
                }
            }
        }
    }
}
