using NUnit.Framework;
using SubGame.Entities.InventoryManagement;

namespace SubGameTests.SimpleInventoryTests
{
    public class GetItemTests
    {
        [Test]
        public void IfTheItemIsNotPresentthrowAnError()
        {
            // Arrange
            var inv = new SimpleInventory(3);

            // Act
            void act() => inv.GetItem("test_id");

            Assert.Throws<NoSuchItemInInventoryException>(act);
        }

        [Test]
        public void IfTheItemIsPresentInTheInventoryTheWholeStackIsReturned()
        {
            // Arrange
            var inv = new SimpleInventory(3);
            inv.AddItems(new ItemStack("test_id", 3));

            // Act
            var item = inv.GetItem("test_id");

            // Assert
            Assert.That(item, Is.Not.Null);
            Assert.That(item, Is.EqualTo(new ItemStack("test_id", 3)));
        }
    }
}
