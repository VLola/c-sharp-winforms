using Project_66_Client.View;

namespace Project_66_Client.Controller
{
    internal class HealthController
    {
        HealthView _healthView;
        public HealthController(HealthView healthView)
        {
            _healthView = healthView;
        }

        public void Load(int health)
        {
            try
            {
                if (_healthView.InvokeRequired)
                {
                    _healthView.Invoke(new Action(() =>
                    {
                        _healthView.Size = new Size((health * 2), 2);
                    }));
                }
                else
                {
                    _healthView.Size = new Size((health * 2), 2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
