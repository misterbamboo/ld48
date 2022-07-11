using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHudController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textGoldInventory;
    [SerializeField] private TextMeshProUGUI textPlatinumInventory;
    [SerializeField] private TextMeshProUGUI textCopperInventory;
    [SerializeField] private TextMeshProUGUI textDiamondInventory;
    [SerializeField] private TextMeshProUGUI textIronInventory;

    [SerializeField] private TextMeshProUGUI textSellInventory;

    [SerializeField] private TextMeshProUGUI textCash;

    void Start()
    {
        textSellInventory.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ShowCanSellLabel();
        ShowInventory();
    }

    private void ShowInventory()
    {
        textGoldInventory.text = Inventory.Instance.goldQuantity.ToString();
        textPlatinumInventory.text = Inventory.Instance.platinumQuantity.ToString();
        textCopperInventory.text = Inventory.Instance.copperQuantity.ToString();
        textIronInventory.text = Inventory.Instance.ironQuantity.ToString();
        textDiamondInventory.text = Inventory.Instance.diamondQuantity.ToString();
        textCash.text = Inventory.Instance.inventoryReward.ToString();
    }

    private void ShowCanSellLabel()
    {
        bool showLabel = Inventory.Instance.canSell && Game.Instance.State == GameState.InAction;
        textSellInventory.gameObject.SetActive(showLabel);
    }
}
