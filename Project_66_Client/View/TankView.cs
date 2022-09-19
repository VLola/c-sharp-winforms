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
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    BackColor = Color.Gray;
                }));
            }
            else
            {
                BackColor = Color.Gray;
            }
        }
        //public void Life()
        //{
        //    if (InvokeRequired)
        //    {
        //        Invoke(new Action(() =>
        //        {
        //            BackColor = Color.ForestGreen;
        //        }));
        //    }
        //    else
        //    {
        //        BackColor = Color.ForestGreen;
        //    }
        //}
        public void Up()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    GraphicsPath myGraphicsPath = new GraphicsPath();
                    myGraphicsPath.AddRectangle(new Rectangle(0, 5, 10, 44));
                    myGraphicsPath.AddRectangle(new Rectangle(39, 5, 10, 44));
                    myGraphicsPath.AddRectangle(new Rectangle(22, 0, 5, 8));
                    myGraphicsPath.AddEllipse(new Rectangle(8, 8, 32, 32));
                    Region = new Region(myGraphicsPath);
                }));
            }
            else
            {
                GraphicsPath myGraphicsPath = new GraphicsPath();
                myGraphicsPath.AddRectangle(new Rectangle(0, 5, 10, 44));
                myGraphicsPath.AddRectangle(new Rectangle(39, 5, 10, 44));
                myGraphicsPath.AddRectangle(new Rectangle(22, 0, 5, 8));
                myGraphicsPath.AddEllipse(new Rectangle(8, 8, 32, 32));
                Region = new Region(myGraphicsPath);
            }
        }
        public void Down()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    GraphicsPath myGraphicsPath = new GraphicsPath();
                    myGraphicsPath.AddRectangle(new Rectangle(0, 0, 10, 44));
                    myGraphicsPath.AddRectangle(new Rectangle(39, 0, 10, 44));
                    myGraphicsPath.AddRectangle(new Rectangle(22, 40, 5, 8));
                    myGraphicsPath.AddEllipse(new Rectangle(8, 8, 32, 32));
                    Region = new Region(myGraphicsPath);
                }));
            }
            else
            {
                GraphicsPath myGraphicsPath = new GraphicsPath();
                myGraphicsPath.AddRectangle(new Rectangle(0, 0, 10, 44));
                myGraphicsPath.AddRectangle(new Rectangle(39, 0, 10, 44));
                myGraphicsPath.AddRectangle(new Rectangle(22, 40, 5, 8));
                myGraphicsPath.AddEllipse(new Rectangle(8, 8, 32, 32));
                Region = new Region(myGraphicsPath);
            }
        }
        public void IsRight()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    GraphicsPath myGraphicsPath = new GraphicsPath();
                    myGraphicsPath.AddRectangle(new Rectangle(0, 0, 44, 10));
                    myGraphicsPath.AddRectangle(new Rectangle(0, 39, 44, 10));
                    myGraphicsPath.AddRectangle(new Rectangle(40, 22, 8, 5));
                    myGraphicsPath.AddEllipse(new Rectangle(8, 8, 32, 32));
                    Region = new Region(myGraphicsPath);
                }));
            }
            else
            {
                GraphicsPath myGraphicsPath = new GraphicsPath();
                myGraphicsPath.AddRectangle(new Rectangle(0, 0, 44, 10));
                myGraphicsPath.AddRectangle(new Rectangle(0, 39, 44, 10));
                myGraphicsPath.AddRectangle(new Rectangle(40, 22, 8, 5));
                myGraphicsPath.AddEllipse(new Rectangle(8, 8, 32, 32));
                Region = new Region(myGraphicsPath);
            }
        }
        public void IsLeft()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    GraphicsPath myGraphicsPath = new GraphicsPath();
                    myGraphicsPath.AddRectangle(new Rectangle(5, 0, 44, 10));
                    myGraphicsPath.AddRectangle(new Rectangle(5, 39, 44, 10));
                    myGraphicsPath.AddRectangle(new Rectangle(0, 22, 8, 5));
                    myGraphicsPath.AddEllipse(new Rectangle(8, 8, 32, 32));
                    Region = new Region(myGraphicsPath);
                }));
            }
            else
            {
                GraphicsPath myGraphicsPath = new GraphicsPath();
                myGraphicsPath.AddRectangle(new Rectangle(5, 0, 44, 10));
                myGraphicsPath.AddRectangle(new Rectangle(5, 39, 44, 10));
                myGraphicsPath.AddRectangle(new Rectangle(0, 22, 8, 5));
                myGraphicsPath.AddEllipse(new Rectangle(8, 8, 32, 32));
                Region = new Region(myGraphicsPath);
            }
        }
        public void AddHealth(HealthView healthView)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    Controls.Add(healthView);
                }));
            }
            else
            {
                Controls.Add(healthView);
            }
        }
        public void SetColor(Color color)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    BackColor = color;
                }));
            }
            else
            {
                BackColor = color;
            }
        }
        public void SetLocation(int x, int y)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    Location = new(x, y);
                }));
            }
            else
            {
                Location = new(x, y);
            }
        }
    }
}
