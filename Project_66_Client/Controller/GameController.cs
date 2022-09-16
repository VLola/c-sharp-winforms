using Project_66_Client.Model;
using Project_66_Client.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Text.Json;

namespace Project_66_Client.Controller
{
    public class GameController
    {
        GameView _gameView;
        ClientController _clientController = new();
        RoomController _roomController;
        Socket _socket;
        public GameController(GameView gameView)
        {
            try
            {
                _gameView = gameView;
                _gameView.LoginVisible();
                _roomController = new(_gameView.RoomView);
                _gameView.RoomView.PreviewKeyDown += RoomView_PreviewKeyDown;
                _gameView.LoginView.Login.Click += Login_Click;
                Load();
                Listen();
            }
            catch (Exception ex)
            {
                MessageBox.Show("3");
            }
        }
        private void Connect()
        {
            try
            {
                _clientController.SetName(_gameView.LoginView.FirstName.Text);
                _gameView.Invoke(new Action(() => {
                    _gameView.LoginView.FirstName.Text = "";
                }));
                _clientController.SetPassword(_gameView.LoginView.Password.Text);
                _gameView.Invoke(new Action(() => {
                    _gameView.LoginView.Password.Text = "";
                }));
                _clientController.SetPlayers((int)_gameView.LoginView.Players.SelectedItem);
                if (_gameView.LoginView.IsRegister.Checked == true)
                {
                    _clientController.SetIsRegister(true);
                    SendClient();
                    _clientController.SetIsLogin(false);
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
                MessageBox.Show("4");
            }
        }
        private void Disconnect()
        {
            //ClientModel.IsLoginClient = false;
            //Close();
            //Reload();
            //Load();
            //Listen();
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
                MessageBox.Show("5");
            }
        }
        private void Login_Click(object? sender, EventArgs e)
        {
            Connect();
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
                if (_clientController.GetLocationY() < _gameView.Height - 50)
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
                if (_clientController.GetLocationX() < _gameView.Width - 50)
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
                            Client? client = JsonSerializer.Deserialize<Client>(builder.ToString());
                            if (client != null)
                            {
                                if (client.IsLogin)
                                {
                                    if (client.Login)
                                    {
                                        _gameView.LoginHidden();
                                        foreach (var item in client.Tanks)
                                        {
                                            if(item.Name == _clientController.GetName())
                                            {
                                                _clientController.SetDirection(item.Direction);
                                                _clientController.SetLocationX(item.X);
                                                _clientController.SetLocationY(item.Y);
                                            }
                                        }
                                    }
                                    else MessageBox.Show("Login error!");
                                }
                                else if (client.IsRegister)
                                {
                                    if (client.Login)
                                    {
                                        _gameView.LoginHidden(); 
                                        foreach (var item in client.Tanks)
                                        {
                                            if (item.Name == _clientController.GetName())
                                            {
                                                _clientController.SetDirection(item.Direction);
                                                _clientController.SetLocationX(item.X);
                                                _clientController.SetLocationY(item.Y);
                                            }
                                        }
                                    }
                                    else MessageBox.Show("Register error!");
                                }
                                Task.Run(() =>
                                {
                                    _roomController.LoadTanks(client.Tanks);
                                    _roomController.LoadBullets(client.Bullets);
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            });
        }


    }
}
