using Battleships.Entities;
using Battleships.Enums;
using Battleships.Services;

namespace BattleshipsTests.Services
{
    [TestClass()]
    public class GridServiceTests
    {
        private const int gridRowCount = 10;
        private const int gridColCount = 10;

        [TestMethod()]
        public void InitializeGrid_ValidGrid_Success()
        {
            // Arrange

            // Act
            var grid = GridService.InitializeGrid(gridRowCount, gridColCount);

            // Assert
            Assert.AreEqual(100, grid.Length);
            Assert.AreEqual(10, grid.GetLength(0));
            Assert.AreEqual(10, grid.GetLength(1));
        }

        [TestMethod()]
        public void AddShips_ValidShips_Success()
        {
            // Arrange
            List<ShipType> shipTypes = [ShipType.Battleship, ShipType.Destroyer, ShipType.Destroyer];

            // Act
            List<Ship> ships = GridService.AddShips(shipTypes);

            // Assert
            Assert.AreEqual(3, ships.Count);
            Assert.AreEqual(2, ships.Where(x => x.Size == 4).Count());
            Assert.AreEqual(1, ships.Where(x => x.Size == 5).Count());
        }
    }
}