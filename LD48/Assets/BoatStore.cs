using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatStore : MonoBehaviour
{
    [SerializeField] private GameObject textSellInventory;

    private float inventoryReward;
    
    // Start is called before the first frame update
    void Start()
    {
        textSellInventory.SetActive(false);
        inventoryReward = 0;
    }

    
    private void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            textSellInventory.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                // sellInventory();
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            textSellInventory.SetActive(false);
        }
    }

    /*
    private void sellInventory()
    {
        print("Sold inventory!");
        //Boucle sur l'inventaire
        for (int i = 0; i < InventoryManager.inventory.Count; i++)
        {
            
        }
    }
    */
}
