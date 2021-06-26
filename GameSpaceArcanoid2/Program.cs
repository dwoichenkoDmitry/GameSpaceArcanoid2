using System.Windows.Forms;

namespace GameSpaceArcanoid2
{
    public class Program : Form
    {
        public Label label1;
        public Controller control;

        public Program(int width, int height)
        {
            InitializeComponent();
            Width = width;
            Height = height;



            DoubleBuffered = true;
            Load += ((sender, args) =>
            {
                if (control == null)
                    control = new Controller(this, this.label1);

                control.Start();
            });
        }
        public static void Main()
        {
            Application.Run(new Program(600, 500));
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Gold;
            this.label1.Location = new System.Drawing.Point(22, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 29);
            this.label1.TabIndex = 0;
            // 
            // Program
            // 
            this.BackgroundImage = global::GameSpaceArcanoid2.Properties.Resources.bolot;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(461, 358);
            this.Controls.Add(this.label1);
            this.Name = "Program";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}