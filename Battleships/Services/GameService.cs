using Battleships.Entities;
using Battleships.Enums;
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
            if (!IsGridValid(grid, gridRowCount, gridColCount))
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

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine(UserMessages.InvalidInput);
                    continue;
                }

                var result = ProcessInput(grid, ships, gridRowCount, gridColCount, userInput);

                switch (result)
                {
                    case EventResult.Invalid:
                        Console.WriteLine(UserMessages.InvalidInput);
                        break;
                    case EventResult.RepeatInput:
                        Console.WriteLine(UserMessages.AlreadyFiredAtPosition);
                        break;
                    case EventResult.Miss:
                        Console.WriteLine(UserMessages.Miss);
                        break;
                    case EventResult.Hit:
                        Console.WriteLine(UserMessages.Hit);
                        break;
                    case EventResult.HitAndSunk:
                        Console.WriteLine(UserMessages.ShipSunk);
                        break;

                }
            }

            Console.WriteLine(UserMessages.GameOver);
        }

        public static EventResult ProcessInput(
            char[,] grid,
            List<Ship> ships,
            int gridRowCount,
            int gridColCount,
            string userInput)
        {
            UserSelectedItem userSelectedItem = CheckInputValidAndGetColumnAndRow(userInput, gridRowCount, gridColCount);

            if (!userSelectedItem.InputValid)
            {
                return EventResult.Invalid;
            }

            if (IsInputRepeatValue(grid, userSelectedItem.Row, userSelectedItem.Column))
            {
                return EventResult.RepeatInput;
            }

            return CheckInputHitShipAndUpdateGrid(grid, ships, userSelectedItem.Row, userSelectedItem.Column);
        }

        public static UserSelectedItem CheckInputValidAndGetColumnAndRow(
            string input,
            int gridRowCount,
            int gridColCount)
        {
            UserSelectedItem invalidResult = new()
            {
                InputValid = false,
                Row = 0,
                Column = 0
            };

            if (string.IsNullOrEmpty(input))
            {
                return invalidResult;
            }

            input = input.ToUpper();

            if (input.Length < 2 || input.Length > 3)
            {
                return invalidResult;
            }

            char rowValue = input.First();
            string columnValue = input.Length == 2
                ? input.Substring(1)
                : input.Substring(input.Length - 2);

            int intValueA = 65; // Represents 'A' in ASCII
            int row = rowValue - intValueA;

            if (row < 0 || row >= gridRowCount)
            {
                return invalidResult;
            }

            if (!int.TryParse(columnValue, out int column))
            {
                return invalidResult;
            }

            // index is 0 based so move column value down one from human 1 based input
            column -= 1;

            if (column < 0 || column >= gridColCount)
            {
                return invalidResult;
            }

            return new UserSelectedItem()
            {
                InputValid = true,
                Row = row,
                Column = column
            };
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

        private static EventResult CheckInputHitShipAndUpdateGrid(
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

                    if (ship.IsSunk())
                    {
                        return EventResult.HitAndSunk;
                    }

                    return EventResult.Hit;
                }
            }

            grid[row, column] = GridChars.MISS_VALUE;
            return EventResult.Miss;
        }

        private static bool IsGridValid(
            char[,] grid,
            int gridRowCount,
            int gridColCount)
        {
            if (grid == null || gridRowCount <= 0 || gridColCount <= 0)
            {
                return false;
            }

            if (grid.GetLength(0) == gridRowCount || grid.GetLength(1) == gridColCount)
            {
                return true;
            }

            return false;
        }

        private static bool IsShipsValid(
            List<Ship> ships)
        {
            if (ships != null && ships.Count != 0)
            {
                return true;
            }

            return false;
        }
    }
}