using Assets.MapGeneration;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    public float inventoryReward;
    public bool canSell;

    public bool hasInventory => inventory.Any();

    void Start()
    {
        inventoryReward = 0;

        resetInventory();
    }

    public void Add(Ore ore, int amount)
    {
        if (inventory.ContainsKey(ore.Type.ToString()))
        {
            inventory[ore.Type.ToString()] += amount;
        }
        else
        {
            inventory.Add(ore.Type.ToString(), amount);
        }

        AudioManager.Instance.PlaySound(SoundName.material1);
        Map.Instance.RemoveRessource(ore);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("BoatStore"))
        {
            canSell = true;

            if (Input.GetKey(KeyCode.E) && hasInventory)
            {
                sellInventory();
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
        inventory.Sum(o => o.Value);
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
    }
}