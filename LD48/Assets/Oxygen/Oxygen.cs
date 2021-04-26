using Assets.Ressources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.OxygenManagement
{
    public interface IOxygen
    {
        float Quantity { get; }

        float Capacity { get;  }
    }

    public class Oxygen : MonoBehaviour, IOxygen
    {
        public static IOxygen Instance { get; private set; }

        public float Quantity { get; private set; }

        public float Capacity { get; private set; }

        [SerializeField] private float startingQuantity = 100;
        [SerializeField] private float startingCapacity = 100;
        [SerializeField] private float reductionPerSec = 3f + 1f / 3f; // 30 seconds
        [SerializeField] private float recuperationPerSec = 20f; // 5 seconds

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            Quantity = startingQuantity;
            Capacity = startingCapacity;
        }

        void Update()
        {
            if (Submarine.Instance.Deepness > 0 && !Game.Instance.Invincible)
            {
                Quantity -= Time.deltaTime * reductionPerSec;
            }
            else
            {
                Quantity += Time.deltaTime * recuperationPerSec;
            }

            Quantity = Mathf.Clamp(Quantity, 0, Capacity);

            UpdateOxygen();
        }

        private void UpdateOxygen()
        {
            
            if (Submarine.Instance.OxygenUpgradeBought)
            {
                Capacity += 50;
                Submarine.Instance.OxygenUpgradeBought = false;
            }
            
        }
    }
}