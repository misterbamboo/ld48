// using NUnit.Framework;
// using RooIdle.Entities.InventoryManagement;
// using System.Linq;

// namespace RooIdleTests.TabInventoryTests
// {
//     public class NonVoidItemsTests
//     {
//         [Test]
//         public void WhenOnlyVoidItemsAreInTheInventoryAnEmptyListIsReturned() 
//         {
//             // Arrange
//             var inv = new DefaultInventory(3);

//             // Act
//             var items = inv.NonVoidItems();

//             // Assert
//             Assert.That(items, Is.Empty);
//         }

//         [Test]
//         public void NonVoidItemsAreNotReturnedWhenMultipleItemsAreInTheInventory()
//         {
//             // Arrange
//             var inv = new DefaultInventory(3);
//             inv.AddItems(new ItemStack("test_id"));

//             // Act
//             var items = inv.NonVoidItems();

//             // Assert
//             Assert.That(items.Count(), Is.EqualTo(1));
//         }
//     }
// }
