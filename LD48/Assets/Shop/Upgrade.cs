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
        LightUpgrade,
        HookUpgrade,
        HullUpgrade
        
    }

    public static int getCost(UpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            
             default:   
            case UpgradeType.HealthUpgrade: return 10; 
            case UpgradeType.LightUpgrade: return 25;
            case UpgradeType.OxygenUpgrade: return 50;
            case UpgradeType.SpeedUpgrade: return 40;
             case UpgradeType.HookUpgrade: return 40;
             case UpgradeType.HullUpgrade: return 20;
            
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
            case UpgradeType.HookUpgrade: return GameAssets.instance.hookUpgradeImage;
            case UpgradeType.HullUpgrade: return GameAssets.instance.hullUpgradeImage;
        }
    }
}
