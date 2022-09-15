namespace Project_66_Client.View
{
    public partial class GameView : UserControl
    {
        public LoginView LoginView = new LoginView();
        public RoomView RoomView = new RoomView();
        public GameView()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            Controls.Add(RoomView);
            Controls.Add(LoginView);
        }

        public void LoginVisible()
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        RoomView.Visible = false;
                        LoginView.Visible = true;
                    }));
                }
                else
                {
                    RoomView.Visible = false;
                    LoginView.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void LoginHidden()
        {
            try 
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        RoomView.Visible = true;
                        LoginView.Visible = false;
                    }));
                }
                else
                {
                    RoomView.Visible = true;
                    LoginView.Visible = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
