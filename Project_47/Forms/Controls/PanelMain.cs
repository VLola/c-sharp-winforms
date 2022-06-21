using System.Drawing;
using System.Windows.Forms;

namespace Project_47.Forms.Controls
{
    public class PanelMain: Panel
    {
        public GroupBoxClipboard BoxClipboard { get; set; } = new GroupBoxClipboard();
        public GroupBoxFont BoxFont { get; set; } = new GroupBoxFont();
        public GroupBoxParagraph BoxParagraph { get; set; } = new GroupBoxParagraph();
        public GroupBoxInsert BoxInsert { get; set; } = new GroupBoxInsert();
        public GroupBoxEditing BoxEditing { get; set; } = new GroupBoxEditing();
        public PanelMain()
        {
            Location = new Point(0, 18);
            Size = new Size(1060, 105);
            Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            BackColor = Color.White;

            Controls.Add(BoxClipboard);
            Controls.Add(BoxFont);
            Controls.Add(BoxParagraph);
            Controls.Add(BoxInsert);
            Controls.Add(BoxEditing);
        }
    }
}
