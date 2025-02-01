using Battleships.Entities;
using Battleships.Helpers;

namespace Battleships.Services
{
    public static class GridService
    {
        public static void InitializeGrid(
            char[,] grid,
            int gridWith,
            int gridHeight)
        {
            for (int colIndex = 0; colIndex < gridWith; colIndex++)
            {
                for (int rowIndex = 0; rowIndex < gridHeight; rowIndex++)
                {
                    grid[colIndex, rowIndex] = GridChars.DEFAULT_GRID_VALUE;
                }
            }
        }

        public static void PlaceShips(char[,] grid, List<Ship> ships)
        {
            AddBattleship(ships); // Battleship
            AddDestroyer(ships); // Destroyer 1
            AddDestroyer(ships); // Destroyer 2

            Random rand = new();

            foreach (var ship in ships)
            {
                bool placed = false;

                while (!placed)
                {
                    int row = rand.Next(10);
                    int col = rand.Next(10);
                    bool horizontal = rand.Next(2) == 0;

                    if (CanPlaceShip(grid, row, col, ship.Size, horizontal))
                    {
                        PlaceShip(grid, row, col, ship.Size, horizontal, ship);
                        placed = true;
                    }
                }
            }
        }

        private static void AddBattleship(List<Ship> ships)
        {
            ships.Add(new Ship(5));
        }

        private static void AddDestroyer(List<Ship> ships)
        {
            ships.Add(new Ship(4));
        }

        private static bool CanPlaceShip(char[,] grid, int row, int col, int size, bool horizontal)
        {
            // TODO improve this check
            if ((horizontal && (col + size > 10)) || (!horizontal && (row + size > 10)))
            {
                return false;
            }

            for (int i = 0; i < size; i++)
            {
                // TODO improve this check
                if ((horizontal && grid[row, col + i] != GridChars.DEFAULT_GRID_VALUE) || (!horizontal && grid[row + i, col] != GridChars.DEFAULT_GRID_VALUE))
                {
                    return false;
                }
            }

            return true;
        }

        private static void PlaceShip(char[,] grid, int row, int col, int size, bool horizontal, Ship ship)
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
    }
}