using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Project_48.Forms.Control
{
    public class GroupBoxLogin: GroupBox
    {
        private Label lab_email = new Label();
        private Label lab_pass = new Label();
        private TextBox Email = new TextBox();
        private TextBox Pass = new TextBox();
        private Button EnterButton = new Button();
        private RadioButton LoginButton = new RadioButton();
        private RadioButton RegisterButton = new RadioButton();
        private string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
        public GroupBoxLogin()
        {
            Location = new Point(10, 40);
            Size = new Size(200, 160);

            lab_email.Text = "Email:";
            lab_email.Size = new Size(60, 20);
            lab_email.Location = new Point(30, 30);

            lab_pass.Text = "Password:";
            lab_pass.Size = new Size(60, 20);
            lab_pass.Location = new Point(30, 60);

            Email.Location = new Point(100, 25);
            Email.Size = new Size(70, 20);

            Pass.UseSystemPasswordChar = true;
            Pass.Location = new Point(100, 55);
            Pass.Size = new Size(70, 20);

            EnterButton.Location = new Point(60, 100);
            EnterButton.Size = new Size(75, 23);
            EnterButton.Text = "Login";
            EnterButton.Click += EnterButton_Click;

            LoginButton.Location = new Point(20, 130);
            LoginButton.Size = new Size(75, 23);
            LoginButton.Text = "Login";
            LoginButton.Checked = true;
            LoginButton.Click += LoginButton_Click;

            RegisterButton.Location = new Point(110, 130);
            RegisterButton.Size = new Size(75, 23);
            RegisterButton.Text = "Register";
            RegisterButton.Click += RegisterButton_Click;

            Controls.Add(lab_email);
            Controls.Add(lab_pass);
            Controls.Add(Email);
            Controls.Add(Pass);
            Controls.Add(EnterButton);
            Controls.Add(LoginButton);
            Controls.Add(RegisterButton);
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            if (CheckEmail())
            {
                if (RegisterButton.Checked) Connect.Registration(Email.Text, Pass.Text);
                else
                {
                    if (Connect.Login(Email.Text, Pass.Text)) Visible = false;
                    else MessageBox.Show("Error login!");
                }
                Email.Text = "";
                Pass.Text = "";
            }
            else MessageBox.Show("Error!");
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            EnterButton.Text = "Register";
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            EnterButton.Text = "Login";
        }
        private bool CheckEmail()
        {
            if (Email.Text != "" && Pass.Text != "")
            {
                if (Regex.IsMatch(Email.Text, pattern, RegexOptions.IgnoreCase)) return true;
                else return false;
            }
            else return false;
        }
    }
}
