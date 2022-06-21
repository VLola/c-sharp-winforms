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
    public class NewToolStripMenuItem: ToolStripMenuItem
    {
        public NewToolStripMenuItem(string text, Image image)
        {
            BackColor = Color.White;
            Text = text;
            Image = image;
        }
        public NewToolStripMenuItem(Image image)
        {
            BackColor = Color.White;
            Image = image;
            AutoSize = false;
            ImageScaling = ToolStripItemImageScaling.None;
            Size = new Size(23, 22);
        }
        public NewToolStripMenuItem(Image image, bool CheckOnClick)
        {
            this.CheckOnClick = CheckOnClick;
            BackColor = Color.White;
            Image = image;
            AutoSize = false;
            ImageScaling = ToolStripItemImageScaling.None;
            Size = new Size(23, 22);
            Margin = new Padding(1);
            CheckStateChanged += NewToolStripMenuItem_CheckStateChanged;
        }

        private void NewToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (Checked) BackColor = SystemColors.GradientActiveCaption;
            else BackColor = Color.White;
        }


    }
}
