using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace GameSpaceArcanoid2
{
    class Frog
    {
        public static PictureBox ImgFrog = new PictureBox();

        public static List<Bitmap> FrogStateList = new List<Bitmap>();

        private static Form _control;

        public Frog(Form control)
        {
            FrogStateList.Add(Properties.Resources._81);
            FrogStateList.Add(Properties.Resources._71);
            FrogStateList.Add(Properties.Resources._61);
            FrogStateList.Add(Properties.Resources._51);
            FrogStateList.Add(Properties.Resources._41);
            FrogStateList.Add(Properties.Resources._31);
            FrogStateList.Add(Properties.Resources._21);
            FrogStateList.Add(Properties.Resources._11);
            FrogStateList.Add(Properties.Resources._0);
            FrogStateList.Add(Properties.Resources._1);
            FrogStateList.Add(Properties.Resources._2);
            FrogStateList.Add(Properties.Resources._3);
            FrogStateList.Add(Properties.Resources._4);
            FrogStateList.Add(Properties.Resources._5);
            FrogStateList.Add(Properties.Resources._6);
            FrogStateList.Add(Properties.Resources._7);
            FrogStateList.Add(Properties.Resources._8);

            ImgFrog.Image = FrogStateList[8];
            ImgFrog.BackColor = Color.Transparent;
            ImgFrog.Size = new Size(150, 150);
            ImgFrog.SizeMode = PictureBoxSizeMode.StretchImage;

            _control = control;
            PaintFrog(_control.Width / 2 - ImgFrog.Width / 2 - 35);

        }

        public static void ChangeImg(int state)
        {
            state += 8;
            ImgFrog.Image = FrogStateList[state];
            ImgFrog.BackColor = Color.Transparent;
            ImgFrog.Size = new Size(150, 150);
            ImgFrog.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public static void PaintFrog(int x)
        {
            ImgFrog.Location = new Point(x,
                _control.Height - ImgFrog.Height);
            _control.Controls.Add(ImgFrog);
        }
    }
}