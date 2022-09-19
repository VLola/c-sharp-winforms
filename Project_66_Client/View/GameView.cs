using Project_66_Client.Controller;

namespace Project_66_Client.View
{
    public partial class GameView : UserControl
    {
        GameController _gameController;
        Panel Panel = new();
        Panel PanelPlayers = new();
        ListBox Players = new();
        LoginView _loginView;
        RoomView _roomView;
        ClientView _clientView;
        public GameView()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            _loginView = new();
            _clientView = new();
            Controls.Add(_clientView);
            Controls.Add(_loginView);

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
        public void SetController(GameController gameController)
        {
            _gameController = gameController;
            _roomView.PreviewKeyDown += _gameController.RoomView_PreviewKeyDown;
            _loginView.Login.Click += _gameController.Login_Click;
            _clientView.Start.Click += _gameController.Start_Click;
            _clientView.Exit.Click += _gameController.Exit_Click;
            _clientView.BuyPower.Click += _gameController.BuyPower_Click;
            _clientView.BuyDefence.Click += _gameController.BuyDefence_Click;
            Disposed += _gameController._gameView_Disposed;
        }
        public void LoginVisible()
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        _roomView.Visible = false;
                        _clientView.Visible = false;
                        Panel.Visible = false;
                        PanelPlayers.Visible = false;
                        _loginView.Visible = true;
                    }));
                }
                else
                {
                    _roomView.Visible = false;
                    _clientView.Visible = false;
                    Panel.Visible = false;
                    PanelPlayers.Visible = false;
                    _loginView.Visible = true;
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
                        _loginView.Visible = false;
                        _clientView.Visible = false;
                        Panel.Visible = true;
                        PanelPlayers.Visible = true;
                        _roomView.Visible = true;
                    }));
                }
                else
                {
                    _loginView.Visible = false;
                    _clientView.Visible = false;
                    Panel.Visible = true;
                    PanelPlayers.Visible = true;
                    _roomView.Visible = true;
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
                        _loginView.Visible = false;
                        _roomView.Visible = false;
                        PanelPlayers.Visible = false;
                        Panel.Visible = true;
                        _clientView.Visible = true;
                    }));
                }
                else
                {
                    _loginView.Visible = false;
                    _roomView.Visible = false;
                    PanelPlayers.Visible = false;
                    Panel.Visible = true;
                    _clientView.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public RoomView AddRoomView()
        {
            _roomView = new();
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    Controls.Add(_roomView);
                }));
            }
            else
            {
                Controls.Add(_roomView);
            }
            return _roomView;
        }
        public InfoView AddInfoView()
        {
            InfoView infoView = new();
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    Panel.Controls.Add(infoView);
                }));
            }
            else
            {
                Panel.Controls.Add(infoView);
            }
            return infoView;
        }
        public void PlayersClear()
        {
            Players.Items.Clear();
        }
        public void PlayersAdd(string[] arr)
        {
            Players.Items.AddRange(arr);
        }
        public int GetPlayers()
        {
            return (int)_clientView.Players.SelectedItem;
        }
        public string GetFirstName()
        {
            return _loginView.FirstName.Text;
        }
        public string GetPassword()
        {
            return _loginView.Password.Text;
        }
        public void ResetFirstName()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    _loginView.FirstName.Text = "";
                }));
            }
            else
            {
                _loginView.FirstName.Text = "";
            }
        }
        public void ResetPassword()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    _loginView.Password.Text = "";
                }));
            }
            else
            {
                _loginView.Password.Text = "";
            }
        }
        public bool GetCheckedRegister()
        {
            return _loginView.IsRegister.Checked;
        }
        public bool GetCheckedLogin()
        {
            return _loginView.IsLogin.Checked;
        }
        public void SetClientName(string name)
        {
            _clientView.ClientName.Text = name;
        }
    }
}
