namespace Project_66_Client.View
{
    public partial class LoginView : UserControl
    {
        public TextBox Password = new();
        public TextBox FirstName = new();
        public Button Login = new();
        public RadioButton IsLogin = new();
        public RadioButton IsRegister = new();
        public LoginView()
        {
            InitializeComponent();
            Location = new(350, 150);
            AutoSize = true;

            Label labelName = new();
            labelName.Text = "Name:";
            labelName.Location = new(0, 5);

            Label labelPass = new();
            labelPass.Text = "Password:";
            labelPass.Location = new(0, 55);

            FirstName.Location = new(100, 0);

            Password.Location = new(100, 50);
            Password.UseSystemPasswordChar = true;

            IsRegister.Text = "Register";
            IsRegister.Location = new(20, 100);

            IsLogin.Text = "Login";
            IsLogin.Checked = true;
            IsLogin.Location = new(120, 100);

            Login.Text = "Click";
            Login.Location = new(60, 150);


            Controls.Add(labelPass);
            Controls.Add(labelName);
            Controls.Add(FirstName);
            Controls.Add(IsLogin);
            Controls.Add(IsRegister);
            Controls.Add(Password);
            Controls.Add(Login);
        }
    }
}
