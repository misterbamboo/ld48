using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopCustomer
{
   void BoughtUpgrade(Upgrade.UpgradeType upgradeType);
}
