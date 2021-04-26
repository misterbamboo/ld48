using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatStore : MonoBehaviour
{
    [SerializeField] private GameObject textSellInventory;

    void Start()
    {
        textSellInventory.SetActive(false);
    }
    
    private void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            textSellInventory.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            textSellInventory.SetActive(false);
        }
    }
}
