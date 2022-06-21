using Project_47;
using Project_47.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project_47.Forms.Controls
{
    public class NewMenu:MenuStrip
    {
        public ToolStripMenuItem File;

        public ToolStripMenuItem Create;
        public ToolStripMenuItem Open;
        public ToolStripMenuItem Save;
        public ToolStripMenuItem PrintPageSettings;
        public ToolStripMenuItem SendByEmail;
        public ToolStripMenuItem AboutTheProgram;
        public ToolStripMenuItem Exit;
        // Print
        public ToolStripMenuItem Print;
        public ToolStripMenuItem RegularPrinting;
        public ToolStripMenuItem FastPrinting;
        public ToolStripMenuItem Preview;
        // Save As
        public ToolStripMenuItem SaveAs;
        public ToolStripMenuItem RTF_document;
        public ToolStripMenuItem OfficeOpenXML_document;
        public ToolStripMenuItem TextOpenDocument;
        public ToolStripMenuItem PlainText;
        public ToolStripMenuItem Save_As;

        public PanelMain panelMain;
        public PanelView panelView;

        public ToolStripMenuItem HomeToolStripMenuItem;
        public ToolStripMenuItem ViewToolStripMenuItem;
        public NewMenu(Form1 form)
        {
            ImageScalingSize = new Size(36, 36);
            BackColor = Color.White;

            Items.AddRange(new ToolStripItem[] { File = new ToolStripMenuItem("File"), HomeToolStripMenuItem = new ToolStripMenuItem("Home"), ViewToolStripMenuItem = new ToolStripMenuItem("View") });

            File.DropDownItems.AddRange(new ToolStripItem[] {
                Create = new NewToolStripMenuItem("Create", Resources.Create)
                ,Open = new NewToolStripMenuItem("Open", Resources.Open)
                ,Save = new NewToolStripMenuItem("Save", Resources.Save)
                ,SaveAs = new NewToolStripMenuItem("Save As", Resources.SaveAs)
                ,new ToolStripSeparator()
                ,Print = new NewToolStripMenuItem("Print", Resources.Print)
                ,PrintPageSettings = new NewToolStripMenuItem("Page settings", Resources.PrintPageSettings)
                ,SendByEmail = new NewToolStripMenuItem("Send by email", Resources.SendByEmail)
                ,new ToolStripSeparator()
                ,AboutTheProgram = new NewToolStripMenuItem("About the program", Resources.AboutTheProgram)
                ,Exit = new NewToolStripMenuItem("Exit", Resources.Exit)
            });

            SaveAs.DropDownItems.AddRange(new ToolStripItem[] {
                new NewToolStripTextBox("Save a copy of the document")
                ,new ToolStripSeparator()
                ,RTF_document = new NewToolStripMenuItem("RTF document", Resources.RTF_document)
                ,OfficeOpenXML_document = new NewToolStripMenuItem("OfficeOpen XML document", Resources.OfficeOpenXML_document)
                ,TextOpenDocument = new NewToolStripMenuItem("Text OpenDocument", Resources.TextOpenDocument)
                ,PlainText = new NewToolStripMenuItem("Plain text", Resources.PlainText)
                ,Save_As = new NewToolStripMenuItem("Other formats", Resources.SaveAs)
            });

            Print.DropDownItems.AddRange(new ToolStripItem[] {
                new NewToolStripTextBox("Document preview and printing")
                ,new ToolStripSeparator()
                ,RegularPrinting = new NewToolStripMenuItem("Print", Resources.Print)
                ,FastPrinting = new NewToolStripMenuItem("FastPrinting", Resources.FastPrinting)
                ,Preview = new NewToolStripMenuItem("Preview", Resources.Preview)
            });

            panelMain = new PanelMain();
            panelMain.Visible = true;

            panelView = new PanelView();
            panelView.Visible = false;

            HomeToolStripMenuItem.Click += new EventHandler(Home);
            ViewToolStripMenuItem.Click += new EventHandler(View);
            form.Controls.Add(this);
            form.Controls.Add(panelMain);
            form.Controls.Add(panelView);
        }
        private void Home(object sender, EventArgs e)
        {
            panelMain.Visible = true;
            panelView.Visible = false;
        }

        private void View(object sender, EventArgs e)
        {
            panelMain.Visible = false;
            panelView.Visible = true;
        }
    }
}
