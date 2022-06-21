using System.Windows.Forms;

namespace Project_47.Forms.Controls
{
    public class NewToolStripContainer: ToolStripContainer
    {
        public NewRichTextBox newRichTextBox { get; set; } = new NewRichTextBox();
        public NewToolStripContainer()
        {
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ContentPanel.Controls.Add(newRichTextBox);
            ContentPanel.Size = new System.Drawing.Size(1036, 390);
            Location = new System.Drawing.Point(12, 173);
            Size = new System.Drawing.Size(1036, 490);
            Padding = new Padding(120,0,120,0);
            newRichTextBox.MouseWheel += NewToolStripContainer_MouseWheel;
        }
        private void NewToolStripContainer_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ModifierKeys == Keys.Control) Padding = new Padding(Padding.Left - e.Delta / 10, 0, Padding.Left - e.Delta / 10, 0);
        }
    }
}
