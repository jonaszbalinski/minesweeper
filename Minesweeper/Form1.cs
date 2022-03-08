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
        public Form1()
        {
            InitializeComponent();
            InitializeGrid(20, 20);
        }

        private void InitializeGrid(uint x, uint y)
        {
            //float scaleX = (this.Width / x); 
            //float scaleY = (this.Height / y); 

            int scaleX = (int)(800 / x); 
            int scaleY = (int)(800 / y); 

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Button btn = new Button();
                    btn.Size = new Size((int)scaleX, (int)scaleY);
                    btn.Location = new Point((int)(i*scaleX), (int)(j*scaleY));
                    btn.Click += (s, e) => { btn.BackColor = Color.DarkBlue; };
                    this.gamePanel.Controls.Add(btn);
                }
            }
        }
    }
}
