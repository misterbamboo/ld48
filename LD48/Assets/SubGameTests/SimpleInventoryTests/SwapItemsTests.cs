// using System.Linq;
// using NUnit.Framework;
// using RooIdle;
// using RooIdle.Entities.InventoryManagement;
// using UnityEngine;

// namespace RooIdleTests.TabInventoryTests
// {
//     public class SwapItemsTest
//     {
//         [Test]
//         public void AnItemCannotBeSwappedWithATrailingVoidItem()
//         {
//             // Arrange
//             var inv = new DefaultInventory(5);
//             inv.AddItems(new ItemStack("test_id")); // only one item so 4 trailing voids

//             // Act
//             inv.SwapItems(0, 2);

//             Assert.That(inv.Items().ElementAt(0).ItemId, Is.EqualTo("test_id"));
//         }

//         [Test]
//         public void AnItemCanBeSwappedWithAVoidItemIfTheVoidIsOnlyAHoleInTheMiddleOfRealItems()
//         {
//             // Arrange
//             var inv = new DefaultInventory(3);
//             inv.AddItems(new ItemStack("test_id"));
//             inv.AddItems(new ItemStack("test_id2"));
//             inv.AddItems(new ItemStack("test_id3"));

//             // remove middle item to leave a hole in the middle
//             inv.RemoveItems(new ItemStack("test_id2"));

//             // Act
//             inv.SwapItems(0, 1);

//             Assert.That(inv.Items().ElementAt(0).ItemId, Is.EqualTo(Terms.VoidItem));
//             Assert.That(inv.Items().ElementAt(1).ItemId, Is.EqualTo("test_id"));
//         }
//     }
// }
