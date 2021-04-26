using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using CodeMonkey.Utils;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class UI_Shop : MonoBehaviour
{
    private Transform container;

    private Transform shopUpgradeTemplate;

    private IShopCustomer shopCustomer;
    
    private void Awake()
    {
        container = transform.Find("container");
        shopUpgradeTemplate = container.Find("shopItemTemplate");
        
        
           
    }

    private void Start()
    {
        
        
        createUpgradeButton(Upgrade.UpgradeType.HealthUpgrade, Upgrade.getSprite(Upgrade.UpgradeType.HealthUpgrade), "Health pack", Upgrade.getCost(Upgrade.UpgradeType.HealthUpgrade),0);
        createUpgradeButton(Upgrade.UpgradeType.OxygenUpgrade, Upgrade.getSprite(Upgrade.UpgradeType.OxygenUpgrade), "Oxy upgrade", Upgrade.getCost(Upgrade.UpgradeType.OxygenUpgrade),1);
        createUpgradeButton(Upgrade.UpgradeType.LightUpgrade, Upgrade.getSprite(Upgrade.UpgradeType.LightUpgrade), "Light upgrade", Upgrade.getCost(Upgrade.UpgradeType.LightUpgrade),2);
        createUpgradeButton(Upgrade.UpgradeType.SpeedUpgrade, Upgrade.getSprite(Upgrade.UpgradeType.SpeedUpgrade), "Speed upgrade", Upgrade.getCost(Upgrade.UpgradeType.SpeedUpgrade),3);
        createUpgradeButton(Upgrade.UpgradeType.HookUpgrade, Upgrade.getSprite(Upgrade.UpgradeType.HookUpgrade), "Hook upgrade", Upgrade.getCost(Upgrade.UpgradeType.HookUpgrade),4);
        createUpgradeButton(Upgrade.UpgradeType.HullUpgrade, Upgrade.getSprite(Upgrade.UpgradeType.HullUpgrade), "Hull upgrade", Upgrade.getCost(Upgrade.UpgradeType.HullUpgrade),5);
        
        shopUpgradeTemplate.gameObject.SetActive(false);
        
        Hide();
    }

    private void createUpgradeButton(Upgrade.UpgradeType  upgradeType, Sprite upgradeSprite, string upgradeName, int upgradeCost, int positionIndex)
    {
        Transform shopUpgradeTransform = Instantiate(shopUpgradeTemplate, container);
        RectTransform shopUpgradeRectTransform = shopUpgradeTransform.GetComponent<RectTransform>();

        float shopUpgradeHeight = 80f;
        shopUpgradeRectTransform.anchoredPosition = new Vector2(0, (-shopUpgradeHeight * positionIndex)+200);
        
        shopUpgradeTransform.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(upgradeName);

        
        shopUpgradeTransform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(upgradeCost.ToString());

        shopUpgradeTransform.Find("itemImage").GetComponent<Image>().sprite = upgradeSprite;

        shopUpgradeTransform.GetComponent<Button_UI>().ClickFunc = () =>
        {
            TryBuyUpgrade(upgradeType);
        };
    }

    private void TryBuyUpgrade(Upgrade.UpgradeType upgradeType)
    {
        if (shopCustomer.TrySpendMoneyAmount(Upgrade.getCost(upgradeType)))
        {
            shopCustomer.BoughtUpgrade(upgradeType);
        }        
    }

    public void Show(IShopCustomer shopCustomer)
    {
        this.shopCustomer = shopCustomer;
        gameObject.SetActive(true);
        Game.Instance.InShopMenu = true;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Game.Instance.InShopMenu = false;
    }

}
