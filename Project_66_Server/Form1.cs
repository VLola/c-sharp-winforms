using Project_66_Server.Controller;
using Project_66_Server.View;
using System.Windows.Forms;

namespace Project_66_Server
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