using System;

namespace SubGame.Entities.InventoryManagement
{
    public class InventoryIsFullException : Exception
    {
        public string ItemId { get; }
        public string InventoryName { get; }

        public InventoryIsFullException(ItemStack stack, string inventoryName)
        : base($"{stack.Amount} {stack.ItemId} could not be added to the {inventoryName}")
        {
            ItemId = stack.ItemId;
            InventoryName = inventoryName;
        }
    }
}
