using System.Data.Common;
using Battleships.Entities;
using Battleships.Helpers;

namespace Battleships.Services
{
    public static class GameService
    {
        public static void PlayGame(
            char[,] grid, 
            List<Ship> ships, 
            int gridRowCount, 
            int gridColCount)
        {
            if(!IsGridValid(grid, gridRowCount, gridColCount))
            {
                Console.Write(UserMessages.Error);
                return;
            }

            if (!IsShipsValid(ships))
            {
                Console.Write(UserMessages.Error);
                return;
            }

            while (ships.Exists(ship => !ship.IsSunk()))
            {
                Console.Write(UserMessages.InitialInstructions);

                string? userInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput)) {
                    Console.WriteLine(UserMessages.InvalidInput);
                    continue;
                }

                string input = userInput.ToUpper();
                (bool inputValid, int column, int row) = CheckInputValidAndGetColumnAndRow(input, gridRowCount, gridColCount);

                if (!inputValid)
                {
                    Console.WriteLine(UserMessages.InvalidInput);
                    continue;
                }

                if (IsInputRepeatValue(grid,row, column))
                {
                    Console.WriteLine(UserMessages.AlreadyFiredAtPosition);
                    continue;
                }

                bool hit = CheckInputHitShipAndUpdateGrid(grid, ships, row, column);
               
                if (!hit)
                {
                    grid[row, column] = GridChars.MISS_VALUE;
                    Console.WriteLine(UserMessages.Miss);
                }
            }

            Console.WriteLine(UserMessages.GameOver);
        }

        private static (bool, int, int) CheckInputValidAndGetColumnAndRow(
            string input,
            int gridRowCount,
            int gridColCount)
        {
            if (input.Length < 2 || input.Length > 3)
            {
                return (false, 0, 0);
            }

            char rowValue = input[0];
            char columnValue = input[1];

            int intValueA = 65; // Represents 'A' in ASCII
            int row = rowValue - intValueA;

            if (row < 0 || row > gridRowCount)
            {
                return (false, 0, 0);
            }

            if (!int.TryParse(columnValue.ToString(), out int column))
            {
                return (false, 0, 0);
            }

            // index is 0 based so move column value down one from human 1 based input
            column -= 1;

            if ( column < 0 || column > gridColCount)
            {
                return (false, 0, 0);
            }

            return (true, column, row);
        }

        private static bool IsInputRepeatValue(
            char[,] grid,
            int row,
            int column)
        {
            if (grid[row, column] == GridChars.SHIP_HIT_VALUE || grid[row, column] == GridChars.MISS_VALUE)
            {
                return true;
            }

            return false;
        }

        private static bool CheckInputHitShipAndUpdateGrid(
            char[,] grid, 
            List<Ship> ships,
            int row, 
            int column)
        {
            foreach (var ship in ships)
            {
                if (ship.Hit(row, column))
                {
                    grid[row, column] = GridChars.SHIP_HIT_VALUE;
                    Console.WriteLine(UserMessages.Hit);

                    if (ship.IsSunk())
                    {
                        Console.WriteLine(UserMessages.ShipSunk);
                    }
                    return true;
                }
            }

            return false;
        }

        private static bool IsGridValid(
            char[,] grid, 
            int gridRowCount, 
            int gridColCount)
        {
            if(grid == null || gridRowCount <= 0 || gridColCount <= 0)
            {
                return false;
            }

            if (grid.GetLength(0) == gridRowCount || grid.GetLength(1) == gridColCount)
            {
                return true;
            }

            return false;
        }

        private static bool IsShipsValid(List<Ship> ships)
        {
            if (ships != null && ships.Count != 0)
            {
                return true;
            }

            return false;
        }
    }
}