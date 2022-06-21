using Project_47.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace Project_47.Forms.Controls
{
    public class NewToolStripItem : ToolStripItem
    {
        public NewToolStripItem()
        {
            Size = new Size(200, 50);
            //AutoSize = true;
            Text = "dfsbgafdbafdt";
            Image = Resources.FastPrinting;
        }
    }
}