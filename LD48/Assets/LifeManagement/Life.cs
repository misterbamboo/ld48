using Assets.OxygenManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILife
{
    float Quantity { get; }

    float Capacity { get; }
}

public class Life : MonoBehaviour, ILife
{
    public static ILife Instance { get; private set; }

    public float Quantity { get; private set; }

    public float Capacity { get; private set; }

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
        if (Oxygen.Instance.Quantity <= 0)
        {
            Quantity -= Time.deltaTime * reductionPerSecWhenNoOxy;
        }

        Quantity = Mathf.Clamp(Quantity, 0, Capacity);
    }
}