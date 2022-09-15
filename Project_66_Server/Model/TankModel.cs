namespace Project_66_Server.Model
{
    public class TankModel
    {
        public string Name { get; set; }
        public string Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
