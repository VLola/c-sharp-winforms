using System.Drawing;
using System.Windows.Forms;

namespace Project_47.Forms.Controls
{
    public class NewRichTextBox: RichTextBox
    {
        public NewRichTextBox()
        {
            Location = new Point(3, 3);
            Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            BorderStyle = BorderStyle.Fixed3D; 
            Multiline = true;
            BackColor = Color.White;
            WordWrap = true;
        }
    }
}
