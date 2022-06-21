using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_42
{
    public partial class Task3 : Form
    {
        public Task3()
        {
            InitializeComponent();
        }

        private void Task3_MouseClick(object sender, MouseEventArgs e)
        {
            int x = Size.Width - 17;
            int y = Size.Height - 40;
            if (ModifierKeys == Keys.Control && e.Button == MouseButtons.Left) Close();
            else if (e.Button == MouseButtons.Left)
            {
                if (e.X > 10 && e.X < x - 10 && e.Y > 10 && e.Y < y - 10) MessageBox.Show($"Сoursor inside the rectangle");
                else if (e.X == 0 || e.Y == 0 || e.X == x || e.Y == y) MessageBox.Show($"Сursor on the border of the rectangle");
                else MessageBox.Show($"Сursor outside the rectangle");
            }
            else if (e.Button == MouseButtons.Right) MessageBox.Show($"Size form: x={x}, y={y}");
        }

        private void Task3_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = $"Cursor coordinates: x={e.X}, y={e.Y}";
        }
    }
}
