using System.Drawing.Drawing2D;

namespace Project_66_Client.View
{
    public class HealthView: Control
    {
        public HealthView()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;
            Location = new Point(15, 24);
            Size = new Size(20, 2);
            Enabled = false;
            BackColor = Color.Red;
        }
    }
}
