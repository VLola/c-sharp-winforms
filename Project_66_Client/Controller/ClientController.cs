using Project_66_Client.Model;

namespace Project_66_Client.Controller
{
    internal class ClientController
    {
        Client _client = new Client();
        public ClientController()
        {
            _client.Tank = new();
        }
        public void SetDirection(string value) => _client.Tank.Direction = value;
        public void SetLocationX(int value) => _client.Tank.X = value;
        public void SetLocationY(int value) => _client.Tank.Y = value;
        public void SetName(string value) => _client.Tank.Name = value;
        public void SetPassword(string value) => _client.Password = value;
        public void SetPlayers(int value) => _client.Players = value;
        public void SetIsRegister(bool value) => _client.IsRegister = value;
        public void SetIsLogin(bool value) => _client.IsLogin = value;
        public void SetIsDirection(bool value) => _client.IsDirection = value;
        public void SetIsShot(bool value) => _client.IsShot = value;
        public void SetTankModel(TankModel value) => _client.Tank = value;
        public int GetLocationX() => _client.Tank.X;
        public int GetLocationY() => _client.Tank.Y;
        public string GetName() => _client.Tank.Name;
        public string GetDirection() => _client.Tank.Direction;
        public TankModel GetTankModel() => _client.Tank;
        public Client GetClient() => _client;
        public void SetClient(Client client)
        {
            _client.Players = client.Players;
            _client.Tank = client.Tank;
            _client.IsDirection = client.IsDirection;
            _client.IsLogin = client.IsLogin;
            _client.IsRegister = client.IsRegister;
            _client.Login = client.Login;
            _client.IsShot = client.IsShot;
        }
        public void Up()
        {
            _client.Tank.Direction = "Up";
            _client.Tank.Y = _client.Tank.Y - 1;
        }
        public void Down()
        {
            _client.Tank.Direction = "Down";
            _client.Tank.Y = _client.Tank.Y + 1;
        }
        public void Left()
        {
            _client.Tank.Direction = "Left";
            _client.Tank.X = _client.Tank.X - 1;
        }
        public void Right()
        {
            _client.Tank.Direction = "Right";
            _client.Tank.X = _client.Tank.X + 1;
        }
    }
}
