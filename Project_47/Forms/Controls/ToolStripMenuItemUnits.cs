using System;
using System.Windows.Forms;

namespace Project_47.Forms.Controls
{
    public class ToolStripMenuItemUnits : ToolStripMenuItem
    {
        public GroupBoxParameters box;
        public ToolStripMenuItemUnits(string text, GroupBoxParameters box)
        {
            Text = text;
            this.box = box;
            Click += NewToolStripMenuItemCheck_Click;
        }

        private void NewToolStripMenuItemCheck_Click(object sender, EventArgs e)
        {
            if (!Checked)
            {
                if (box.inches != this) box.inches.Checked = false;
                if (box.centimeters != this) box.centimeters.Checked = false;
                if (box.points != this) box.points.Checked = false;
                if (box.peaks != this) box.peaks.Checked = false;
                Checked = true;
            }

        }
    }
}
