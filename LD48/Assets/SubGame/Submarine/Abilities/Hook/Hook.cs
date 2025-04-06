using System.Collections.Generic;
using SubGame.Components;
using SubGame.Entities.InventoryManagement;
using UnityEngine;

[RequireComponent(typeof(SimpleInventoryComponent))]
public class Hook : MonoBehaviour
{
    [SerializeField] private SimpleInventoryComponent inventory;
    private float hookdistanceLastFrame = 0;
    private float pullSpeed = 3f;
    private List<IHookable> hookedObjects = new List<IHookable>();

    void Start()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var hookBehaviour = other.GetComponent<IHookable>();
        if (hookBehaviour != null)
        {
            var canAddResult = inventory.CanAddItems(new ItemStack(hookBehaviour.Key));
            if (canAddResult.CanAdd)
            {
                inventory.AddItems(canAddResult.Addable);
                hookedObjects.Add(hookBehaviour);
                hookBehaviour.Hook(this);
            }
        }
    }
}
