using Battleships.Entities;
using Battleships.Enums;

namespace Battleships.Services.Tests
{
    [TestClass()]
    public class GameServiceTests
    {
        private const int gridRowCount = 10;
        private const int gridColCount = 10;
        private readonly IEnumerable<char> validLetters = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'];
        private readonly IEnumerable<int> validNumbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

        [TestMethod()]
        [DataRow(null)]
        [DataRow(" ")]
        [DataRow("K1")]
        [DataRow("A11")]
        [DataRow("z99")]
        [DataRow("A101")]
        public void CheckInputValidAndGetColumnAndRowTest_InvalidInputForGrid(string input)
        {
            // Arrange

            // Act
            UserSelectedItem userSelectedItem = GameService.CheckInputValidAndGetColumnAndRow(input, gridRowCount, gridColCount);

            // Assert
            Assert.AreEqual(false, userSelectedItem.InputValid);
            Assert.AreEqual(0, userSelectedItem.Row);
            Assert.AreEqual(0, userSelectedItem.Column);
        }

        [TestMethod()]
        [DataRow("A1")]
        public void CheckInputValidAndGetColumnAndRowTest_ValidInputForGrid(string input)
        {
            // Arrange

            // Act
            UserSelectedItem userSelectedItem = GameService.CheckInputValidAndGetColumnAndRow(input, gridRowCount, gridColCount);

            // Assert
            Assert.AreEqual(true, userSelectedItem.InputValid);
            Assert.AreEqual(0, userSelectedItem.Row);
            Assert.AreEqual(0, userSelectedItem.Column);
        }

        [TestMethod()]
        public void CheckInputValidAndGetColumnAndRowTest_ValidInputForGrid_b2()
        {
            // Arrange
            string input = "b2";

            // Act
            UserSelectedItem userSelectedItem = GameService.CheckInputValidAndGetColumnAndRow(input, gridRowCount, gridColCount);

            // Assert
            Assert.AreEqual(true, userSelectedItem.InputValid);
            Assert.AreEqual(1, userSelectedItem.Row);
            Assert.AreEqual(1, userSelectedItem.Column);
        }

        [TestMethod()]
        public void CheckInputValidAndGetColumnAndRowTest_ValidInputForGrid_c1()
        {
            // Arrange
            string input = "c1";

            // Act
            UserSelectedItem userSelectedItem = GameService.CheckInputValidAndGetColumnAndRow(input, gridRowCount, gridColCount);

            // Assert
            Assert.AreEqual(true, userSelectedItem.InputValid);
            Assert.AreEqual(2, userSelectedItem.Row);
            Assert.AreEqual(0, userSelectedItem.Column);
        }

        [TestMethod()]
        public void ProcessInput_ValidInput_HitShip()
        {
            // Arrange
            char[,] grid = GridService.InitializeGrid(gridRowCount, gridColCount);
            List<Ship> ships = [];
            SetTestGridWithBattleShipAtFixedLocation(grid, ships);
            string userInput = "C1";

            // Act
            var eventResult = GameService.ProcessInput(grid, ships, gridRowCount, gridColCount, userInput);

            // Assert
            Assert.AreEqual(EventResult.Hit, eventResult);
        }

        [TestMethod()]
        public void ProcessInput_ValidInput_MissShip()
        {
            // Arrange
            char[,] grid = GridService.InitializeGrid(gridRowCount, gridColCount);
            List<Ship> ships = [];
            SetTestGridWithBattleShipAtFixedLocation(grid, ships);
            string userInput = "A1";

            // Act
            var eventResult = GameService.ProcessInput(grid, ships, gridRowCount, gridColCount, userInput);

            // Assert
            Assert.AreEqual(EventResult.Miss, eventResult);
        }

        [TestMethod()]
        public void ProcessInput_InvalidInput_MissShip()
        {
            // Arrange
            char[,] grid = GridService.InitializeGrid(gridRowCount, gridColCount);
            List<Ship> ships = [];
            SetTestGridWithBattleShipAtFixedLocation(grid, ships);
            string userInput = "Z1";

            // Act
            var eventResult = GameService.ProcessInput(grid, ships, gridRowCount, gridColCount, userInput);

            // Assert
            Assert.AreEqual(EventResult.Invalid, eventResult);
        }

        [TestMethod()]
        public void ProcessInput_ValidInput_RepeatInput()
        {
            // Arrange
            char[,] grid = GridService.InitializeGrid(gridRowCount, gridColCount);
            List<Ship> ships = [];
            SetTestGridWithBattleShipAtFixedLocation(grid, ships);
            string userInput = "A1";
            var eventResult = GameService.ProcessInput(grid, ships, gridRowCount, gridColCount, userInput);

            // Act
            eventResult = GameService.ProcessInput(grid, ships, gridRowCount, gridColCount, userInput);

            // Assert
            Assert.AreEqual(EventResult.RepeatInput, eventResult);
        }

        [TestMethod()]
        public void ProcessInput_ValidInput_SinkShip()
        {
            // Arrange
            char[,] grid = GridService.InitializeGrid(gridRowCount, gridColCount);
            List<Ship> ships = [];
            SetTestGridWithBattleShipAtFixedLocation(grid, ships);
            GameService.ProcessInput(grid, ships, gridRowCount, gridColCount, "C1");
            GameService.ProcessInput(grid, ships, gridRowCount, gridColCount, "C2");
            GameService.ProcessInput(grid, ships, gridRowCount, gridColCount, "C3");
            GameService.ProcessInput(grid, ships, gridRowCount, gridColCount, "C4");
            string userInput = "C5";

            // Act
            var eventResult = GameService.ProcessInput(grid, ships, gridRowCount, gridColCount, userInput);

            // Assert
            Assert.AreEqual(EventResult.HitAndSunk, eventResult);
        }

        [TestMethod()]
        public void ProcessInput_TestValidInputs()
        {
            // Arrange
            char[,] grid = GridService.InitializeGrid(gridRowCount, gridColCount);
            List<Ship> ships = [];
            var results = new List<EventResult>();

            // Act
            foreach (var letter in validLetters)
            {
                foreach (var number in validNumbers)
                {
                    var result = GameService.ProcessInput(grid, ships, gridRowCount, gridColCount, $"{letter}{number}");
                    results.Add(result);
                }

            }

            // Assert
            Assert.AreEqual(100, results.Count);
            Assert.AreEqual(true, results.All(x => x == EventResult.Miss));
        }

        [TestMethod()]
        public void ProcessInput_TestValidInputs_OneBattleShipFixedLocation_OneSunkShip()
        {
            // Arrange
            char[,] grid = GridService.InitializeGrid(gridRowCount, gridColCount);
            List<Ship> ships = [];
            SetTestGridWithBattleShipAtFixedLocation(grid, ships);
            var results = new List<EventResult>();

            // Act
            foreach (var letter in validLetters)
            {
                foreach (var number in validNumbers)
                {
                    var result = GameService.ProcessInput(grid, ships, gridRowCount, gridColCount, $"{letter}{number}");
                    results.Add(result);
                }
            }

            // Assert
            Assert.AreEqual(100, results.Count);
            Assert.AreEqual(4, results.Count(x => x == EventResult.Hit));
            Assert.AreEqual(1, results.Count(x => x == EventResult.HitAndSunk));
            Assert.AreEqual(95, results.Count(x => x == EventResult.Miss));
            Assert.AreEqual(false, results.Any(x => x == EventResult.Invalid));
            Assert.AreEqual(false, results.Any(x => x == EventResult.RepeatInput));
        }

        [TestMethod()]
        public void ProcessInput_TestValidInputs_OneBattleShipFixedLocation_OneSunkShip_StopWhenSunk()
        {
            // Arrange
            char[,] grid = GridService.InitializeGrid(gridRowCount, gridColCount);
            List<Ship> ships = [];
            SetTestGridWithBattleShipAtFixedLocation(grid, ships);
            var results = new List<EventResult>();
            
            // Act
            foreach (var letter in validLetters)
            {
                if (results.Any(x => x == EventResult.HitAndSunk))
                {
                    break;
                }

                foreach (var number in validNumbers)
                {
                    if(results.Any(x => x == EventResult.HitAndSunk))
                    {
                        break;
                    }

                    var result = GameService.ProcessInput(grid, ships, gridRowCount, gridColCount, $"{letter}{number}");
                    results.Add(result);
                }
            }

            // Assert
            Assert.AreEqual(4, results.Count(x => x == EventResult.Hit));
            Assert.AreEqual(1, results.Count(x => x == EventResult.HitAndSunk));
            Assert.AreEqual(20, results.Count(x => x == EventResult.Miss));
            Assert.AreEqual(false, results.Any(x => x == EventResult.Invalid));
            Assert.AreEqual(false, results.Any(x => x == EventResult.RepeatInput));
        }

        [TestMethod()]
        public void ProcessInput_TestValidInputs_ThreeShipsFixedLocation_ThreeSunkShips()
        {
            // Arrange
            char[,] grid = GridService.InitializeGrid(gridRowCount, gridColCount);
            List<Ship> ships = [];
            SetTestGridWitOneBattleShipTwoDestroyersAtFixedLocations(grid, ships);
            var results = new List<EventResult>();

            // Act
            foreach (var letter in validLetters)
            {
                foreach (var number in validNumbers)
                {
                    var result = GameService.ProcessInput(grid, ships, gridRowCount, gridColCount, $"{letter}{number}");
                    results.Add(result);
                }
            }

            // Assert
            Assert.AreEqual(100, results.Count);
            Assert.AreEqual(10, results.Count(x => x == EventResult.Hit));
            Assert.AreEqual(3, results.Count(x => x == EventResult.HitAndSunk));
            Assert.AreEqual(87, results.Count(x => x == EventResult.Miss));
            Assert.AreEqual(false, results.Any(x => x == EventResult.Invalid));
            Assert.AreEqual(false, results.Any(x => x == EventResult.RepeatInput));
        }

        [TestMethod()]
        public void ProcessInput_TestValidInputs_ThreeShipsFixedLocation_ThreeSunkShips_StopWhenSunk()
        {
            // Arrange
            char[,] grid = GridService.InitializeGrid(gridRowCount, gridColCount);
            List<Ship> ships = [];
            SetTestGridWitOneBattleShipTwoDestroyersAtFixedLocations(grid, ships);
            var results = new List<EventResult>();

            // Act
            foreach (var letter in validLetters)
            {
                if (results.Where(x => x == EventResult.HitAndSunk).Count() == 3)
                {
                    break;
                }

                foreach (var number in validNumbers)
                {
                    if (results.Where(x => x == EventResult.HitAndSunk).Count() == 3)
                    {
                        break;
                    }

                    var result = GameService.ProcessInput(grid, ships, gridRowCount, gridColCount, $"{letter}{number}");
                    results.Add(result);
                }
            }

            // Assert
            Assert.AreEqual(44, results.Count);
            Assert.AreEqual(10, results.Count(x => x == EventResult.Hit));
            Assert.AreEqual(3, results.Count(x => x == EventResult.HitAndSunk));
            Assert.AreEqual(31, results.Count(x => x == EventResult.Miss));
            Assert.AreEqual(false, results.Any(x => x == EventResult.Invalid));
            Assert.AreEqual(false, results.Any(x => x == EventResult.RepeatInput));
        }

        [TestMethod()]
        public void ProcessInput_TestValidInputs_ThreeShipsRandomLocation_ThreeSunkShips()
        {
            // Arrange
            char[,] grid = GridService.InitializeGrid(gridRowCount, gridColCount);
            List<Ship> ships = GridService.PlaceShips(grid, gridRowCount, gridColCount);
            var results = new List<EventResult>();

            // Act
            foreach (var letter in validLetters)
            {
                foreach (var number in validNumbers)
                {
                    var result = GameService.ProcessInput(grid, ships, gridRowCount, gridColCount, $"{letter}{number}");
                    results.Add(result);
                }
            }

            // Assert
            Assert.AreEqual(100, results.Count);
            Assert.AreEqual(10, results.Count(x => x == EventResult.Hit));
            Assert.AreEqual(3, results.Count(x => x == EventResult.HitAndSunk));
            Assert.AreEqual(87, results.Count(x => x == EventResult.Miss));
            Assert.AreEqual(false, results.Any(x => x == EventResult.Invalid));
            Assert.AreEqual(false, results.Any(x => x == EventResult.RepeatInput));
        }

        #region Helper Methods
        private static void SetTestGridWithBattleShipAtFixedLocation(
            char[,] grid,
            List<Ship> ships)
        {
            Ship battleship = new(ShipType.Battleship);
            ships.Add(battleship);

            GridService.PlaceShip(grid, 2, 0, (int)ShipType.Battleship, true, battleship);
        }

        private static void SetTestGridWitOneBattleShipTwoDestroyersAtFixedLocations(
            char[,] grid,
            List<Ship> ships)
        {
            Ship battleship = new(ShipType.Battleship);
            ships.Add(battleship);

            Ship destroyer1 = new(ShipType.Destroyer);
            ships.Add(destroyer1);

            Ship destroyer2 = new(ShipType.Destroyer);
            ships.Add(destroyer2);

            GridService.PlaceShip(grid, 2, 0, (int)ShipType.Battleship, true, battleship);
            GridService.PlaceShip(grid, 3, 0, (int)ShipType.Destroyer, true, destroyer1);
            GridService.PlaceShip(grid, 4, 0, (int)ShipType.Destroyer, true, destroyer2);
        }
        #endregion
    }
}