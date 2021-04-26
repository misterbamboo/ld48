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

        textGoldInventory.text = InventoryManager.Instance.goldQuantity.ToString();
        textPlatinumInventory.text = InventoryManager.Instance.platinumQuantity.ToString();
        textCopperInventory.text = InventoryManager.Instance.copperQuantity.ToString();
        textIronInventory.text = InventoryManager.Instance.ironQuantity.ToString();
        textDiamondInventory.text = InventoryManager.Instance.diamondQuantity.ToString();
        textCash.text = InventoryManager.Instance.inventoryReward.ToString();
    }

    private void ShowCanSellLabel()
    {
        bool showLabel = InventoryManager.Instance.canSell && !Game.Instance.GameOver;
        textSellInventory.gameObject.SetActive(showLabel);
    }
}
