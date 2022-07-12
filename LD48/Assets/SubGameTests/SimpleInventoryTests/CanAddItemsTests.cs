using NUnit.Framework;
using SubGame.Entities.InventoryManagement;

namespace SubGameTests.SimpleInventoryTests
{
    public class CanAddItemsTests
    {
        [Test]
        public void AStackCanBeAddedCompletelyIfTheInventoryHasEnoughCapacityLeftForAllTheItems()
        {
            // Arrange
            var inv = new SimpleInventory(3);

            // Act
            var canAdd = inv.CanAddItems(new ItemStack("test_id", 2));

            // Assert
            Assert.That(canAdd.Fit, Is.EqualTo(AddFit.Full));
            Assert.That(canAdd.Addable.Amount, Is.EqualTo(2));
            Assert.That(canAdd.Remainder, Is.EqualTo(ItemStack.Nothing()));
        }

        [Test]
        public void AStackCanBeAddedPartiallyIfTheInventoryHasEnoughCapacityLeftForSomeOfTheItems()
        {
            // Arrange
            var inv = new SimpleInventory(3);

            // act
            var canAdd = inv.CanAddItems(new ItemStack("test_id", 4));

            // Assert
            Assert.That(canAdd.Fit, Is.EqualTo(AddFit.Partial));
            Assert.That(canAdd.Addable.Amount, Is.EqualTo(3));
            Assert.That(canAdd.Remainder.Amount, Is.EqualTo(1));
        }

        [Test]
        public void AStackCannotBeAddedIfTheInventoryIsAlreadyFull()
        {
            // Arrange
            var inv = new SimpleInventory(0);

            // act
            var canAdd = inv.CanAddItems(new ItemStack("other_id", 4));

            Assert.That(canAdd.Fit, Is.EqualTo(AddFit.None));
            Assert.That(canAdd.Addable, Is.EqualTo(ItemStack.Nothing()));
            Assert.That(canAdd.Remainder.Amount, Is.EqualTo(4));
        }

    }
}
