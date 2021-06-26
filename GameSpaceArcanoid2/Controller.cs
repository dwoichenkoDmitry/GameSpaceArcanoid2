using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace GameSpaceArcanoid2
{
    public class Controller
    {
        private Form _control;
        private Ball _ball;
        private List<Enemy> _enemies;
        private int score = 0;

        Label lab;

        private Dictionary<Keys, Action> PressKey;
        private bool isActive = false;
        public Controller(Form control, Label lable)
        {
            _control = control;
            lab = lable;
            timer.Tick += (s, e) =>
            {
                if (isActive)
                    Update();
            };
            timer.Interval = 4;
            timer.Start();



            _ball = new Ball(_control);

            _enemies = new List<Enemy>() { new Enemy(_control) };
            PressKey = new Dictionary<Keys, Action>()
            {
                { Keys.Right, () => _ball.DirectionX += 1 },
                { Keys.Left, () => _ball.DirectionX -= 1 },
                { Keys.Space, () =>
                    {
                        if (_ball.StateMoving is Ball.StateMove.Fly)
                            _ball.StopFlight();
                        else if (_ball.StateMoving is Ball.StateMove.Stoped)
                            _ball.StateMoving = Ball.StateMove.Fly;
                        Frog.PaintFrog(Ball.Img.Left-Ball.Img.Width/2-50);
                    }
                }
            };
            _control.KeyDown += (s, e) =>
            {
                if (PressKey.ContainsKey(e.KeyCode))
                    PressKey[e.KeyCode].Invoke();
            };
        }

        public Timer timer = new Timer();

        public void Start()
        {
            if (!isActive)
            {
                isActive = true;
            }
        }

        public void Stop()
        {
            isActive = false;
        }

        public void CheckWindow()
        {
            if (_control.WindowState == FormWindowState.Minimized)
            {
                Stop();
            }
        }


        private Queue<Enemy> _queueEnemysAdd = new Queue<Enemy>();
        private Queue<Enemy> _queueEnemysDelete = new Queue<Enemy>();
        private void Update()
        {
            if (_ball.StateMoving is Ball.StateMove.Fly)
                _ball.Move();
            foreach (var e in _enemies)
            {
                e.Move();
                if (Collider.OnTriger(Ball.Img, e.Img))
                {
                    e.Delete();
                    score = score + 1;
                    lab.Text = $"Счёт: {score}";
                    if (F(_enemies.Count, score, (x) => x * x))
                    {
                        _queueEnemysAdd.Enqueue(new Enemy(_control, 1000));
                    }
                }
            }
            while (_queueEnemysAdd.Count != 0)
                _enemies.Add(_queueEnemysAdd.Dequeue());
        }

        private bool F(int X, int Y, Func<int, int> F)
            => F(X + 1) == Y;

        private Font _font = new Font(FontFamily.GenericSerif, 12);
        private Brush _brush = new SolidBrush(Color.Black);
        private PointF _labelPositionX = new PointF(10, 20);
        private PointF _labelPositionY = new PointF(10, 35);

    }

    public static class Collider
    {
        public static bool OnTriger(PictureBox box1, PictureBox box2)
        {
            return box1.Bounds.IntersectsWith(box2.Bounds);
        }
    }
}