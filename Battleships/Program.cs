using Battleships.Entities;
using Battleships.Helpers;
using Battleships.Services;

class BattleshipsGame
{
    /// <summary>
    /// gridRowCount must have a minimum value of with a maximimum value of 26 for en-GB chars and square grid
    /// </summary>
    private const int gridRowCount = 10;

    /// <summary>
    /// gridColCount must have a minimum value of 1 with a maximimum value of 26 for en-GB chars and square grid
    /// </summary>
    private const int gridColCount = 10;

    static void Main()
    {
        try
        {
            char[,] grid = GridService.InitializeGrid(gridRowCount, gridColCount);
            List<Ship> ships = GridService.PlaceShips(grid, gridRowCount, gridColCount);
            GameService.PlayGame(grid, ships, gridRowCount, gridColCount);
        }
        catch (Exception ex)
        {
            //TODO ex should be logged in log file
            Console.Write(UserMessages.Error);
        }
    }
}