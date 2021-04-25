using Assets.Ressources;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static List<IRessource> inventory;

    private void Start()
    {
        inventory = new List<IRessource>();
    }

    private void OnTriggerEnter(Collider collision)
    {    
        var ressource = collision.GetComponent<IRessource>();
        if (ressource != null) 
        {            
            inventory.Add(ressource);
            Destroy(collision.gameObject);
        }
    }
}
