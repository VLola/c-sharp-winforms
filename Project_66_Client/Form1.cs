using Project_66_Client.Controller;
using Project_66_Client.View;

namespace Project_66_Client
{
    public partial class Form1 : Form
    {
        GameView _gameView;
        GameController _gameController;
        public Form1()
        {
            InitializeComponent();
            _gameView = new GameView();
            _gameController = new GameController(_gameView);
            Controls.Add(_gameView);
        }
    }
}