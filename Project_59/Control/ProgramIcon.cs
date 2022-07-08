using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_59.Control
{
    public partial class ProgramIcon : UserControl
    {
        public ProgramIcon(Image img, string name)
        {
            InitializeComponent();
            PictureBox pictureBox = new PictureBox();
            Label label = new Label();

            BackColor = Color.Transparent;
            pictureBox.Image = img;
            pictureBox.Size = new Size(32, 32);
            pictureBox.Location = new Point(19, 0);
            pictureBox.BackColor = Color.Transparent;

            label.Text = name;
            label.Location = new Point(0, 32);
            label.Size = new Size(70, 35);
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.ForeColor = Color.White;
            label.BackColor = Color.Transparent;

            Controls.Add(pictureBox);
            Controls.Add(label);
        }
    }
}
