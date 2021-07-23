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
        public static Label LabelLive;
        private PictureBox Img = new PictureBox();
        

        private Dictionary<Keys, Action> PressKey;
        private bool isActive = false;
        public Controller(Form control, Label lable, Label labelLifes)
        {
            _control = control;
            lab = lable;
            LabelLive = labelLifes;
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
                { Keys.Right, () => {
                    _ball.DirectionX += 1;
                    Frog.ChangeImg(_ball.DirectionX);

                    } 
                
                },
                { Keys.Left, () =>{
                    _ball.DirectionX -= 1 ;
                    Frog.ChangeImg(_ball.DirectionX);

                    } },
                {Keys.Escape, () =>
                {
                    _control.Close();
                    Controller.Lifes = 10;
                } },
                { Keys.Space, () =>
                    {
                        if (_ball.StateMoving is Ball.StateMove.Fly)
                            _ball.StopFlight();
                        else if (_ball.StateMoving is Ball.StateMove.Stoped)
                            _ball.StateMoving = Ball.StateMove.Fly;
                        
                        Frog.PaintFrog(Ball.Img.Left-Ball.Img.Width/2-50);
                        Frog.ChangeImg(_ball.DirectionX);
                        
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
            timer.Stop();
        }

        public void CheckWindow()
        {
            if (_control.WindowState == FormWindowState.Minimized)
            {
                Stop();
            }
        }

        public static int Lifes = 10; 
        private Queue<Enemy> _queueEnemysAdd = new Queue<Enemy>();
       
        
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
                    score = score + 5;
                    lab.Text = $"Счёт: {score}";
                    if(score == 30)
                    {
                        _control.Close();
                        MenuForm.GetTextBox().Text = "Вы победили!";
                    }
                    if (F(_enemies.Count, score, (x) => x * x))
                    {
                        _queueEnemysAdd.Enqueue(new Enemy(_control, 1000));
                    }
                    Random rnd = new Random();

                    int value = rnd.Next(0,2);
                    
                    if(value == 1 && Enemy.StateFly == Enemy.FlyStatus.Standart)
                    {
                        Enemy.StateFly = Enemy.FlyStatus.Gravitation;
                        Img.Image = Properties.Resources.hole;
                        Img.BackColor = Color.Transparent;
                        Img.Size = new Size(150, 150);
                        Img.SizeMode = PictureBoxSizeMode.StretchImage;
                        Img.Location = new Point(rnd.Next(0, _control.Width), rnd.Next(0, _control.Height - 230));
                        Img.Name = "Hole";
                        _control.Controls.Add(Img);

                         Enemy.MosqitWay = Algoritm.FindPaths(_control.Width,
                            _control.Height, Img.Location,
                            e.CheckMosqit());
                        
                        
                           var GravityTimer = new Timer();
                        GravityTimer.Interval = 10000;
                        GravityTimer.Tick += (sender, args) =>
                        {
                            Enemy.StateFly = Enemy.FlyStatus.Standart;
                            GravityTimer.Stop();
                        };
                        GravityTimer.Start();
                    }
                }
                
            }
            while (_queueEnemysAdd.Count != 0)
                _enemies.Add(_queueEnemysAdd.Dequeue());
        }

        private bool F(int X, int Y, Func<int, int> F)
            => F(X + 1) == Y;

        

    }

    public static class Collider
    {
        public static bool OnTriger(PictureBox box1, PictureBox box2)
        {
            return box1.Bounds.IntersectsWith(box2.Bounds);
        }
    }
}