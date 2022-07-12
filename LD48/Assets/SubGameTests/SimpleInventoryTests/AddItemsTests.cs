using NUnit.Framework;
using System;
using System.Linq;
using SubGame.Entities.InventoryManagement;

namespace SubGameTests.SimpleInventoryTests
{
    public class AddItemsTests
    {
        [Test]
        public void AnItemStackCanBeAddedToAnInventoryWhenItIsEmpty()
        {
            // Arrange
            var inv = new SimpleInventory(3);

            // Act
            inv.AddItems(new ItemStack("test_id"));

            // Assert
            Assert.That(inv.Items(), Has.Exactly(1).Matches<ItemStack>(i => i.ItemId == "test_id"));
        }

        [Test]
        public void AnItemStackIsAddedToAnExistingItemStackIfTheirItemIdsMatch()
        {
            // Arramge
            var inv = new SimpleInventory(5);

            // Act
            inv.AddItems(new ItemStack("test_id", 2));
            inv.AddItems(new ItemStack("test_id", 3));

            // Assert
            var stack = inv.Items().First(i => i.ItemId == "test_id");
            Assert.That(stack.Amount, Is.EqualTo(5));
        }

        [Test]
        public void AnExceptionIsThrownIfAddingItemsToTheInventoryWouldGoOverMaxCapacity()
        {
            // Arrange
            var inv = new SimpleInventory(3);

            // Act
            void act() => inv.AddItems(new ItemStack("test_id", 4));

            // Assert
            Assert.Throws<InventoryIsFullException>(act);
        }

        [Test]
        public void NothingItemIsNeverAdded()
        {
            // Arrange
            var inv = new SimpleInventory(3);

            // Act
            inv.AddItems(ItemStack.Nothing());

            // Assert
            Assert.That(inv.Items(), Is.Empty);
        }
    }
}
