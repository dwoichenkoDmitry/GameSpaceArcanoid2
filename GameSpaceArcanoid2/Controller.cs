using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GameSpaceArcanoid2
{
    public class Controller
    {
        private Control _control;
        private Ball _ball;

        private Dictionary<Keys, Action> PressKey;

        public Controller(Control control)
        {
            _control = control;
            _ball = new Ball(_control);
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
            timer.Interval = 20;
            timer.Tick += (s, e) => Update();
            timer.Tick += (s, e) => _control.Invalidate();
            timer.Start();
        }

        private void Update()
        {
            if (_ball.IsFlying)
                _ball.Move();
        }
    }
}