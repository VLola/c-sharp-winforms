using System.Drawing;
using System.Windows.Forms;

namespace Project_47.Forms.Controls
{
    public class PanelView: Panel
    {
        public GroupBoxScale BoxScale { get; set; } = new GroupBoxScale();
        public GroupBoxShowOrHide BoxShowOrHide { get; set; } = new GroupBoxShowOrHide();
        public GroupBoxParameters BoxParameters { get; set; } = new GroupBoxParameters();
        public PanelView()
        {
            Location = new Point(0, 18);
            Size = new Size(1060, 105);
            Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            BackColor = Color.White;

            Controls.Add(BoxScale);
            Controls.Add(BoxShowOrHide);
            Controls.Add(BoxParameters);
        }
    }
}
