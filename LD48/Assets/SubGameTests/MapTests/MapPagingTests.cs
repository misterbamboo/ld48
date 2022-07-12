using Assets.Map.Application;
using NUnit.Framework;
using UnityEngine;

namespace Assets.SubGameTests.MapTests
{
    public class MapPagingTests
    {
        private MapPaging.MapPageInfo PageInfo { get; set; }
        public MapPaging MapPaging { get; set; }

        private void Setup(int size = 0)
        {
            MapPaging = new MapPaging(size);
            MapPaging.PageChanged += (mpi) => PageInfo = mpi;
        }

        [Test]
        public void WhenPlayerDidintMoved_DoesntChangePage()
        {
            // Arrange
            Setup(size: 10);

            // Act
            MapPaging.UpdatePlayerPosition(Vector2.zero);

            // Assert
            Assert.That(PageInfo, Is.Null);
        }

        [Test]
        public void WhenPlayerAlmostReachedSide_DoesntChangePage()
        {
            // Arrange
            Setup(size: 10);

            // Act
            MapPaging.UpdatePlayerPosition(new Vector2(9.99999f, 0));

            // Assert
            Assert.That(PageInfo, Is.Null);
        }

        [Test]
        public void WhenPlayerReachedSide_PageChange()
        {
            // Arrange
            Setup(size: 10);

            // Act
            MapPaging.UpdatePlayerPosition(new Vector2(10, 0));

            // Assert
            Assert.That(PageInfo, Is.Not.Null);
        }

        [Test]
        public void WhenPlayerDidntMoved_VisibleRange_IsOnePageInAllDirections()
        {
            // Arrange
            Setup(size: 10);

            // Act
            var pageInfo = MapPaging.GetCurrentPageInfo();

            // Assert
            var bottomLeft = pageInfo.FromPoint();
            var topRight = pageInfo.ToPoint();
            Assert.That(bottomLeft, Is.EqualTo(new Vector2(-10, -10)));
            Assert.That(topRight, Is.EqualTo(new Vector2(20, 20)));
        }

        [Test]
        public void WhenPlayerReachedRightSide_VisibleRange_IsShifOnePageRight()
        {
            // Arrange
            Setup(size: 10);

            // Act
            MapPaging.UpdatePlayerPosition(new Vector2(10, 0));

            // Assert
            var bottomLeft = PageInfo.FromPoint();
            var topRight = PageInfo.ToPoint();
            Assert.That(bottomLeft, Is.EqualTo(new Vector2(0, -10)));
            Assert.That(topRight, Is.EqualTo(new Vector2(30, 20)));
        }

        [Test]
        public void WhenPlayerReachedLeftSide_VisibleRange_IsShifOnePageLeft()
        {
            // Arrange
            Setup(size: 10);

            // Act
            MapPaging.UpdatePlayerPosition(new Vector2(-1f, 0));

            // Assert
            var bottomLeft = PageInfo.FromPoint();
            var topRight = PageInfo.ToPoint();
            Assert.That(bottomLeft, Is.EqualTo(new Vector2(-20, -10)));
            Assert.That(topRight, Is.EqualTo(new Vector2(10, 20)));
        }

        [Test]
        public void WhenPlayerReachedTop_VisibleRange_IsShifOnePageUp()
        {
            // Arrange
            Setup(size: 10);

            // Act
            MapPaging.UpdatePlayerPosition(new Vector2(0f, 10f));

            // Assert
            var bottomLeft = PageInfo.FromPoint();
            var topRight = PageInfo.ToPoint();
            Assert.That(bottomLeft, Is.EqualTo(new Vector2(-10, 0)));
            Assert.That(topRight, Is.EqualTo(new Vector2(20, 30)));
        }

        [Test]
        public void WhenPlayerReachedBottom_VisibleRange_IsShifOnePageDown()
        {
            // Arrange
            Setup(size: 10);

            // Act
            MapPaging.UpdatePlayerPosition(new Vector2(0f, -1f));

            // Assert
            var bottomLeft = PageInfo.FromPoint();
            var topRight = PageInfo.ToPoint();
            Assert.That(bottomLeft, Is.EqualTo(new Vector2(-10, -20)));
            Assert.That(topRight, Is.EqualTo(new Vector2(20, 10)));
        }
    }
}
