namespace Project_66_Server.Model
{
    internal class RoomModel
    {
        public int Id { get; set; } 
        public List<string> Users { get; set; }
        public RoomModel(int id, List<string> users)
        {
            Id = id;
            Users = users;
        }
        public override string ToString()
        {
            return "Room " + Id;
        }
    }
}
