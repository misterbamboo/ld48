using Assets.Ressources;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static List<IRessource> inventory;
    [SerializeField] private GameObject textGoldInventory;
    [SerializeField] private GameObject textPlatinumInventory;
    [SerializeField] private GameObject textCopperInventory;
    [SerializeField] private GameObject textDiamondInventory;
    [SerializeField] private GameObject textIronInventory;
    
    [SerializeField] private GameObject textSellInventory;

    [SerializeField] private GameObject textCash; 

    private float goldQuantity;
    private float platinumQuantity;
    private float copperQuantity;
    private float diamondQuantity;
    private float ironQuantity;
    
    private float inventoryReward;

    private bool hasInventory = false;
    
    void Start()
    {
        inventory = new List<IRessource>();

        inventoryReward = 0;
        
        resetInventory();
        
        textSellInventory.SetActive(false);

    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {    
        
        var ressource = collider2D.GetComponent<IRessource>();
        if (ressource != null)
        {
            hasInventory = true;
            print("ressource name: " + ressource.Name);

            if (ressource.Name.Equals("Gold"))
            {
                goldQuantity++;
            }
            else if (ressource.Name.Equals("Platinum"))
            {
                platinumQuantity++;
            }
            else if (ressource.Name.Equals("Copper"))
            {
                copperQuantity++;
            }
            else if (ressource.Name.Equals("Diamond"))
            {
                diamondQuantity++;
            }
            else if (ressource.Name.Equals("Iron"))
            {
                ironQuantity++;
            }

            refreshQuantities();
            inventory.Add(ressource);
            Destroy(collider2D.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
       
        if (collision.CompareTag("BoatStore"))
        {
            textSellInventory.SetActive(true);
            if (Input.GetKey(KeyCode.E) && hasInventory)
            {
               sellInventory();
               hasInventory = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BoatStore"))
        {
            textSellInventory.SetActive(false);
        }
    }

    private void sellInventory()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            inventoryReward += inventory[i].SellPrice;
        }
        
        resetInventory();
        refreshQuantities();
        
    }

    private void resetInventory()
    {
        inventory.Clear();
        goldQuantity = 0;
        platinumQuantity = 0;
        copperQuantity = 0;
        diamondQuantity = 0;
        ironQuantity = 0;

    }

    private void refreshQuantities()
    {
        textGoldInventory.GetComponent<Text>().text = goldQuantity.ToString();
        textPlatinumInventory.GetComponent<Text>().text = platinumQuantity.ToString();
        textCopperInventory.GetComponent<Text>().text = copperQuantity.ToString();
        textIronInventory.GetComponent<Text>().text = ironQuantity.ToString();
        textDiamondInventory.GetComponent<Text>().text = diamondQuantity.ToString();
        
        textCash.GetComponent<Text>().text = "Cash: " + inventoryReward;
    }
}
