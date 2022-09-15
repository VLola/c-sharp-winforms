using Project_66_Client.Model;

namespace Project_66_Client.View
{
    public class TankView : Control
    {
        public TankView()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;
            Size = new Size(50, 50);
            BackColor = Color.Red;
            Enabled = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphics.Clear(Parent.BackColor);
            Rectangle rectangle = new Rectangle(0, 0, Width - 1, Height - 1);
            graphics.DrawRectangle(new Pen(Color.Orange), rectangle);
            graphics.FillRectangle(new SolidBrush(BackColor), rectangle);
        }
        public void Loading(TankModel tankModel)
        {
            Location = new(tankModel.X, tankModel.Y);
        }
    }
}
