﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Timer = System.Windows.Forms.Timer;


namespace GameSpaceArcanoid2
{
    public partial class Form1 : Form
    {
        
        double sx = 4, sy = 5, bx, by, sin = 1, cos = 0;

        double nap1 = 0, width, height;

        double nap2 = -10;

        float LX, LY;
        bool BallIsBottom = true;
       

        

        
        private void Form1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {

                Thread.Sleep(100);
                if (nap1 < 8)
                {
                    nap1 += 0.5;
                    if (nap1 < 0)
                    {
                        nap2 = -10 - nap1;
                    }
                    else { nap2 = -10 + nap1; }
                }
                label1.Text = $"{nap1}";
                label2.Text = $"{nap2}";
            }
            if (e.KeyCode == Keys.Left)
            {
                Thread.Sleep(100);
                if (nap1 > -8)
                {
                    nap1 -= 0.5;
                    if (nap1 < 0)
                    {
                        nap2 = -10 - nap1;
                    }
                    else { nap2 = -10 + nap1; }
                }
                label1.Text = $"{nap1}";
                label2.Text = $"{nap2}";
            }
            if (e.KeyCode == Keys.Space)
            {
                if (BallIsBottom == false)
                {
                    BallImg.Top = panel1.Height - BallImg.Height - 30;
                    BallImg.Left = panel1.Width / 2;
                    BallIsBottom = true;
                    sx = nap1;
                    sy = nap2;
                }
            }
        }

       




        public Form1()
        {
            
            InitializeComponent();
            
            InsertMosqits();
            

            bx = BallImg.Left;
            by = BallImg.Top;

            var timer = new Timer();
            timer.Interval = 10;
            timer.Tick += (sender, args) =>
            {
                
                MoveBall();
                
                EnemyMove();
                Invalidate();
                
            };
            timer.Start();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

       
        public void MoveBall()
        {
            if (BallIsBottom == true && !(this.WindowState == FormWindowState.Minimized))
            {
                double radius = (panel1.Height - 120);
                LX = (float)((panel1.Width / 2) + radius * Math.Cos(cos));
                LY = (float)((panel1.Height) - radius * Math.Sin(sin));
                bx += sx;
                by += sy;

                BallImg.Left = (int)bx;
                BallImg.Top = (int)by;
                
                if (BallImg.Top <= 0)
                {
                    sy *= -1;
                }
                if ((BallImg.Right >= panel1.Width) || (BallImg.Left <= 0))
                {
                    sx *= -1;
                }
                if (BallImg.Top + BallImg.Height >= panel1.Height)
                {
                    BallIsBottom = false;
                    sx = 0; sy = 0;
                    by = panel1.Height - BallImg.Height;
                    bx = panel1.Width / 2;
                    BallImg.Top = panel1.Height - BallImg.Height;
                    BallImg.Left = panel1.Width / 2;

                    CreateEnemys();
                    
                }
            }
            width = panel1.Width;
            height = panel1.Height;
        }

        List<PictureBox> mosqits = new List<PictureBox>();
        private void InsertMosqits()
        {
            foreach (Control c in this.Controls)
            {
                if (c is PictureBox && c.Name == "Mosqit")
                {
                    PictureBox mosqit = (PictureBox)c;
                    mosqits.Add(mosqit);
                }
            }
        }

        public void CreateEnemys()
        {
            double width = panel1.Width/10;
            int oneEnemyWidth = (int)Math.Round(width);
            Random rnd = new Random();
            int position1 = rnd.Next(1, 10);
            int position2 = rnd.Next(1, 10);
            int position3 = rnd.Next(1, 10);
            List<int> values = new List<int>();
            values.Add(position1);
            
            values.Add(position3);
            while (values.Contains(position2)) { position2 = rnd.Next(1, 10); }
            values.Add(position2);
            while (values.Contains(position3)) { position3 = rnd.Next(1, 10); }

            new Enemy().CreateSprites(this, position1);
        }
        private void EnemyMove()
        {
            foreach (PictureBox enemy in mosqits)
            {
                enemy.Location = new Point(enemy.Location.X, enemy.Location.Y + 5);

            }
        }
    }
}