namespace SubGame.Entities.InventoryManagement
{
    public struct ItemStack
    {
        public ItemStack(string itemId, int amount = 1)
        {
            ItemId = itemId;
            Amount = amount;
        }

        public string ItemId { get; }
        public int Amount { get; }

        public static ItemStack Nothing()
        {
            return new ItemStack("", 0);
        }

        public ItemStack AddAmount(int amount)
        {
            return new ItemStack(ItemId, Amount + amount);
        }

        public ItemStack RemoveAmount(int amount)
        {
            return new ItemStack(ItemId, Amount - amount);
        }

        public static bool operator ==(ItemStack a, ItemStack b)
        {
            return a.ItemId == b.ItemId && a.Amount == b.Amount;
        }

        public static bool operator !=(ItemStack a, ItemStack b)
        {
            return !(a == b);
        }
    }
}

