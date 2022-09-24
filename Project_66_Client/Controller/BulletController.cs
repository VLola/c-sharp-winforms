using Project_66_Client.Model;
using Project_66_Client.View;

namespace Project_66_Client.Controller
{
    internal class BulletController
    {
        BulletView _bulletView;
        RoomView _roomView;
        public int Id{get;set;}
        public BulletController(RoomView roomView, BulletModel bulletModel)
        {
            _roomView = roomView;
            _bulletView = _roomView.AddBullet();
            Id = bulletModel.Id;
            _bulletView.Loading(bulletModel);
        }
        public void LoadBullet(BulletModel bulletModel)
        {
            _bulletView.Loading(bulletModel);
        }
        public void RemoveBullet()
        {
            _roomView.RemoveBullet(_bulletView);
        }
    }
}
