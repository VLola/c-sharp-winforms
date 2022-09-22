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
                _roomController = new(_gameView.AddRoomView());
                _infoController = new(_gameView.AddInfoView());
                _gameView.SetController(this);
                _gameView.LoginVisible();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void BuyDefence_Click(object? sender, EventArgs e)
        {
            _clientController.SetBuyDefence(true);
            SendClient();
            _clientController.SetBuyDefence(false);
        }

        public void BuyPower_Click(object? sender, EventArgs e)
        {
            _clientController.SetBuyPower(true);
            SendClient();
            _clientController.SetBuyPower(false);
        }

        public void Exit_Click(object? sender, EventArgs e)
        {
            Disconnect();
        }

        public void _gameView_Disposed(object? sender, EventArgs e)
        {
            Disconnect();
        }

        public void Login_Click(object? sender, EventArgs e)
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
                _clientController.SetName(_gameView.GetFirstName());
                _gameView.ResetFirstName();
                _clientController.SetPassword(_gameView.GetPassword());
                _gameView.ResetPassword();
                if (_gameView.GetCheckedRegister())
                {
                    _clientController.SetIsRegister(true);
                    SendClient();
                    _clientController.SetIsRegister(false);
                    _clientController.SetPassword("");
                }
                else if (_gameView.GetCheckedLogin())
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

        public void Start_Click(object? sender, EventArgs e)
        {
            Start();
        }
        private void Start()
        {
            try
            {
                _clientController.SetPlayers((int)_gameView.GetPlayers());
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
        public void RoomView_PreviewKeyDown(object? sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (_clientController.GetLocationY() > 0) 
                {
                    if (IsRun())
                    {
                        _clientController.Up();
                        _clientController.SetIsDirection(true);
                        SendClient();
                        _clientController.SetIsDirection(false);
                    }
                }
            }
            else if (e.KeyCode == Keys.Down)
            {

                if (IsRun())
                {
                    if (_clientController.GetLocationY() < 400)
                    {
                        _clientController.Down();
                        _clientController.SetIsDirection(true);
                        SendClient();
                        _clientController.SetIsDirection(false);
                    }
                }
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (IsRun())
                {
                    if (_clientController.GetLocationX() > 0)
                    {
                        _clientController.Left();
                        _clientController.SetIsDirection(true);
                        SendClient();
                        _clientController.SetIsDirection(false);
                    }
                }
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (IsRun())
                {
                    if (_clientController.GetLocationX() < 750)
                    {
                        _clientController.Right();
                        _clientController.SetIsDirection(true);
                        SendClient();
                        _clientController.SetIsDirection(false);
                    }
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
        private bool IsRun()
        {
            lock (_roomController.BrickControllers) foreach (var item in _roomController.BrickControllers)
                {
                    if (new Rectangle(new Point(_clientController.GetLocationX(), _clientController.GetLocationY()), new Size(50, 50)).IntersectsWith(new Rectangle(item.GetBrickView().GetLocation(), new Size(25, 25))))
                    {
                        if (_clientController.GetDirection() == "Down") _clientController.SetLocationY(_clientController.GetLocationY() - 1);
                        else if (_clientController.GetDirection() == "Up") _clientController.SetLocationY(_clientController.GetLocationY() + 1);
                        else if (_clientController.GetDirection() == "Right") _clientController.SetLocationX(_clientController.GetLocationX() - 1);
                        else if (_clientController.GetDirection() == "Left") _clientController.SetLocationX(_clientController.GetLocationX() + 1);
                        return false;
                    }
                }
            return true;
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
                                        _gameView.SetClientName(_clientController.GetName());
                                        _infoController.SetInfo(client.Tank);
                                    }
                                    else MessageBox.Show("Login error!");
                                }
                                else if (client.IsRegister)
                                {
                                    if (client.Login)
                                    {
                                        _gameView.ClientVisible();
                                        _gameView.SetClientName(_clientController.GetName());
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
                                    if (!_playersCheck.SequenceEqual(_players))
                                    {
                                        _playersCheck.Clear();
                                        _playersCheck.AddRange(_players);
                                        _gameView.PlayersClear();
                                        _gameView.PlayersAdd(_players.ToArray());
                                    }
                                    _roomController.LoadBricks(client.Bricks);
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
