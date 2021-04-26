using Assets.Ressources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour, IRessource
{
    public string Name => nameof(Diamond);

    [SerializeField] private int sellPrice = 10;
    public int SellPrice => sellPrice;

    [SerializeField] private int buyPrice = 15;
    public int BuyPrice => buyPrice;

    public int SpawnX { get; set; }
    public int SpawnY { get; set; }

    public bool IsConsume()
    {
        return !gameObject.activeSelf;
    }

    public void Consume(bool value)
    {
        gameObject.SetActive(!value);
    }
}
