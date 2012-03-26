using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MinesweeperClone.Logic;

namespace MinesweeperClone.UI
{
    public partial class MainForm : Form
    {
        private Minesweeper _minesweeper;
        private PictureBox[,] _squares;
        private Dictionary<PictureBox, Tuple<int, int>> _squaresDictionary;

        private const int SquareSize = 30;
        private GameOptions _currentGameOptions;

        private readonly int _formMargin;

        public MainForm()
        {
            InitializeComponent();

            _formMargin = gridPanel.Location.X;

            _currentGameOptions = OptionsForm.GetDefaultOptions();
            NewGame();
        }

        private void NewGame()
        {
            timer.Stop();

            minesLeftLabel.Text = _currentGameOptions.GridMines.ToString();
            elapsedTimeLabel.Text = "0";
            
            AdjustControlsPosition();

            _minesweeper = new Minesweeper(_currentGameOptions.GridColumns, _currentGameOptions.GridRows,
                                           _currentGameOptions.GridMines);

            _squares = new PictureBox[_currentGameOptions.GridRows, _currentGameOptions.GridColumns];
            _squaresDictionary = new Dictionary<PictureBox, Tuple<int, int>>();

            gridPanel.Visible = false;
            gridPanel.Controls.Clear();
            gridPanel.Visible = true;

            for (int i = 0; i < _currentGameOptions.GridRows; i++)
            {
                for (int j = 0; j < _currentGameOptions.GridColumns; j++)
                {
                    PictureBox square = new PictureBox()
                                            {
                                                Name = Square.Unopened.ToString(),
                                                Width = SquareSize,
                                                Height = SquareSize,
                                                Location = new Point(j * SquareSize, i * SquareSize),
                                                Image = Properties.Resources.Unopened,
                                                SizeMode = PictureBoxSizeMode.StretchImage,
                                            };
                    square.MouseDown += OnButtonMouseDown;

                    gridPanel.Controls.Add(square);

                    _squares[i, j] = square;
                    _squaresDictionary.Add(square, new Tuple<int, int>(i, j));
                }
            }

            gridPanel.Enabled = true;
            timer.Start();
        }

        private void AdjustControlsPosition()
        {
            gridPanel.Width = _currentGameOptions.GridColumns * SquareSize;
            gridPanel.Height = _currentGameOptions.GridRows * SquareSize;

            minesLeftLabel.Location = new Point(gridPanel.Location.X + gridPanel.Width - 35, minesLeftLabel.Location.Y);
            minePicture.Location = new Point(gridPanel.Location.X + gridPanel.Width - 68, minePicture.Location.Y);

            ClientSize = new Size(gridPanel.Location.X + gridPanel.Width + _formMargin,
                                  gridPanel.Location.Y + gridPanel.Height + menu.Height + _formMargin);

            CenterToScreen();
        }

        private void OnButtonMouseDown(object sender, MouseEventArgs e)
        {
            PictureBox clickedSquare = (PictureBox)sender;

            if (clickedSquare.Name == Square.Unopened.ToString() || clickedSquare.Name == "Flag")
            {
                if (e.Button == MouseButtons.Left)
                {
                    RevealSquare(clickedSquare);

                    if (_minesweeper.SquaresLeft == _currentGameOptions.GridMines)
                    {
                        GameWon();
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    if (clickedSquare.Name == "Flag")
                    {
                        clickedSquare.Name = Square.Unopened.ToString();
                        clickedSquare.Image = Properties.Resources.Unopened;
                        minesLeftLabel.Text = (int.Parse(minesLeftLabel.Text) + 1).ToString();
                    }
                    else
                    {
                        clickedSquare.Name = "Flag";
                        clickedSquare.Image = Properties.Resources.Flag;
                        minesLeftLabel.Text = (int.Parse(minesLeftLabel.Text) - 1).ToString();
                    }
                }
            }
        }

        private void RevealSquare(PictureBox squareToReveal)
        {
            if (squareToReveal.Name != Square.Unopened.ToString())
            {
                return;
            }
            
            int row = _squaresDictionary[squareToReveal].Item1;
            int column = _squaresDictionary[squareToReveal].Item2;

            Square revealedSquare = _minesweeper.RevealSquare(row, column);

            Bitmap image = (Bitmap)Properties.Resources.ResourceManager.GetObject(revealedSquare.ToString());
            squareToReveal.Image = image;
            squareToReveal.Name = revealedSquare.ToString();

            if (revealedSquare == Square.Mine)
            {
                GameLost();
            }
            else if (revealedSquare == Square.Blank)
            {
                foreach (Tuple<int, int> adjacentSquare in _minesweeper.GetAdjacentSquares(row, column))
                {
                    RevealSquare(_squares[adjacentSquare.Item1, adjacentSquare.Item2]);
                }
            }
        }

        private void GameLost()
        {
            timer.Stop();
            gridPanel.Enabled = false;

            MessageBox.Show("You Lost :(", "Minesweeper");
            ShowMines();
        }

        private void GameWon()
        {
            timer.Stop();
            gridPanel.Enabled = false;

            MessageBox.Show("You Won!", "Minesweeper");
            ShowMines();
        }

        private void ShowMines()
        {
            foreach (Tuple<int, int> mine in _minesweeper.GetAllMines())
            {
                _squares[mine.Item1, mine.Item2].Image = Properties.Resources.Mine;
            }
        }

        private void exitMenu_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void newGameMenu_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void optionsMenu_Click(object sender, EventArgs e)
        {
            new OptionsForm(_currentGameOptions).ShowDialog();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            elapsedTimeLabel.Text = (int.Parse(elapsedTimeLabel.Text) + 1).ToString();
        }
    }
}
