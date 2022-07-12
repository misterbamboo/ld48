// using NUnit.Framework;
// using RooIdle;
// using RooIdle.Entities.InventoryManagement;

// namespace RooIdleTests.TabInventoryTests
// {
//     public class TabInventoryTests
//     {
//         [Test]
//         public void TabInventorySlotsAreFilledWithVoidItemsUponCreation()
//         {
//             // Arrange
//             var inv = new DefaultInventory(3);

//             // Assert
//             Assert.That(inv.Items(), Has.Exactly(3).Matches<ItemStack>(i => i.ItemId == Terms.VoidItem));
//         }
//     }
// }

