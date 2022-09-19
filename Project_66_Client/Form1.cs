using Project_66_Client.Controller;
using Project_66_Client.View;

namespace Project_66_Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GameView _gameView;
            _gameView = new GameView();
            new GameController(_gameView);
            Controls.Add(_gameView);
        }
    }
}