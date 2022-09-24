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
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Project_66_Server.Controller
{
    internal class GameController
    {
        int _bulletId = 1;
        int _room = 1;
        GameView _gameView;
        List<int> _deleteBricks = new List<int>();
        List<int> _deleteBullets = new List<int>();
        List<RoomModel> _rooms = new List<RoomModel>();
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
                    try
                    {
                        ConnectClient(serverSocket.Accept());
                    } 
                    catch { }
                }
            });
        }
        int count = 0;
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
                        byte[] buffer = new byte[1024];
                        StringBuilder builder = new StringBuilder();
                        do
                        {
                            bytes = clientSocket.Receive(buffer);
                        } while (clientSocket.Available > 0);
                        builder.Append(Encoding.Unicode.GetString(buffer, 0, bytes));
                        _gameView.SendPacket(count++);

                        if (builder.ToString() == "")
                        {
                            lock (roomModel)
                            {
                                int i = 0;
                                foreach (var item in roomModel.Tanks)
                                {
                                    if (item.Name == NameTank) break;
                                    i++;
                                }
                                roomModel.Tanks.RemoveAt(i);
                                roomModel.Sockets.Remove(clientSocket);
                                roomModel.IsReload = true;
                            }
                            clientSocket.Shutdown(SocketShutdown.Both);
                            clientSocket.Close();
                            break;
                        }
                        else
                        {
                            if (Regex.Matches(builder.ToString(), "IsLogin").Count == 1)
                            {
                                Client client = JsonConvert.DeserializeObject<Client>(builder.ToString());

                                if (client != null)
                                {
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
                                        Run(roomModel, client);
                                    }
                                    else if (client.IsShot)
                                    {
                                        lock (roomModel)
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
                                                bulletModel.Power = client.Tank.Power;
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
                                                roomModel.Bullets.Add(bulletModel);
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
                        }
                    }
                    catch(Exception ex) { MessageBox.Show(ex.ToString());  }
                }
            });
        }
        private async void Run(RoomModel roomModel, Client client)
        {
            await Task.Run(()=>{
                try
                {
                    lock (roomModel)
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
                } catch { }
            });
        }
        private RoomModel GetRoom(int players)
        {
            lock (_rooms) foreach (RoomModel item in _rooms)
            {
                if (item.Players == players && item.Tanks.Count < players) return item;
            }
            RoomModel roomModel = new RoomModel();
            roomModel.Id = _room++;
            roomModel.Players = players;
            roomModel.Sockets = new List<Socket>();
            roomModel.Tanks = new List<TankModel>();
            roomModel.Bullets = new List<BulletModel>();
            roomModel.Bricks = GetBricks();
            RunBullets(roomModel);
            SendRoom(roomModel);
            lock(_rooms)_rooms.Add(roomModel);
            _gameView.AddRoom(roomModel);
            return roomModel;
        }
        private RoomModel AddedTank(Client client, Socket clientSocket)
        {
            RoomModel roomModel = GetRoom(client.Players);
            roomModel.Sockets.Add(clientSocket);
            lock (roomModel)
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
                    client.Tank.X = 650;
                    client.Tank.Y = 0;
                    client.Tank.Direction = "Down";
                }
                else if (client.Tank.Id == 4)
                {
                    client.Tank.X = 650;
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
                lock (roomModel)
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
                                item.X = 650;
                                item.Y = 0;
                                item.Direction = "Down";
                            }
                            else if (item.Id == 4)
                            {
                                item.X = 650;
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
                while (true)
                {
                    try
                    {
                        await Task.Delay(100);
                        if (roomModel.Bullets.Count > 0)
                        {
                            lock (roomModel)
                            {
                                int i = 0;
                                foreach (var bullet in roomModel.Bullets)
                                {
                                    if (bullet.Direction == "Down") bullet.Y = bullet.Y + 10;
                                    else if (bullet.Direction == "Up") bullet.Y = bullet.Y - 10;
                                    else if (bullet.Direction == "Right") bullet.X = bullet.X + 10;
                                    else if (bullet.Direction == "Left") bullet.X = bullet.X - 10;
                                    if (bullet.X < 0 || bullet.X > 800 || bullet.Y < 0 || bullet.Y > 450) _deleteBullets.Add(i);
                                    bool check = true;
                                    int j = 0;
                                    foreach (var brick in roomModel.Bricks)
                                    {
                                        if (bullet.Y >= brick.Y && bullet.X >= brick.X && bullet.Y <= brick.Y + 25 && bullet.X <= brick.X + 25 || bullet.Y + 5 >= brick.Y && bullet.X + 5 >= brick.X && bullet.Y + 5 <= brick.Y + 25 && bullet.X + 5 <= brick.X + 25)
                                        {
                                            check = false;
                                            _deleteBricks.Add(j);
                                        }
                                        j++;
                                    }
                                    if (!check)
                                    {
                                        if (!_deleteBullets.Contains(i)) _deleteBullets.Add(i);
                                        _deleteBricks.Reverse();
                                        foreach (var item in _deleteBricks)
                                        {
                                            roomModel.Bricks.RemoveAt(item);
                                        }

                                        _deleteBricks.Clear();
                                    }
                                    if (check)
                                    {
                                        foreach (var it in roomModel.Tanks)
                                        {
                                            if (!it.Killed && bullet.Y > it.Y && bullet.X > it.X && bullet.Y < it.Y + 50 && bullet.X < it.X + 50)
                                            {
                                                if (!_deleteBullets.Contains(i)) _deleteBullets.Add(i);
                                                int damage = it.Defence - 1 - bullet.Power;
                                                if (damage < 0)
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
                                _deleteBullets.Reverse();
                                foreach (var item in _deleteBullets)
                                {
                                    roomModel.Bullets.RemoveAt(item);
                                }
                                _deleteBullets.Clear();
                                roomModel.IsReload = true;
                            }
                        }
                    }
                    catch { }
                }
            });
        }
        private async void SendRoom(RoomModel roomModel)
        {
            await Task.Run(() =>
            {
                Client client = new Client();
                client.Tanks = roomModel.Tanks;
                client.Bullets = roomModel.Bullets;
                client.Bricks = roomModel.Bricks;
                while (true)
                {
                    try
                    {
                        Task.Delay(100);
                        if (roomModel.IsReload)
                        {
                            lock (roomModel) foreach (var item in roomModel.Sockets)
                            {
                                try
                                {
                                    Task.Delay(10);
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

        private List<BrickModel> GetBricks()
        {
            List<BrickModel> list = new List<BrickModel>();
            int id = 1;
            // -----------------------------------------------------------
            list.Add(new BrickModel() { Id = id++, X = 50, Y = 50 });
            list.Add(new BrickModel() { Id = id++, X = 50, Y = 75 });
            list.Add(new BrickModel() { Id = id++, X = 50, Y = 100 });
            list.Add(new BrickModel() { Id = id++, X = 50, Y = 125 });
            list.Add(new BrickModel() { Id = id++, X = 75, Y = 50 });
            list.Add(new BrickModel() { Id = id++, X = 75, Y = 75 });
            list.Add(new BrickModel() { Id = id++, X = 75, Y = 100 });
            list.Add(new BrickModel() { Id = id++, X = 75, Y = 125 });

            list.Add(new BrickModel() { Id = id++, X = 50, Y = 300 });
            list.Add(new BrickModel() { Id = id++, X = 50, Y = 325 });
            list.Add(new BrickModel() { Id = id++, X = 50, Y = 350 });
            list.Add(new BrickModel() { Id = id++, X = 50, Y = 375 });
            list.Add(new BrickModel() { Id = id++, X = 75, Y = 300 });
            list.Add(new BrickModel() { Id = id++, X = 75, Y = 325 });
            list.Add(new BrickModel() { Id = id++, X = 75, Y = 350 });
            list.Add(new BrickModel() { Id = id++, X = 75, Y = 375 });
            // -----------------------------------------------------------
            list.Add(new BrickModel() { Id = id++, X = 150, Y = 50 });
            list.Add(new BrickModel() { Id = id++, X = 150, Y = 75 });
            list.Add(new BrickModel() { Id = id++, X = 150, Y = 100 });
            list.Add(new BrickModel() { Id = id++, X = 150, Y = 125 });
            list.Add(new BrickModel() { Id = id++, X = 175, Y = 50 });
            list.Add(new BrickModel() { Id = id++, X = 175, Y = 75 });
            list.Add(new BrickModel() { Id = id++, X = 175, Y = 100 });
            list.Add(new BrickModel() { Id = id++, X = 175, Y = 125 });

            list.Add(new BrickModel() { Id = id++, X = 150, Y = 300 });
            list.Add(new BrickModel() { Id = id++, X = 150, Y = 325 });
            list.Add(new BrickModel() { Id = id++, X = 150, Y = 350 });
            list.Add(new BrickModel() { Id = id++, X = 150, Y = 375 });
            list.Add(new BrickModel() { Id = id++, X = 175, Y = 300 });
            list.Add(new BrickModel() { Id = id++, X = 175, Y = 325 });
            list.Add(new BrickModel() { Id = id++, X = 175, Y = 350 });
            list.Add(new BrickModel() { Id = id++, X = 175, Y = 375 });
            // -----------------------------------------------------------
            list.Add(new BrickModel() { Id = id++, X = 250, Y = 50 });
            list.Add(new BrickModel() { Id = id++, X = 250, Y = 75 });
            list.Add(new BrickModel() { Id = id++, X = 250, Y = 100 });
            list.Add(new BrickModel() { Id = id++, X = 250, Y = 125 });
            list.Add(new BrickModel() { Id = id++, X = 275, Y = 50 });
            list.Add(new BrickModel() { Id = id++, X = 275, Y = 75 });
            list.Add(new BrickModel() { Id = id++, X = 275, Y = 100 });
            list.Add(new BrickModel() { Id = id++, X = 275, Y = 125 });

            list.Add(new BrickModel() { Id = id++, X = 250, Y = 300 });
            list.Add(new BrickModel() { Id = id++, X = 250, Y = 325 });
            list.Add(new BrickModel() { Id = id++, X = 250, Y = 350 });
            list.Add(new BrickModel() { Id = id++, X = 250, Y = 375 });
            list.Add(new BrickModel() { Id = id++, X = 275, Y = 300 });
            list.Add(new BrickModel() { Id = id++, X = 275, Y = 325 });
            list.Add(new BrickModel() { Id = id++, X = 275, Y = 350 });
            list.Add(new BrickModel() { Id = id++, X = 275, Y = 375 });
            // -----------------------------------------------------------
            list.Add(new BrickModel() { Id = id++, X = 250, Y = 50 });
            list.Add(new BrickModel() { Id = id++, X = 250, Y = 75 });
            list.Add(new BrickModel() { Id = id++, X = 250, Y = 100 });
            list.Add(new BrickModel() { Id = id++, X = 250, Y = 125 });
            list.Add(new BrickModel() { Id = id++, X = 275, Y = 50 });
            list.Add(new BrickModel() { Id = id++, X = 275, Y = 75 });
            list.Add(new BrickModel() { Id = id++, X = 275, Y = 100 });
            list.Add(new BrickModel() { Id = id++, X = 275, Y = 125 });

            list.Add(new BrickModel() { Id = id++, X = 250, Y = 300 });
            list.Add(new BrickModel() { Id = id++, X = 250, Y = 325 });
            list.Add(new BrickModel() { Id = id++, X = 250, Y = 350 });
            list.Add(new BrickModel() { Id = id++, X = 250, Y = 375 });
            list.Add(new BrickModel() { Id = id++, X = 275, Y = 300 });
            list.Add(new BrickModel() { Id = id++, X = 275, Y = 325 });
            list.Add(new BrickModel() { Id = id++, X = 275, Y = 350 });
            list.Add(new BrickModel() { Id = id++, X = 275, Y = 375 });
            // -----------------------------------------------------------
            list.Add(new BrickModel() { Id = id++, X = 350, Y = 50 });
            list.Add(new BrickModel() { Id = id++, X = 350, Y = 75 });
            list.Add(new BrickModel() { Id = id++, X = 350, Y = 100 });
            list.Add(new BrickModel() { Id = id++, X = 350, Y = 125 });
            list.Add(new BrickModel() { Id = id++, X = 375, Y = 50 });
            list.Add(new BrickModel() { Id = id++, X = 375, Y = 75 });
            list.Add(new BrickModel() { Id = id++, X = 375, Y = 100 });
            list.Add(new BrickModel() { Id = id++, X = 375, Y = 125 });

            list.Add(new BrickModel() { Id = id++, X = 350, Y = 300 });
            list.Add(new BrickModel() { Id = id++, X = 350, Y = 325 });
            list.Add(new BrickModel() { Id = id++, X = 350, Y = 350 });
            list.Add(new BrickModel() { Id = id++, X = 350, Y = 375 });
            list.Add(new BrickModel() { Id = id++, X = 375, Y = 300 });
            list.Add(new BrickModel() { Id = id++, X = 375, Y = 325 });
            list.Add(new BrickModel() { Id = id++, X = 375, Y = 350 });
            list.Add(new BrickModel() { Id = id++, X = 375, Y = 375 });
            // -----------------------------------------------------------
            list.Add(new BrickModel() { Id = id++, X = 400, Y = 50 });
            list.Add(new BrickModel() { Id = id++, X = 400, Y = 75 });
            list.Add(new BrickModel() { Id = id++, X = 400, Y = 100 });
            list.Add(new BrickModel() { Id = id++, X = 400, Y = 125 });
            list.Add(new BrickModel() { Id = id++, X = 425, Y = 50 });
            list.Add(new BrickModel() { Id = id++, X = 425, Y = 75 });
            list.Add(new BrickModel() { Id = id++, X = 425, Y = 100 });
            list.Add(new BrickModel() { Id = id++, X = 425, Y = 125 });

            list.Add(new BrickModel() { Id = id++, X = 400, Y = 300 });
            list.Add(new BrickModel() { Id = id++, X = 400, Y = 325 });
            list.Add(new BrickModel() { Id = id++, X = 400, Y = 350 });
            list.Add(new BrickModel() { Id = id++, X = 400, Y = 375 });
            list.Add(new BrickModel() { Id = id++, X = 425, Y = 300 });
            list.Add(new BrickModel() { Id = id++, X = 425, Y = 325 });
            list.Add(new BrickModel() { Id = id++, X = 425, Y = 350 });
            list.Add(new BrickModel() { Id = id++, X = 425, Y = 375 });
            // -----------------------------------------------------------
            list.Add(new BrickModel() { Id = id++, X = 500, Y = 50 });
            list.Add(new BrickModel() { Id = id++, X = 500, Y = 75 });
            list.Add(new BrickModel() { Id = id++, X = 500, Y = 100 });
            list.Add(new BrickModel() { Id = id++, X = 500, Y = 125 });
            list.Add(new BrickModel() { Id = id++, X = 525, Y = 50 });
            list.Add(new BrickModel() { Id = id++, X = 525, Y = 75 });
            list.Add(new BrickModel() { Id = id++, X = 525, Y = 100 });
            list.Add(new BrickModel() { Id = id++, X = 525, Y = 125 });

            list.Add(new BrickModel() { Id = id++, X = 500, Y = 300 });
            list.Add(new BrickModel() { Id = id++, X = 500, Y = 325 });
            list.Add(new BrickModel() { Id = id++, X = 500, Y = 350 });
            list.Add(new BrickModel() { Id = id++, X = 500, Y = 375 });
            list.Add(new BrickModel() { Id = id++, X = 525, Y = 300 });
            list.Add(new BrickModel() { Id = id++, X = 525, Y = 325 });
            list.Add(new BrickModel() { Id = id++, X = 525, Y = 350 });
            list.Add(new BrickModel() { Id = id++, X = 525, Y = 375 });
            // -----------------------------------------------------------
            list.Add(new BrickModel() { Id = id++, X = 600, Y = 50 });
            list.Add(new BrickModel() { Id = id++, X = 600, Y = 75 });
            list.Add(new BrickModel() { Id = id++, X = 600, Y = 100 });
            list.Add(new BrickModel() { Id = id++, X = 600, Y = 125 });
            list.Add(new BrickModel() { Id = id++, X = 625, Y = 50 });
            list.Add(new BrickModel() { Id = id++, X = 625, Y = 75 });
            list.Add(new BrickModel() { Id = id++, X = 625, Y = 100 });
            list.Add(new BrickModel() { Id = id++, X = 625, Y = 125 });

            list.Add(new BrickModel() { Id = id++, X = 600, Y = 300 });
            list.Add(new BrickModel() { Id = id++, X = 600, Y = 325 });
            list.Add(new BrickModel() { Id = id++, X = 600, Y = 350 });
            list.Add(new BrickModel() { Id = id++, X = 600, Y = 375 });
            list.Add(new BrickModel() { Id = id++, X = 625, Y = 300 });
            list.Add(new BrickModel() { Id = id++, X = 625, Y = 325 });
            list.Add(new BrickModel() { Id = id++, X = 625, Y = 350 });
            list.Add(new BrickModel() { Id = id++, X = 625, Y = 375 });
            // -----------------------------------------------------------
            list.Add(new BrickModel() { Id = id++, X = 700, Y = 50 });
            list.Add(new BrickModel() { Id = id++, X = 700, Y = 75 });
            list.Add(new BrickModel() { Id = id++, X = 700, Y = 100 });
            list.Add(new BrickModel() { Id = id++, X = 700, Y = 125 });
            list.Add(new BrickModel() { Id = id++, X = 725, Y = 50 });
            list.Add(new BrickModel() { Id = id++, X = 725, Y = 75 });
            list.Add(new BrickModel() { Id = id++, X = 725, Y = 100 });
            list.Add(new BrickModel() { Id = id++, X = 725, Y = 125 });

            list.Add(new BrickModel() { Id = id++, X = 700, Y = 300 });
            list.Add(new BrickModel() { Id = id++, X = 700, Y = 325 });
            list.Add(new BrickModel() { Id = id++, X = 700, Y = 350 });
            list.Add(new BrickModel() { Id = id++, X = 700, Y = 375 });
            list.Add(new BrickModel() { Id = id++, X = 725, Y = 300 });
            list.Add(new BrickModel() { Id = id++, X = 725, Y = 325 });
            list.Add(new BrickModel() { Id = id++, X = 725, Y = 350 });
            list.Add(new BrickModel() { Id = id++, X = 725, Y = 375 });
            // -----------------------------------------------------------
            list.Add(new BrickModel() { Id = id++, X = 0, Y = 200 });
            list.Add(new BrickModel() { Id = id++, X = 0, Y = 225 });
            list.Add(new BrickModel() { Id = id++, X = 25, Y = 200 });
            list.Add(new BrickModel() { Id = id++, X = 25, Y = 225 });

            list.Add(new BrickModel() { Id = id++, X = 100, Y = 200 });
            list.Add(new BrickModel() { Id = id++, X = 100, Y = 225 });
            list.Add(new BrickModel() { Id = id++, X = 125, Y = 200 });
            list.Add(new BrickModel() { Id = id++, X = 125, Y = 225 });

            list.Add(new BrickModel() { Id = id++, X = 200, Y = 200 });
            list.Add(new BrickModel() { Id = id++, X = 200, Y = 225 });
            list.Add(new BrickModel() { Id = id++, X = 225, Y = 200 });
            list.Add(new BrickModel() { Id = id++, X = 225, Y = 225 });

            list.Add(new BrickModel() { Id = id++, X = 300, Y = 200 });
            list.Add(new BrickModel() { Id = id++, X = 300, Y = 225 });
            list.Add(new BrickModel() { Id = id++, X = 325, Y = 200 });
            list.Add(new BrickModel() { Id = id++, X = 325, Y = 225 });

            list.Add(new BrickModel() { Id = id++, X = 450, Y = 200 });
            list.Add(new BrickModel() { Id = id++, X = 450, Y = 225 });
            list.Add(new BrickModel() { Id = id++, X = 475, Y = 200 });
            list.Add(new BrickModel() { Id = id++, X = 475, Y = 225 });

            list.Add(new BrickModel() { Id = id++, X = 550, Y = 200 });
            list.Add(new BrickModel() { Id = id++, X = 550, Y = 225 });
            list.Add(new BrickModel() { Id = id++, X = 575, Y = 200 });
            list.Add(new BrickModel() { Id = id++, X = 575, Y = 225 });

            list.Add(new BrickModel() { Id = id++, X = 650, Y = 200 });
            list.Add(new BrickModel() { Id = id++, X = 650, Y = 225 });
            list.Add(new BrickModel() { Id = id++, X = 675, Y = 200 });
            list.Add(new BrickModel() { Id = id++, X = 675, Y = 225 });

            list.Add(new BrickModel() { Id = id++, X = 750, Y = 200 });
            list.Add(new BrickModel() { Id = id++, X = 750, Y = 225 });
            list.Add(new BrickModel() { Id = id++, X = 775, Y = 200 });
            list.Add(new BrickModel() { Id = id++, X = 775, Y = 225 });
            // -----------------------------------------------------------
            list.Add(new BrickModel() { Id = id++, X = 200, Y = 0 });
            list.Add(new BrickModel() { Id = id++, X = 200, Y = 25 });
            list.Add(new BrickModel() { Id = id++, X = 225, Y = 0 });
            list.Add(new BrickModel() { Id = id++, X = 225, Y = 25 });

            list.Add(new BrickModel() { Id = id++, X = 300, Y = 0 });
            list.Add(new BrickModel() { Id = id++, X = 300, Y = 25 });
            list.Add(new BrickModel() { Id = id++, X = 325, Y = 0 });
            list.Add(new BrickModel() { Id = id++, X = 325, Y = 25 });

            list.Add(new BrickModel() { Id = id++, X = 450, Y = 0 });
            list.Add(new BrickModel() { Id = id++, X = 450, Y = 25 });
            list.Add(new BrickModel() { Id = id++, X = 475, Y = 0 });
            list.Add(new BrickModel() { Id = id++, X = 475, Y = 25 });

            list.Add(new BrickModel() { Id = id++, X = 550, Y = 0 });
            list.Add(new BrickModel() { Id = id++, X = 550, Y = 25 });
            list.Add(new BrickModel() { Id = id++, X = 575, Y = 0 });
            list.Add(new BrickModel() { Id = id++, X = 575, Y = 25 });
            // -----------------------------------------------------------
            list.Add(new BrickModel() { Id = id++, X = 200, Y = 400 });
            list.Add(new BrickModel() { Id = id++, X = 200, Y = 425 });
            list.Add(new BrickModel() { Id = id++, X = 225, Y = 400 });
            list.Add(new BrickModel() { Id = id++, X = 225, Y = 425 });

            list.Add(new BrickModel() { Id = id++, X = 300, Y = 400 });
            list.Add(new BrickModel() { Id = id++, X = 300, Y = 425 });
            list.Add(new BrickModel() { Id = id++, X = 325, Y = 400 });
            list.Add(new BrickModel() { Id = id++, X = 325, Y = 425 });

            list.Add(new BrickModel() { Id = id++, X = 450, Y = 400 });
            list.Add(new BrickModel() { Id = id++, X = 450, Y = 425 });
            list.Add(new BrickModel() { Id = id++, X = 475, Y = 400 });
            list.Add(new BrickModel() { Id = id++, X = 475, Y = 425 });

            list.Add(new BrickModel() { Id = id++, X = 550, Y = 400 });
            list.Add(new BrickModel() { Id = id++, X = 550, Y = 425 });
            list.Add(new BrickModel() { Id = id++, X = 575, Y = 400 });
            list.Add(new BrickModel() { Id = id++, X = 575, Y = 425 });
            return list;
        }
    }
}
