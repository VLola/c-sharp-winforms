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
    public class GroupBoxFont: GroupBox
    {
        public ComboBox FontName;
        public ComboBox FontSize;
        public MenuStrip TopButtons;
        public NewToolStripMenuItem IncreaseTheSize;
        public NewToolStripMenuItem ReduceTheSize;
        public MenuStrip BottomButtons;
        public NewToolStripMenuItem Bold;
        public NewToolStripMenuItem Italic;
        public NewToolStripMenuItem Underlined;
        public NewToolStripMenuItem StrikeOut;
        public NewToolStripMenuItem Subscripr;
        public NewToolStripMenuItem Superscript;
        public ToolStripSplitButton TextColor;
        public ToolStripSplitButton TextBackground;
        public GroupBoxFont()
        {
            FontName = new ComboBox() { DropDownStyle = ComboBoxStyle.DropDownList };
            FontName.DataSource = FontFamily.Families.Select(it => it.Name).ToList();
            FontName.Location = new Point(10, 20);

            FontSize = new ComboBox() { DropDownStyle = ComboBoxStyle.DropDownList };
            FontSize.DataSource = new int[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 24, 26, 28, 36, 48, 72 };
            FontSize.Location = new Point(135, 20);
            FontSize.Size = new Size(40, 10);

            TopButtons = new MenuStrip() { BackColor = Color.White, AutoSize = false, Dock = DockStyle.None, Location = new Point(170, 18), Width = 55 };
            TopButtons.Items.Add(IncreaseTheSize = new NewToolStripMenuItem(Resources.IncreaseTheSize));
            TopButtons.Items.Add(ReduceTheSize = new NewToolStripMenuItem(Resources.ReduceTheSize));

            BottomButtons = new MenuStrip() { BackColor = Color.White, AutoSize = false, Dock = DockStyle.None, Location = new Point(0, 50), Width = 230 };
            BottomButtons.Items.Add(Bold = new NewToolStripMenuItem(Resources.Bold, true));
            BottomButtons.Items.Add(Italic = new NewToolStripMenuItem(Resources.Italic, true));
            BottomButtons.Items.Add(Underlined = new NewToolStripMenuItem(Resources.Underlined, true));
            BottomButtons.Items.Add(StrikeOut = new NewToolStripMenuItem(Resources.CrossedOut, true));
            BottomButtons.Items.Add(Subscripr = new NewToolStripMenuItem(Resources.Subscripr, true));
            BottomButtons.Items.Add(Superscript = new NewToolStripMenuItem(Resources.Superscript, true));
            BottomButtons.Items.Add(TextColor = new ToolStripSplitButton(Resources.TextColor) { AutoSize = false, Size = new Size(36, 22)});
            BottomButtons.Items.Add(TextBackground = new ToolStripSplitButton(Resources.TextBackground) { AutoSize = false, Size = new Size(36, 22) });

            Label label2 = new Label();
            label2.Text = "Font";
            label2.Location = new Point(100, 80);
            label2.ForeColor = Color.Gray;


            Location = new Point(149, 0);
            Size = new Size(240, 105);
            Controls.Add(FontName);
            Controls.Add(FontSize);
            Controls.Add(TopButtons);
            Controls.Add(BottomButtons);
            Controls.Add(label2);
        }
    }
}
