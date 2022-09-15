namespace Project_66_Client.View
{
    public partial class LoginView : UserControl
    {
        public TextBox Password = new TextBox();
        public TextBox FirstName = new TextBox();
        public Button Login = new Button();
        public RadioButton IsLogin = new RadioButton();
        public RadioButton IsRegister = new RadioButton();
        public ComboBox Players = new ComboBox();
        public LoginView()
        {
            InitializeComponent();
            Location = new Point(300, 100);
            AutoSize = true;
            Label labelName = new Label();
            labelName.Text = "Name:";
            labelName.Location = new Point(0, 5);
            Label labelPass = new Label();
            labelPass.Text = "Password:";
            labelPass.Location = new Point(0, 55);
            FirstName.Location = new Point(100, 0);
            Password.Location = new Point(100, 50);
            Password.UseSystemPasswordChar = true;
            Label labelPlayers = new Label();
            labelPlayers.Text = "Players:";
            labelPlayers.Location = new Point(0, 105);
            Players.Location = new Point(100, 100);
            Players.Items.Add(2);
            Players.Items.Add(3);
            Players.Items.Add(4);
            Players.Size = new Size(100, 20);
            Players.SelectedIndex = 0;
            IsRegister.Text = "Register";
            IsRegister.Location = new Point(20, 150);
            IsLogin.Text = "Login";
            IsLogin.Checked = true;
            IsLogin.Location = new Point(120, 150);
            Login.Text = "Click";
            Login.Location = new Point(60, 200);
            Controls.Add(labelPass);
            Controls.Add(labelName);
            Controls.Add(FirstName);
            Controls.Add(labelPlayers);
            Controls.Add(Players);
            Controls.Add(IsLogin);
            Controls.Add(IsRegister);
            Controls.Add(Password);
            Controls.Add(Login);
        }
    }
}
