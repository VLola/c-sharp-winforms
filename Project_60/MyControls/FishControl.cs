using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<FoodControl> collection_control;
        public FishControl(List<Image> rigth, List<Image> left, int width, int heidth, ref ObservableCollection<FoodControl> collection_control)
        {
            InitializeComponent();
            Load += FishControl_Load;
            this.collection_control = collection_control;
            FormWidth = width;
            FormHeidth = heidth;
            RigthImages = rigth;
            LeftImages = left;
        }

        private void FishControl_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            Size = new Size(100, 100);
            BackColor = Color.Transparent;
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
            int LocationX = 0;
            int LocationY = 0;
            Invoke(new Action(()=> {
                LocationX = Location.X;
                LocationY = Location.Y;
            }));
            RelocationFish(LocationX, LocationY);
        }

        private async void RelocationFish(int LocationX, int LocationY)
        {
            await Task.Run(()=> {
                FishMove(LocationX, LocationY);
            });
            
        }
        private void FishMove(int LocationX, int LocationY)
        {
            if (Count >= RigthImages.Count - 1) Count = 0;
            else Count++;
            if (_isRigth) pictureBox.Image = RigthImages[Count];
            else if (_isLeft) pictureBox.Image = LeftImages[Count];
            if (_isRigth && _isUp)
            {
                if (LocationX < X && LocationY > Y) NextLocation(LocationX + Step, LocationY - Step);
                else if (LocationX < X && LocationY <= Y) NextLocation(LocationX + Step, Location.Y);
                else if (LocationX >= X && LocationY > Y) NextLocation(LocationX, LocationY - Step);
                else if (LocationX >= X && LocationY <= Y)
                {
                    if (_Move && collection_control.Count > 0) collection_control[0].Eaten = true;
                     _Move = false;
                    NextPoint();
                }
            }
            else if (_isRigth && _isDown)
            {
                if (LocationX < X && LocationY < Y) NextLocation(LocationX + Step, LocationY + Step);
                else if (LocationX < X && LocationY >= Y) NextLocation(LocationX + Step, LocationY);
                else if (LocationX >= X && LocationY < Y) NextLocation(LocationX, LocationY + Step);
                else if (LocationX >= X && LocationY >= Y)
                {
                    if (_Move && collection_control.Count > 0) collection_control[0].Eaten = true;
                    _Move = false;
                    NextPoint();
                }
            }
            else if (_isLeft && _isUp)
            {
                if (LocationX > X && LocationY > Y) NextLocation(LocationX - Step, LocationY - Step);
                else if (LocationX > X && LocationY <= Y) NextLocation(LocationX - Step, LocationY);
                else if (LocationX <= X && LocationY > Y) NextLocation(LocationX, LocationY - Step);
                else if (LocationX <= X && LocationY <= Y)
                {
                    if (_Move && collection_control.Count > 0) collection_control[0].Eaten = true;
                    _Move = false;
                    NextPoint();
                }
            }
            else if (_isLeft && _isDown)
            {
                if (LocationX > X && LocationY < Y) NextLocation(LocationX - Step, LocationY + Step);
                else if (LocationX > X && LocationY >= Y) NextLocation(LocationX - Step, LocationY);
                else if (LocationX <= X && LocationY < Y) NextLocation(LocationX, LocationY + Step);
                else if (LocationX <= X && LocationY >= Y)
                {
                    if (_Move && collection_control.Count > 0) collection_control[0].Eaten = true;
                    _Move = false;
                    NextPoint();
                }
            }
        }
        private void NextLocation(int LocationX, int LocationY)
        {
            Invoke(new Action(() => {
                Location = new Point(LocationX, LocationY);
            }));
        }
        bool _Move { get; set; }
        private void NextPoint()
        {
            if (collection_control.Count > 0)
            {
                foreach (var item in collection_control)
                {
                    if (!item.Selected && item.Ready)
                    {
                        item.Selected = true;
                        X = item.Location.X;
                        Y = item.Location.Y - 50;
                        _Move = true;
                        break;
                    }
                    
                }
            }
            if(!_Move)
            {
                X = RandomNumber(0, FormWidth);
                Y = RandomNumber(0, FormHeidth);
            }
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
