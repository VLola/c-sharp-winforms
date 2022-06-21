using Project_47.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace Project_47.Forms.Controls
{
    public class GroupBoxParagraph: GroupBox
    {
        public MenuStrip TopButtons;
        public NewToolStripMenuItem DecreaseIndent;
        public NewToolStripMenuItem IncreaseIndent;
        public ToolStripSplitButton StartList;
        public NewToolStripMenuItem LineSpacing;

        public MenuStrip BottomButtons;
        public NewToolStripMenuItem LeftButton;
        public NewToolStripMenuItem CentreButton;
        public NewToolStripMenuItem RightButton;
        public NewToolStripMenuItem AlignButton;
        public NewToolStripMenuItem ParagraphButton;
        public GroupBoxParagraph()
        {
            TopButtons = new MenuStrip() { BackColor = Color.White, AutoSize = false, Dock = DockStyle.None, Location = new Point(0, 18), Width = 125 };
            TopButtons.Items.Add(DecreaseIndent = new NewToolStripMenuItem(Resources.DecreaseIndent));
            TopButtons.Items.Add(IncreaseIndent = new NewToolStripMenuItem(Resources.IncreaseIndent));
            TopButtons.Items.Add(StartList = new ToolStripSplitButton(Resources.StartList) { AutoSize = false, Size = new Size(36, 22) });
            TopButtons.Items.Add(LineSpacing = new NewToolStripMenuItem(Resources.LineSpacing));

            BottomButtons = new MenuStrip() { BackColor = Color.White, AutoSize = false, Dock = DockStyle.None, Location = new Point(0, 50), Width = 125 };
            BottomButtons.Items.Add(LeftButton = new NewToolStripMenuItem(Resources.Left));
            BottomButtons.Items.Add(CentreButton = new NewToolStripMenuItem(Resources.Centre));
            BottomButtons.Items.Add(RightButton = new NewToolStripMenuItem(Resources.Right));
            BottomButtons.Items.Add(AlignButton = new NewToolStripMenuItem(Resources.Align));
            BottomButtons.Items.Add(ParagraphButton = new NewToolStripMenuItem(Resources.Paragraph));

            Label label = new Label();
            label.Text = "Paragraph";
            label.Location = new Point(40, 80);
            label.ForeColor = Color.Gray;
            label.Width = 60;

            Location = new Point(388, 0);
            Size = new Size(130, 105);
            Controls.Add(TopButtons);
            Controls.Add(BottomButtons);
            Controls.Add(label);
        }
    }
}
