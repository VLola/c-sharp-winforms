using Project_66_Client.Model;

namespace Project_66_Client.Controller
{
    internal class ClientController
    {
        ClientModel _client = new ClientModel();
        public ClientController()
        {
            _client.Tank = new();
        }
        public void SetId(int value) => _client.Tank.Id = value;
        public void SetTank(TankModel value) => _client.Tank = value;
        public void SetBuyPower(bool value) => _client.BuyPower = value;
        public void SetBuyDefence(bool value) => _client.BuyDefence = value;
        public void SetDirection(string value) => _client.Tank.Direction = value;
        public void SetLocationX(int value) => _client.Tank.X = value;
        public void SetLocationY(int value) => _client.Tank.Y = value;
        public void SetName(string value) => _client.Tank.Name = value;
        public void SetPassword(string value) => _client.Password = value;
        public void SetPlayers(int value) => _client.Players = value;
        public void SetIsStart(bool value) => _client.IsStart = value;
        public void SetIsRegister(bool value) => _client.IsRegister = value;
        public void SetIsLogin(bool value) => _client.IsLogin = value;
        public void SetIsDirection(bool value) => _client.IsDirection = value;
        public void SetIsShot(bool value) => _client.IsShot = value;
        public void SetTankModel(TankModel value) => _client.Tank = value;
        public int GetLocationX() => _client.Tank.X;
        public int GetLocationY() => _client.Tank.Y;
        public string GetName() => _client.Tank.Name;
        public string GetDirection() => _client.Tank.Direction;
        public ClientModel GetClient() => _client;
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
