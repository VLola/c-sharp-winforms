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
        private int Step { get; set; } = 5;
        private int X { get; set; }
        private int Y { get; set; }
        private int FormWidth { get; set; }
        private int FormHeidth { get; set; }
        private bool _isUp { get; set; }
        private bool _isDown { get; set; }
        private bool _isRigth { get; set; }
        private bool _isLeft { get; set; }
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
            NextPoint();
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            RelocationFish();
        }

        private async void RelocationFish()
        {
            await Task.Run(FishMove);
        }
        private void FishMove()
        {
            if (Count >= RigthImages.Count - 1) Count = 0;
            else Count++;
            if (_isRigth) pictureBox.Image = RigthImages[Count];
            else if (_isLeft) pictureBox.Image = LeftImages[Count];
            if (_isRigth && _isUp)
            {
                if(Location.X < X && Location.Y > Y) Location = new Point(Location.X + Step, Location.Y - Step);
                else if(Location.X < X && Location.Y <= Y) Location = new Point(Location.X + Step, Location.Y);
                else if(Location.X >= X && Location.Y > Y) Location = new Point(Location.X, Location.Y - Step);
                else if(Location.X >= X && Location.Y <= Y) NextPoint();
            }
            else if (_isRigth && _isDown)
            {
                if (Location.X < X && Location.Y < Y) Location = new Point(Location.X + Step, Location.Y + Step);
                else if (Location.X < X && Location.Y >= Y) Location = new Point(Location.X + Step, Location.Y);
                else if (Location.X >= X && Location.Y < Y) Location = new Point(Location.X, Location.Y + Step);
                else if (Location.X >= X && Location.Y >= Y) NextPoint();
            }
            else if (_isLeft && _isUp)
            {
                if (Location.X > X && Location.Y > Y) Location = new Point(Location.X - Step, Location.Y - Step);
                else if (Location.X > X && Location.Y <= Y) Location = new Point(Location.X - Step, Location.Y);
                else if (Location.X <= X && Location.Y > Y) Location = new Point(Location.X, Location.Y - Step);
                else if (Location.X <= X && Location.Y <= Y) NextPoint();
            }
            else if (_isLeft && _isDown)
            {
                if (Location.X > X && Location.Y < Y) Location = new Point(Location.X - Step, Location.Y + Step);
                else if (Location.X > X && Location.Y >= Y) Location = new Point(Location.X - Step, Location.Y);
                else if (Location.X <= X && Location.Y < Y) Location = new Point(Location.X, Location.Y + Step);
                else if (Location.X <= X && Location.Y >= Y) NextPoint();
            }
        }
        private void NextPoint()
        {
            X = RandomNumber(0, FormWidth);
            Y = RandomNumber(0, FormHeidth);
            if (Location.X <= X)
            {
                _isLeft = false;
                _isRigth = true;
            }
            else if (Location.X > X)
            {
                _isRigth = false;
                _isLeft = true;
            }
            if (Location.Y >= Y)
            {
                _isDown = false;
                _isUp = true;
            }
            else if (Location.Y < Y)
            {
                _isUp = false;
                _isDown = true;
            }
        }
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public static int RandomNumber(int min, int max)
        {
            lock (syncLock) return random.Next(min, max);
        }
    }
}
