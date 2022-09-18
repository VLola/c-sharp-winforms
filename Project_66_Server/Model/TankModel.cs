namespace Project_66_Server.Model
{
    public class TankModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Health { get; set; } = 10;
        public int Power { get; set; }
        public int Defence { get; set; }
        public int Coins { get; set; }
        public int Murders { get; set; }
        public int Deaths { get; set; }
        public bool Killed { get; set; } 
        public string Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
