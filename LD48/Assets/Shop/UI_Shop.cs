using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class UI_Shop : MonoBehaviour
{
    private Transform container;

    private Transform shopUpgradeTemplate;

   
    private void Awake()
    {
        container = transform.Find("container");
        shopUpgradeTemplate = container.Find("shopItemTemplate");
        //shopUpgradeTemplate.gameObject.SetActive(false);
           
    }

    private void Start()
    {
        createUpgradeButton(Upgrade.getSprite(Upgrade.UpgradeType.HealthUpgrade), "Health upgrade", Upgrade.getCost(Upgrade.UpgradeType.HealthUpgrade),0);
        createUpgradeButton(Upgrade.getSprite(Upgrade.UpgradeType.OxygenUpgrade), "Oxygen upgrade", Upgrade.getCost(Upgrade.UpgradeType.OxygenUpgrade),1);
        createUpgradeButton(Upgrade.getSprite(Upgrade.UpgradeType.LightUpgrade), "Light upgrade", Upgrade.getCost(Upgrade.UpgradeType.LightUpgrade),2);
        createUpgradeButton(Upgrade.getSprite(Upgrade.UpgradeType.SpeedUpgrade), "Speed upgrade", Upgrade.getCost(Upgrade.UpgradeType.SpeedUpgrade),3);
        
        shopUpgradeTemplate.gameObject.SetActive(false);
    }

    private void createUpgradeButton(Sprite upgradeSprite, string upgradeName, int upgradeCost, int positionIndex)
    {
        Transform shopUpgradeTransform = Instantiate(shopUpgradeTemplate, container);
        RectTransform shopUpgradeRectTransform = shopUpgradeTransform.GetComponent<RectTransform>();

        float shopUpgradeHeight = 150f;
        shopUpgradeRectTransform.anchoredPosition = new Vector2(0, (-shopUpgradeHeight * positionIndex)+200);
        
        shopUpgradeTransform.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(upgradeName);

        if (shopUpgradeTransform.Find("nameText").GetComponent<TextMeshProUGUI>() != null)
        {
            print(shopUpgradeTransform.Find("nameText").GetComponent<TextMeshProUGUI>());
        }

        shopUpgradeTransform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(upgradeCost.ToString());

        shopUpgradeTransform.Find("itemImage").GetComponent<Image>().sprite = upgradeSprite;
    }

}
