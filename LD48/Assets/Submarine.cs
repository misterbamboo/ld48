using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public interface ISubmarine
    {
        int Deepness { get; }

        float SensitiveDeepness { get; }
        
        bool SpeedUpgradeBought { get; set; }
        
        bool OxygenUpgradeBought { get; set; }
        
        bool LifeUpgradeBought { get; set; }
        
        bool LightUpgradeBought { get; set; }
        bool HookUpgradeBought { get; set; }
    }

    public class Submarine : MonoBehaviour, ISubmarine, IShopCustomer
    {
        public static ISubmarine Instance { get; private set; }

        public static GameObject GameObject { get; private set; }

        public int Deepness => (int)SensitiveDeepness;

        public float SensitiveDeepness => -Mathf.Clamp(transform.position.y, float.MinValue, 0);

        public bool SpeedUpgradeBought { get; set; } = false;
        public bool OxygenUpgradeBought { get; set; }
        public bool LifeUpgradeBought { get; set; }
        
        public bool LightUpgradeBought { get; set; }

        public bool HookUpgradeBought { get; set; }
        
        private Rigidbody rb;

        private void Awake()
        {
            Instance = this;
            GameObject = gameObject;
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void IncreaseOxygen()
        {
            OxygenUpgradeBought = true;
        }
        private void IncreaseHealth()
        {
            LifeUpgradeBought = true;
        }
        private void IncreaseLight()
        {
            LightUpgradeBought = true;
        }
        private void IncreaseSpeed()
        {
            SpeedUpgradeBought = true;
        }
        private void IncreaseHook()
        {
            HookUpgradeBought = true;
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
                case Upgrade.UpgradeType.HookUpgrade:  IncreaseHook();
                    break;
               
            }
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
