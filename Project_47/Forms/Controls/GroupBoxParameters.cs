using Project_47.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace Project_47.Forms.Controls
{
    public class GroupBoxParameters: GroupBox
    {
        public MenuStrip MenuWordWrap;
        public ToolStripMenuItem WordWrap;
        public ToolStripMenuItemWordWrap without_hyphenation;
        public ToolStripMenuItemWordWrap within_the_window;
        public ToolStripMenuItemWordWrap within_the_lines;

        public MenuStrip MenuUnits;
        public ToolStripMenuItem Units;
        public ToolStripMenuItemUnits inches;
        public ToolStripMenuItemUnits centimeters;
        public ToolStripMenuItemUnits points;
        public ToolStripMenuItemUnits peaks;
        public GroupBoxParameters()
        {

            MenuWordWrap = new MenuStrip() { BackColor = Color.White, AutoSize = false, Dock = DockStyle.None, Location = new Point(10, 10), Size = new Size(100, 24) };
            MenuWordWrap.Items.Add(WordWrap = new ToolStripMenuItem("Word wrap", Resources.WordWrap));

            WordWrap.DropDownItems.AddRange(new ToolStripMenuItemWordWrap[] {
                without_hyphenation = new ToolStripMenuItemWordWrap("Without hyphenation", this ){Checked = false}
                ,within_the_window = new ToolStripMenuItemWordWrap("Within the window", this ){Checked = false}
                ,within_the_lines = new ToolStripMenuItemWordWrap("Within the lines", this ){Checked = true}
            }); ;


            MenuUnits = new MenuStrip() { BackColor = Color.White, AutoSize = false, Dock = DockStyle.None, Location = new Point(10, 32), Size = new Size(100, 24) };
            MenuUnits.Items.Add(Units = new ToolStripMenuItem("Units", Resources.Units));

            Units.DropDownItems.AddRange(new ToolStripMenuItemUnits[] {
                inches = new ToolStripMenuItemUnits("Inches", this ){Checked = false}
                ,centimeters = new ToolStripMenuItemUnits("Centimeters", this ){Checked = true}
                ,points = new ToolStripMenuItemUnits("Points", this ){Checked = false}
                ,peaks = new ToolStripMenuItemUnits("Peaks", this ){Checked = false}
            }); ;

            Label label = new Label();
            label.Text = "Parameters";
            label.Location = new Point(45, 80);
            label.ForeColor = Color.Gray;

            Location = new Point(348, 0);
            Size = new Size(150, 105);

            Controls.Add(MenuWordWrap);
            Controls.Add(MenuUnits);
            Controls.Add(label);
        }
    }
}
