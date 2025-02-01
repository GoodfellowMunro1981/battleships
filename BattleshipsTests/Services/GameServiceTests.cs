namespace Battleships.Services.Tests
{
    [TestClass()]
    public class GameServiceTests
    {

        [TestMethod()]
        public void CheckInputValidAndGetColumnAndRowTest_InvalidInputForGrid_NULL()
        {
            // Arrange
            string input = null;
            int gridRowCount = 10;
            int gridColCount = 10;

            // Act
            (bool inputValid, int row, int column) = GameService.CheckInputValidAndGetColumnAndRow(input, gridRowCount, gridColCount);

            // Assert
            Assert.AreEqual(inputValid, false);
            Assert.AreEqual(column, 0);
            Assert.AreEqual(row, 0);
        }

        [TestMethod()]
        public void CheckInputValidAndGetColumnAndRowTest_InvalidInputForGrid_Whitespace()
        {
            // Arrange
            string input = " ";
            int gridRowCount = 10;
            int gridColCount = 10;

            // Act
            (bool inputValid, int row, int column) = GameService.CheckInputValidAndGetColumnAndRow(input, gridRowCount, gridColCount);

            // Assert
            Assert.AreEqual(inputValid, false);
            Assert.AreEqual(column, 0);
            Assert.AreEqual(row, 0);
        }

        [TestMethod()]
        public void CheckInputValidAndGetColumnAndRowTest_ValidInputForGrid_A1()
        {
            // Arrange
            string input = "A1";
            int gridRowCount = 10;
            int gridColCount = 10;

            // Act
            (bool inputValid, int row, int column) = GameService.CheckInputValidAndGetColumnAndRow(input, gridRowCount, gridColCount);

            // Assert
            Assert.AreEqual(inputValid, true);
            Assert.AreEqual(column, 0);
            Assert.AreEqual(row, 0);
        }

        [TestMethod()]
        public void CheckInputValidAndGetColumnAndRowTest_ValidInputForGrid_b2()
        {
            // Arrange
            string input = "b2";
            int gridRowCount = 10;
            int gridColCount = 10;

            // Act
            (bool inputValid, int row, int column) = GameService.CheckInputValidAndGetColumnAndRow(input, gridRowCount, gridColCount);

            // Assert
            Assert.AreEqual(inputValid, true);
            Assert.AreEqual(row, 1);
            Assert.AreEqual(column, 1);
        }

        [TestMethod()]
        public void CheckInputValidAndGetColumnAndRowTest_ValidInputForGrid_c1()
        {
            // Arrange
            string input = "c1";
            int gridRowCount = 10;
            int gridColCount = 10;

            // Act
            (bool inputValid, int row, int column) = GameService.CheckInputValidAndGetColumnAndRow(input, gridRowCount, gridColCount);

            // Assert
            Assert.AreEqual(inputValid, true);
            Assert.AreEqual(row, 2);
            Assert.AreEqual(column, 0);    
        }

        [TestMethod()]
        public void CheckInputValidAndGetColumnAndRowTest_InvalidInputForGrid()
        {
            // Arrange
            string input = "Z1";
            int gridRowCount = 10;
            int gridColCount = 10;

            // Act
            (bool inputValid, int row, int column) = GameService.CheckInputValidAndGetColumnAndRow(input, gridRowCount, gridColCount);

            // Assert
            Assert.AreEqual(inputValid, false);
            Assert.AreEqual(row, 0);
            Assert.AreEqual(column, 0);
        }
    }
}