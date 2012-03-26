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
        private List<int> _minesLocation;

        public Minesweeper(int columns, int rows, int mines)
        {
            Rows = rows;
            Columns = columns;
            Mines = mines;

            _grid = new Square[Rows,Columns];
            _minesLocation = new List<int>();

            ResetGrid();
        }

        public int CountAdjacentMines(int row, int column)
        {
            int minesCount = 0;

            foreach (GridSquare adjacentSquare in GetAdjacentSquares(row, column))
            {
                int mineLocation = (adjacentSquare.Row * Rows) + adjacentSquare.Column;
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
                int mineLocation = (row * Rows) + column;
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
