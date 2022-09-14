using Project_66_Client.Model;
using Project_66_Client.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_66_Client.Controller
{
    public class GameController
    {
        GameView _gameView;
        TankView _tankView;
        public GameController(GameView gameView)
        {
            _gameView = gameView;
            _tankView = new TankView();
            _gameView.Controls.Add(_tankView);
            _gameView.PreviewKeyDown += _gameView_PreviewKeyDown;
        }

        private void _gameView_PreviewKeyDown(object? sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                //_gameView._tankModel.Direction = "Up";
                if (_tankView.Location.Y > 0) _tankView.Location = new Point(_tankView.Location.X, _tankView.Location.Y - 1);
            }
            else if (e.KeyCode == Keys.Down)
            {
                //_gameView._tankModel.Direction = "Down";
                if (_tankView.Location.Y < _gameView.Height - 50) _tankView.Location = new Point(_tankView.Location.X, _tankView.Location.Y + 1);
            }
            else if (e.KeyCode == Keys.Left)
            {
                //_gameView._tankModel.Direction = "Left";
                if (_tankView.Location.X > 0) _tankView.Location = new Point(_tankView.Location.X - 1, _tankView.Location.Y);
            }
            else if (e.KeyCode == Keys.Right)
            {
                //_gameView._tankModel.Direction = "Right";
                if (_tankView.Location.X < _gameView.Width - 50) _tankView.Location = new Point(_tankView.Location.X + 1, _tankView.Location.Y);
            }
            else if (e.KeyCode == Keys.Space)
            {
                Shot();
            }
        }
        private void Shot()
        {
            
        }
    }
}
