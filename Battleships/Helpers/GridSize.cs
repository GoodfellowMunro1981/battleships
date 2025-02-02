namespace Battleships.Helpers
{
    public static class GridSize
    {
        /// <summary>
        /// gridRowCount must have a minimum value of with a maximimum value of 26 for en-GB chars and square grid
        /// </summary>
        public const int GridRowCount = 10;

        /// <summary>
        /// gridColCount must have a minimum value of 1 with a maximimum value of 26 for en-GB chars and square grid
        /// </summary>
        public const int GridColCount = 10;
    }
}