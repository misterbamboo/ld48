using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Ressources;
using UnityEngine;

public enum OreType
{
    Copper,
    Iron,
    Gold,
    Platinum,
    Diamond
}


public class Ore : MonoBehaviour
{
    public OreType Type;
    public int SellPrice;
    public int BuyPrice;
    public int SpawnX;
    public int SpawnY;

    [Obsolete("Use Hook instead")]
    public void Consume(bool value)
    {
    }

    public bool IsConsume()
    {
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
