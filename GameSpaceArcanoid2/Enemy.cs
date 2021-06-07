using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using static System.Windows.Forms.Control;


namespace GameSpaceArcanoid2
{
    class Enemy
    {
        
        private int width, height, x, y;
        Random rnd = new Random();
        public void Enemies()
        {
            width = 50;
            height = 42;
            x = rnd.Next(0, width * 10);
            y = 0;
        }
        private void CreateControl(Control p, int a, int h)
        {
            PictureBox pb = new PictureBox();
            x = a*67;
            pb.Location = new Point(x, h);
            pb.Size = new Size(50, 42);
            pb.BackgroundImage = Image.FromFile("C:/Users/Lenovo/source/repos/GameSpaceArcanoid2/GameSpaceArcanoid2/img/mosqit.png");
            pb.BackgroundImageLayout = ImageLayout.Stretch;
            pb.Name = "Mosqit";
            p.Controls.Add(pb);
            
        }
        public void CreateSprites(Control p, int a, int h)
        {
            CreateControl(p, a, h);
        }
        
    }
}
