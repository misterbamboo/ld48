using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.OxygenManagement;
using UnityEngine;

namespace Assets
{
    public interface ISubmarine
    {
        int Deepness { get; }
    }

    public class Submarine : MonoBehaviour, ISubmarine, IShopCustomer
    {

        public static ISubmarine Instance { get; private set; }

        public int Deepness => (int)(-Mathf.Clamp(transform.position.y, float.MinValue, 0));

        private Rigidbody rb;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void IncreaseHealth()
        {
            
        }

        public void BoughtUpgrade(Upgrade.UpgradeType upgradeType)
        {

            switch (upgradeType)
            {
               case Upgrade.UpgradeType.HealthUpgrade:  IncreaseHealth();
                   break;
               
            }
            Debug.Log("Bought item : " + upgradeType);
        }

        public bool TrySpendMoneyAmount(int spendMoneyAmount)
        {
            if (InventoryManager.Instance.inventoryReward >= spendMoneyAmount)
            {
                InventoryManager.Instance.inventoryReward -= spendMoneyAmount;
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
