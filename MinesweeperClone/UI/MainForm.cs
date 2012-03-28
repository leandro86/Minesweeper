using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using MinesweeperClone.Logic;

namespace MinesweeperClone.UI
{
    public partial class MainForm : Form
    {
        private Minesweeper _minesweeper;

        private const int SquareSize = 30;
        private GameOptions _currentGameOptions;

        private readonly int _formMargin;
        private GridSquare _previousClickedSquare;

        private enum GameState
        {
            NotStarted,
            Playing,
            Ended
        }

        private GameState _currentGameState;

        public MainForm()
        {
            InitializeComponent();

            _formMargin = gridArea.Location.X;
            _previousClickedSquare = new GridSquare(0, 0);
            
            _currentGameOptions = OptionsForm.GetDefaultOptions();
            NewGame();
        }

        private void NewGame()
        {
            timer.Stop();
            gridArea.Visible = false;
            _currentGameState = GameState.NotStarted;
            
            _previousClickedSquare.Row = 0;
            _previousClickedSquare.Column = 0;

            minesLeftLabel.Text = _currentGameOptions.GridMines.ToString();
            elapsedTimeLabel.Text = "0";

            _minesweeper = new Minesweeper(_currentGameOptions.GridRows, _currentGameOptions.GridColumns,
                                           _currentGameOptions.GridMines);

            AdjustControlsPosition();

            gridArea.Visible = true;
            gridArea.Enabled = true;

            facePicture.Image = Properties.Resources.PlainFace;
        }

        private void AdjustControlsPosition()
        {
            gridArea.Width = _currentGameOptions.GridColumns * SquareSize;
            gridArea.Height = _currentGameOptions.GridRows * SquareSize;

            backgroundPanel.Width = gridArea.Width;
            backgroundPanel.Height = gridArea.Height;
            backgroundPanel.Location = gridArea.Location;
            
            minesLeftLabel.Location = new Point(gridArea.Location.X + gridArea.Width - 35, minesLeftLabel.Location.Y);
            minePicture.Location = new Point(gridArea.Location.X + gridArea.Width - 68, minePicture.Location.Y);

            facePicture.Location = new Point(gridArea.Width / 2 - gridArea.Location.X, facePicture.Location.Y);

            ClientSize = new Size(gridArea.Location.X + gridArea.Width + _formMargin,
                                  gridArea.Location.Y + gridArea.Height + menu.Height + _formMargin);

            CenterToScreen();
        }

        private void RevealSquare(int row, int column)
        {
            if (_minesweeper[row, column] == Square.Unopened)
            {
                Square revealedSquare = _minesweeper.RevealSquare(row, column);

                if (_currentGameState == GameState.NotStarted)
                {
                    if (revealedSquare != Square.Blank)
                    {
                        _minesweeper.RearrangeForBlankSquare(row, column);
                        revealedSquare = Square.Blank;
                    }
                    _currentGameState = GameState.Playing;    
                    timer.Start();
                }

                DrawImage(gridArea.CreateGraphics(), revealedSquare, row, column);

                if (revealedSquare == Square.Mine)
                {
                    GameEnded(false);
                }
                else if (revealedSquare == Square.Blank)
                {
                    foreach (GridSquare adjacentSquare in _minesweeper.GetAdjacentSquares(row, column))
                    {
                        RevealSquare(adjacentSquare.Row, adjacentSquare.Column);
                    }
                }
            }

            if (_minesweeper.SquaresLeft == _currentGameOptions.GridMines)
            {
                GameEnded(true);
            }
        }

        private void GameEnded(bool won)
        {
            timer.Stop();
            gridArea.Enabled = false;
            _currentGameState = GameState.Ended;

            facePicture.Image = won ? Properties.Resources.BigSmileFace : Properties.Resources.CryingFace;
            RevealMines();
        }

        private void RevealMines()
        {
            foreach (GridSquare mine in _minesweeper.GetAllMines())
            {
                _minesweeper[mine.Row, mine.Column] = Square.Mine;                
            }
            gridArea.Invalidate();
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
            if (new OptionsForm(_currentGameOptions).ShowDialog() == DialogResult.OK)
            {
                NewGame();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            elapsedTimeLabel.Text = (int.Parse(elapsedTimeLabel.Text) + 1).ToString();
        }

        private void gridArea_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < _currentGameOptions.GridRows; i++)
            {
                for (int j = 0; j < _currentGameOptions.GridColumns; j++)
                {
                    DrawImage(e.Graphics, _minesweeper[i, j], i, j);
                }
            }
        }

        private void gridArea_MouseDown(object sender, MouseEventArgs e)
        {
            GridSquare clickedSquare = GetClickedSquare(e.X, e.Y);

            if (_minesweeper[clickedSquare.Row, clickedSquare.Column] == Square.Unopened || 
                _minesweeper[clickedSquare.Row, clickedSquare.Column] == Square.Flag)
            {
                if (e.Button == MouseButtons.Left)
                {
                    RevealSquare(clickedSquare.Row, clickedSquare.Column);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    Square square;

                    if (_minesweeper[clickedSquare.Row, clickedSquare.Column] == Square.Flag)
                    {
                        minesLeftLabel.Text = (int.Parse(minesLeftLabel.Text) + 1).ToString();
                        square = _minesweeper[clickedSquare.Row, clickedSquare.Column] = Square.Unopened;
                    }
                    else
                    {
                        minesLeftLabel.Text = (int.Parse(minesLeftLabel.Text) - 1).ToString();
                        square = _minesweeper[clickedSquare.Row, clickedSquare.Column] = Square.Flag;
                    }

                    DrawImage(gridArea.CreateGraphics(), square, clickedSquare.Row, clickedSquare.Column);
                }
            }
        }

        private GridSquare GetClickedSquare(int x, int y)
        {
            return new GridSquare(y / SquareSize, x / SquareSize);
        }

        private void gridArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (_currentGameState != GameState.Playing)
            {
                return;
            }
            
            GridSquare clickedSquare = GetClickedSquare(e.X, e.Y);

            if (clickedSquare.Row != _previousClickedSquare.Row ||
                clickedSquare.Column != _previousClickedSquare.Column)
            {
                if (_minesweeper[clickedSquare.Row, clickedSquare.Column] == Square.Unopened)
                {
                    DrawImage(gridArea.CreateGraphics(), Properties.Resources.UnopenedBright, clickedSquare.Row,
                              clickedSquare.Column);
                }
                
                if (_minesweeper[_previousClickedSquare.Row,_previousClickedSquare.Column] == Square.Unopened)
                {
                    DrawImage(gridArea.CreateGraphics(), Properties.Resources.Unopened, _previousClickedSquare.Row,
                              _previousClickedSquare.Column);
                }    
            }

            _previousClickedSquare.Row = clickedSquare.Row;
            _previousClickedSquare.Column = clickedSquare.Column;
        }

        private void DrawImage(Graphics g, Bitmap image, int row, int column)
        {
            g.DrawImage(image, column * SquareSize, row * SquareSize, SquareSize, SquareSize);   
        }

        private void DrawImage(Graphics g, Square square, int row, int column)
        {
            Bitmap image = (Bitmap)Properties.Resources.ResourceManager.GetObject(square.ToString());
            g.DrawImage(image, column * SquareSize, row * SquareSize, SquareSize, SquareSize);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            //TODO: remove, only for testing
            if (e.KeyCode == Keys.Space)
            {
                ShowMines();
            }
            gridArea.Invalidate();
        }

        private void ShowMines()
        {
            foreach (GridSquare mine in _minesweeper.GetAllMines())
            {
                DrawImage(gridArea.CreateGraphics(), Square.Mine, mine.Row, mine.Column);
            }
        }
    }
}
