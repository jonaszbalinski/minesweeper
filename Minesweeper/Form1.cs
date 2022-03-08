using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form1 : Form
    {
        private class GameButton : Button
        {
            private bool isBomb;
            private int bombsNearCount;

            public bool IsBomb { get => isBomb; set => isBomb = value; }
            public int BombsNearCount { get => bombsNearCount; set => bombsNearCount = value; }
        }

        private GameButton[,] gameFields;
        private int fieldSize = 20;
        private int bombCount = 20;
        public Form1()
        {
            InitializeComponent();


            InitializeGrid(15, 15);
        }

        private void InitializeGrid(int x, int y)
        {
            Random random = new Random();
            
            gameFields = new GameButton[x, y];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    GameButton btn = new GameButton();
                    gameFields[i, j] = btn;
                    btn.Size = new Size(fieldSize, fieldSize);
                    btn.Margin = new Padding(0, 0, 0, 0);
                    btn.Location = new Point(i*fieldSize, j*fieldSize);
                    btn.Click += (s, e) => { 
                        if (s is GameButton)
                        {
                           GameButton b = s as GameButton;
                            int indexX = (int)b.Location.X / fieldSize;
                            int indexY = (int)b.Location.Y / fieldSize;
                           if (b.IsBomb)
                           {
                                b.BackColor = Color.Red;
                                MessageBox.Show("Game Over");
                           }
                           else
                           {
                               b.Text = b.BombsNearCount.ToString();
                               //showBombFreeFields(indexX, indexY, x, y);

                                for(int m = 0; m < x; m++)
                                {
                                    for (int n = 0; n < y; n++)
                                    {
                                        if(!gameFields[m, n].IsBomb)
                                        {
                                            gameFields[m, n].Text = gameFields[m, n].BombsNearCount.ToString();
                                        }
                                    }
                                }
                           }
                        }
                    };
                    this.gamePanel.Controls.Add(btn);

                    btn.IsBomb = false;
                    btn.BombsNearCount = 0;
                }
            }

            for (int i = 0; i < bombCount; i++)
            {
                int randX = random.Next(x);
                int randY = random.Next(y);

                if (!gameFields[randX, randY].IsBomb)
                {
                    gameFields[randX, randY].IsBomb = true;
                    if (randX > 0 && randX < x-1)
                    {
                        if (randY > 0 && randY < y-1)
                        {
                            gameFields[randX - 1, randY - 1].BombsNearCount++;
                            gameFields[randX, randY - 1].BombsNearCount++;
                            gameFields[randX + 1, randY - 1].BombsNearCount++;

                            gameFields[randX - 1, randY].BombsNearCount++;
                            gameFields[randX + 1, randY].BombsNearCount++;

                            gameFields[randX - 1, randY + 1].BombsNearCount++;
                            gameFields[randX, randY + 1].BombsNearCount++;
                            gameFields[randX + 1, randY + 1].BombsNearCount++;
                        }
                        else if (randY == 0)
                        {
                            gameFields[randX - 1, randY].BombsNearCount++;
                            gameFields[randX + 1, randY].BombsNearCount++;

                            gameFields[randX - 1, randY + 1].BombsNearCount++;
                            gameFields[randX, randY + 1].BombsNearCount++;
                            gameFields[randX + 1, randY + 1].BombsNearCount++;
                        }
                        else
                        {
                            gameFields[randX - 1, randY - 1].BombsNearCount++;
                            gameFields[randX, randY - 1].BombsNearCount++;
                            gameFields[randX + 1, randY - 1].BombsNearCount++;

                            gameFields[randX - 1, randY].BombsNearCount++;
                            gameFields[randX + 1, randY].BombsNearCount++;
                        }
                    }
                    else if (randX == 0)
                    {
                        if (randY > 0 && randY < y-1)
                        {
                            gameFields[randX, randY - 1].BombsNearCount++;
                            gameFields[randX + 1, randY - 1].BombsNearCount++;

                            gameFields[randX + 1, randY].BombsNearCount++;

                            gameFields[randX, randY + 1].BombsNearCount++;
                            gameFields[randX + 1, randY + 1].BombsNearCount++;
                        }
                        else if (randY == 0)
                        {
                            gameFields[randX + 1, randY].BombsNearCount++;

                            gameFields[randX, randY + 1].BombsNearCount++;
                            gameFields[randX + 1, randY + 1].BombsNearCount++;
                        }
                        else
                        {
                            gameFields[randX, randY - 1].BombsNearCount++;
                            gameFields[randX + 1, randY - 1].BombsNearCount++;

                            gameFields[randX + 1, randY].BombsNearCount++;
                        }
                    }
                    else
                    {
                        if (randY > 0 && randY < y-1)
                        {
                            gameFields[randX - 1, randY - 1].BombsNearCount++;
                            gameFields[randX, randY - 1].BombsNearCount++;

                            gameFields[randX - 1, randY].BombsNearCount++;

                            gameFields[randX - 1, randY + 1].BombsNearCount++;
                            gameFields[randX, randY + 1].BombsNearCount++;
                        }
                        else if (randY == 0)
                        {
                            gameFields[randX - 1, randY].BombsNearCount++;

                            gameFields[randX - 1, randY + 1].BombsNearCount++;
                            gameFields[randX, randY + 1].BombsNearCount++;
                        }
                        else
                        {
                            gameFields[randX - 1, randY - 1].BombsNearCount++;
                            gameFields[randX, randY - 1].BombsNearCount++;

                            gameFields[randX - 1, randY].BombsNearCount++;
                        }
                    }
                }
                else
                {
                    i--;
                }
            }
        }

        private void showBombFreeFields(int x, int y, int maxX, int maxY)
        { /*
            if (x > 0 && x < maxX - 1)
            {
                if (y > 0 && y < maxY - 1)
                {
                    if (bombsNearCounter[x - 1, y - 1] == 0) gameFields[x - 1, y - 1].PerformClick();
                    if (bombsNearCounter[x, y - 1] == 0) gameFields[x, y - 1].PerformClick();
                    if (bombsNearCounter[x + 1, y - 1] == 0) gameFields[x + 1, y - 1].PerformClick();

                    if (bombsNearCounter[x - 1, y] == 0) gameFields[x - 1, y].PerformClick();
                    if (bombsNearCounter[x + 1, y] == 0) gameFields[x + 1, y].PerformClick();

                    if (bombsNearCounter[x - 1, y + 1] == 0) gameFields[x - 1, y + 1].PerformClick();
                    if (bombsNearCounter[x, y + 1] == 0) gameFields[x, y + 1].PerformClick();
                    if (bombsNearCounter[x + 1, y + 1] == 0) gameFields[x + 1, y + 1].PerformClick();
                }
                else if (y == 0)
                {
                    if (bombsNearCounter[x - 1, y] == 0) gameFields[x - 1, y].PerformClick();
                    if (bombsNearCounter[x + 1, y] == 0) gameFields[x + 1, y].PerformClick();

                    if (bombsNearCounter[x - 1, y + 1] == 0) gameFields[x - 1, y + 1].PerformClick();
                    if (bombsNearCounter[x, y + 1] == 0) gameFields[x, y + 1].PerformClick();
                    if (bombsNearCounter[x + 1, y + 1] == 0) gameFields[x + 1, y + 1].PerformClick();
                }
                else
                {
                    if (bombsNearCounter[x - 1, y - 1] == 0) gameFields[x - 1, y - 1].PerformClick();
                    if (bombsNearCounter[x, y - 1] == 0) gameFields[x, y - 1].PerformClick();
                    if (bombsNearCounter[x + 1, y - 1] == 0) gameFields[x + 1, y - 1].PerformClick();

                    if (bombsNearCounter[x - 1, y] == 0) gameFields[x - 1, y].PerformClick();
                    if (bombsNearCounter[x + 1, y] == 0) gameFields[x + 1, y].PerformClick();
                }
            }
            else if (x == 0)
            {
                if (y > 0 && y < maxY - 1)
                {
                    if (bombsNearCounter[x, y - 1] == 0) gameFields[x, y - 1].PerformClick();
                    if (bombsNearCounter[x + 1, y - 1] == 0) gameFields[x + 1, y - 1].PerformClick();

                    if (bombsNearCounter[x + 1, y] == 0) gameFields[x + 1, y].PerformClick();

                    if (bombsNearCounter[x, y + 1] == 0) gameFields[x, y + 1].PerformClick();
                    if (bombsNearCounter[x + 1, y + 1] == 0) gameFields[x + 1, y + 1].PerformClick();
                }
                else if (y == 0)
                {
                    if (bombsNearCounter[x + 1, y] == 0) gameFields[x + 1, y].PerformClick();

                    if (bombsNearCounter[x, y + 1] == 0) gameFields[x, y + 1].PerformClick();
                    if (bombsNearCounter[x + 1, y + 1] == 0) gameFields[x + 1, y + 1].PerformClick();
                }
                else
                {
                    if (bombsNearCounter[x, y - 1] == 0) gameFields[x, y - 1].PerformClick();
                    if (bombsNearCounter[x + 1, y - 1] == 0) gameFields[x + 1, y - 1].PerformClick();

                    if (bombsNearCounter[x + 1, y] == 0) gameFields[x + 1, y].PerformClick();
                }
            }
            else
            {
                if (y > 0 && y < maxY - 1)
                {
                    if (bombsNearCounter[x - 1, y - 1] == 0) gameFields[x - 1, y - 1].PerformClick();
                    if (bombsNearCounter[x, y - 1] == 0) gameFields[x, y - 1].PerformClick();

                    if (bombsNearCounter[x - 1, y] == 0) gameFields[x - 1, y].PerformClick();

                    if (bombsNearCounter[x - 1, y + 1] == 0) gameFields[x - 1, y + 1].PerformClick();
                    if (bombsNearCounter[x, y + 1] == 0) gameFields[x, y + 1].PerformClick();
                }
                else if (y == 0)
                {
                    if (bombsNearCounter[x - 1, y] == 0) gameFields[x - 1, y].PerformClick();

                    if (bombsNearCounter[x - 1, y + 1] == 0) gameFields[x - 1, y + 1].PerformClick();
                    if (bombsNearCounter[x, y + 1] == 0) gameFields[x, y + 1].PerformClick();
                }
                else
                {
                    if (bombsNearCounter[x - 1, y - 1] == 0) gameFields[x - 1, y - 1].PerformClick();
                    if (bombsNearCounter[x, y - 1] == 0) gameFields[x, y - 1].PerformClick();

                    if (bombsNearCounter[x - 1, y] == 0) gameFields[x - 1, y].PerformClick();
                }
            }*/
        }
            
    }
}
