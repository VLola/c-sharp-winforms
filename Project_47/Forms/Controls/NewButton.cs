using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_47.Forms.Controls
{
    class NewButton: Button
    {
        public NewButton(string text, Image image)
        {
            Text = text;
            Image = image;
            FlatAppearance.BorderColor = Color.Blue;
            FlatAppearance.MouseDownBackColor = Color.White;
            FlatAppearance.MouseOverBackColor = Color.LightSkyBlue;
            FlatAppearance.BorderSize = 0;
            FlatStyle = FlatStyle.Flat;
            ImageAlign = ContentAlignment.TopCenter;
            Size = new Size(46, 60);
            TextAlign = ContentAlignment.BottomCenter;
        }
    }
}
