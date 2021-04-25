using Assets.Ressources;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static List<IRessource> inventory;

    void Start()
    {
        inventory = new List<IRessource>();
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {    
        var ressource = collider2D.GetComponent<IRessource>();
        if (ressource != null) 
        {      
            ressource.Consume(true);
            inventory.Add(ressource);
        }
    }
}
