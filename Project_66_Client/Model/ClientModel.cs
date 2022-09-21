namespace Project_66_Client.Model
{
    internal class ClientModel
    {
        public bool IsLogin { get; set; }
        public bool Login { get; set; }
        public bool BuyPower { get; set; }
        public bool BuyDefence { get; set; }
        public bool IsStart { get; set; }
        public bool IsRegister { get; set; }
        public bool IsDirection { get; set; }
        public bool IsShot { get; set; }
        public int Players { get; set; }
        public string Password { get; set; }
        public TankModel Tank { get; set; }
        public List<TankModel> Tanks { get; set; }
        public List<BulletModel> Bullets { get; set; }
        public List<BrickModel> Bricks { get; set; }
    }
}
