using Project_66_Client.Model;
using System.Drawing.Drawing2D;

namespace Project_66_Client.View
{
    public class TankView : Control
    {
        public TankView()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;
            Location = new Point(0, 0);
            Size = new Size(50, 50);
            Enabled = false;
            BackColor = Color.ForestGreen;
        }
        public void Death()
        {
            BackColor = Color.Gray;
        }
        public void Life()
        {
            BackColor = Color.ForestGreen;
        }
        public void Up()
        {
            GraphicsPath myGraphicsPath = new GraphicsPath();
            myGraphicsPath.AddRectangle(new Rectangle(0, 5, 10, 44));
            myGraphicsPath.AddRectangle(new Rectangle(39, 5, 10, 44));
            myGraphicsPath.AddRectangle(new Rectangle(22, 0, 5, 8));
            myGraphicsPath.AddEllipse(new Rectangle(8, 8, 32, 32));
            Region = new Region(myGraphicsPath);
        }
        public void Down()
        {
            GraphicsPath myGraphicsPath = new GraphicsPath();
            myGraphicsPath.AddRectangle(new Rectangle(0, 0, 10, 44));
            myGraphicsPath.AddRectangle(new Rectangle(39, 0, 10, 44));
            myGraphicsPath.AddRectangle(new Rectangle(22, 40, 5, 8));
            myGraphicsPath.AddEllipse(new Rectangle(8, 8, 32, 32));
            Region = new Region(myGraphicsPath);
        }
        public void IsRight()
        {
            GraphicsPath myGraphicsPath = new GraphicsPath();
            myGraphicsPath.AddRectangle(new Rectangle(0, 0, 44, 10));
            myGraphicsPath.AddRectangle(new Rectangle(0, 39, 44, 10));
            myGraphicsPath.AddRectangle(new Rectangle(40, 22, 8, 5));
            myGraphicsPath.AddEllipse(new Rectangle(8, 8, 32, 32));
            Region = new Region(myGraphicsPath);
        }
        public void IsLeft()
        {
            GraphicsPath myGraphicsPath = new GraphicsPath();
            myGraphicsPath.AddRectangle(new Rectangle(5, 0, 44, 10));
            myGraphicsPath.AddRectangle(new Rectangle(5, 39, 44, 10));
            myGraphicsPath.AddRectangle(new Rectangle(0, 22, 8, 5));
            myGraphicsPath.AddEllipse(new Rectangle(8, 8, 32, 32));
            Region = new Region(myGraphicsPath);
        }
    }
}
