using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameSpaceArcanoid2
{
    class Program: Form
    {
        static void Main()
        {
            Application.Run(new Program());

        }

        public Program()
        {
            Load += (s, e) => 
            {
                var controller = new Controller(this);
                controller.Start();
            };
        }
    }
}
