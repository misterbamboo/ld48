using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHudController : MonoBehaviour
{
    [SerializeField] private Text textGoldInventory;
    [SerializeField] private Text textPlatinumInventory;
    [SerializeField] private Text textCopperInventory;
    [SerializeField] private Text textDiamondInventory;
    [SerializeField] private Text textIronInventory;

    [SerializeField] private Text textSellInventory;

    [SerializeField] private Text textCash;

    void Start()
    {
        textSellInventory.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        refreshQuantities();
    }

    private void refreshQuantities()
    {
        textSellInventory.gameObject.SetActive(InventoryManager.Instance.canSell);

        textGoldInventory.text = InventoryManager.Instance.goldQuantity.ToString();
        textPlatinumInventory.text = InventoryManager.Instance.platinumQuantity.ToString();
        textCopperInventory.text = InventoryManager.Instance.copperQuantity.ToString();
        textIronInventory.text = InventoryManager.Instance.ironQuantity.ToString();
        textDiamondInventory.text = InventoryManager.Instance.diamondQuantity.ToString();
        textCash.text = "$" + InventoryManager.Instance.inventoryReward;
    }
}
