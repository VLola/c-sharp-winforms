using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Project_46.Forms.Controls
{
    public class NewPrint
    {
        private PrintDocument document = new PrintDocument();
        private PrintDialog PrintDialog = new PrintDialog();
        private NewTabPage newTabPage;
        public NewPrint(NewTabControl newTabControl)
        {
            newTabPage = (NewTabPage)newTabControl.SelectedTab;
            if (newTabPage != null && newTabPage.newRichTextBox.Text != "")
            {
                PrintDialog.AllowSomePages = true;
                PrintDialog.ShowHelp = true;
                PrintDialog.Document = document;
                document.PrintPage += new PrintPageEventHandler(document_PrintPage);
                if (newTabPage.newRichTextBox.path != "") document.DocumentName = newTabPage.newRichTextBox.path;
                else document.DocumentName = newTabPage.Text;

                DialogResult result = PrintDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    document.Print();
                }
            }
        }
        private void document_PrintPage(object sender, PrintPageEventArgs e)
        {
            string text = newTabPage.newRichTextBox.Text;
            Font printFont = new Font("Arial", 35, FontStyle.Regular);
            e.Graphics.DrawString(text, printFont, Brushes.Black, 0, 0);
        }
    }
}
