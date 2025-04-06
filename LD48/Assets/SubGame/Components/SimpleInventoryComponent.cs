using System.Collections;
using System.Collections.Generic;
using SubGame.Entities.InventoryManagement;
using UnityEngine;

namespace SubGame.Components
{
    public class SimpleInventoryComponent : MonoBehaviour, IInventory
    {
        [SerializeField] private int maxCapacity;
        private SimpleInventory inventory;

        // SimpleInventoryComponent should be initialized with Init(), this is just a fallback
        public void Awake()
        {
            inventory = new SimpleInventory(this.maxCapacity);
        }

        public void Init(int maxCapacity)
        {
            inventory = new SimpleInventory(maxCapacity);
        }

        public int Capacity => inventory.Capacity;

        public void AddItems(ItemStack items)
        {
            inventory.AddItems(items);
        }

        public CanAddResult CanAddItems(ItemStack items)
        {
            return inventory.CanAddItems(items);
        }

        public ItemStack GetItem(string itemId)
        {
            return inventory.GetItem(itemId);
        }

        public bool HasItem(string itemId)
        {
            return inventory.HasItem(itemId);
        }

        public IEnumerable<ItemStack> Items()
        {
            return inventory.Items();
        }

        public void RemoveItems(ItemStack items)
        {
            inventory.RemoveItems(items);
        }
    }
}
