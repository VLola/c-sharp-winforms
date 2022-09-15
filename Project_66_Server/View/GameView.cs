using Project_66_Server.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project_66_Server.View
{
    public partial class GameView : UserControl
    {
        public ListBox Games { get; set; }
        public ListBox Users { get; set; }
        public GameView()
        {
            InitializeComponent();
            Load += GameView_Load;
        }

        private void GameView_Load(object sender, EventArgs e)
        {
            AutoSize = true;
            Games = new ListBox();
            Users = new ListBox();
            Users.Location = new Point(200, 0);
            Controls.Add(Games);
            Controls.Add(Users);
            Games.SelectedIndexChanged += Games_SelectedIndexChanged;
        }

        private void Games_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox list = (ListBox)sender;
            RoomModel roomModel = (RoomModel)list.SelectedItem;
            Users.Items.Clear();
            Users.Items.AddRange(roomModel.Tanks.ToArray());
        }

    }
}
