using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MinesweeperClone.UI
{
    public partial class OptionsForm : Form
    {
        private GameOptions _gameOptions;
        
        public OptionsForm(GameOptions gameOptions)
        {
            InitializeComponent();

            _gameOptions = gameOptions;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (beginnerInput.Checked)
            {
                _gameOptions.Name = "Beginner";
                _gameOptions.GridRows = 9;
                _gameOptions.GridColumns = 9;
                _gameOptions.GridMines = 10;
            }
            else if (intermediateInput.Checked)
            {
                _gameOptions.Name = "Intermediate";
                _gameOptions.GridRows = 16;
                _gameOptions.GridColumns = 16;
                _gameOptions.GridMines = 40;
            }
            else if (advancedInput.Checked)
            {
                _gameOptions.Name = "Advanced";
                _gameOptions.GridRows = 16;
                _gameOptions.GridColumns = 30;
                _gameOptions.GridMines = 99;
            }
        }

        public static GameOptions GetDefaultOptions()
        {
            return new GameOptions("Beginner", 9, 9, 10);
        }
    }
}
