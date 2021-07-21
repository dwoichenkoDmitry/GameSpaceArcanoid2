using System.Windows.Forms;

namespace GameSpaceArcanoid2
{
    public class Program : Form
    {
        public Label label1;
        public Label label2;
        public Controller control;

        public Program(int width, int height)
        {
            InitializeComponent();

            Width = width;
            Height = height;
            WindowState = FormWindowState.Maximized;

            DoubleBuffered = true;
            Load += ((sender, args) =>
            {
                if (control == null)
                    control = new Controller(this, this.label1, this.label2);

                control.Start();
                Closed += (s, e) => control.Stop();
            });
        }
        public static void Main()
        {
            Application.Run(new MenuForm());
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.DarkRed;
            this.label2.Location = new System.Drawing.Point(22, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 25);
            this.label2.TabIndex = 1;
            // 
            // Program
            // 
            this.BackgroundImage = global::GameSpaceArcanoid2.Properties.Resources.bolot;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(461, 358);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Program";
            this.Load += new System.EventHandler(this.Program_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Program_Load(object sender, System.EventArgs e)
        {

        }
    }
}