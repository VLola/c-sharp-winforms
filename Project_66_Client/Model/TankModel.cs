namespace Project_66_Client.Model
{
    public class TankModel
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
        private int _healthPoints = 100;
        public int HealthPoints
        {
            get { return _healthPoints; }
            set
            {
                _healthPoints = value;
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
    }
}
