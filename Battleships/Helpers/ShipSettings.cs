using Battleships.Enums;

namespace Battleships.Helpers
{
    public static class ShipSettings
    {
        public static readonly Dictionary<ShipType, int> ShipSizes = new()
        {
            { ShipType.Battleship, 5 },
            { ShipType.Destroyer, 4 },
        };
    }
}