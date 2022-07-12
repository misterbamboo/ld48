using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubGame.Entities.InventoryManagement
{
    public enum AddFit
    {
        Full,
        Partial,
        None
    }

    public struct CanAddResult
    {
        public AddFit Fit { get; }
        public ItemStack Addable { get; }
        public ItemStack Remainder { get; }

        public CanAddResult(AddFit fit, ItemStack addable, ItemStack remainder)
        {
            Fit = fit;
            Addable = addable;
            Remainder = remainder;
        }

        public bool CanAdd() => Fit == AddFit.Full || Fit == AddFit.Partial;

        public static CanAddResult Full(ItemStack addable)
        {
            return new CanAddResult(AddFit.Full, addable, ItemStack.Nothing());
        }

        public static CanAddResult Partial(ItemStack addable, ItemStack remainder)
        {
            return new CanAddResult(AddFit.Partial, addable, remainder);
        }

        public static CanAddResult None(ItemStack remainder)
        {
            return new CanAddResult(AddFit.None, ItemStack.Nothing(), remainder);
        }
    }
}
