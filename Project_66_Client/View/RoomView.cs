namespace Project_66_Client.View
{
    public partial class RoomView : UserControl
    {
        public RoomView()
        {
            InitializeComponent();
            Size = new(800, 450);
            BackColor = Color.Black;
        }
        public void AddTank(TankView tankView)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    Controls.Add(tankView);
                }));
            }
            else
            {
                Controls.Add(tankView);
            }
        }
        public void RemoveTank(TankView tankView)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    Controls.Remove(tankView);
                }));
            }
            else
            {
                Controls.Remove(tankView);
            }
        }
        public BrickView AddBrick()
        {
            BrickView brickView = new();
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    Controls.Add(brickView);
                }));
            }
            else
            {
                Controls.Add(brickView);
            }
            return brickView;
        }
        public void RemoveBrick(BrickView brickView)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    Controls.Remove(brickView);
                }));
            }
            else
            {
                Controls.Remove(brickView);
            }
        }
        public BulletView AddBullet()
        {
            BulletView bulletView = new();
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    Controls.Add(bulletView);
                }));
            }
            else
            {
                Controls.Add(bulletView);
            }
            return bulletView;
        }
        public void RemoveBullet(BulletView bulletView)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    Controls.Remove(bulletView);
                }));
            }
            else
            {
                Controls.Remove(bulletView);
            }
        }
    }
}
