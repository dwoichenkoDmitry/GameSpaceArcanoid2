using System;
using System.Drawing;
using System.Windows.Forms;
namespace GameSpaceArcanoid2
{
    public class Enemy
    {
        private Control _control;
        public PictureBox Img = new PictureBox();
        private int speed = 2;
        public Enemy(Control control)
        {
            _control = control;
            InitializeComponent();
        }
        public void Move()
        {
            Img.Top += speed;
            if (Img.Top >= _control.Bottom)
                ChangeLocation();
        }
        private void InitializeComponent()
        {
            rnd = new Random();
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

        private Random rnd;

        private void ChangeLocation()
        {
            var x = rnd.Next();
            var y = -Img.Height;
            Img.Location = new Point(x, y);
        }
    }
}