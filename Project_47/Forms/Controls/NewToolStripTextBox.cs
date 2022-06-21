using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_47.Forms.Controls
{
    public class NewToolStripTextBox: ToolStripTextBox
    {
        public NewToolStripTextBox(string text)
        {
            Text = text;
            Enabled = false;
            Font = new Font("Segoe UI", 9F);
            BackColor = Color.White;
            BorderStyle = BorderStyle.None;
            Size = new Size(200, 20);
        }
    }
}
