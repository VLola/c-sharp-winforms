using Project_66_Server.Controller;
using Project_66_Server.View;

namespace Project_66_Server
{
    public partial class Form1 : Form
    {
        GameView _gameView;
        GameController _gameController;
        public Form1()
        {
            InitializeComponent();
            _gameView = new();
            _gameController = new(_gameView);
            Controls.Add(_gameView);
        }
    }
}