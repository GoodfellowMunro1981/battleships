using Battleships.Entities;
using Battleships.Enums;
using Battleships.Helpers;
using Battleships.Services;

class BattleshipsGame
{
    static void Main()
    {
        try
        {
            char[,] grid = GridService.InitializeGrid(GridSize.GridRowCount, GridSize.GridColCount);
            List<Ship> ships = GridService.AddShips([ShipType.Battleship, ShipType.Destroyer, ShipType.Destroyer]);
            GridService.PlaceShips(grid, ships, GridSize.GridRowCount, GridSize.GridColCount);
            GameService.PlayGame(grid, ships, GridSize.GridRowCount, GridSize.GridColCount);
        }
        catch (Exception ex)
        {
            //TODO ex should be logged in log file or database
            Console.Write(UserMessages.Error);
        }
    }
}