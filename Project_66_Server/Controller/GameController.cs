using Project_66_Server.View;
using Project_66_Server.Model;
using Project_66_Server.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Project_66_Server.Controller
{
    internal class GameController
    {
        int _bulletId = 1;
        int _room = 1;
        GameView _gameView;
        public GameController(GameView gameView) {
            _gameView = gameView;
            Listen();
        }
        void Listen()
        {
            Task.Run(() => {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8086);
                Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(10);
                while (true)
                {
                    ConnectClient(serverSocket.Accept());
                }
            });
        }
        void ConnectClient(Socket clientSocket)
        {
            Task.Run(() => {
                RoomModel roomModel = new RoomModel();
                while (true)
                {
                    try
                    {
                        int bytes = 0;
                        byte[] buffer = new byte[1024];
                        StringBuilder builder = new StringBuilder();
                        do
                        {
                            bytes = clientSocket.Receive(buffer);
                            builder.Append(Encoding.Unicode.GetString(buffer, 0, bytes));
                        } while (clientSocket.Available > 0);

                        if (builder.ToString() == "")
                        {
                            clientSocket.Shutdown(SocketShutdown.Both);
                            clientSocket.Close();
                            break;
                        }
                        else
                        {
                            Client client = JsonConvert.DeserializeObject<Client>(builder.ToString());
                            if (client.IsLogin && client.Tank.Name != null && client.Tank.Name != "" && client.Password != null && client.Password != "")
                            {
                                if (Connect.LoginUser(client.Tank.Name, client.Password))
                                {
                                    roomModel = GetRoom(client.Players);
                                    roomModel.Sockets.Add(clientSocket);
                                    if (roomModel.Tanks.Count == 0)
                                    {
                                        client.Tank.X = 100;
                                        client.Tank.Direction = "Down";
                                    }
                                    else if (roomModel.Tanks.Count == 1)
                                    {
                                        client.Tank.X = 600;
                                        client.Tank.Direction = "Down";
                                    }
                                    else if (roomModel.Tanks.Count == 2)
                                    {
                                        client.Tank.X = 100;
                                        client.Tank.Y = 400;
                                        client.Tank.Direction = "Up";
                                    }
                                    else if (roomModel.Tanks.Count == 3)
                                    {
                                        client.Tank.X = 600;
                                        client.Tank.Y = 400;
                                        client.Tank.Direction = "Up";
                                    }
                                    roomModel.Tanks.Add(client.Tank);
                                    client.Tanks = roomModel.Tanks;
                                    client.Bullets = roomModel.Bullets;
                                    client.Login = true;
                                    foreach (var item in roomModel.Sockets)
                                    {
                                        try
                                        {
                                            item.Send(Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(client)));
                                        }
                                        catch { }
                                    }
                                }
                                else
                                {
                                    client.Login = false;
                                    clientSocket.Send(Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(client)));
                                }
                            }
                            else if (client.IsRegister && client.Tank.Name != null && client.Tank.Name != "" && client.Password != null && client.Password != "")
                            {
                                if (Connect.RegistrationUser(client.Tank.Name, client.Password))
                                {
                                    roomModel = GetRoom(client.Players);
                                    roomModel.Sockets.Add(clientSocket);
                                    if (roomModel.Tanks.Count == 0) {
                                        client.Tank.X = 100;
                                        client.Tank.Direction = "Down";
                                    }
                                    else if (roomModel.Tanks.Count == 1)
                                    {
                                        client.Tank.X = 600;
                                        client.Tank.Direction = "Down";
                                    }
                                    else if (roomModel.Tanks.Count == 2)
                                    {
                                        client.Tank.X = 100;
                                        client.Tank.Y = 400;
                                        client.Tank.Direction = "Up";
                                    }
                                    else if (roomModel.Tanks.Count == 3)
                                    {
                                        client.Tank.X = 600;
                                        client.Tank.Y = 400;
                                        client.Tank.Direction = "Up";
                                    }
                                    roomModel.Tanks.Add(client.Tank);
                                    client.Tanks = roomModel.Tanks;
                                    client.Bullets = roomModel.Bullets;
                                    client.Login = true;
                                    foreach (var item in roomModel.Sockets)
                                    {
                                        try
                                        {
                                            item.Send(Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(client)));
                                        }
                                        catch { }
                                    }
                                }
                                else
                                {
                                    client.Login = false;
                                    clientSocket.Send(Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(client)));
                                }
                            }
                            else if (client.IsDirection)
                            {
                                int i = 0;
                                foreach (var item in roomModel.Tanks)
                                {
                                    if (item.Name == client.Tank.Name) break;
                                    i++;
                                }
                                roomModel.Tanks[i] = client.Tank;
                                roomModel.IsReload = true;
                            }
                            else if (client.IsShot)
                            {
                                client.Tanks = roomModel.Tanks;
                                BulletModel bulletModel = new BulletModel();
                                bulletModel.Id = _bulletId++;
                                bulletModel.Name = client.Tank.Name;
                                bulletModel.Direction = client.Tank.Direction;
                                if (bulletModel.Direction == "Right")
                                {
                                    bulletModel.X = client.Tank.X + 50;
                                    bulletModel.Y = client.Tank.Y + 23;
                                }
                                else if (bulletModel.Direction == "Left")
                                {
                                    bulletModel.X = client.Tank.X - 5;
                                    bulletModel.Y = client.Tank.Y + 23;
                                }
                                else if (bulletModel.Direction == "Up")
                                {
                                    bulletModel.X = client.Tank.X + 23;
                                    bulletModel.Y = client.Tank.Y - 5;
                                }
                                else if (bulletModel.Direction == "Down")
                                {
                                    bulletModel.X = client.Tank.X + 23;
                                    bulletModel.Y = client.Tank.Y + 50;
                                }
                                lock (roomModel.LockBullets) roomModel.Bullets.Add(bulletModel);
                                roomModel.IsReload = true;
                            }
                        }
                    }
                    catch {  }
                }
            });
        }

        private async void RunBullets(RoomModel roomModel)
        {
            await Task.Run(async() => {
                while (true)
                {
                    await Task.Delay(100);
                    if (roomModel.Bullets.Count > 0)
                    {
                        lock (roomModel.LockBullets)
                        {
                            int j = -1;
                            int i = 0;
                            foreach (var item in roomModel.Bullets)
                            {
                                if (item.Direction == "Down") item.Y = item.Y + 5;
                                else if (item.Direction == "Up") item.Y = item.Y - 5;
                                else if (item.Direction == "Right") item.X = item.X + 5;
                                else if (item.Direction == "Left") item.X = item.X - 5;
                                if (item.X < 0 || item.X > 800 || item.Y < 0 || item.Y > 450) j = i;
                                i++;
                            }
                            if (j >= 0) roomModel.Bullets.RemoveAt(j);
                            roomModel.IsReload = true;
                        }
                    }
                }
            });
        }
        private async void SendRoom(RoomModel roomModel)
        {
            await Task.Run(() =>
            {
                Client client = new Client();
                while (true)
                {
                    try
                    {
                        Task.Delay(10);
                        if (roomModel.IsReload)
                        {
                            foreach (var item in roomModel.Sockets)
                            {
                                try
                                {
                                    Task.Delay(10);
                                    client.Tanks = roomModel.Tanks;
                                    client.Bullets = roomModel.Bullets;
                                    item.Send(Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(client)));
                                    roomModel.IsReload = false;
                                }
                                catch { }
                            }
                        }
                    }
                    catch { }
                }
            });
        }
        private RoomModel GetRoom(int players)
        {
            foreach (RoomModel item in _gameView.Games.Items)
            {
                if (item.Players == players && item.Tanks.Count < players) return item;
            }
            RoomModel roomModel = new RoomModel();
            roomModel.Id = _room++;
            roomModel.Players = players;
            roomModel.LockBullets = new object();
            roomModel.Sockets = new List<Socket>();
            roomModel.Tanks = new List<TankModel>();
            roomModel.Bullets = new List<BulletModel>();
            RunBullets(roomModel);
            SendRoom(roomModel);
            _gameView.Invoke(new Action(() => { 
                _gameView.Games.Items.Add(roomModel);
            }));
            return roomModel;
        }
    }
}
