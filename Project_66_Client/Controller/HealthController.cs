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
            _healthView.Load(health);
        }
    }
}
