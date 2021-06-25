using System.Windows.Forms;
namespace GameSpaceArcanoid2
{
    public class Program : Form
    {
        public Program(int width, int height)
        {
            Width = width;
            Height = height;
            DoubleBuffered = true;
            Load += ((sender, args) =>
            {
                var controller = new Controller(this);
                controller.Start();
            });

        }
        public static void Main()
        {
            Application.Run(new Program(600, 500));
        }
    }
}