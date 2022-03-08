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
        private Button[,] gameFields;
        private bool[,] bombPositions;
        private int fieldSize = 20;
        public Form1()
        {
            InitializeComponent();


            InitializeGrid(20, 20);
        }

        private void InitializeGrid(int x, int y)
        {
            Random random = new Random();
            
            gameFields = new Button[x, y];
            bombPositions = new bool[x, y];

            //float scaleX = (this.Width / x); 
            //float scaleY = (this.Height / y); 
            int scaleX = (int)(400 / x); 
            int scaleY = (int)(400 / y); 

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Button btn = new Button();
                    gameFields[i, j] = btn;
                    btn.Size = new Size(fieldSize, fieldSize);
                    btn.Margin = new Padding(0, 0, 0, 0);
                    btn.Location = new Point(i*fieldSize, j*fieldSize);
                    btn.Click += (s, e) => { 
                        if (s is Button)
                        {
                           Button b = s as Button;
                           if(bombPositions[(int)b.Location.X/fieldSize, (int)b.Location.Y/fieldSize])
                           {
                                b.BackColor = Color.Red;
                           }
                           else
                           {
                                b.BackColor = Color.Green;
                           }
                        }
                    };
                    this.gamePanel.Controls.Add(btn);

                    bombPositions[i, j] = false;
                }
            }

            for (int i = 0; i < 10; i++)
            {
                int randX = random.Next(x);
                int randY = random.Next(y);

                if (!bombPositions[randX, randY])
                    bombPositions[randX, randY] = true;
                else i--;
            }
        }
    }
}
