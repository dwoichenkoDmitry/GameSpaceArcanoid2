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
        private Control _control;
        private Ball _ball;
        private List<Enemy> _enemies;
        private int score = 0;
        private Dictionary<Keys, Action> PressKey;
        public Controller(Control control)
        {
            _control = control;
            _control.Paint += OnPaint;
            _ball = new Ball(_control);
            _enemies = new List<Enemy>() { new Enemy(_control) };
            PressKey = new Dictionary<Keys, Action>()
            {
            { Keys.Right, () => _ball.DirectionX += 1 },
            { Keys.Left, () => _ball.DirectionX -= 1 },
            { Keys.Space, () =>
            {

             if (_ball.IsFlying)
                    _ball.StopFlight();
                else
                    _ball.IsFlying = true;
            }
                }
            };
            _control.KeyDown += (s, e) =>
            {
                if (PressKey.ContainsKey(e.KeyCode))
                    PressKey[e.KeyCode].Invoke();
            };
        }

        public void Start()
        {
            var timer = new Timer();
            timer.Interval = 10;
            timer.Tick += (s, e) => Update();
            timer.Tick += (s, e) => _control.Invalidate();
            timer.Start();
        }

        private Queue<Enemy> _queueEnemysAdd = new Queue<Enemy>();
        private Queue<Enemy> _queueEnemysDelete = new Queue<Enemy>();
        private void Update()
        {
           if (_ball.IsFlying)
                _ball.Move();
            foreach (var e in _enemies)
            {
                e.Move();
                if (Collider.OnTriger(_ball.Img, e.Img))
                {
                    e.Delete();
                    score = score + 1;
                    if (F(_enemies.Count, (x) => x * x))
                        _queueEnemysAdd.Enqueue(new Enemy(_control));
                }
            }
            while (_queueEnemysAdd.Count != 0)
                _enemies.Add(_queueEnemysAdd.Dequeue());
        }

        private bool F(int X, Func<int, int> Y)
        {
            return Y(X) < score;
        }
        private Font _font = new Font(FontFamily.GenericSerif, 12);
        private Brush _brush = new SolidBrush(Color.Black);
        private PointF _labelPositionX = new PointF(10, 20);
        private PointF _labelPositionY = new PointF(10, 35);

        private void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            e.Graphics.DrawString($"x : {_ball.DirectionX}", _font, _brush, _labelPositionX);
            e.Graphics.DrawString($"y : {_ball.DirectionY}", _font, _brush, _labelPositionY);
            e.Graphics.DrawString($"score : {score}", _font, _brush, new PointF(10, 55));
        }
    }

    public static class Collider
    {
        public static bool OnTriger(PictureBox box1, PictureBox box2)
        {
            return box1.Bounds.IntersectsWith(box2.Bounds);
        }
    }
}