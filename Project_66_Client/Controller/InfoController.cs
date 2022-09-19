using Project_66_Client.Model;
using Project_66_Client.View;

namespace Project_66_Client.Controller
{
    public class InfoController
    {
        private InfoModel _infoModel = new();
        InfoView _infoView;
        public InfoController(InfoView infoView)
        {
            _infoView = infoView;
        }
        public void SetInfo(TankModel tankModel)
        {
            if (_infoModel.Power != tankModel.Power)
            {
                _infoModel.Power = tankModel.Power;
                _infoView.SetPower(_infoModel.Power.ToString());
            }
            if (_infoModel.Defence != tankModel.Defence)
            {
                _infoModel.Defence = tankModel.Defence;
                _infoView.SetDefence(_infoModel.Defence.ToString());
            }
            if (_infoModel.Coins != tankModel.Coins)
            {
                _infoModel.Coins = tankModel.Coins;
                _infoView.SetCoins(_infoModel.Coins.ToString());
            }
            if (_infoModel.Murders != tankModel.Murders)
            {
                _infoModel.Murders = tankModel.Murders;
                _infoView.SetMurders(_infoModel.Murders.ToString());
            }
            if (_infoModel.Deaths != tankModel.Deaths)
            {
                _infoModel.Deaths = tankModel.Deaths;
                _infoView.SetDeaths(_infoModel.Deaths.ToString());
            }
        }
    }
}
