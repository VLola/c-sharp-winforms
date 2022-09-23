using Project_66_Client.Model;

namespace Project_66_Client.View
{
    public partial class InfoView : UserControl
    {
        Label Power = new();
        Label Defence = new();
        Label Coins = new();
        Label Murders = new();
        Label Deaths = new();
        public InfoView()
        {
            InitializeComponent();
            Location = new(5, 5);

            Label labelCoins = new();
            Label labelMurders = new();
            Label labelDeaths = new();
            Label labelPower = new();
            Label labelDefence = new();

            labelCoins.Location = new(0, 0);
            labelMurders.Location = new(0, 25);
            labelDeaths.Location = new(0, 50);
            labelPower.Location = new(0, 75);
            labelDefence.Location = new(0, 100);

            labelCoins.Text = "Coins:";
            labelMurders.Text = "Murders:";
            labelDeaths.Text = "Deaths:";
            labelPower.Text = "Power:";
            labelDefence.Text = "Defence:";

            labelCoins.AutoSize = true;
            labelMurders.AutoSize = true;
            labelDeaths.AutoSize = true;
            labelPower.AutoSize = true;
            labelDefence.AutoSize = true;

            Controls.Add(labelCoins);
            Controls.Add(labelMurders);
            Controls.Add(labelDeaths);
            Controls.Add(labelPower);
            Controls.Add(labelDefence);

            Coins.Location = new(60, 0);
            Murders.Location = new(60, 25);
            Deaths.Location = new(60, 50);
            Power.Location = new(60, 75);
            Defence.Location = new(60, 100);

            Coins.AutoSize = true;
            Murders.AutoSize = true;
            Deaths.AutoSize = true;
            Power.AutoSize = true;
            Defence.AutoSize = true;

            Controls.Add(Power);
            Controls.Add(Defence);
            Controls.Add(Coins);
            Controls.Add(Murders);
            Controls.Add(Deaths);
        }
        public void SetPower(string value)
        {
            if (Power.InvokeRequired)
            {
                Power.Text = value;
            }
            else
            {
                Power.Text = value;
            }
        }
        public void SetDefence(string value)
        {
            if (Defence.InvokeRequired)
            {
                Defence.Text = value;
            }
            else
            {
                Defence.Text = value;
            }
        }
        public void SetCoins(string value)
        {
            if (Coins.InvokeRequired)
            {
                Coins.Text = value;
            }
            else
            {
                Coins.Text = value;
            }
        }
        public void SetMurders(string value)
        {
            if (Murders.InvokeRequired)
            {
                Murders.Text = value;
            }
            else
            {
                Murders.Text = value;
            }
        }
        public void SetDeaths(string value)
        {
            if (Deaths.InvokeRequired)
            {
                Deaths.Text = value;
            }
            else
            {
                Deaths.Text = value;
            }
        }
    }
}
