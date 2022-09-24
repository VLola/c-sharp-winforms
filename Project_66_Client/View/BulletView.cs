using Project_66_Client.Model;

namespace Project_66_Client.View
{
    public class BulletView : Control
    {
        public BulletView()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;
            Size = new Size(5, 5);
            BackColor = Color.White;
            Enabled = false;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphics.Clear(Parent.BackColor);
            Rectangle rectangle = new Rectangle(0, 0, Width - 1, Height - 1);
            graphics.DrawRectangle(new Pen(Color.Black), rectangle);
            graphics.FillRectangle(new SolidBrush(BackColor), rectangle);
        }
        public void Loading(BulletModel value)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        Location = new(value.X, value.Y);
                    }));
                }
                else
                {
                    Location = new(value.X, value.Y);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
