using Battleships.Enums;
using Battleships.Helpers;

namespace Battleships.Entities
{
    public class Ship
    {
        public int Size { get; } = 0;

        public List<(int, int)> Positions { get; } = [];

        public ShipType ShipType { get; }

        public Ship(ShipType shipType)
        {
            if (!ShipSettings.ShipSizes.TryGetValue(shipType, out int size))
            {
                throw new Exception("Invalid ShipSize for ShipType.Destroyer");
            }

            Size = size;
            ShipType = shipType;
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
}