namespace Project_66_Client.View
{
    public partial class GameView : UserControl
    {
        public Panel Panel = new();
        public Panel PanelPlayers = new();
        public ListBox Players = new();
        public LoginView LoginView = new();
        public RoomView RoomView = new();
        public ClientView ClientView = new();
        public GameView()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            Controls.Add(RoomView);
            Controls.Add(ClientView);
            Controls.Add(LoginView);

            PanelPlayers.Location = new(5, 175);
            PanelPlayers.Size = new(80, 150);

            Label labelPlayers = new();
            labelPlayers.Text = "Players:";
            labelPlayers.Location = new(0, 0);

            Players.Location = new(0, 25);
            Players.Size = new(70, 90);
            Players.BackColor = Color.LightGray;

            Panel.Enabled = false;
            Panel.Size = new(100, 450);
            Panel.Location = new(800, 0);
            Panel.BackColor = Color.LightGray;
            PanelPlayers.Controls.Add(labelPlayers);
            PanelPlayers.Controls.Add(Players);
            Panel.Controls.Add(PanelPlayers);
            Controls.Add(Panel);
        }

        public void LoginVisible()
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        RoomView.Visible = false;
                        ClientView.Visible = false;
                        Panel.Visible = false;
                        PanelPlayers.Visible = false;
                        LoginView.Visible = true;
                    }));
                }
                else
                {
                    RoomView.Visible = false;
                    ClientView.Visible = false;
                    Panel.Visible = false;
                    PanelPlayers.Visible = false;
                    LoginView.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void GameVisible()
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        LoginView.Visible = false;
                        ClientView.Visible = false;
                        Panel.Visible = true;
                        PanelPlayers.Visible = true;
                        RoomView.Visible = true;
                    }));
                }
                else
                {
                    LoginView.Visible = false;
                    ClientView.Visible = false;
                    Panel.Visible = true;
                    PanelPlayers.Visible = true;
                    RoomView.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void ClientVisible()
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        LoginView.Visible = false;
                        RoomView.Visible = false;
                        PanelPlayers.Visible = false;
                        Panel.Visible = true;
                        ClientView.Visible = true;
                    }));
                }
                else
                {
                    LoginView.Visible = false;
                    RoomView.Visible = false;
                    PanelPlayers.Visible = false;
                    Panel.Visible = true;
                    ClientView.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
