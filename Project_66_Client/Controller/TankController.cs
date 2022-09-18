using Project_66_Client.Model;
using Project_66_Client.View;

namespace Project_66_Client.Controller
{
    public class TankController
    {
        private int _power { get; set; }
        private int _health { get; set; }
        private string _direction { get; set; }
        private bool _killed { get; set; }
        RoomView _roomView;
        public TankView _tankView;
        public string Name { get; set; }
        HealthController _healthController;
        public TankController(RoomView roomView, TankView tankView, TankModel tankModel)
        {
            Name = tankModel.Name;
            _roomView = roomView;
            _tankView = tankView;
            HealthView healthView = new();
            _healthController = new(healthView);
            _tankView.Controls.Add(healthView);
            try
            {
                if (_roomView.InvokeRequired)
                {
                    _roomView.Invoke(new Action(() =>
                    {
                        _roomView.Controls.Add(_tankView);
                    }));
                }
                else
                {
                    _roomView.Controls.Add(_tankView);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            Load(tankModel);
        }
        public void DisposeTank()
        {
            try
            {
                if (_roomView.InvokeRequired)
                {
                    _roomView.Invoke(new Action(() =>
                    {
                        _roomView.Controls.Remove(_tankView);
                    }));
                }
                else
                {
                    _roomView.Controls.Remove(_tankView);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void Load(TankModel tankModel)
        {
            try
            {
                if (_tankView.InvokeRequired)
                {
                    _tankView.Invoke(new Action(() =>
                    {
                        if (tankModel.Direction == "Down" && _direction != "Down") _tankView.Down();
                        else if (tankModel.Direction == "Up" && _direction != "Up") _tankView.Up();
                        else if (tankModel.Direction == "Right" && _direction != "Right") _tankView.IsRight();
                        else if (tankModel.Direction == "Left" && _direction != "Left") _tankView.IsLeft();
                        if (tankModel.Killed && !_killed)
                        {
                            _killed = true;
                            _tankView.Death();
                        }
                        else if (!tankModel.Killed && _killed)
                        {
                            _killed = false;
                            _tankView.Life();
                        }
                        if(tankModel.Health != _health)
                        {
                            _health = tankModel.Health;
                            _healthController.Load(_health);
                        }
                        if (tankModel.Power > 15 && _power != tankModel.Power)
                        {
                            _power = tankModel.Power;
                            _tankView.BackColor = Color.Yellow;
                        }
                        else if (tankModel.Power > 10 && _power != tankModel.Power)
                        {
                            _power = tankModel.Power;
                            _tankView.BackColor = Color.Orange;
                        }
                        else if (tankModel.Power > 5 && _power != tankModel.Power)
                        {
                            _power = tankModel.Power;
                            _tankView.BackColor = Color.LightGreen;
                        }
                        _direction = tankModel.Direction;
                        _tankView.Location = new(tankModel.X, tankModel.Y);
                    }));
                }
                else
                {
                    if (tankModel.Direction == "Down" && _direction != "Down") _tankView.Down();
                    else if (tankModel.Direction == "Up" && _direction != "Up") _tankView.Up();
                    else if (tankModel.Direction == "Right" && _direction != "Right") _tankView.IsRight();
                    else if (tankModel.Direction == "Left" && _direction != "Left") _tankView.IsLeft();
                    if (tankModel.Killed && !_killed)
                    {
                        _killed = true;
                        _tankView.Death();
                    }
                    else if (!tankModel.Killed && _killed)
                    {
                        _killed = false;
                        _tankView.Life();
                    }
                    if (tankModel.Health != _health)
                    {
                        _health = tankModel.Health;
                        _healthController.Load(_health);
                    }
                    _direction = tankModel.Direction;
                    _tankView.Location = new(tankModel.X, tankModel.Y);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
