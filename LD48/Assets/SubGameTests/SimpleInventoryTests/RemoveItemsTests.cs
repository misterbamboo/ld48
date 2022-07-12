using NUnit.Framework;
using SubGame.Entities.InventoryManagement;
using System.Linq;

namespace SubGameTests.SimpleInventoryTests
{
    public class RemoveItemsTests
    {
        [Test]
        public void IfAnItemIsNotPresentForRemovalNothingHappens()
        {
            // Arrange
            var inv = new SimpleInventory(15);

            // Assert
            inv.RemoveItems(new ItemStack("test_id", 8));

            // if this doesn't throw i guess nothing happened ?
        }

        [Test]
        public void IfTheAmountOfItemsRemovedIsGreaterOrEqualThanWhatIsAlreadyInTheInventoryTheStackIsCompletelyRemoved()
        {
            // Arrange
            var inv = new SimpleInventory(15);
            inv.AddItems(new ItemStack("test_id", 5));
            inv.AddItems(new ItemStack("test_id2", 5));

            // Act
            inv.RemoveItems(new ItemStack("test_id", 5));
            inv.RemoveItems(new ItemStack("test_id2", 10));

            // Assert
            Assert.That(inv.HasItem("test_id"), Is.False);
            Assert.That(inv.HasItem("test_id2"), Is.False);
        }

        [Test]
        public void IfMoreItemsAreInTheInventoryThanTheAmountRemovedTheDifferenceIsLeftInTheInventory()
        {
            // Arrange
            var inv = new SimpleInventory(15);
            inv.AddItems(new ItemStack("test_id", 5));

            // Act
            inv.RemoveItems(new ItemStack("test_id", 3));

            // Assert
            var stack = inv.Items().First(i => i.ItemId == "test_id");
            Assert.That(stack.Amount, Is.EqualTo(2));
        }
    }
}
