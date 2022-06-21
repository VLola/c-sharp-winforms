using System;
using System.IO;
using System.Windows.Forms;
namespace Project_46.Forms.Controls
{
    public class NewRichTextBox: RichTextBox
    {
        public string path = "";
        public NewRichTextBox(string name ,string path)
        {
            Name = name;
            if (path != "")
            {
                this.path = path;
                Text = File.ReadAllText(path);
            }
        }
    }
}
