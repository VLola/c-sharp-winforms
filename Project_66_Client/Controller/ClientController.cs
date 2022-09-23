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
        public void SetId(int value) { lock (_client) _client.Tank.Id = value; }
        public void SetTank(TankModel value) { lock (_client) _client.Tank = value; }
        public void SetBuyPower(bool value) { lock (_client) _client.BuyPower = value; }
        public void SetBuyDefence(bool value) { lock (_client) _client.BuyDefence = value; }
        public void SetLocationX(int value) { lock (_client) _client.Tank.X = value; }
        public void SetLocationY(int value) { lock (_client) _client.Tank.Y = value; }
        public void SetName(string value) { lock (_client) _client.Tank.Name = value; }
        public void SetPassword(string value) { lock (_client) _client.Password = value; }
        public void SetPlayers(int value) { lock (_client) _client.Players = value; }
        public void SetIsStart(bool value) { lock (_client) _client.IsStart = value; }
        public void SetIsRegister(bool value) { lock (_client) _client.IsRegister = value; }
        public void SetIsLogin(bool value) { lock (_client) _client.IsLogin = value; }
        public void SetIsDirection(bool value) { lock (_client) _client.IsDirection = value; }
        public void SetIsShot(bool value) { lock (_client) _client.IsShot = value; }
        public int GetLocationX() { lock (_client) return _client.Tank.X; }
        public int GetLocationY() { lock (_client) return _client.Tank.Y; }
        public string GetName() { lock (_client) return _client.Tank.Name; }
        public string GetDirection() { lock (_client) return _client.Tank.Direction; }
        public ClientModel GetClient() { lock (_client) return _client; }
        public void Up()
        {
            lock (_client) {
                _client.Tank.Direction = "Up";
                _client.Tank.Y = _client.Tank.Y - 1;
            }
        }
        public void Down()
        {
            lock (_client)
            {
                _client.Tank.Direction = "Down";
                _client.Tank.Y = _client.Tank.Y + 1;
            }
        }
        public void Left()
        {
            lock (_client)
            {
                _client.Tank.Direction = "Left";
                _client.Tank.X = _client.Tank.X - 1;
            }
        }
        public void Right()
        {
            lock (_client)
            {
                _client.Tank.Direction = "Right";
                _client.Tank.X = _client.Tank.X + 1;
            }
        }
    }
}
