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
                (bool inputValid, int column) = CheckInputValidAndGetColumnChar(input);

                if (!inputValid)
                {
                    Console.WriteLine(UserMessages.InvalidInput);
                    continue;
                }

                // TODO improve this
                int row = input[0] - 'A';
                column -= 1;

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

        private static (bool, int) CheckInputValidAndGetColumnChar(string input)
        {
            // TODO improve this
            if (input.Length < 2 || input.Length > 3 || input[0] < 'A' || input[0] > 'J' || !int.TryParse(input.Substring(1), out int column) || column < 1 || column > 10)
            {
                return (false, 0);
            }

            return (true, column);
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