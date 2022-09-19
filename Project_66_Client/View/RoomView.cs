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
    }
}
