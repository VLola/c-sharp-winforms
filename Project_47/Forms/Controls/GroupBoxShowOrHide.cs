using System.Drawing;
using System.Windows.Forms;

namespace Project_47.Forms.Controls
{
    public class GroupBoxShowOrHide: GroupBox
    {
        public CheckBox Ruler;
        public CheckBox StatusBar;
        public GroupBoxShowOrHide()
        {
            Ruler = new CheckBox() { Text = "Ruler", Location = new Point(10, 10) };
            StatusBar = new CheckBox() { Text = "Status bar", Location = new Point(10, 30) };

            Label label = new Label();
            label.Text = "Show or hide";
            label.Location = new Point(40, 80);
            label.ForeColor = Color.Gray;

            Location = new Point(199, 0);
            Size = new Size(150, 105);

            Controls.Add(label);
            Controls.Add(Ruler);
            Controls.Add(StatusBar);
        }
    }
}
