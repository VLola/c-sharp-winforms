using Project_66_Client.Model;
using Project_66_Client.View;

namespace Project_66_Client.Controller
{
    internal class RoomController
    {
        RoomView _roomView;
        List<int> _deleteBullets = new();
        List<int> _deleteBricks = new();
        List<TankController> _tankControllers = new();
        public List<BrickController> BrickControllers = new();
        List<BulletController> _bulletControllers = new();
        public RoomController(RoomView roomView)
        {
            _roomView = roomView;
        }
        public void LoadTanks(List<TankModel> tankModels)
        {
            try
            {
                if (tankModels != null)
                {
                    // Add
                    lock (tankModels) foreach (var item in tankModels)
                        {
                            if (!CheckName(item.Name))
                            {
                                lock (_tankControllers) _tankControllers.Add(new TankController(_roomView, new TankView(), item));
                            }
                            else
                            {
                                LoadTank(item);
                            }
                        }
                    // Delete
                    int i = 0;
                    bool check = false;
                    lock (_tankControllers) foreach (var item in _tankControllers)
                        {
                            if (!CheckDeleteName(item.Name, tankModels))
                            {
                                check = true;
                                break;
                            }
                            i++;
                        }
                    if (check)
                        lock (_tankControllers)
                        {
                            _tankControllers[i].DisposeTank();
                            _tankControllers.RemoveAt(i);
                        }
                }
            }
            catch { }
        }
        private void LoadTank(TankModel tankModel)
        {
            try
            {
                lock (_tankControllers)
                {
                    foreach (var item in _tankControllers)
                    {
                        if (item.Name == tankModel.Name) item.Load(tankModel);
                    }
                }
            }
            catch { }
        }
        private bool CheckDeleteName(string name, List<TankModel> tankModels)
        {
            try
            {
                lock (tankModels)
                {
                    foreach (var item in tankModels)
                    {
                        if (item.Name == name) return true;
                    }
                }
            }
            catch { }
            return false;
        }
        private bool CheckName(string name)
        {
            try
            {
                lock (_tankControllers)
                {
                    foreach (var item in _tankControllers)
                    {
                        if (item.Name == name) return true;
                    }
                }
            }
            catch { }
            return false;
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
            try
            {
                int i = 0;
                foreach (var controller in _bulletControllers)
                {
                    bool check = false;
                    foreach (var it in value)
                    {
                        if (it.Id == controller.Id)
                        {
                            check = true;
                            break;
                        }
                    }
                    if (!check) {
                        controller.RemoveBullet();
                        _deleteBullets.Add(i);
                    }
                    i++;
                }
                _deleteBullets.Reverse();
                foreach (var item in _deleteBullets)
                {
                    _bulletControllers.RemoveAt(item);
                }
            }
            catch { }
            _deleteBullets.Clear();
        }
        private void AddBullet(List<BulletModel> value)
        {
            try
            {
                if (value != null)
                    foreach (var it in value)
                    {
                        bool check = false;
                        foreach (var controller in _bulletControllers)
                        {
                            if (it.Id == controller.Id)
                            {
                                controller.LoadBullet(it);
                                check = true;
                                break;
                            }
                        }
                        if (!check)
                        {
                            _bulletControllers.Add(new BulletController(_roomView, it));
                        }
                    }
            }
            catch { }
        }
        public void LoadBricks(List<BrickModel> bricks)
        {
            try
            {
                lock (BrickControllers)
                {
                    if (bricks != null)
                    {
                        foreach (var model in bricks)
                        {
                            if (!CheckBrickToControllers(model))
                            {
                                BrickControllers.Add(new BrickController(_roomView.AddBrick(), model));
                            }
                        }

                        int i = 0;
                        foreach (var controller in BrickControllers)
                        {
                            if (!CheckBrickToModels(bricks, controller.GetBrickModel()))
                            {
                                _roomView.RemoveBrick(controller.GetBrickView());
                                _deleteBricks.Add(i);
                            }
                            i++;
                        }
                        _deleteBricks.Reverse();
                        foreach (var item in _deleteBricks)
                        {
                            BrickControllers.RemoveAt(item);
                        }
                    }
                }
            }
            catch { }
            _deleteBricks.Clear();
        }
        public bool CheckBrickToControllers(BrickModel brickModel)
        {
            try
            {
                foreach (var controller in BrickControllers)
                {
                    if (controller.GetBrickModel().Id == brickModel.Id) return true;
                }
            }
            catch { }
            return false;
        }
        public bool CheckBrickToModels(List<BrickModel> bricks, BrickModel brickModel)
        {
            try
            {
                foreach (var model in bricks)
                {
                    if (model.Id == brickModel.Id) return true;
                }
            }
            catch { }
            return false;
        }
    }
}
