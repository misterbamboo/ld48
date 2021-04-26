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
        bool SpeedUpgradeBought { get; }
        
        bool OxygenUpdateBought { get; }
    }

    public class Submarine : MonoBehaviour, ISubmarine, IShopCustomer
    {

        public static ISubmarine Instance { get; private set; }

        public int Deepness => (int)(-Mathf.Clamp(transform.position.y, float.MinValue, 0));



        public bool SpeedUpgradeBought { get; set; } = false;
        public bool OxygenUpdateBought { get; set; }

        private Rigidbody rb;

       
        
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void IncreaseOxygen()
        {
            OxygenUpdateBought = true;
        }
        private void IncreaseHealth()
        {
            
        }
        private void IncreaseLight()
        {
            
        }
        private void IncreaseSpeed()
        {
            SpeedUpgradeBought = true;
        }

        public void BoughtUpgrade(Upgrade.UpgradeType upgradeType)
        {

            switch (upgradeType)
            {
               case Upgrade.UpgradeType.OxygenUpgrade:  IncreaseOxygen();
                   break;
               case Upgrade.UpgradeType.HealthUpgrade:  IncreaseHealth();
                   break;
               case Upgrade.UpgradeType.LightUpgrade:  IncreaseLight();
                   break;
               case Upgrade.UpgradeType.SpeedUpgrade:  IncreaseSpeed();
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
