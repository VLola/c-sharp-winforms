namespace Project_66_Client.Model
{
    internal class BulletModel
    {
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
            }
        }
        private string _direction;
        public string Direction
        {
            get { return _direction; }
            set
            {
                _direction = value;
            }
        }
        private int _x = 0;
        public int X
        {
            get { return _x; }
            set
            {
                _x = value;
            }
        }
        private int _y = 0;
        public int Y
        {
            get { return _y; }
            set
            {
                _y = value;
            }
        }
        private int _maxX;
        public int MaxX
        {
            get { return _maxX; }
            set
            {
                _maxX = value;
            }
        }
        private int _maxY;
        public int MaxY
        {
            get { return _maxY; }
            set
            {
                _maxY = value;
            }
        }
        public BulletModel() { }
        public BulletModel(TankModel tankModel, int maxX, int maxY)
        {
            FirstName = tankModel.Name;
            Direction = tankModel.Direction;
            X = tankModel.X;
            Y = tankModel.Y;
            MaxX = maxX;
            MaxY = maxY;
        }
    }
}
