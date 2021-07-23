using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace GameSpaceArcanoid2
{
    public class Ball
    {
        public enum StateMove
        {
            Fly,
            Stoped,
            Fall
        }

        private Frog _frog;

        public StateMove StateMoving = StateMove.Stoped;
        public static PictureBox Img = new PictureBox();

        public int DirectionX
        {
            get => _directionX;
            set
            {
                if (StateMoving is StateMove.Stoped && Math.Abs(value) <= 8)
                {
                    _directionY = -10 + Math.Abs(value);
                    _directionX = value;
                    _moveState.x = value > 0
                        ? MoveStateX.Rigth
                        : MoveStateX.Left;
                    _moveState.y = _directionY >= 0
                        ? MoveStateY.Down
                        : MoveStateY.Up;
                    
                }
            }
        }
        public int DirectionY
        {
            get => _directionY;
            set
            {
                if (StateMoving is StateMove.Stoped && Math.Abs(value) <= 8)
                {
                    _directionX = -10 + Math.Abs(value);
                    _directionY = value;
                    _moveState.x = _directionX > 0
                        ? MoveStateX.Rigth
                        : MoveStateX.Left;
                    _moveState.y = _directionY >= 0
                        ? MoveStateY.Down
                        : MoveStateY.Up;
                }
            }
        }

        private Form _control;

        private int _directionX = 0;
        private int _directionY = -10;
        private (MoveStateX x, MoveStateY y) _moveState = (MoveStateX.Rigth, MoveStateY.Up);

        public Ball(Form control)
        {
            _control = control;
            _frog = new Frog(_control);
            InitializeComponent();
        }

        public void StopFlight()
        {
            if (StateMoving is StateMove.Fly)
            {
                StateMoving = StateMove.Fall;
                new Thread(o =>
                {
                    //while (Img.Top < _control.Height - 38 - Img.Height)
                    //{
                    //    Img.Top += 10;
                    //    Thread.Sleep(10);
                    //}
                    //Img.Top = _control.Height - 38 - Img.Height;
                    _moveState.y = MoveStateY.Up;
                    _directionY = -Math.Abs(_directionY);
                    StateMoving = StateMove.Stoped;
                }).Start();
            }
        }

        public void Move()
        {
            if (_control.WindowState != FormWindowState.Minimized)
            {
                Img.Top = Img.Location.Y + _directionY;
                Img.Left = Img.Location.X + _directionX;
                if (Img.Right >= _control.Width && _moveState.x == MoveStateX.Rigth)
                {
                    _directionX = -_directionX;
                    _moveState.x = MoveStateX.Left;
                }
                if (Img.Left <= 0 && _moveState.x == MoveStateX.Left)
                {
                    _directionX = -_directionX;
                    _moveState.x = MoveStateX.Rigth;
                }

                if (Img.Top <= 0 && _moveState.y == MoveStateY.Up)
                {
                    _directionY = -_directionY;
                    _moveState.y = MoveStateY.Down;
                }

                if (Img.Bottom >= _control.Height && _moveState.y == MoveStateY.Down)
                {
                    _directionY = -_directionY;
                    StopFlight();
                    Frog.PaintFrog(Img.Left - Img.Width / 2 - 50);
                    
                }
            }
        }

        private void InitializeComponent()
        {
            Img.Image = Properties.Resources.balled;
            Img.BackColor = Color.Transparent;
            Img.Size = new Size(30, 30);
            Img.SizeMode = PictureBoxSizeMode.StretchImage;
            Img.Location = new Point(
                _control.Width / 2 - Img.Width / 2,
                _control.Height - 38 - Img.Height
                );

            _control.Controls.Add(Frog.ImgFrog);
            _control.Controls.Add(Img);
        }

        private enum MoveStateY
        {
            Up,
            Down
        }
        private enum MoveStateX
        {
            Rigth,
            Left
        }
    }
}