using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace Project_66_Client.View
{
    public partial class ClientView : UserControl
    {
        public Label ClientName = new Label();
        public ComboBox Players = new ComboBox();
        public Button Start = new Button();
        public Button Exit = new();
        public Button BuyPower = new();
        public Button BuyDefence = new();
        public ClientView()
        {
            InitializeComponent();
            Location = new Point(250, 150);
            AutoSize = true;

            Label labelName = new Label();
            labelName.Text = "Name:";
            labelName.Location = new Point(0, 5);

            ClientName.Text = "Valik";
            ClientName.Location = new Point(100, 5);

            Label labelPlayers = new Label();
            labelPlayers.Text = "Players:";
            labelPlayers.Location = new Point(0, 55);

            Players.Location = new Point(100, 50);
            Players.Items.Add(2);
            Players.Items.Add(3);
            Players.Items.Add(4);
            Players.Size = new Size(50, 20);
            Players.SelectedIndex = 0;

            Start.Text = "Start";
            Start.Location = new Point(40, 100);


            Exit.Text = "Exit";
            Exit.Location = new(250, 0);

            BuyPower.Text = "Power+";
            BuyPower.Location = new(250, 50);

            BuyDefence.Text = "Defence+";
            BuyDefence.Location = new(250, 100);

            Controls.Add(labelName);
            Controls.Add(ClientName);
            Controls.Add(labelPlayers);
            Controls.Add(Players);
            Controls.Add(Start);
            Controls.Add(Exit);
            Controls.Add(BuyPower);
            Controls.Add(BuyDefence);
        }
    }
}
