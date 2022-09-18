﻿using Project_66_Client.Model;
using Project_66_Client.View;

namespace Project_66_Client.Controller
{
    internal class RoomController
    {
        RoomView _roomView;
        List<int> _deleteBullets = new List<int>();
        List<TankController> tankControllers = new List<TankController>();
        public RoomController(RoomView roomView)
        {
            _roomView = roomView;
        }
        public void LoadTanks(List<TankModel> tankModels)
        {
            // Add
            lock (tankModels) foreach (var item in tankModels)
            {
                    if (!CheckName(item.Name)) { 
                        lock (tankControllers) tankControllers.Add(new TankController(_roomView, new TankView(), item)); 
                    }
                    else
                    {
                        LoadTank(item);
                    }
            }
            // Delete
            int i = 0;
            bool check = false;
            lock(tankControllers) foreach (var item in tankControllers)
            {
                    if (!CheckDeleteName(item.Name, tankModels)) {
                        check = true;
                        break;
                    }
                    i++;
            }
            if (check) 
                lock (tankControllers)
                {
                    tankControllers[i].DisposeTank();
                    tankControllers.RemoveAt(i);
                }
        }
        private void LoadTank(TankModel tankModel)
        {
            lock (tankControllers)
            {
                foreach (var item in tankControllers)
                {
                    if (item.Name == tankModel.Name) item.Load(tankModel);
                }
            }
        }
        private bool CheckDeleteName(string name, List<TankModel> tankModels)
        {
            lock (tankModels)
            {
                foreach (var item in tankModels)
                {
                    if (item.Name == name) return true;
                }
                return false;
            }
        }
        private bool CheckName(string name)
        {
            lock (tankControllers)
            {
                foreach (var item in tankControllers)
                {
                    if (item.Name == name) return true;
                }
                return false;
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
                        AddBullet(value);
                        DeleteBullet(value);
                    }));
                }
                else
                {
                    AddBullet(value);
                    DeleteBullet(value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void DeleteBullet(List<BulletModel> value)
        {
            int i = 0;
            foreach (Control bulletView in _roomView.Controls)
            {
                bool check = false;
                if (bulletView is BulletView)
                {
                    foreach (var it in value)
                    {
                        if (it.Id.ToString() == bulletView.Name) {
                            check = true;
                            break;
                        }
                    }
                    if (!check) _deleteBullets.Add(i);
                }
                i++;
            }
            _deleteBullets.Reverse();
            foreach (var item in _deleteBullets)
            {
                _roomView.Controls.RemoveAt(item);
            }
            _deleteBullets.Clear();
        }
        private void AddBullet(List<BulletModel> value)
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
}
