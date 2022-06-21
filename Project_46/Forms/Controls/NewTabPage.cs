using System.Windows.Forms;

namespace Project_46.Forms.Controls
{
    public class NewTabPage : TabPage
    {
        public NewRichTextBox newRichTextBox;
        public NewTabControl newTabControl;

        
        public NewTabPage(NewTabControl newTabControl, string name, string path)
        {
            this.newTabControl = newTabControl;
            Text = NewName(name);
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            newRichTextBox = new NewRichTextBox(name, path)
            {
                Multiline = true,
                WordWrap = false,
                Width = Width - 5,
                Height = Height - 55,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            Controls.Add(newRichTextBox);

        }

        
        private bool CheckName(string name)
        {
            bool check = false;
            foreach (NewTabPage it in newTabControl.TabPages)
            {
                if (it.Text == name )
                {
                    check = true;
                }
            }
            if (check) return true;
            else return false;
        }
        private string NewName(string name)
        {
            string new_name = name;
            if (!CheckName(new_name)) return new_name;
            for (int i = 1; ; i++)
            {
                new_name = name + " " + i.ToString();
                if (!CheckName(new_name)) return new_name;
            }
        }
    }
}
