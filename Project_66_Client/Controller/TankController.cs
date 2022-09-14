using Project_66_Client.Model;
using Project_66_Client.View;

namespace Project_66_Client.Controller
{
    internal class TankController
    {
        TankView _tankView;
        TankModel _tankModel;
        public TankController(TankView tankView, TankModel tankModel)
        {
            _tankView = tankView;
            _tankModel = tankModel;
        }
        public void SetDirection(string direction)
        {
            _tankModel.Direction = direction;
        }
        public void SetFirstName(string name)
        {
            _tankModel.FirstName = name;
            _tankView.Name = name;
        }
        public void Up()
        {
            _tankModel.Direction = "Up";
            _tankModel.Y = _tankModel.Y - 1;
            NewLocation();
        }
        public void Down()
        {
            _tankModel.Direction = "Down";
            _tankModel.Y = _tankModel.Y + 1;
            NewLocation();
        }
        public void Left()
        {
            _tankModel.Direction = "Left";
            _tankModel.X = _tankModel.X - 1; 
            NewLocation();
        }
        public void Right()
        {
            _tankModel.Direction = "Right";
            _tankModel.X = _tankModel.X + 1;
            NewLocation();
        }
        public void NewLocation(int x, int y)
        {
            _tankModel.X = x;
            _tankModel.Y = y;
            _tankView.Location = new Point(x, y);
        }
        private void NewLocation()
        {
            _tankView.Location = new Point(_tankModel.X, _tankModel.Y);
        }
        public int GetLocationX()
        {
            return _tankModel.X;
        }
        public int GetLocationY()
        {
            return _tankModel.Y;
        }
        public string GetDirection()
        {
            return _tankModel.Direction;
        }
        public TankModel GetTankModel()
        {
            return _tankModel;
        }
        public void SetTankModel(TankModel tankModel)
        {
            _tankModel = tankModel;
            _tankView.Name = tankModel.FirstName;
            _tankView.Location = new Point(tankModel.X, tankModel.Y);
        }
    }
}
