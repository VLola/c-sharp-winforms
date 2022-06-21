using Project_47.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_47.Forms.Controls
{
    public class GroupBoxScale: GroupBox
    {
        private NewButton Increase;
        private NewButton Decrease;
        private NewButton Default;
        public GroupBoxScale()
        {

            Increase = new NewButton("Increase", Resources.Increase) { Location = new Point(0, 8), Width = 60 };
            Decrease = new NewButton("Decrease", Resources.Decrease) { Location = new Point(60, 8), Width = 65 };
            Default = new NewButton("100 %", Resources.Default) { Location = new Point(125, 8), Width = 60 };

            Label label = new Label();
            label.Text = "Scale";
            label.Location = new Point(80, 80);
            label.ForeColor = Color.Gray;

            Location = new Point(0, 0);
            Size = new Size(200, 105);

            Controls.Add(Increase);
            Controls.Add(Decrease);
            Controls.Add(Default);
            Controls.Add(label);
        }
    }
}
