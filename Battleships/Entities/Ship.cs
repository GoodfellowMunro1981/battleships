namespace Battleships.Entities
{
    public class Ship
    {
        public int Size { get; }

        public List<(int, int)> Positions { get; }

        public Ship(int size)
        {
            Size = size;
            Positions = [];
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