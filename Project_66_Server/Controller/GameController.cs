using Project_66_Server.View;
using Project_66_Server.Model;
using Project_66_Server.DataBase;
using System;
using System.Collections.Generic;
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
                string NameTank = "";
                RoomModel roomModel = new RoomModel();
                while (true)
                {
                    try
                    {
                        int bytes = 0;
                        byte[] buffer = new byte[64000];
                        StringBuilder builder = new StringBuilder();
                        do
                        {
                            bytes = clientSocket.Receive(buffer);
                            builder.Append(Encoding.Unicode.GetString(buffer, 0, bytes));
                        } while (clientSocket.Available > 0);

                        if (builder.ToString() == "")
                        {
                            lock (roomModel.Tanks)
                            {
                                int i = 0;
                                foreach (var item in roomModel.Tanks)
                                {
                                    if (item.Name == NameTank) break;
                                    i++;
                                }
                                roomModel.Tanks.RemoveAt(i);
                                lock (roomModel.Sockets) roomModel.Sockets.Remove(clientSocket);
                                roomModel.IsReload = true;
                            }
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
                                    NameTank = client.Tank.Name;
                                    User user = Connect.GetUser(NameTank);
                                    client.Tank.Coins = user.Coins;
                                    client.Tank.Power = user.Power;
                                    client.Tank.Defence = user.Defence;
                                    client.Tank.Murders = user.Murders;
                                    client.Tank.Deaths = user.Deaths;
                                    client.Login = true;
                                    clientSocket.Send(Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(client)));
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
                                    NameTank = client.Tank.Name;
                                    client.Tank.Coins = 0;
                                    client.Tank.Power = 0;
                                    client.Tank.Defence = 0;
                                    client.Tank.Murders = 0;
                                    client.Tank.Deaths = 0;
                                    client.Login = true;
                                    clientSocket.Send(Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(client)));
                                }
                                else
                                {
                                    client.Login = false;
                                    clientSocket.Send(Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(client)));
                                }
                            }
                            else if (client.IsStart)
                            {
                                User user = Connect.GetUser(NameTank);
                                client.Tank.Coins = user.Coins;
                                client.Tank.Power = user.Power;
                                client.Tank.Defence = user.Defence;
                                client.Tank.Murders = user.Murders;
                                client.Tank.Deaths = user.Deaths;
                                roomModel = AddedTank(client, clientSocket);
                                clientSocket.Send(Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(client)));
                                roomModel.IsReload = true;
                            }
                            else if (client.IsDirection)
                            {
                                lock (roomModel.Tanks)
                                {
                                    int i = 0;
                                    foreach (var item in roomModel.Tanks)
                                    {
                                        if (item.Name == client.Tank.Name) break;
                                        i++;
                                    }
                                    if (!roomModel.Tanks[i].Killed)
                                    {
                                        roomModel.Tanks[i] = client.Tank;
                                        roomModel.IsReload = true;
                                    }
                                }
                            }
                            else if (client.IsShot)
                            {
                                lock (roomModel.Tanks)
                                {
                                    int i = 0;
                                    foreach (var item in roomModel.Tanks)
                                    {
                                        if (item.Name == client.Tank.Name) break;
                                        i++;
                                    }
                                    if (!roomModel.Tanks[i].Killed)
                                    {
                                        BulletModel bulletModel = new BulletModel();
                                        bulletModel.Id = _bulletId++;
                                        bulletModel.Name = client.Tank.Name;
                                        bulletModel.Direction = client.Tank.Direction;
                                        if (bulletModel.Direction == "Right")
                                        {
                                            bulletModel.X = client.Tank.X + 50;
                                            bulletModel.Y = client.Tank.Y + 22;
                                        }
                                        else if (bulletModel.Direction == "Left")
                                        {
                                            bulletModel.X = client.Tank.X - 5;
                                            bulletModel.Y = client.Tank.Y + 22;
                                        }
                                        else if (bulletModel.Direction == "Up")
                                        {
                                            bulletModel.X = client.Tank.X + 22;
                                            bulletModel.Y = client.Tank.Y - 5;
                                        }
                                        else if (bulletModel.Direction == "Down")
                                        {
                                            bulletModel.X = client.Tank.X + 22;
                                            bulletModel.Y = client.Tank.Y + 50;
                                        }
                                        lock (roomModel.Bullets) roomModel.Bullets.Add(bulletModel);
                                        roomModel.IsReload = true;
                                    }
                                }
                            }
                            else if (client.BuyDefence)
                            {
                                int coins = Connect.GetCoins(client.Tank.Name);
                                if (coins >= 5)
                                {
                                    User user = Connect.GetUser(NameTank);
                                    client.Tank.Coins = user.Coins - 5;
                                    client.Tank.Power = user.Power;
                                    client.Tank.Defence = user.Defence + 1;
                                    client.Tank.Murders = user.Murders;
                                    client.Tank.Deaths = user.Deaths;
                                    Connect.UpdateDefence(client.Tank.Name, client.Tank.Defence, client.Tank.Coins);
                                    clientSocket.Send(Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(client)));
                                }
                            }
                            else if (client.BuyPower)
                            {
                                int coins = Connect.GetCoins(client.Tank.Name);
                                if (coins >= 5)
                                {
                                    User user = Connect.GetUser(NameTank);
                                    client.Tank.Coins = user.Coins - 5;
                                    client.Tank.Power = user.Power + 1;
                                    client.Tank.Defence = user.Defence;
                                    client.Tank.Murders = user.Murders;
                                    client.Tank.Deaths = user.Deaths;
                                    Connect.UpdatePower(client.Tank.Name, client.Tank.Power, client.Tank.Coins);
                                    clientSocket.Send(Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(client)));
                                }
                            }
                        }
                    }
                    catch {  }
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
        private RoomModel AddedTank(Client client, Socket clientSocket)
        {
            RoomModel roomModel = GetRoom(client.Players);
            roomModel.Sockets.Add(clientSocket);
            lock (roomModel.Tanks)
            {
                client.Tank.Id = TankNewId(roomModel);
                if (client.Tank.Id == 1)
                {
                    client.Tank.X = 100;
                    client.Tank.Y = 0;
                    client.Tank.Direction = "Down";
                }
                else if (client.Tank.Id == 2)
                {
                    client.Tank.X = 100;
                    client.Tank.Y = 400;
                    client.Tank.Direction = "Up";
                }
                else if (client.Tank.Id == 3)
                {
                    client.Tank.X = 600;
                    client.Tank.Y = 0;
                    client.Tank.Direction = "Down";
                }
                else if (client.Tank.Id == 4)
                {
                    client.Tank.X = 600;
                    client.Tank.Y = 400;
                    client.Tank.Direction = "Up";
                }
                roomModel.Tanks.Add(client.Tank);
            }
            return roomModel;
        }
        private int TankNewId(RoomModel roomModel)
        {
            if (!CheckIdTank(roomModel, 1)) return 1;
            else if (!CheckIdTank(roomModel, 2)) return 2;
            else if (!CheckIdTank(roomModel, 3)) return 3;
            else return 4;
        }
        private bool CheckIdTank(RoomModel roomModel, int id)
        {
            foreach (var item in roomModel.Tanks)
            {
                if (id == item.Id) return true;
            }
            return false;
        }
        private async void Respawn(string name, RoomModel roomModel) {
            await Task.Run(async() =>
            {
                await Task.Delay(5000);
                lock (roomModel.Tanks)
                {
                    foreach (var item in roomModel.Tanks)
                    {
                        if (item.Name == name) {
                            item.Killed = false;
                            item.Health = 10;
                            if (item.Id == 1)
                            {
                                item.X = 100;
                                item.Y = 0;
                                item.Direction = "Down";
                            }
                            else if (item.Id == 2)
                            {
                                item.X = 100;
                                item.Y = 400;
                                item.Direction = "Up";
                            }
                            else if (item.Id == 3)
                            {
                                item.X = 600;
                                item.Y = 0;
                                item.Direction = "Down";
                            }
                            else if (item.Id == 4)
                            {
                                item.X = 600;
                                item.Y = 400;
                                item.Direction = "Up";
                            }
                        }
                    }
                    roomModel.IsReload = true;
                }
            });
        }
        private async void RunBullets(RoomModel roomModel)
        {
            await Task.Run(async() => {
                List<int> removeBullets = new List<int>();
                while (true)
                {
                    await Task.Delay(100);
                    if (roomModel.Bullets.Count > 0)
                    {
                        lock (roomModel.Bullets)
                        {
                            int i = 0;
                            foreach (var bullet in roomModel.Bullets)
                            {
                                if (bullet.Direction == "Down") bullet.Y = bullet.Y + 10;
                                else if (bullet.Direction == "Up") bullet.Y = bullet.Y - 10;
                                else if (bullet.Direction == "Right") bullet.X = bullet.X + 10;
                                else if (bullet.Direction == "Left") bullet.X = bullet.X - 10;
                                if (bullet.X < 0 || bullet.X > 800 || bullet.Y < 0 || bullet.Y > 450) removeBullets.Add(i);
                                foreach (var it in roomModel.Tanks)
                                {
                                    if (!it.Killed && bullet.Y > it.Y && bullet.X > it.X && bullet.Y < it.Y + 50 && bullet.X < it.X + 50) {
                                        if(!removeBullets.Contains(i)) removeBullets.Add(i);
                                        lock (roomModel.Tanks) {
                                            int power = 0;
                                            foreach (var tank in roomModel.Tanks)
                                            {
                                                if (tank.Name == bullet.Name)
                                                {
                                                    power = tank.Power;
                                                    break;
                                                }
                                            }
                                            int damage = it.Defence - 1 - power;
                                            if(damage < 0)
                                            {
                                                it.Health += damage;
                                                if (it.Health <= 0)
                                                {
                                                    it.Killed = true;
                                                    it.Deaths++;
                                                    Connect.UpdateDeaths(it.Name, it.Deaths);
                                                    Respawn(it.Name, roomModel);
                                                    foreach (var tank in roomModel.Tanks)
                                                    {
                                                        if (tank.Name == bullet.Name)
                                                        {
                                                            tank.Murders++;
                                                            tank.Coins += 10;
                                                            Connect.UpdateMurders(tank.Name, tank.Murders, tank.Coins);
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                            
                                            
                                        }
                                    }
                                }
                                i++;
                            }
                            removeBullets.Reverse();
                            foreach (var item in removeBullets)
                            {
                                roomModel.Bullets.RemoveAt(item);
                            }
                            removeBullets.Clear();
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
                            lock (roomModel.Sockets) foreach (var item in roomModel.Sockets)
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
    }
}
