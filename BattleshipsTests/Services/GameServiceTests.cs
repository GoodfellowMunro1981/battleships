using Battleships.Entities;
using Battleships.Enums;

namespace Battleships.Services.Tests
{
    [TestClass()]
    public class GameServiceTests
    {
        private const int gridRowCount = 10;
        private const int gridColCount = 10;

        [TestMethod()]
        public void CheckInputValidAndGetColumnAndRowTest_InvalidInputForGrid_NULL()
        {
            // Arrange

            // Act
            UserSelectedItem userSelectedItem = GameService.CheckInputValidAndGetColumnAndRow(null, gridRowCount, gridColCount);

            // Assert
            Assert.AreEqual(userSelectedItem.InputValid, false);
            Assert.AreEqual(userSelectedItem.Row, 0);
            Assert.AreEqual(userSelectedItem.Column, 0);
        }

        [TestMethod()]
        public void CheckInputValidAndGetColumnAndRowTest_InvalidInputForGrid_Whitespace()
        {
            // Arrange
            string input = " ";

            // Act
            UserSelectedItem userSelectedItem = GameService.CheckInputValidAndGetColumnAndRow(input, gridRowCount, gridColCount);

            // Assert
            Assert.AreEqual(userSelectedItem.InputValid, false);
            Assert.AreEqual(userSelectedItem.Row, 0);
            Assert.AreEqual(userSelectedItem.Column, 0);
        }

        [TestMethod()]
        public void CheckInputValidAndGetColumnAndRowTest_ValidInputForGrid_A1()
        {
            // Arrange
            string input = "A1";

            // Act
            UserSelectedItem userSelectedItem = GameService.CheckInputValidAndGetColumnAndRow(input, gridRowCount, gridColCount);

            // Assert
            Assert.AreEqual(userSelectedItem.InputValid, true);
            Assert.AreEqual(userSelectedItem.Row, 0);
            Assert.AreEqual(userSelectedItem.Column, 0);
        }

        [TestMethod()]
        public void CheckInputValidAndGetColumnAndRowTest_ValidInputForGrid_b2()
        {
            // Arrange
            string input = "b2";

            // Act
            UserSelectedItem userSelectedItem = GameService.CheckInputValidAndGetColumnAndRow(input, gridRowCount, gridColCount);

            // Assert
            Assert.AreEqual(userSelectedItem.InputValid, true);
            Assert.AreEqual(userSelectedItem.Row, 1);
            Assert.AreEqual(userSelectedItem.Column, 1);
        }

        [TestMethod()]
        public void CheckInputValidAndGetColumnAndRowTest_ValidInputForGrid_c1()
        {
            // Arrange
            string input = "c1";

            // Act
            UserSelectedItem userSelectedItem = GameService.CheckInputValidAndGetColumnAndRow(input, gridRowCount, gridColCount);

            // Assert
            Assert.AreEqual(userSelectedItem.InputValid, true);
            Assert.AreEqual(userSelectedItem.Row, 2);
            Assert.AreEqual(userSelectedItem.Column, 0);
        }

        [TestMethod()]
        public void CheckInputValidAndGetColumnAndRowTest_InvalidInputForGrid()
        {
            // Arrange
            string input = "Z1";

            // Act
            UserSelectedItem userSelectedItem = GameService.CheckInputValidAndGetColumnAndRow(input, gridRowCount, gridColCount);

            // Assert
            Assert.AreEqual(userSelectedItem.InputValid, false);
            Assert.AreEqual(userSelectedItem.Row, 0);
            Assert.AreEqual(userSelectedItem.Column, 0);
        }

        [TestMethod()]
        public void ProcessInput_ValidInput_HitShip()
        {
            // Arrange
            char[,] grid = GridService.InitializeGrid(gridRowCount, gridColCount);
            List<Ship> ships = [];
            SetTestGridWithBattleShipAtFixedPoint(grid, ships);
            string userInput = "C1";

            // Act
            var eventResult = GameService.ProcessInput(grid, ships, gridRowCount, gridColCount, userInput);

            // Assert
            Assert.AreEqual(eventResult, EventResult.Hit);
        }

        [TestMethod()]
        public void ProcessInput_ValidInput_MissShip()
        {
            // Arrange
            char[,] grid = GridService.InitializeGrid(gridRowCount, gridColCount);
            List<Ship> ships = [];
            SetTestGridWithBattleShipAtFixedPoint(grid, ships);
            string userInput = "A1";

            // Act
            var eventResult = GameService.ProcessInput(grid, ships, gridRowCount, gridColCount, userInput);

            // Assert
            Assert.AreEqual(eventResult, EventResult.Miss);
        }

        [TestMethod()]
        public void ProcessInput_InvalidInput_MissShip()
        {
            // Arrange
            char[,] grid = GridService.InitializeGrid(gridRowCount, gridColCount);
            List<Ship> ships = [];
            SetTestGridWithBattleShipAtFixedPoint(grid, ships);
            string userInput = "Z1";

            // Act
            var eventResult = GameService.ProcessInput(grid, ships, gridRowCount, gridColCount, userInput);

            // Assert
            Assert.AreEqual(eventResult, EventResult.Invalid);
        }

        [TestMethod()]
        public void ProcessInput_ValidInput_RepeatInput()
        {
            // Arrange
            char[,] grid = GridService.InitializeGrid(gridRowCount, gridColCount);
            List<Ship> ships = [];
            SetTestGridWithBattleShipAtFixedPoint(grid, ships);
            string userInput = "A1";
            var eventResult = GameService.ProcessInput(grid, ships, gridRowCount, gridColCount, userInput);

            // Act
            eventResult = GameService.ProcessInput(grid, ships, gridRowCount, gridColCount, userInput);

            // Assert
            Assert.AreEqual(eventResult, EventResult.RepeatInput);
        }

        [TestMethod()]
        public void ProcessInput_ValidInput_SinkShip()
        {
            // Arrange
            char[,] grid = GridService.InitializeGrid(gridRowCount, gridColCount);
            List<Ship> ships = [];
            SetTestGridWithBattleShipAtFixedPoint(grid, ships);
            GameService.ProcessInput(grid, ships, gridRowCount, gridColCount, "C1");
            GameService.ProcessInput(grid, ships, gridRowCount, gridColCount, "C2");
            GameService.ProcessInput(grid, ships, gridRowCount, gridColCount, "C3");
            GameService.ProcessInput(grid, ships, gridRowCount, gridColCount, "C4");
            string userInput = "C5";

            // Act
            var eventResult = GameService.ProcessInput(grid, ships, gridRowCount, gridColCount, userInput);

            // Assert
            Assert.AreEqual(eventResult, EventResult.Sunk);
        }


        #region Helper Methods
        private static void SetTestGridWithBattleShipAtFixedPoint(
            char[,] grid,
            List<Ship> ships)
        {
            Ship battleship = new(ShipType.Battleship);
            ships.Add(battleship);

            GridService.PlaceShip(grid, 2, 0, (int)ShipType.Battleship, true, battleship);
        }
        #endregion
    }
}