namespace Project_66_Client.View
{
    public class BrickView: Control
    {
        public BrickView()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;
            Location = new Point(0, 0);
            Size = new Size(25, 25);
            Enabled = false;
            BackColor = Color.DarkGray;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;
            Brush brush = new SolidBrush(Color.Brown);
            Brush brushOrange = new SolidBrush(Color.OrangeRed);
            // upper
            graphics.FillRectangle(brush, new Rectangle(0, 0, 13, 10));
            graphics.FillRectangle(brush, new Rectangle(15, 0, 10, 10));
            // lower
            graphics.FillRectangle(brush, new Rectangle(3, 13, 22, 10));
            // upper
            graphics.FillRectangle(brushOrange, new Rectangle(0, 2, 13, 8));
            graphics.FillRectangle(brushOrange, new Rectangle(18, 2, 7, 8));
            // lower
            graphics.FillRectangle(brushOrange, new Rectangle(6, 15, 19, 8));
        }
        public void Relocation(int x, int y)
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
