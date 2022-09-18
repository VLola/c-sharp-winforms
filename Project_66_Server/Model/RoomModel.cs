using System.Collections.Generic;
using System.Net.Sockets;

namespace Project_66_Server.Model
{
    public class RoomModel
    {
        public int Id { get; set; }
        public int Players { get; set; }
        public bool IsReload { get; set; }
        public List<Socket> Sockets { get; set; }
        public List<TankModel> Tanks { get; set; }
        public List<BulletModel> Bullets { get; set; }
        public override string ToString()
        {
            return "Room " + Id;
        }
    }
}
