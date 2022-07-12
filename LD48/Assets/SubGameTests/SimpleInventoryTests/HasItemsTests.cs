// using NUnit.Framework;
// using RooIdle.Entities.InventoryManagement;

// namespace RooIdleTests.TabInventoryTests
// {
//     public class HasItemsTests
//     {
//         [Test]
//         public void AnItemIsPresentWhenAStackWithTheSameItemIdExistsInTheInventory()
//         {
//             // Arrange
//             var inv = new DefaultInventory(3);
//             inv.AddItems(new ItemStack("test_id"));

//             // Act
//             var hasItem = inv.HasItem("test_id");

//             Assert.That(hasItem, Is.True);
//         }

//         [Test]
//         public void AnItemIsNotPresentIfNoStackWithTheSameItemIdExistsInTheInventory()
//         {
//             // Arrange
//             var inv = new DefaultInventory(3);

//             // Act
//             var hasItem = inv.HasItem("test_id");

//             // Assert
//             Assert.That(hasItem, Is.False);
//         }
//     }
// }
