namespace Battleships.Services.Tests
{
    [TestClass()]
    public class GameServiceTests
    {
        [TestMethod()]
        public void CheckInputValidAndGetColumnAndRowTest()
        {
            // Arrange
            string input = "A1";
            int gridRowCount = 10;
            int gridColCount = 10;

            // Act
            var result = GameService.CheckInputValidAndGetColumnAndRow(input, gridRowCount, gridColCount);

            // Assert
            Assert.AreEqual(result.Item1, true);
            Assert.AreEqual(result.Item2, 0);
            Assert.AreEqual(result.Item3, 0);
        }
    }
}