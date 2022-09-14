using Project_66_Server.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_66_Server.View
{
    public partial class GameView : UserControl
    {
        int _room = 0;
        public ListBox Games { get; set; }
        public ListBox Users { get; set; }
        public GameView()
        {
            InitializeComponent();
            Load += GameView_Load;
        }

        private void GameView_Load(object? sender, EventArgs e)
        {
            AutoSize = true;
            Games = new();
            Users = new();
            Users.Location = new Point(200, 0);
            Controls.Add(Games);
            Controls.Add(Users);
            List<string> names = new();
            names.Add("valik");
            names.Add("valik1");
            Games.Items.Add(new RoomModel(_room++, names));
            List<string> names1 = new();
            names1.Add("valik2");
            names1.Add("valik4");
            Games.SelectedIndexChanged += Games_SelectedIndexChanged;
        }

        private void Games_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ListBox? list = (ListBox?)sender;
            if(list != null)
            {
                RoomModel roomModel = (RoomModel)list.SelectedItem;
                Users.Items.Clear();
                Users.Items.AddRange(roomModel.Users.ToArray());
            }
        }


    }
}
