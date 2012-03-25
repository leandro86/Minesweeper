using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinesweeperClone.UI
{
    public class GameOptions
    {
        public string Name { get; set; }
        public int GridRows { get; set; }
        public int GridColumns { get; set; }
        public int GridMines { get; set; }

        public GameOptions(string name, int rows, int columns, int mines)
        {
            Name = name;
            GridRows = rows;
            GridColumns = columns;
            GridMines = mines;
        }
    }
}
