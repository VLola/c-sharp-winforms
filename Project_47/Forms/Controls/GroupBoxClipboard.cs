using Project_47.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace Project_47.Forms.Controls
{
    public class GroupBoxClipboard: GroupBox
    {
        public MenuStrip Home;
        public ToolStripSplitButton HomeInsert;
        public ToolStripMenuItem Insert;
        public ToolStripMenuItem SpecialInsert;

        public MenuStrip TextChange;
        public NewToolStripMenuItem Cut { get; set; }
        public NewToolStripMenuItem Copy { get; set; }
        public GroupBoxClipboard()
        {
            Home = new MenuStrip() { BackColor = Color.White, AutoSize = false, Dock = DockStyle.None, Location = new Point(10, 10), Size = new Size(60, 60) };
            Home.Items.Add(HomeInsert = new ToolStripSplitButton("Insert", Resources.Insert)
            {
                TextAlign = ContentAlignment.BottomCenter
                ,ImageAlign = ContentAlignment.TopCenter
                ,ImageScaling = ToolStripItemImageScaling.None
                ,TextImageRelation = TextImageRelation.Overlay
            });

            HomeInsert.DropDownItems.AddRange(new ToolStripItem[] {
                Insert = new NewToolStripMenuItem("Insert", Resources.Insert_20x20)
                ,SpecialInsert = new NewToolStripMenuItem("Special insert", Resources.SpecialInsert)
            });

            TextChange = new MenuStrip() { BackColor = Color.White, AutoSize = false, Dock = DockStyle.None, Location = new Point(70, 10), Size = new Size(70, 50), LayoutStyle = ToolStripLayoutStyle.Flow, Enabled = false };
            TextChange.Items.Add(Cut = new NewToolStripMenuItem("Cut", Resources.Cut));
            TextChange.Items.Add(Copy = new NewToolStripMenuItem("Copy", Resources.Copy));

            Label label = new Label();
            label.Text = "Сlipboard";
            label.Location = new Point(50, 80);
            label.ForeColor = Color.Gray;
            label.Width = 60;

            Location = new Point(0, 0);
            Size = new Size(150, 105);
            Controls.Add(Home);
            Controls.Add(TextChange);
            Controls.Add(label);
        }
    }
}
