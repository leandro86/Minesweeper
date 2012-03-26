namespace MinesweeperClone.Logic
{
    public class GridSquare
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public GridSquare(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
