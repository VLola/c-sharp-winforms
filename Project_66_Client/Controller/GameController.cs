using Project_66_Client.Model;
using Project_66_Client.View;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Text.Json;

namespace Project_66_Client.Controller
{
    public class GameController
    {
        GameView _gameView;
        ClientController _clientController = new();
        InfoController _infoController;
        RoomController _roomController;
        Socket _socket;
        List<string> _players = new();
        List<string> _playersCheck = new();
        public GameController(GameView gameView)
        {
            try
            {
                _gameView = gameView;
                _gameView.LoginVisible();
                _roomController = new(_gameView.RoomView);
                _gameView.RoomView.PreviewKeyDown += RoomView_PreviewKeyDown;
                _gameView.LoginView.Login.Click += Login_Click;
                _gameView.ClientView.Start.Click += Start_Click;
                _gameView.ClientView.Exit.Click += Exit_Click;
                _gameView.ClientView.BuyPower.Click += BuyPower_Click;
                _gameView.ClientView.BuyDefence.Click += BuyDefence_Click;
                _gameView.Disposed += _gameView_Disposed;
                InfoView infoView = new();
                _infoController = new(infoView);
                _gameView.Panel.Controls.Add(infoView);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BuyDefence_Click(object? sender, EventArgs e)
        {
            _clientController.SetBuyDefence(true);
            SendClient();
            _clientController.SetBuyDefence(false);
        }

        private void BuyPower_Click(object? sender, EventArgs e)
        {
            _clientController.SetBuyPower(true);
            SendClient();
            _clientController.SetBuyPower(false);
        }

        private void Exit_Click(object? sender, EventArgs e)
        {
            Disconnect();
        }

        private void _gameView_Disposed(object? sender, EventArgs e)
        {
            Disconnect();
        }

        private void Login_Click(object? sender, EventArgs e)
        {
            Login();
        }
        private void Login()
        {
            try
            {
                Load();
                Listen();
                Thread.Sleep(1000);
                _clientController.SetName(_gameView.LoginView.FirstName.Text);
                _gameView.Invoke(new Action(() => {
                    _gameView.LoginView.FirstName.Text = "";
                }));
                _clientController.SetPassword(_gameView.LoginView.Password.Text);
                _gameView.Invoke(new Action(() => {
                    _gameView.LoginView.Password.Text = "";
                }));
                if (_gameView.LoginView.IsRegister.Checked == true)
                {
                    _clientController.SetIsRegister(true);
                    SendClient();
                    _clientController.SetIsRegister(false);
                    _clientController.SetPassword("");
                }
                else if (_gameView.LoginView.IsLogin.Checked == true)
                {
                    _clientController.SetIsLogin(true);
                    SendClient();
                    _clientController.SetIsLogin(false);
                    _clientController.SetPassword("");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Start_Click(object? sender, EventArgs e)
        {
            Start();
        }
        private void Start()
        {
            try
            {
                _clientController.SetPlayers((int)_gameView.ClientView.Players.SelectedItem);
                _clientController.SetIsStart(true);
                SendClient();
                _clientController.SetIsStart(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void Disconnect()
        {
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
            _gameView.LoginVisible();
        }
        private void Load()
        {
            try
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _socket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8086));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void RoomView_PreviewKeyDown(object? sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (_clientController.GetLocationY() > 0) 
                {
                    _clientController.Up();
                    _clientController.SetIsDirection(true);
                    SendClient();
                    _clientController.SetIsDirection(false);
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (_clientController.GetLocationY() < 400)
                {
                    _clientController.Down();
                    _clientController.SetIsDirection(true);
                    SendClient();
                    _clientController.SetIsDirection(false);
                }
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (_clientController.GetLocationX() > 0)
                {
                    _clientController.Left();
                    _clientController.SetIsDirection(true);
                    SendClient();
                    _clientController.SetIsDirection(false);
                }
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (_clientController.GetLocationX() < 750)
                {
                    _clientController.Right();
                    _clientController.SetIsDirection(true);
                    SendClient();
                    _clientController.SetIsDirection(false);
                }
            }
            else if (e.KeyCode == Keys.Space)
            {
                _clientController.SetIsShot(true);
                SendClient();
                _clientController.SetIsShot(false);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Disconnect();
            }
        }
        private void SendClient()
        {
            try
            {
                _socket.Send(Encoding.Unicode.GetBytes(JsonSerializer.Serialize(_clientController.GetClient())));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Listen()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        int bytes = 0;
                        byte[] buffer = new byte[64000];
                        StringBuilder builder = new StringBuilder();
                        do
                        {
                            bytes = _socket.Receive(buffer);
                        } while (_socket.Available > 0);

                        builder.Append(Encoding.Unicode.GetString(buffer, 0, bytes));
                        if (builder.ToString() == "")
                        {
                            _socket.Shutdown(SocketShutdown.Both);
                            _socket.Close();
                            break;
                        }
                        else
                        {
                            ClientModel? client = JsonSerializer.Deserialize<ClientModel>(builder.ToString());
                            if (client != null)
                            {
                                if (client.IsLogin)
                                {
                                    if (client.Login)
                                    {
                                        _gameView.ClientVisible();
                                        _gameView.ClientView.ClientName.Text = _clientController.GetName();
                                        _infoController.SetInfo(client.Tank);
                                    }
                                    else MessageBox.Show("Login error!");
                                }
                                else if (client.IsRegister)
                                {
                                    if (client.Login)
                                    {
                                        _gameView.ClientVisible();
                                        _gameView.ClientView.ClientName.Text = _clientController.GetName();
                                        _infoController.SetInfo(client.Tank);
                                    }
                                    else MessageBox.Show("Register error!");
                                }
                                else if (client.IsStart)
                                {
                                    _gameView.GameVisible();
                                }
                                else if(client.BuyDefence || client.BuyPower)
                                {
                                    _infoController.SetInfo(client.Tank);
                                }
                                Task.Run(() =>
                                {
                                    _players.Clear();
                                    foreach (var item in client.Tanks)
                                    {
                                        _players.Add(item.Name);
                                        if (item.Name == _clientController.GetName())
                                        {
                                            _clientController.SetTank(item);
                                            _infoController.SetInfo(item);
                                        }
                                    }
                                    if(_playersCheck.Count != _players.Count)
                                    {
                                        _gameView.Players.Items.Clear();
                                        _gameView.Players.Items.AddRange(_players.ToArray());
                                        _playersCheck.Clear();
                                        foreach (var item in _players)
                                        {
                                            _playersCheck.Add(item);
                                        }
                                    }
                                    _roomController.LoadTanks(client.Tanks);
                                    _roomController.LoadBullets(client.Bullets);
                                });
                            }
                        }
                    }
                    catch
                    {
                        if(_socket != null)
                        {
                            _socket.Shutdown(SocketShutdown.Both);
                            _socket.Close();
                        }
                    }
                }
            });
        }


    }
}
