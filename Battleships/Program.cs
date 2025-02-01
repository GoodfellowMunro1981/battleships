using Battleships.Entities;
using Battleships.Services;

class BattleshipsGame
{
    private const int gridWidth = 10;
    private const int gridHeight = 10;

    static void Main()
    {
        char[,] grid = new char[gridWidth, gridHeight];
        List<Ship> ships = [];

        GridService.InitializeGrid(grid, gridWidth, gridHeight);
        GridService.PlaceShips(grid, ships);
        GameService.PlayGame(grid, ships);
    }
}