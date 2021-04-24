using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionManager : MonoBehaviour
{
    public static List<GameObject> inventory;

    [SerializeField] private GameObject itemtext1;
    [SerializeField] private GameObject itemtext2;

    private float numberOfGemType1;
    private float numberOfGemType2;
    private void Start()
    {
        inventory = new List<GameObject>();
        numberOfGemType1 = 0;
        numberOfGemType2 = 0;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Gem")) //Ici on vas changer pour IRessource
        {
            GameObject itemCollided = collision.gameObject;
            
            inventory.Add(itemCollided);
            
            if(itemCollided.name.Equals("item1"))//Ici on vas utiliser genre getName() de IRessource
            {
                numberOfGemType1++;
                itemtext1.GetComponent<Text>().text = "GemType1: " + numberOfGemType1;
            }
            else if(itemCollided.name.Equals("item2"))
            {
                numberOfGemType2++;
                itemtext2.GetComponent<Text>().text = "GemType2: " + numberOfGemType2;
            }
            
            Destroy(collision.gameObject);
        }
            
            
        }
        

        
    }

