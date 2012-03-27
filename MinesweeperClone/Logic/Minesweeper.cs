using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinesweeperClone.Logic
{
    public class Minesweeper
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public int Mines { get; private set; }
        public int SquaresLeft { get; private set; }
        
        private Square[,] _grid;
        private HashSet<int> _minesLocation;

        public Minesweeper(int rows, int columns, int mines)
        {
            Rows = rows;
            Columns = columns;
            Mines = mines;

            _grid = new Square[Rows,Columns];
            _minesLocation = new HashSet<int>();

            ResetGrid();
        }

        public int CountAdjacentMines(int row, int column)
        {
            int minesCount = 0;

            foreach (GridSquare adjacentSquare in GetAdjacentSquares(row, column))
            {
                int mineLocation = (adjacentSquare.Row * Columns) + adjacentSquare.Column;
                if (_minesLocation.Contains(mineLocation))
                {
                    minesCount++;
                }
            }

            return minesCount;
        }

        public GridSquare[] GetAdjacentSquares(int row, int column)
        {
            List<GridSquare> adjacentSquares = new List<GridSquare>();

            /* some loops to get the adjacent squares in a grid to a given row and column. The ugly
             * conditions are for boundaries checking */
            for (int i = row > 0 ? -1 : 0; i <= (row < Rows - 1 ? 1 : 0); i++)
            {
                for (int j = column > 0 ? -1 : 0; j <= (column < Columns - 1 ? 1 : 0); j++)
                {
                    if (i != 0 || j != 0)
                    {
                        adjacentSquares.Add(new GridSquare(row + i, column + j));
                    }
                }
            }

            return adjacentSquares.ToArray();
        }

        public Square RevealSquare(int row, int column)
        {
            if (_grid[row, column] == Square.Unopened)
            {
                int mineLocation = (row * Columns) + column;
                _grid[row, column] = _minesLocation.Contains(mineLocation)
                                         ? Square.Mine
                                         : (Square)CountAdjacentMines(row, column);

                SquaresLeft--;
            }
            
            return _grid[row, column];
        }

        public GridSquare[] GetAllMines()
        {
            List<GridSquare> mines = new List<GridSquare>();

            foreach (int mine in _minesLocation)
            {
                mines.Add(new GridSquare(mine / Columns, mine % Columns));
            }

            return mines.ToArray();
        }

        public Square this[int row, int column]
        {
            get { return _grid[row, column]; }
            set { _grid[row, column] = value; }
        }

        /* Given a row and a column, this method will rearrange the grid in order to clear all mines adjacent to
         * the square, and it's going to move those mines to other squares. In the current implementation, the mines
         * are moved starting at the [0, 0] square, then [0, 1], and so on. A better way will be to modify the method
         * "GetSafeUnopenedSquares" to return squares at random. */
        public void RearrangeForBlankSquare(int row, int column)
        {            
            List<GridSquare> adjacentSquares = new List<GridSquare>(GetAdjacentSquares(row, column))
                                                   {
                                                       new GridSquare(row, column)
                                                   };
            GridSquare[] mines = adjacentSquares.Where(s => _minesLocation.Contains((s.Row * Columns) + s.Column)).ToArray();
            GridSquare[] safeUnopenedSquares = GetSafeUnopenedSquares(mines.Count(), adjacentSquares);

            for (int i = 0; i < safeUnopenedSquares.Length; i++)
            {
                _grid[mines[i].Row, mines[i].Column] = Square.Unopened;

                _minesLocation.Remove((mines[i].Row * Columns) + mines[i].Column);
                _minesLocation.Add((safeUnopenedSquares[i].Row * Columns) + safeUnopenedSquares[i].Column);              
            }

            _grid[row, column] = Square.Blank;
        }

        private GridSquare[] GetSafeUnopenedSquares(int squaresToGet, IEnumerable<GridSquare> squaresException)
        {
            List<GridSquare> safeUnopenedSquares = new List<GridSquare>();

            int i = 0;
            while (i < Rows &&  safeUnopenedSquares.Count != squaresToGet)
            {
                int j = 0;
                while (j < Columns && safeUnopenedSquares.Count != squaresToGet)
                {
                    int square = (i * Columns) + j;

                    if (_grid[i, j] == Square.Unopened && 
                        !_minesLocation.Contains(square) && 
                        squaresException.FirstOrDefault(s => s.Row == i && s.Column == j) == null)
                    {
                        safeUnopenedSquares.Add(new GridSquare(i, j));
                    }
                    j++;
                }
                i++;
            }

            if (safeUnopenedSquares.Count != squaresToGet)
            {
                throw new Exception("Unable to get requested number of squares");
            }

            return safeUnopenedSquares.ToArray();
        }

        private void ResetGrid()
        {
            SquaresLeft = Rows * Columns;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    _grid[i, j] = Square.Unopened;
                }
            }

            FillWithMines();
        }

        private void FillWithMines()
        {
            _minesLocation.Clear();
            Random random = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < Mines; i++)
            {
                int minePosition;

                do
                {
                    minePosition = random.Next(0, Rows * Columns);
                } while (_minesLocation.Contains(minePosition));
            
                _minesLocation.Add(minePosition);
            }
        }
    }
}
