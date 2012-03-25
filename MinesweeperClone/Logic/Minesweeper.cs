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

        public Minesweeper(int columns, int rows, int mines)
        {
            Rows = rows;
            Columns = columns;
            Mines = mines;

            _grid = new Square[Rows,Columns];

            ResetGrid();
        }

        public int CountAdjacentMines(int row, int column)
        {
            int minesCount = 0;

            foreach (Tuple<int, int> adjacentSquare in GetAdjacentSquares(row, column))
            {
                if (_grid[adjacentSquare.Item1,adjacentSquare.Item2] == Square.Mine)
                {
                    minesCount++;
                }
            }

            return minesCount;
        }

        public Tuple<int, int>[] GetAdjacentSquares(int row, int column)
        {
            List<Tuple<int, int>> adjacentSquares = new List<Tuple<int, int>>();

            /* some loops to get the adjacent squares in a grid to a given row and column. The ugly
             * conditions are for boundaries checking */
            for (int i = row > 0 ? -1 : 0; i <= (row < Rows - 1 ? 1 : 0); i++)
            {
                for (int j = column > 0 ? -1 : 0; j <= (column < Columns - 1 ? 1 : 0); j++)
                {
                    if (i != 0 || j != 0)
                    {
                        adjacentSquares.Add(new Tuple<int, int>(row + i, column + j));
                    }
                }
            }

            return adjacentSquares.ToArray();
        }

        public Square RevealSquare(int row, int column)
        {
            Square square = _grid[row,column];

            if (square == Square.Unopened)
            {
                _grid[row, column] = (Square)CountAdjacentMines(row, column);
                SquaresLeft--;
            }

            return _grid[row, column];
        }

        public Tuple<int, int>[] GetAllMines()
        {
            List<Tuple<int, int>> mines = new List<Tuple<int, int>>();

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (_grid[i,j] == Square.Mine)
                    {
                        mines.Add(new Tuple<int, int>(i,j));
                    }
                }
            }

            return mines.ToArray();
        }

        public Square this[int row, int column]
        {
            get { return _grid[row, column]; }
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
            List<int> placedMinesPosition = new List<int>();
            Random random = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < Mines; i++)
            {
                int minePosition;

                do
                {
                    minePosition = random.Next(0, Rows * Columns);
                } while (placedMinesPosition.Contains(minePosition));

                _grid[minePosition / Columns, minePosition % Columns] = Square.Mine;                
                placedMinesPosition.Add(minePosition);
            }
        }
    }
}
