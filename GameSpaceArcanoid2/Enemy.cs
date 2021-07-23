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
        
        public static FlyStatus StateFly = FlyStatus.Standart;
        public static SinglyLinkedList<Point> MosqitWay;
        public static Point MosqitPosition;

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
            if (FlyStatus.Standart == StateFly)
            {
                Img.Top += speed;
                if (Img.Top >= _control.Bottom && _control.WindowState != FormWindowState.Minimized)
                {
                    ChangeLocation();
                    Controller.Lifes -= 1;
                    Controller.LabelLive.Text = $"Жизней осталось {Controller.Lifes}";
                    if (Controller.Lifes == 0)
                    {
                        _control.Close();
                        MenuForm.GetTextBox().Text = "Вы проиграли";
                        Controller.Lifes = 10;
                    }
                }
            }
            else
            {
                if (MosqitWay.Previous != null)
                {
                    MosqitWay = MosqitWay.Previous;
                    Img.Location = MosqitWay.Value;
                }
            }
        }

        private Random rnd = new Random();
        private void InitializeComponent()
        {
            Img.Image = Properties.Resources.komar;
            Img.BackColor = Color.Transparent;
            Img.Size = new Size(60, 60);
            Img.SizeMode = PictureBoxSizeMode.StretchImage;
            Img.Name = "Mosqit";

            ChangeLocation();
            _control.Controls.Add(Img);
        }

        public void Delete()
        {
            ChangeLocation();
        }

        public Point CheckMosqit()
        {
            return Img.Location;
        }

        private void ChangeLocation()
        {
            var x = rnd.Next(0, _control.Width - Img.Width);
            var y = -Img.Height;
            Img.Location = new Point(x, y);
        }

        public enum FlyStatus
        {
            Standart,
            Gravitation
        }
    }
}
