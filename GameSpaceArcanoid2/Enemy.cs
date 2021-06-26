using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameSpaceArcanoid2
{
    public class Enemy
    {
        private Form _control;
        public PictureBox Img = new PictureBox();
        private int speed = 2;

        public Enemy(Form control)
        {
            _control = control;
            InitializeComponent();
        }

        public Enemy(Form control, int timeOut)
        {
            _control = control;
            InitializeComponent();
            Img.Top -= timeOut / 10 * speed;
        }

        public void Move()
        {
            Img.Top += speed;
            if (Img.Top >= _control.Bottom && _control.WindowState != FormWindowState.Minimized)
                ChangeLocation();
        }

        private Random rnd = new Random();
        private void InitializeComponent()
        {
            Img.Image = Properties.Resources.mosqit;
            Img.Size = new Size(30, 30);
            Img.SizeMode = PictureBoxSizeMode.StretchImage;
            ChangeLocation();
            _control.Controls.Add(Img);
        }

        public void Delete()
        {
            ChangeLocation();
        }

        private void ChangeLocation()
        {
            var x = rnd.Next(0, _control.Width - Img.Width);
            var y = -Img.Height;
            Img.Location = new Point(x, y);
        }
    }
}
