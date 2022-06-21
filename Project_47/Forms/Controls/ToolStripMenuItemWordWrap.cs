using System;
using System.Windows.Forms;

namespace Project_47.Forms.Controls
{
    public class ToolStripMenuItemWordWrap: ToolStripMenuItem
    {
        public GroupBoxParameters box;
        public ToolStripMenuItemWordWrap(string text, GroupBoxParameters box)
        {
            Text = text;
            this.box = box;
            Click += NewToolStripMenuItemCheck_Click;
        }

        private void NewToolStripMenuItemCheck_Click(object sender, EventArgs e)
        {
            if (!Checked)
            {
                if (box.within_the_lines != this) box.within_the_lines.Checked = false;
                if (box.without_hyphenation != this) box.without_hyphenation.Checked = false;
                if (box.within_the_window != this) box.within_the_window.Checked = false;
                Checked = true;
            }

        }
    }
}
