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
            try
            {
                if (_infoView.InvokeRequired)
                {
                    _infoView.Invoke(new Action(() =>
                    {
                        if (_infoModel.Power != tankModel.Power)
                        {
                            _infoModel.Power = tankModel.Power;
                            _infoView.Power.Text = _infoModel.Power.ToString();
                        }
                        if (_infoModel.Defence != tankModel.Defence)
                        {
                            _infoModel.Defence = tankModel.Defence;
                            _infoView.Defence.Text = _infoModel.Defence.ToString();
                        }
                        if (_infoModel.Coins != tankModel.Coins)
                        {
                            _infoModel.Coins = tankModel.Coins;
                            _infoView.Coins.Text = _infoModel.Coins.ToString();
                        }
                        if (_infoModel.Murders != tankModel.Murders)
                        {
                            _infoModel.Murders = tankModel.Murders;
                            _infoView.Murders.Text = _infoModel.Murders.ToString();
                        }
                        if (_infoModel.Deaths != tankModel.Deaths)
                        {
                            _infoModel.Deaths = tankModel.Deaths;
                            _infoView.Deaths.Text = _infoModel.Deaths.ToString();
                        }
                    }));
                }
                else
                {
                    if (_infoModel.Power != tankModel.Power)
                    {
                        _infoModel.Power = tankModel.Power;
                        _infoView.Power.Text = _infoModel.Power.ToString();
                    }
                    if (_infoModel.Defence != tankModel.Defence)
                    {
                        _infoModel.Defence = tankModel.Defence;
                        _infoView.Defence.Text = _infoModel.Defence.ToString();
                    }
                    if (_infoModel.Coins != tankModel.Coins)
                    {
                        _infoModel.Coins = tankModel.Coins;
                        _infoView.Coins.Text = _infoModel.Coins.ToString();
                    }
                    if (_infoModel.Murders != tankModel.Murders)
                    {
                        _infoModel.Murders = tankModel.Murders;
                        _infoView.Murders.Text = _infoModel.Murders.ToString();
                    }
                    if (_infoModel.Deaths != tankModel.Deaths)
                    {
                        _infoModel.Deaths = tankModel.Deaths;
                        _infoView.Deaths.Text = _infoModel.Deaths.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
