using Project_48.Forms.Control;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project_48.Forms
{
    public partial class Login : Form
    {
        private GroupBoxLogin boxLogin = new GroupBoxLogin();

        private Label Status = new Label();
        private Button Exit = new Button();
        public Login()
        {
            InitializeComponent();

            boxLogin.VisibleChanged += BoxLogin_VisibleChanged;

            Status.Text = "Offline";
            Status.ForeColor = Color.Red;
            Status.Size = new Size(60, 20);
            Status.Location = new Point(20, 20);

            Exit.Text = "Exit";
            Exit.Size = new Size(75, 23);
            Exit.Location = new Point(130, 15);
            Exit.Visible = false;
            Exit.Click += Exit_Click;

            Controls.Add(Status);
            Controls.Add(Exit);
            Controls.Add(boxLogin);
        }

        private void BoxLogin_VisibleChanged(object sender, EventArgs e)
        {
            if (boxLogin.Visible)
            {
                Status.Text = "Offline";
                Status.ForeColor = Color.Red;
                Exit.Visible = false;
            }
            else
            {
                Status.Text = "Online";
                Status.ForeColor = Color.Green;
                Exit.Visible = true;
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            boxLogin.Visible = true;
        }
    }
}
