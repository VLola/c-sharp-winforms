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


namespace Project_66_Server.Controller
{
    internal class GameController
    {
        int _room = 0;
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
                                    roomModel.Tanks.Add(client.Tank);
                                    client.Tanks = roomModel.Tanks;
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
                                    roomModel = GetRoom(client.Players);
                                    roomModel.Sockets.Add(clientSocket);
                                    roomModel.Tanks.Add(client.Tank);
                                    client.Tanks = roomModel.Tanks;
                                    client.Login = true;
                                    clientSocket.Send(Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(client)));
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
                                client.Tanks = roomModel.Tanks;

                                foreach (var item in roomModel.Sockets)
                                {
                                    item.Send(Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(client)));
                                }
                            }
                        }
                    }
                    catch {  }
                }
            });
        }
        private int GetPlayerId(string name)
        {
            foreach (RoomModel item in _gameView.Games.Items)
            {
                int i = 0;
                foreach (var it in item.Tanks)
                {
                    if (it.Name == name) return i;
                    i++;
                }
            }
            return 0;
        }
        private RoomModel GetRoom(int size)
        {
            foreach (RoomModel item in _gameView.Games.Items)
            {
                if (item.Size == size) return item;
            }
            RoomModel roomModel = new RoomModel();
            roomModel.Id = _room++;
            roomModel.Size = size;
            roomModel.Sockets = new List<Socket>();
            roomModel.Tanks = new List<TankModel>();
            roomModel.Bullets = new List<TankModel>();
            _gameView.Invoke(new Action(() => { 
                _gameView.Games.Items.Add(roomModel);
            }));
            return roomModel;
        }
    }
}
