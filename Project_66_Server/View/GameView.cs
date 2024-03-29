﻿using Project_66_Server.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project_66_Server.View
{
    public partial class GameView : UserControl
    {
        ListBox Games { get; set; }
        ListBox Users { get; set; }
        ListBox User { get; set; }
        Label CountPackts { get; set; }
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
            CountPackts = new Label();
            Users.Location = new Point(200, 0);

            User = new ListBox();
            User.Location = new Point(400, 0);

            CountPackts.Text = "0";
            CountPackts.Location = new Point(0, 95);

            Controls.Add(Games);
            Controls.Add(Users);
            Controls.Add(User);
            Controls.Add(CountPackts);
            Games.SelectedIndexChanged += Games_SelectedIndexChanged;
            Users.SelectedIndexChanged += Users_SelectedIndexChanged;
        }

        private void Users_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox list = (ListBox)sender;
            TankModel tankModel = (TankModel)list.SelectedItem;
            User.Items.Clear();
            User.Items.Add($"Name:\t{tankModel.Name}");
            User.Items.Add($"Coins:\t{tankModel.Coins}");
            User.Items.Add($"Power:\t{tankModel.Power}");
            User.Items.Add($"Defence:\t{tankModel.Defence}");
            User.Items.Add($"Murders:\t{tankModel.Murders}");
            User.Items.Add($"Deaths:\t{tankModel.Deaths}");
        }

        private void Games_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox list = (ListBox)sender;
            RoomModel roomModel = (RoomModel)list.SelectedItem;
            Users.Items.Clear();
            Users.Items.AddRange(roomModel.Tanks.ToArray());
        }
        public void AddRoom(RoomModel roomModel)
        {
            Invoke(new Action(() => {
                Games.Items.Add(roomModel);
            }));
        }
        public void SendPacket(int count)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    CountPackts.Text = count.ToString();
                }));
            }
            else
            {
                CountPackts.Text = count.ToString();
            }
        }

    }
}
