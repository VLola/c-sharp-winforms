using System;
using System.Windows.Forms;

namespace Project_46.Forms.Controls
{
    public class NewMenu:MenuStrip
    {
        public ToolStripMenuItem File;

        public ToolStripMenuItem New;
        public ToolStripMenuItem Open;
        public ToolStripMenuItem Close;
        public ToolStripMenuItem CloseAll;
        public ToolStripMenuItem Save;
        public ToolStripMenuItem SaveAll;
        public ToolStripMenuItem SaveAs;
        public ToolStripMenuItem Print;

        public ToolStripMenuItem Edit;

        public ToolStripMenuItem Undo;
        public ToolStripMenuItem Redo;
        public ToolStripMenuItem Cut;
        public ToolStripMenuItem Copy;
        public ToolStripMenuItem Paste;
        public ToolStripMenuItem Delete;
        public ToolStripMenuItem SelectAll;
        public NewMenu(Form1 form)
        {
            Items.AddRange(new ToolStripItem[] { File = new ToolStripMenuItem("File") });

            File.DropDownItems.AddRange(new ToolStripItem[] {
                New = new ToolStripMenuItem("New")
                ,Open = new ToolStripMenuItem("Open")
                ,Close = new ToolStripMenuItem("Close")
                ,CloseAll = new ToolStripMenuItem("Close All")
                ,Save = new ToolStripMenuItem("Save")
                ,SaveAll = new ToolStripMenuItem("Save All")
                ,SaveAs = new ToolStripMenuItem("Save As")
                ,Print = new ToolStripMenuItem("Print") 
            });

            Items.AddRange(new ToolStripItem[] { Edit = new ToolStripMenuItem("Edit") });

            Edit.DropDownItems.AddRange(new ToolStripItem[] {
                Undo = new ToolStripMenuItem("Undo"),
                Redo = new ToolStripMenuItem("Redo"),
                Cut = new ToolStripMenuItem("Cut"),
                Copy = new ToolStripMenuItem("Copy"),
                Paste = new ToolStripMenuItem("Paste"),
                Delete = new ToolStripMenuItem("Delete"),
                SelectAll = new ToolStripMenuItem("Select All")
            });

            form.Controls.Add(this);

        }
    }
}
