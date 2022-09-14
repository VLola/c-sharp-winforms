using Project_66_Server.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_66_Server.Controller
{
    internal class GameController
    {
        GameView _gameView;
        public GameController(GameView gameView) {
            _gameView = gameView;
        }
    }
}
