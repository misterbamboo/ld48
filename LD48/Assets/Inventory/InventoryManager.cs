using Assets.MapGeneration;
using Assets.Ressources;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    static List<IRessource> inventory;

    public float goldQuantity;
    public float platinumQuantity;
    public float copperQuantity;
    public float diamondQuantity;
    public float ironQuantity;

    public float inventoryReward;
    public bool hasInventory;
    public bool canSell;

    void Start()
    {
        inventory = new List<IRessource>();

        inventoryReward = 0;

        resetInventory();
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckRessource(other.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        CheckRessource(collider2D.gameObject);
    }

    private void CheckRessource(GameObject gameObject)
    {
        var ressource = gameObject.GetComponent<IRessource>();
        if (ressource != null)
        {
            hasInventory = true;

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

            AudioManager.Instance.PlaySound(SoundName.material1);
            inventory.Add(ressource);

            // unactive ressource will be recycle
            Map.Instance.RemoveRessource(ressource);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("BoatStore"))
        {
            canSell = true;

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
            canSell = false;
        }
    }

    private void sellInventory()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            inventoryReward += inventory[i].SellPrice;
        }
        AudioManager.Instance.PlaySound(SoundName.sellRessources);
        resetInventory();
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
}