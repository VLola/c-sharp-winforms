using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_60.MyControls
{
    public partial class FishControl : UserControl
    {
        private int FormWidth { get; set; }
        private int FormHeidth { get; set; }
        private bool isRigth { get; set; } = true;
        private int Count = 0;
        private Timer timer = new Timer();
        private List<Image> RigthImages { get; set; }
        private List<Image> LeftImages { get; set; }
        private PictureBox pictureBox { get; set; } = new PictureBox();
        public FishControl(List<Image> rigth, List<Image> left, int width, int heidth)
        {
            InitializeComponent();
            FormWidth = width;
            FormHeidth = heidth;
            DoubleBuffered = true;
            Size = new Size(100, 100);
            BackColor = Color.Transparent;
            RigthImages = rigth;
            LeftImages = left;
            pictureBox.Image = RigthImages[0];
            pictureBox.Location = new Point(0, 0);
            pictureBox.Size = new Size(100, 100);
            pictureBox.BackColor = Color.Transparent;
            Controls.Add(pictureBox);
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            RelocationFish();
            if (Count >= RigthImages.Count - 1) Count = 0;
            else Count++;
            if (isRigth) pictureBox.Image = RigthImages[Count];
            else pictureBox.Image = LeftImages[Count];
        }

        private async void RelocationFish()
        {
            await Task.Run(FishMove);
        }
        private void FishMove()
        {
            if (isRigth) Location = new Point(Location.X + 1, Location.Y);
            else Location = new Point(Location.X - 1, Location.Y);
            if (Location.X >= FormWidth - 100) isRigth = false;
            if (Location.X <= 0) isRigth = true;
        }
    }
}
