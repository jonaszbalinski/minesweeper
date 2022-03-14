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
            private bool isShown;
            private int bombsNearCount;

            public bool IsBomb { get => isBomb; set => isBomb = value; }
            public int BombsNearCount { get => bombsNearCount; set => bombsNearCount = value; }
            public bool IsShown { get => isShown; set => isShown = value; }
        }

        private GameButton[,] gameFields;
        private int fieldSize = 20;
        private int bombCount = 60;
        public Form1()
        {
            InitializeComponent();


            InitializeGrid(30, 30);
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
                    btn.BackColor = Color.LightGray;
                    btn.Margin = new Padding(0, 0, 0, 0);
                    btn.Location = new Point(i*fieldSize, j*fieldSize);
                    btn.Click += (s, e) => { 
                        if (s is GameButton)
                        {
                           GameButton b = s as GameButton;
                           int indexX = (int)b.Location.X / fieldSize;
                           int indexY = (int)b.Location.Y / fieldSize;

                           b.IsShown = true;
                           if (b.IsBomb)
                           {
                                b.BackColor = Color.Red;
                                DialogResult dialog = MessageBox.Show("Restart?", "Game Over",
                                    MessageBoxButtons.YesNo);
                                if (dialog == DialogResult.Yes) 
                                {
                                    this.gamePanel.Controls.Clear();
                                    InitializeGrid(x, y);
                                }
                                if (dialog == DialogResult.No) Application.Exit();
                                
                           }
                           else
                           {
                               b.Text = b.BombsNearCount.ToString();
                               
                               switch(b.BombsNearCount)
                                {
                                    case 0: b.BackColor = Color.WhiteSmoke; b.Text = "";
                                        showSafeFields(indexX, indexY, x, y); break;
                                    case 1: b.BackColor = Color.LightGreen; break;
                                    case 2: b.BackColor = Color.LightBlue; break;
                                    case 3: b.BackColor = Color.Blue; break;
                                    case 4: b.BackColor = Color.BlueViolet; break;
                                    case 5: b.BackColor = Color.Violet; break;
                                    case 6: b.BackColor = Color.Orange; break;
                                    case 7: b.BackColor = Color.OrangeRed; break;
                                    case 8: b.BackColor = Color.DarkOrange; break;
                                    default: b.BackColor = Color.Black; break;
                                }
                                //showBombFreeFields(indexX, indexY, x, y);
                                /*
                                 for(int m = 0; m < x; m++)
                                 {
                                     for (int n = 0; n < y; n++)
                                     {
                                         if(!gameFields[m, n].IsBomb)
                                         {
                                             gameFields[m, n].Text = gameFields[m, n].BombsNearCount.ToString();
                                         }
                                     }
                                 }*/
                            }
                        }
                    };
                    this.gamePanel.Controls.Add(btn);

                    btn.IsBomb = false;
                    btn.BombsNearCount = 0;
                    btn.IsShown = false;
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

        
        private void showSafeFields(int x, int y, int maxX, int maxY)
        { 
            for(int i = x - 1; i <= x+1; i++)
            {
                for(int j = y - 1; j <= y+1; j++)
                {
                    if (i >= 0 && i < maxX && j >= 0 && j < maxY)
                    {
                        if (gameFields[i, j].IsShown) continue;
                        else
                        {
                            gameFields[i, j].IsShown = true;

                            switch (gameFields[i, j].BombsNearCount)
                            {
                                case 0: gameFields[i, j].BackColor = Color.WhiteSmoke; break;
                                case 1: gameFields[i, j].BackColor = Color.LightGreen; break;
                                case 2: gameFields[i, j].BackColor = Color.LightBlue; break;
                                case 3: gameFields[i, j].BackColor = Color.Blue; break;
                                case 4: gameFields[i, j].BackColor = Color.BlueViolet; break;
                                case 5: gameFields[i, j].BackColor = Color.Violet; break;
                                case 6: gameFields[i, j].BackColor = Color.Orange; break;
                                case 7: gameFields[i, j].BackColor = Color.OrangeRed; break;
                                case 8: gameFields[i, j].BackColor = Color.DarkOrange; break;
                                default: gameFields[i, j].BackColor = Color.Black; break;
                            }
                            if (gameFields[i, j].BombsNearCount == 0)
                            {
                                showSafeFields(i, j, maxX, maxY);
                            }
                            else
                            {
                                gameFields[i, j].Text = gameFields[i, j].BombsNearCount.ToString();
                            }
                        }
                    }
                }
            }
        }
            
    }
}
