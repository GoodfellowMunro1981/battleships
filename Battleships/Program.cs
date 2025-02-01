using Battleships.Entities;
using Battleships.Helpers;
using Battleships.Services;

class BattleshipsGame
{
    private const int gridWidth = 10;
    private const int gridHeight = 10;

    static void Main()
    {
        try
        {
            char[,] grid = GridService.InitializeGrid(gridWidth, gridHeight);
            List<Ship> ships = GridService.PlaceShips(grid);
            GameService.PlayGame(grid, ships, gridWidth, gridHeight);
        }
        catch (Exception ex)
        {
            //TODO ex should be logged in log file

            Console.Write(UserMessages.Error);
        }
    }
}