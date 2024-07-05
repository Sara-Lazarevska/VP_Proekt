using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace _2048
{
    public partial class Form1 : Form
    {
        private const int GridSize = 4;
        private Button[,] grid;
        private Random random = new Random();
        private int score = 0;

        public Form1()
        {
            InitializeComponent();
            InitializeGrid();
            StartNewGame();
        }

        private void InitializeGrid()
        {
            grid = new Button[GridSize, GridSize];

            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    var button = new Button
                    {
                        Dock = DockStyle.Fill,
                        Font = new Font("Arial", 24, FontStyle.Bold),
                        BackColor = Color.LightGray
                    };
                    tableLayoutPanel1.Controls.Add(button, j, i);
                    grid[i, j] = button;
                }
            }

            KeyPreview = true;
            KeyDown += Form1_KeyDown;
        }

        private void StartNewGame()
        {
            foreach (var button in grid)
            {
                button.Text = "";
                button.BackColor = Color.LightGray;
            }

            AddRandomTile();
            AddRandomTile();
            score = 0;
            UpdateScore();
        }

        private void AddRandomTile()
        {
            var emptyTiles = new List<Button>();
            foreach (var button in grid)
            {
                if (button.Text == "")
                {
                    emptyTiles.Add(button);
                }
            }

            if (emptyTiles.Count > 0)
            {
                var tile = emptyTiles[random.Next(emptyTiles.Count)];
                tile.Text = random.Next(0, 2) == 0 ? "2" : "4";
                UpdateTileColor(tile);
            }
        }

        private void UpdateTileColor(Button tile)
        {
            switch (tile.Text)
            {
                case "2":
                    tile.BackColor = Color.Beige;
                    break;
                case "4":
                    tile.BackColor = Color.Bisque;
                    break;
                case "8":
                    tile.BackColor = Color.BurlyWood;
                    break;
                case "16":
                    tile.BackColor = Color.Coral;
                    break;
                case "32":
                    tile.BackColor = Color.DarkOrange;
                    break;
                case "64":
                    tile.BackColor = Color.OrangeRed;
                    break;
                case "128":
                    tile.BackColor = Color.Gold;
                    break;
                case "256":
                    tile.BackColor = Color.Goldenrod;
                    break;
                case "512":
                    tile.BackColor = Color.Khaki;
                    break;
                case "1024":
                    tile.BackColor = Color.Yellow;
                    break;
                case "2048":
                    tile.BackColor = Color.YellowGreen;
                    break;
                default:
                    tile.BackColor = Color.LightGray;
                    break;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            bool moved = false;

            switch (e.KeyCode)
            {
                case Keys.Up:
                    moved = MoveUp();
                    break;
                case Keys.Down:
                    moved = MoveDown();
                    break;
                case Keys.Left:
                    moved = MoveLeft();
                    break;
                case Keys.Right:
                    moved = MoveRight();
                    break;
            }

            if (moved)
            {
                AddRandomTile();
                if (IsGameOver())
                {
                    MessageBox.Show("Game Over!");
                }
            }
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if (MoveUp())
            {
                AddRandomTile();
                if (IsGameOver())
                {
                    MessageBox.Show("Game Over!");
                }
            }
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (MoveDown())
            {
                AddRandomTile();
                if (IsGameOver())
                {
                    MessageBox.Show("Game Over!");
                }
            }
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            if (MoveLeft())
            {
                AddRandomTile();
                if (IsGameOver())
                {
                    MessageBox.Show("Game Over!");
                }
            }
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            if (MoveRight())
            {
                AddRandomTile();
                if (IsGameOver())
                {
                    MessageBox.Show("Game Over!");
                }
            }
        }

        private bool MoveUp()
        {
            bool moved = false;
            for (int col = 0; col < GridSize; col++)
            {
                for (int row = 1; row < GridSize; row++)
                {
                    if (grid[row, col].Text != "")
                    {
                        int targetRow = row;
                        while (targetRow > 0 && grid[targetRow - 1, col].Text == "")
                        {
                            targetRow--;
                        }
                        if (targetRow > 0 && grid[targetRow - 1, col].Text == grid[row, col].Text)
                        {
                            grid[targetRow - 1, col].Text = (int.Parse(grid[targetRow - 1, col].Text) * 2).ToString();
                            grid[row, col].Text = "";
                            moved = true;
                            score += int.Parse(grid[targetRow - 1, col].Text);
                        }
                        else if (targetRow != row && grid[targetRow, col].Text == "")
                        {
                            grid[targetRow, col].Text = grid[row, col].Text;
                            grid[row, col].Text = "";
                            moved = true;
                        }
                    }
                }
            }
            UpdateAllTileColors();
            UpdateScore();
            return moved;
        }

        private bool MoveDown()
        {
            bool moved = false;
            for (int col = 0; col < GridSize; col++)
            {
                for (int row = GridSize - 2; row >= 0; row--)
                {
                    if (grid[row, col].Text != "")
                    {
                        int targetRow = row;
                        while (targetRow < GridSize - 1 && grid[targetRow + 1, col].Text == "")
                        {
                            targetRow++;
                        }
                        if (targetRow < GridSize - 1 && grid[targetRow + 1, col].Text == grid[row, col].Text)
                        {
                            grid[targetRow + 1, col].Text = (int.Parse(grid[targetRow + 1, col].Text) * 2).ToString();
                            grid[row, col].Text = "";
                            moved = true;
                            score += int.Parse(grid[targetRow + 1, col].Text);
                        }
                        else if (targetRow != row && grid[targetRow, col].Text == "")
                        {
                            grid[targetRow, col].Text = grid[row, col].Text;
                            grid[row, col].Text = "";
                            moved = true;
                        }
                    }
                }
            }
            UpdateAllTileColors();
            UpdateScore();
            return moved;
        }

        private bool MoveLeft()
        {
            bool moved = false;
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 1; col < GridSize; col++)
                {
                    if (grid[row, col].Text != "")
                    {
                        int targetCol = col;
                        while (targetCol > 0 && grid[row, targetCol - 1].Text == "")
                        {
                            targetCol--;
                        }
                        if (targetCol > 0 && grid[row, targetCol - 1].Text == grid[row, col].Text)
                        {
                            grid[row, targetCol - 1].Text = (int.Parse(grid[row, targetCol - 1].Text) * 2).ToString();
                            grid[row, col].Text = "";
                            moved = true;
                            score += int.Parse(grid[row, targetCol - 1].Text);
                        }
                        else if (targetCol != col && grid[row, targetCol].Text == "")
                        {
                            grid[row, targetCol].Text = grid[row, col].Text;
                            grid[row, col].Text = "";
                            moved = true;
                        }
                    }
                }
            }
            UpdateAllTileColors();
            UpdateScore();
            return moved;
        }

        private bool MoveRight()
        {
            bool moved = false;
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = GridSize - 2; col >= 0; col--)
                {
                    if (grid[row, col].Text != "")
                    {
                        int targetCol = col;
                        while (targetCol < GridSize - 1 && grid[row, targetCol + 1].Text == "")
                        {
                            targetCol++;
                        }
                        if (targetCol < GridSize - 1 && grid[row, targetCol + 1].Text == grid[row, col].Text)
                        {
                            grid[row, targetCol + 1].Text = (int.Parse(grid[row, targetCol + 1].Text) * 2).ToString();
                            grid[row, col].Text = "";
                            moved = true;
                            score += int.Parse(grid[row, targetCol + 1].Text);
                        }
                        else if (targetCol != col && grid[row, targetCol].Text == "")
                        {
                            grid[row, targetCol].Text = grid[row, col].Text;
                            grid[row, col].Text = "";
                            moved = true;
                        }
                    }
                }
            }
            UpdateAllTileColors();
            UpdateScore();
            return moved;
        }

        private void UpdateAllTileColors()
        {
            foreach (var tile in grid)
            {
                UpdateTileColor(tile);
            }
        }

        private void UpdateScore()
        {
            labelScore.Text = $"Score: {score}";
        }

        private bool IsGameOver()
        {
            foreach (var button in grid)
            {
                if (button.Text == "")
                {
                    return false;
                }
            }

            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    if (row < GridSize - 1 && grid[row, col].Text == grid[row + 1, col].Text)
                    {
                        return false;
                    }
                    if (col < GridSize - 1 && grid[row, col].Text == grid[row, col + 1].Text)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
