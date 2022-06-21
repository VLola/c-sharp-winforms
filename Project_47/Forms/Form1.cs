using Project_47.Forms.Controls;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace Project_47
{
    
    public partial class Form1 : Form
    {
        #region Objects
        private string Path;
        private NewToolStripContainer newToolStripContainer;
        private NewRichTextBox text;
        private NewMenu newMenu;

        private GroupBoxClipboard BoxClipboard;
        private GroupBoxFont BoxFont;
        private GroupBoxParagraph BoxParagraph;
        private GroupBoxInsert BoxInsert;
        private GroupBoxEditing BoxEditing;
        private GroupBoxScale BoxScale;
        private GroupBoxShowOrHide BoxShowOrHide;
        private GroupBoxParameters BoxParameters;

        private OpenFileDialog openFileDialog = new OpenFileDialog();
        private SaveFileDialog saveFileDialog = new SaveFileDialog();
        private PrintDialog printDialog = new PrintDialog();
        private PrintDocument printDocument = new PrintDocument();
        private ColorDialog colorDialog = new ColorDialog(); 
        private string fileFormatFilter = "File RTF (*.rtf)|*.rtf|" +
                 "Text Document (*.txt)|*.txt|" +
                 "Document Office Open XML (*.docx)|*.docx|" +
                 "Document OpenDocument (*.odt)|*.odt";
        #endregion
        public Form1()
        {
            InitializeComponent();
            BackColor = Color.WhiteSmoke;
            AddMenu();
            AddBoxes();
            AddEvents();
        }
        #region Object initializer
        private void AddMenu()
        {
            newMenu = new NewMenu(this);
            newToolStripContainer = new NewToolStripContainer();
            text = newToolStripContainer.newRichTextBox;
            Controls.Add(newToolStripContainer);
        }
        private void AddBoxes()
        {
            BoxClipboard = newMenu.panelMain.BoxClipboard;
            BoxFont = newMenu.panelMain.BoxFont;
            BoxParagraph = newMenu.panelMain.BoxParagraph;
            BoxInsert = newMenu.panelMain.BoxInsert;
            BoxEditing = newMenu.panelMain.BoxEditing;
            BoxScale = newMenu.panelView.BoxScale;
            BoxShowOrHide = newMenu.panelView.BoxShowOrHide;
            BoxParameters = newMenu.panelView.BoxParameters;
        }
        #endregion
        #region Events
        private void AddEvents()
        {
            text.SelectionChanged += NewRichTextBox_SelectionChanged;
            BoxClipboard.HomeInsert.Click += Insert;
            BoxClipboard.Insert.Click += Insert;
            BoxClipboard.Cut.Click += Cut;
            BoxClipboard.Copy.Click += Copy;

            BoxFont.FontName.SelectedIndexChanged += FontName;
            BoxFont.FontSize.SelectedIndexChanged += FontSize;
            BoxFont.IncreaseTheSize.Click += IncreaseTheSize_Click;
            BoxFont.ReduceTheSize.Click += ReduceTheSize_Click;
            BoxFont.Bold.Click += Bold_Click;
            BoxFont.Italic.Click += Italic_Click;
            BoxFont.Underlined.Click += Underlined_Click;
            BoxFont.StrikeOut.Click += StrikeOut_Click;
            BoxFont.Subscripr.Click += Subscripr_Click;
            BoxFont.Superscript.Click += Superscript_Click;
            BoxFont.TextColor.Click += TextColor_Click;
            BoxFont.TextBackground.Click += TextBackground_Click;

            BoxParagraph.DecreaseIndent.Click += DecreaseIndent_Click;
            BoxParagraph.IncreaseIndent.Click += IncreaseIndent_Click;
            BoxParagraph.StartList.Click += StartList_Click;
            BoxParagraph.LineSpacing.Click += LineSpacing_Click;
            BoxParagraph.LeftButton.Click += LeftButton_Click;
            BoxParagraph.CentreButton.Click += CentreButton_Click;
            BoxParagraph.RightButton.Click += RightButton_Click;
            BoxParagraph.AlignButton.Click += AlignButton_Click;
            BoxParagraph.ParagraphButton.Click += ParagraphButton_Click;

            newMenu.Create.Click += Create_Click;
            newMenu.Open.Click += Open_Click;
            newMenu.Save.Click += Save_Click;
            newMenu.PrintPageSettings.Click += PrintPageSettings_Click;
            newMenu.SendByEmail.Click += SendByEmail_Click;
            newMenu.AboutTheProgram.Click += AboutTheProgram_Click;
            newMenu.Exit.Click += Exit_Click;
            newMenu.RegularPrinting.Click += RegularPrinting_Click;
            newMenu.FastPrinting.Click += FastPrinting_Click;
            newMenu.Preview.Click += Preview_Click;
            newMenu.RTF_document.Click += RTF_document_Click;
            newMenu.OfficeOpenXML_document.Click += OfficeOpenXML_document_Click;
            newMenu.TextOpenDocument.Click += TextOpenDocument_Click;
            newMenu.PlainText.Click += PlainText_Click;
            newMenu.Save_As.Click += Save_As_Click;
        }
        #endregion
        #region Group Box Paragraph
        private void ParagraphButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AlignButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RightButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CentreButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void LeftButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void LineSpacing_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void StartList_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void IncreaseIndent_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DecreaseIndent_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Group Box Font
        private void TextBackground_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK) text.SelectionBackColor = colorDialog.Color;
        }

        private void TextColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK) text.SelectionColor = colorDialog.Color;
        }

        private void Superscript_Click(object sender, EventArgs e)
        {
            if (BoxFont.Superscript.Checked)
            {
                BoxFont.Subscripr.Checked = false;
                text.SelectionCharOffset = (int)BoxFont.FontSize.SelectedItem / 2;
            }
            else {
                text.SelectionCharOffset = 0;
                FontSizeChanged();
            }
        }

        private void Subscripr_Click(object sender, EventArgs e)
        {

            if (BoxFont.Subscripr.Checked)
            {
                BoxFont.Superscript.Checked = false;
                text.SelectionCharOffset = -(int)BoxFont.FontSize.SelectedItem / 2;
            }
            else {
                text.SelectionCharOffset = 0;
                FontSizeChanged();
            } 
        }

        private void StrikeOut_Click(object sender, EventArgs e) { FontStyleCheck(); }
        private void Underlined_Click(object sender, EventArgs e) { FontStyleCheck(); }
        private void Italic_Click(object sender, EventArgs e) { FontStyleCheck(); }
        private void Bold_Click(object sender, EventArgs e) { FontStyleCheck(); }
        private void FontStyleCheck()
        {
            Font font = text.SelectionFont;
            bool bold = BoxFont.Bold.Checked;
            bool italic = BoxFont.Italic.Checked;
            bool underlined = BoxFont.Underlined.Checked;
            bool strikeout = BoxFont.StrikeOut.Checked;
            if (bold && italic && underlined && strikeout) text.SelectionFont = new Font(font.FontFamily, font.Size, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline | FontStyle.Strikeout);
            else if (!bold && italic && underlined && strikeout) text.SelectionFont = new Font(font.FontFamily, font.Size, FontStyle.Italic | FontStyle.Underline | FontStyle.Strikeout);
            else if(bold && !italic && underlined && strikeout) text.SelectionFont = new Font(font.FontFamily, font.Size, FontStyle.Bold | FontStyle.Underline | FontStyle.Strikeout);
            else if (bold && italic && !underlined && strikeout) text.SelectionFont = new Font(font.FontFamily, font.Size, FontStyle.Bold | FontStyle.Italic | FontStyle.Strikeout);
            else if (bold && italic && underlined && !strikeout) text.SelectionFont = new Font(font.FontFamily, font.Size, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline );
            else if (!bold && !italic && underlined && strikeout) text.SelectionFont = new Font(font.FontFamily, font.Size, FontStyle.Underline | FontStyle.Strikeout);
            else if (bold && !italic && !underlined && strikeout) text.SelectionFont = new Font(font.FontFamily, font.Size, FontStyle.Bold | FontStyle.Strikeout);
            else if (bold && italic && !underlined && !strikeout) text.SelectionFont = new Font(font.FontFamily, font.Size, FontStyle.Bold | FontStyle.Italic);
            else if (!bold && italic && underlined && !strikeout) text.SelectionFont = new Font(font.FontFamily, font.Size, FontStyle.Italic | FontStyle.Underline);
            else if (!bold && !italic && !underlined && strikeout) text.SelectionFont = new Font(font.FontFamily, font.Size, FontStyle.Strikeout);
            else if (bold && !italic && !underlined && !strikeout) text.SelectionFont = new Font(font.FontFamily, font.Size, FontStyle.Bold);
            else if (!bold && !italic && underlined && !strikeout) text.SelectionFont = new Font(font.FontFamily, font.Size, FontStyle.Underline);
            else if (!bold && italic && !underlined && !strikeout) text.SelectionFont = new Font(font.FontFamily, font.Size, FontStyle.Italic);
            else if (!bold && !italic && !underlined && !strikeout) text.SelectionFont = new Font(font.FontFamily, font.Size, FontStyle.Regular);
        }

        private void ReduceTheSize_Click(object sender, EventArgs e)
        {
            if(BoxFont.FontSize.SelectedIndex > 0)
            {
                BoxFont.FontSize.SelectedIndex = BoxFont.FontSize.SelectedIndex - 1;
                FontSizeChanged();
            }
        }

        private void IncreaseTheSize_Click(object sender, EventArgs e)
        {
            if (BoxFont.FontSize.SelectedIndex < 14)
            {
                BoxFont.FontSize.SelectedIndex = BoxFont.FontSize.SelectedIndex + 1;
                FontSizeChanged();
            }
        }
        private void FontName(object sender, EventArgs e)
        {
            Font font = text.SelectionFont;
            text.SelectionFont = new Font(BoxFont.FontName.SelectedItem.ToString(), font.Size, font.Style);
        }
        private void FontSize(object sender, EventArgs e)
        {
            FontSizeChanged();
        }
        private void FontSizeChanged()
        {
            Font font = text.SelectionFont;
            int newSize = (int)BoxFont.FontSize.SelectedItem;
            text.SelectionFont = new Font(font.FontFamily, newSize, font.Style);
        }
        #endregion
        #region Group Box Clicboard
        private void NewRichTextBox_SelectionChanged(object sender, EventArgs e)
        {
            if (text.SelectionLength > 0) BoxClipboard.TextChange.Enabled = true;
            else BoxClipboard.TextChange.Enabled = false;
            if (text.Text.Length > 0) BoxEditing.Editing.Enabled = true;
            else BoxEditing.Editing.Enabled = false;
        }

        private void Insert(object sender, EventArgs e) { SendKeys.Send("^v"); }
        private void Cut(object sender, EventArgs e) { SendKeys.Send("^x"); }
        private void Copy(object sender, EventArgs e) { SendKeys.Send("^c"); }
        #endregion
        #region Menu
        private void Save_As_Click(object sender, EventArgs e)
        {
            if (text.Text != "")
            {
                saveFileDialog.ShowDialog();
                string new_path = saveFileDialog.FileName;
                if (new_path != "")
                {
                    Path = new_path;
                    File.WriteAllText(new_path, text.Text);
                }
                saveFileDialog.Reset();
            }
        }

        private void PlainText_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TextOpenDocument_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OfficeOpenXML_document_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RTF_document_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Preview_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FastPrinting_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RegularPrinting_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AboutTheProgram_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SendByEmail_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void PrintPageSettings_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (text.Text != "")
            {
                if (Path != "")
                {
                    if (text.Text != File.ReadAllText(Path))
                    {
                        File.WriteAllText(Path, text.Text);
                    }
                }
                else Save_As_Click(sender, e);
            }
        }

        private void Open_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = $"All documents WordPad (*.rtf,*.docx,*.odt,*.txt)|*.rtf;*.docx;*.odt;*.txt|{fileFormatFilter}";
            openFileDialog.ShowDialog();
            string path = "";
            path += openFileDialog.FileName;
            if (path != "")
            {
                Text = openFileDialog.SafeFileName + " - WordPad";
                Path = path;
                text.Text = File.ReadAllText(path);
            }
            openFileDialog.Reset();
        }

        private void Create_Click(object sender, EventArgs e)
        {
            if(text.Text != "")                                                     // Write
            {
                if (Path == "")
                {
                    if (text.Text != File.ReadAllText(Path))
                    {
                        File.WriteAllText(Path, text.Text);
                        text.Text = "";
                        Path = "";
                    }
                }
                else Save_As_Click(sender, e);
                
            }
        }
        #endregion
    }
}
