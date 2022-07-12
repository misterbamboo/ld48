using System.Linq;
using System.Collections.Generic;

namespace SubGame.Entities.InventoryManagement
{
    public class SimpleInventory : IInventory
    {
        public int Capacity { get; }
        private List<ItemStack> Inventory { get; }
        private string InventoryName { get; }

        public SimpleInventory(int capacity, string inventoryName = "simple inventory")
        {
            Capacity = capacity;
            Inventory = new List<ItemStack>();
            InventoryName = inventoryName;
        }

        public IEnumerable<ItemStack> Items() => Inventory;

        public ItemStack GetItem(string itemId)
        {
            if (!HasItem(itemId))
            {
                throw new NoSuchItemInInventoryException(itemId, InventoryName);
            }

            return Inventory[ItemIndex(itemId)];
        }

        public void AddItems(ItemStack items)
        {
            if (items == ItemStack.Nothing())
            {
                return;
            }

            if (Inventory.Sum(i => i.Amount) + items.Amount > Capacity)
            {
                throw new InventoryIsFullException(items, InventoryName);
            }

            var index = ItemIndex(items.ItemId);
            if (index != -1)
            {
                Inventory[index] = Inventory[index].AddAmount(items.Amount);
                return;
            }

            Inventory.Add(items);
        }

        private int ItemIndex(string itemId)
        {
            return Inventory.FindIndex(0, i => i.ItemId == itemId);
        }

        public CanAddResult CanAddItems(ItemStack items)
        {
            var total = Inventory.Sum(i => i.Amount);
            if (total + items.Amount <= Capacity)
            {
                return CanAddResult.Full(items);
            }
            else if (total < Capacity && total + items.Amount > Capacity)
            {
                var addable = new ItemStack(items.ItemId, Capacity - total);
                var remainder = new ItemStack(items.ItemId, items.Amount - (Capacity - total));
                return CanAddResult.Partial(addable, remainder);
            }
            else
            {
                return CanAddResult.None(items);
            }
        }

        public void RemoveItems(ItemStack items)
        {
            var index = ItemIndex(items.ItemId);
            if (index == -1)
            {
                return;
            }

            var amountInInventory = Inventory[index].Amount;
            if (items.Amount >= amountInInventory)
            {
                Inventory.RemoveAt(index);
            }
            else if (items.Amount < amountInInventory)
            {
                Inventory[index] = Inventory[index].RemoveAmount(items.Amount);
            }
        }

        public bool HasItem(string itemId)
        {
            return ItemIndex(itemId) != -1;
        }
    }
}

