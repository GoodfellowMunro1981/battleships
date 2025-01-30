using System;
using System.Collections.Generic;

class Battleships
{
    static char[,] grid = new char[10, 10];
    static List<Ship> ships = new List<Ship>();

    static void Main()
    {
        InitializeGrid();
        PlaceShips();
        PlayGame();
    }

    static void InitializeGrid()
    {
        for (int i = 0; i < 10; i++)
            for (int j = 0; j < 10; j++)
                grid[i, j] = '~';
    }

    static void PlaceShips()
    {
        ships.Add(new Ship(5)); // Battleship
        ships.Add(new Ship(4)); // Destroyer 1
        ships.Add(new Ship(4)); // Destroyer 2

        Random rand = new Random();
        foreach (var ship in ships)
        {
            bool placed = false;
            while (!placed)
            {
                int row = rand.Next(10);
                int col = rand.Next(10);
                bool horizontal = rand.Next(2) == 0;

                if (CanPlaceShip(row, col, ship.Size, horizontal))
                {
                    PlaceShip(row, col, ship.Size, horizontal, ship);
                    placed = true;
                }
            }
        }
    }

    static bool CanPlaceShip(int row, int col, int size, bool horizontal)
    {
        if (horizontal && col + size > 10 || !horizontal && row + size > 10)
            return false;

        for (int i = 0; i < size; i++)
        {
            if (horizontal && grid[row, col + i] != '~' || !horizontal && grid[row + i, col] != '~')
                return false;
        }
        return true;
    }

    static void PlaceShip(int row, int col, int size, bool horizontal, Ship ship)
    {
        for (int i = 0; i < size; i++)
        {
            if (horizontal)
            {
                grid[row, col + i] = 'S';
                ship.Positions.Add((row, col + i));
            }
            else
            {
                grid[row + i, col] = 'S';
                ship.Positions.Add((row + i, col));
            }
        }
    }

    static void PlayGame()
    {
        while (ships.Exists(s => !s.IsSunk()))
        {
            Console.Write("Enter target (e.g., A5): ");
            string input = Console.ReadLine().ToUpper();
            if (input.Length < 2 || input.Length > 3 || input[0] < 'A' || input[0] > 'J' || !int.TryParse(input.Substring(1), out int column) || column < 1 || column > 10)
            {
                Console.WriteLine("Invalid input. Try again.");
                continue;
            }

            int row = input[0] - 'A';
            column -= 1;

            if (grid[row, column] == 'X' || grid[row, column] == 'O')
            {
                Console.WriteLine("You already fired at this position.");
                continue;
            }

            bool hit = false;
            foreach (var ship in ships)
            {
                if (ship.Hit(row, column))
                {
                    hit = true;
                    grid[row, column] = 'X';
                    Console.WriteLine("Hit!");
                    if (ship.IsSunk())
                        Console.WriteLine("You sunk a ship!");
                    break;
                }
            }

            if (!hit)
            {
                grid[row, column] = 'O';
                Console.WriteLine("Miss!");
            }
        }

        Console.WriteLine("You sank all the ships! Game over.");
    }
}

class Ship
{
    public int Size { get; }
    public List<(int, int)> Positions { get; }

    public Ship(int size)
    {
        Size = size;
        Positions = new List<(int, int)>();
    }

    public bool Hit(int row, int col)
    {
        for (int i = 0; i < Positions.Count; i++)
        {
            if (Positions[i] == (row, col))
            {
                Positions[i] = (-1, -1); // Mark hit
                return true;
            }
        }
        return false;
    }

    public bool IsSunk()
    {
        return Positions.TrueForAll(pos => pos == (-1, -1));
    }
}
