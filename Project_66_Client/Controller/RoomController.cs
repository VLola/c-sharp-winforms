﻿using Project_66_Client.Model;
using Project_66_Client.View;

namespace Project_66_Client.Controller
{
    internal class RoomController
    {
        RoomView _roomView;
        TankView _tankView1;
        TankView _tankView2;
        TankView _tankView3;
        TankView _tankView4;
        public RoomController(RoomView roomView)
        {
            _roomView = roomView;
            _tankView1 = new();
            _tankView2 = new();
            _tankView3 = new();
            _tankView4 = new();
            _roomView.Controls.Add(_tankView1);
            _roomView.Controls.Add(_tankView2);
            _roomView.Controls.Add(_tankView3);
            _roomView.Controls.Add(_tankView4);
        }
        public void LoadTanks(List<TankModel> tankModels)
        {
            if (tankModels.Count == 1)
            {
                _tankView1.Loading(tankModels[0]);
            }
            else if (tankModels.Count == 2)
            {
                _tankView1.Loading(tankModels[0]);
                _tankView2.Loading(tankModels[1]);
            }
            else if (tankModels.Count == 3)
            {
                _tankView1.Loading(tankModels[0]);
                _tankView2.Loading(tankModels[1]);
                _tankView3.Loading(tankModels[2]);
            }
            else if (tankModels.Count == 4)
            {
                _tankView1.Loading(tankModels[0]);
                _tankView2.Loading(tankModels[1]);
                _tankView3.Loading(tankModels[2]);
                _tankView4.Loading(tankModels[3]);
            }
        }
        public void LoadBullets(List<BulletModel> value)
        {
            try
            {
                if (_roomView.InvokeRequired)
                {
                    _roomView.Invoke(new Action(() =>
                    {
                        foreach (var it in value)
                        {
                            bool check = false;
                            foreach (Control bulletView in _roomView.Controls)
                            {
                                if(it.Id.ToString() == bulletView.Name)
                                {
                                    bulletView.Location = new(it.X, it.Y);
                                    check = true;
                                    break;
                                }
                            }
                            if (!check)
                            {
                                BulletView bulletView = new BulletView();
                                bulletView.Name = it.Id.ToString();
                                bulletView.Location = new(it.X, it.Y);
                                _roomView.Controls.Add(bulletView);
                            }
                        }
                    }));
                }
                else
                {
                    foreach (var it in value)
                    {
                        bool check = false;
                        foreach (Control bulletView in _roomView.Controls)
                        {
                            if (it.Id.ToString() == bulletView.Name)
                            {
                                bulletView.Location = new(it.X, it.Y);
                                check = true;
                                break;
                            }
                        }
                        if (!check)
                        {
                            BulletView bulletView = new BulletView();
                            bulletView.Name = it.Id.ToString();
                            bulletView.Location = new(it.X, it.Y);
                            _roomView.Controls.Add(bulletView);
                        }
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
