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
        private void CreateControl(Form p, int a)
        {
            PictureBox pb = new PictureBox();
            x = a*width;
            pb.Location = new Point(x, y);
            pb.Size = new Size(width, height);
            pb.BackgroundImage = Image.FromFile("C:/Users/dwoic/Desktop/Game/GameSpaceArcanoid2/GameSpaceArcanoid2/img/mosqit.png");
            pb.BackgroundImageLayout = ImageLayout.Stretch;
            pb.Name = "Mosqit";
            p.Controls.Add(pb);
            
        }
        public void CreateSprites(Form p, int a)
        {
            CreateControl(p, a);
        }
        
    }
}
