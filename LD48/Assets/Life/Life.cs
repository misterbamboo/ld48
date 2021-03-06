using Assets.OxygenManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public interface ILife
{
    bool LosingLife { get; }

    float Quantity { get; }

    float Capacity { get; }

    bool IsDead { get; }

    void Hit(float damaga);
}

public class Life : MonoBehaviour, ILife
{
    public static ILife Instance { get; private set; }

    public float Quantity { get; private set; }

    public float Capacity { get; private set; }

    public bool LosingLife { get; private set; }

    public bool IsDead => Quantity <= 0;

    [SerializeField] private float startingQuantity = 100;
    [SerializeField] private float startingCapacity = 100;
    [SerializeField] private float reductionPerSecWhenNoOxy = 20f; // 5 seconds

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Quantity = startingQuantity;
        Capacity = startingCapacity;
    }

    void Update()
    {
        var losingLife = false;
        if (Oxygen.Instance.Quantity <= 0 && !Game.Instance.Invincible)
        {
            Quantity -= Time.deltaTime * reductionPerSecWhenNoOxy;
            losingLife = true;
        }

        LosingLife = losingLife;
        Quantity = Mathf.Clamp(Quantity, 0, Capacity);

        UpdateLife();
    }

    private void UpdateLife()
    {
        if (Submarine.Instance.LifeUpgradeBought)
        {
            Quantity += 25;
            Submarine.Instance.LifeUpgradeBought = false;
        }

        if (Submarine.Instance.HullUpgradeBought)
        {
            if (Submarine.Instance.HullUpgradeBought && Quantity != Capacity)
            {
                Capacity += 25;
                Quantity += 25;
                Submarine.Instance.HullUpgradeBought = false;
                print("Hull upgraded hheeeheee");
            }
        }
    }

    public void Hit(float damage)
    {
        Quantity = Mathf.Clamp(Quantity - damage, 0, Capacity);
        LosingLife = true;
    }
}