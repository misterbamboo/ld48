using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubGame.Entities.InventoryManagement
{
    public class NoSuchItemInInventoryException : Exception
    {
        public string ItemId { get; }
        public string InventoryName { get; }

        public NoSuchItemInInventoryException(string itemId, string inventoryName)
            : base($"There is no {itemId} in {inventoryName}")
        {
            ItemId = itemId;
            InventoryName = inventoryName;
        }
    }
}
