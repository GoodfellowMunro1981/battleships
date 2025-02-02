using Battleships.Entities;
using Battleships.Enums;
using Battleships.Helpers;

namespace Battleships.Services
{
    public static class GridService
    {
        public static char[,] InitializeGrid(
            int gridRowCount,
            int gridColCount)
        {
            char[,] grid = new char[gridRowCount, gridColCount];

            for (int colIndex = 0; colIndex < gridRowCount; colIndex++)
            {
                for (int rowIndex = 0; rowIndex < gridColCount; rowIndex++)
                {
                    grid[colIndex, rowIndex] = GridChars.DEFAULT_GRID_VALUE;
                }
            }

            return grid;
        }

        public static List<Ship> AddShips(
            IEnumerable<ShipType> shipTypes)
        {
            List<Ship> ships = [];

            foreach (ShipType shipType in shipTypes)
            {
                ships.Add(new Ship(shipType));
            }

            return ships;
        }

        public static List<Ship> PlaceShips(
            char[,] grid,
            List<Ship> ships,
            int gridRowCount,
            int gridColCount)
        {
            Random rand = new();

            foreach (var ship in ships)
            {
                bool placed = false;

                while (!placed)
                {
                    int row = rand.Next(gridRowCount);
                    int col = rand.Next(gridColCount);
                    bool horizontal = rand.Next(2) == 0;

                    if (CanPlaceShip(grid, row, col, ship.Size, horizontal))
                    {
                        PlaceShip(grid, row, col, ship.Size, horizontal, ship);
                        placed = true;
                    }
                }
            }

            return ships;
        }

        public static void PlaceShip(
            char[,] grid, 
            int row, 
            int col, 
            int size, 
            bool horizontal,
            Ship ship)
        {
            for (int i = 0; i < size; i++)
            {
                if (horizontal)
                {
                    grid[row, col + i] = GridChars.SHIP_GRID_VALUE;
                    ship.Positions.Add((row, col + i));
                    continue;
                }

                grid[row + i, col] = GridChars.SHIP_GRID_VALUE;
                ship.Positions.Add((row + i, col));
            }
        }

        private static bool CanPlaceShip(
            char[,] grid,
            int row,
            int col,
            int size,
            bool horizontal)
        {
            if (horizontal && (col + size) > 10)
            {
                return false;
            }

            if (!horizontal && (row + size) > 10)
            {
                return false;
            }

            for (int i = 0; i < size; i++)
            {
                if (horizontal && grid[row, col + i] != GridChars.DEFAULT_GRID_VALUE)
                {
                    return false;
                }

                if (!horizontal && grid[row + i, col] != GridChars.DEFAULT_GRID_VALUE)
                {
                    return false;
                }
            }

            return true;
        }
    }
}