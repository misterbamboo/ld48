// using NUnit.Framework;
// using RooIdle.Entities.InventoryManagement;
// using System.Linq;

// namespace RooIdleTests.TabInventoryTests
// {
//     public class RemoveItemsTests
//     {
//         [Test]
//         public void IfAnItemIsNotPresentForRemovalNothingHappens() 
//         {
//             // Arrange
//             var inv = new DefaultInventory(3);

//             // Assert
//             inv.RemoveItems(new ItemStack("test_id", 8));

//             // if this doesn't throw i guess nothing happened ?
//         }

//         [Test]
//         public void IfTheAmountOfItemsRemovedIsGreaterOrEqualThanWhatIsAlreadyInTheInventoryTheStackIsCompletelyRemoved() 
//         {
//             // Arrange
//             var inv = new DefaultInventory(3);
//             inv.AddItems(new ItemStack("test_id", 5));
//             inv.AddItems(new ItemStack("test_id2", 5));

//             // Act
//             inv.RemoveItems(new ItemStack("test_id", 5));
//             inv.RemoveItems(new ItemStack("test_id2", 10));

//             // Assert
//             Assert.That(inv.HasItem("test_id"), Is.False);
//             Assert.That(inv.HasItem("test_id2"), Is.False);
//         }

//         [Test]
//         public void RemainingItemsAfterRemovalKeepTheirOrderInTheInventory()
//         {
//             // Arrange
//             var inv = new DefaultInventory(3);
//             inv.AddItems(new ItemStack("test_id1"));
//             inv.AddItems(new ItemStack("test_id2"));
//             inv.AddItems(new ItemStack("test_id3"));

//             // Act
//             inv.RemoveItems(new ItemStack("test_id2"));

//             // Assert
//             var firstItem = inv.Items().First().ItemId;
//             var thirdItem = inv.Items().Skip(2).First().ItemId;

//             Assert.That(firstItem, Is.EqualTo("test_id1"));
//             Assert.That(thirdItem, Is.EqualTo("test_id3"));
//         }

//         [Test]
//         public void IfMoreItemsAreInTheInventoryThanTheAmountRemovedTheDifferenceIsLeftInTheInventory()
//         {
//             // Arrange
//             var inv = new DefaultInventory(3);
//             inv.AddItems(new ItemStack("test_id", 5));

//             // Act
//             inv.RemoveItems(new ItemStack("test_id", 3));

//             // Assert
//             var stack = inv.Items().First(i => i.ItemId == "test_id");
//             Assert.That(stack.Amount, Is.EqualTo(2));
//         }
//     }
// }
