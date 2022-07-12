using System.Collections.Generic;

namespace SubGame.Entities.InventoryManagement
{
    public interface IInventory
    {
        public int Capacity { get; }

        public IEnumerable<ItemStack> Items();

        /// <summary>
        /// Does not remove the item from the inventory. Only for querying
        /// </summary>
        public ItemStack GetItem(string itemId);

        public void AddItems(ItemStack items);

        public CanAddResult CanAddItems(ItemStack items);

        /// <summary>
        /// Removes a specific amount of items from the inventory. 
        /// If none are left, the item is replaced by void items.
        /// </summary>
        public void RemoveItems(string itemId, int amount);

        public bool HasItem(string itemId);
    }

}
