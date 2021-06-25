using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace GameSpaceArcanoid2
{
    enum MoveStateY
    {
        Up,
        Down
    }
    enum MoveStateX
    {
        Rigth,
        Left
    }
    public class Ball
    {
        public bool IsFlying = false;
        public int DirectionX
        {
            get => _directionX;
            set
            {
                if (!IsFlying && Math.Abs(value) <= 8)
                {
                    _directionY = -10 - Math.Abs(value);
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

        private Control _control;
        private PictureBox Img = new PictureBox();
        private int _locationX => Img.Left;
        private int _locationY => Img.Top;
        private int _directionX = 0;
        private int _directionY = -10;
        private (MoveStateX x, MoveStateY y) _moveState =
            (MoveStateX.Rigth, MoveStateY.Up);

        public Ball(Control control)
        {
            _control = control;
            InitializeComponent();
        }

        public void StopFlight()
        {
            Img.Top = _control.Height - 38 - Img.Height;
            IsFlying = false;
            _moveState.y = MoveStateY.Up;
            _directionY = -Math.Abs(_directionY);
        }

        public void Move()
        {
            Img.Top = _locationY + _directionY;
            Img.Left = _locationX + _directionX;

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
            }
        }

        private void InitializeComponent()
        {
            Img.Image = Properties.Resources.ball;
            Img.Size = new Size(55, 55);
            Img.SizeMode = PictureBoxSizeMode.StretchImage;
            Img.Location = new Point(
                _control.Width / 2 - Img.Width / 2,
                _control.Height - 38 - Img.Height
                );
            _control.Controls.Add(Img);
        }
    }
}
