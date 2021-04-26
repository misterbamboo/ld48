using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Upgrade 
{
    public enum UpgradeType
    {
        SpeedUpgrade,
        OxygenUpgrade,
        HealthUpgrade,
        LightUpgrade
    }

    public static int getCost(UpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            
             default:   
            case UpgradeType.HealthUpgrade: return 50; 
            case UpgradeType.LightUpgrade: return 75;
            case UpgradeType.OxygenUpgrade: return 100;
            case UpgradeType.SpeedUpgrade: return 150;
            
        }
    }

    public static Sprite getSprite(UpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            default:
            case UpgradeType.HealthUpgrade: return GameAssets.instance.healthUpgradeImage;
            case UpgradeType.LightUpgrade: return GameAssets.instance.lightUpgradeImage;
            case UpgradeType.OxygenUpgrade: return GameAssets.instance.oxygenUpgradeImage;
            case UpgradeType.SpeedUpgrade: return GameAssets.instance.speedUpgradeImage;
        }
    }
}
