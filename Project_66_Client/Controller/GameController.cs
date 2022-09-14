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
        TankController _tankController;
        public GameController(GameView gameView)
        {
            _gameView = gameView;
            TankView tankView = new();
            _tankController = new(tankView, new TankModel());
            _gameView.Controls.Add(tankView);
            _gameView.PreviewKeyDown += _gameView_PreviewKeyDown;
        }

        private void _gameView_PreviewKeyDown(object? sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (_tankController.GetLocationY() > 0) 
                {
                    _tankController.Up();
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (_tankController.GetLocationY() < _gameView.Height - 50)
                {
                    _tankController.Down();
                }
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (_tankController.GetLocationX() > 0)
                {
                    _tankController.Left();
                }
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (_tankController.GetLocationX() < _gameView.Width - 50)
                {
                    _tankController.Right();
                }
            }
            else if (e.KeyCode == Keys.Space)
            {
                Shot();
            }
        }
        private void Shot()
        {
            BulletModel bulletModel = new(_tankController.GetTankModel(), _gameView.Width, _gameView.Height);
        }
    }
}
