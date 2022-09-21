namespace Project_66_Server.Model
{
    public class TankModel: BulletModel
    {
        public int Health { get; set; } = 10;
        public int Defence { get; set; }
        public int Coins { get; set; }
        public int Murders { get; set; }
        public int Deaths { get; set; }
        public bool Killed { get; set; }
    }
}
