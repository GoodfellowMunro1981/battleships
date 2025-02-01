using Battleships.Entities;
using Battleships.Helpers;

namespace Battleships.Services
{
    public static class GameService
    {
        public static void PlayGame(char[,] grid, List<Ship> ships)
        {
            while (ships.Exists(ship => !ship.IsSunk()))
            {
                Console.Write(UserMessages.InitialInstructions);

                string userInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput)) {
                    Console.WriteLine(UserMessages.InvalidInput);
                    continue;
                }

                // TODO improve this reading of input and parsing
                string input = userInput.ToUpper();

                if (input.Length < 2 || input.Length > 3 || input[0] < 'A' || input[0] > 'J' || !int.TryParse(input.Substring(1), out int column) || column < 1 || column > 10)
                {
                    Console.WriteLine(UserMessages.InvalidInput);
                    continue;
                }

                int row = input[0] - 'A';
                column -= 1;

                if (grid[row, column] == GridChars.SHIP_HIT_VALUE || grid[row, column] == GridChars.MISS_VALUE)
                {
                    Console.WriteLine(UserMessages.AlreadyFiredAtPosition);
                    continue;
                }

                bool hit = false;
                foreach (var ship in ships)
                {
                    if (ship.Hit(row, column))
                    {
                        hit = true;
                        grid[row, column] = GridChars.SHIP_HIT_VALUE;
                        Console.WriteLine(UserMessages.Hit);

                        if (ship.IsSunk())
                        {
                            Console.WriteLine(UserMessages.ShipSunk);
                        }
                        break;
                    }
                }

                if (!hit)
                {
                    grid[row, column] = GridChars.MISS_VALUE;
                    Console.WriteLine(UserMessages.Miss);
                }
            }

            Console.WriteLine(UserMessages.GameOver);
        }
    }
}