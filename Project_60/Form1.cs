using Project_60.MyControls;
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

namespace Project_60
{
    public partial class Form1 : Form
    {
        public ObservableCollection<FoodControl> collection_control = new ObservableCollection<FoodControl>();
        public Timer timer = new Timer();
        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;
            AddBackground();
            MouseClick += Form1_MouseClick;

            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(collection_control.Count > 0)
            {
                foreach (var item in collection_control)
                {
                    if (item.Eaten)
                    {
                        Controls.Remove(item);
                        collection_control.Remove(item);
                        break;
                    }
                }
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) AddFish(e.X, e.Y);
            else if (e.Button == MouseButtons.Right) AddFood(e.X, e.Y);
            
        }

        private void AddBackground()
        {
            Bitmap objBitmap = new Bitmap(Properties.Resources.fon, new Size(Width, Height));
            BackgroundImage = objBitmap;
        }
        private void AddFood(int X, int Y)
        {
            FoodControl foodControl = new FoodControl((X, Y), Height - 150);
            collection_control.Add(foodControl);
            Controls.Add(collection_control[collection_control.Count - 1]);
        }
        private void AddFish(int X, int Y)
        {
            List<Image> rigth = new List<Image>();
            rigth.Add(new Bitmap(Properties.Resources.green_rigth_1, new Size(100, 100)));
            rigth.Add(new Bitmap(Properties.Resources.green_rigth_2, new Size(100, 100)));
            rigth.Add(new Bitmap(Properties.Resources.green_rigth_3, new Size(100, 100)));
            rigth.Add(new Bitmap(Properties.Resources.green_rigth_4, new Size(100, 100)));
            rigth.Add(new Bitmap(Properties.Resources.green_rigth_5, new Size(100, 100)));
            rigth.Add(new Bitmap(Properties.Resources.green_rigth_6, new Size(100, 100)));
            List<Image> left = new List<Image>();
            left.Add(new Bitmap(Properties.Resources.green_left_1, new Size(100, 100)));
            left.Add(new Bitmap(Properties.Resources.green_left_2, new Size(100, 100)));
            left.Add(new Bitmap(Properties.Resources.green_left_3, new Size(100, 100)));
            left.Add(new Bitmap(Properties.Resources.green_left_4, new Size(100, 100)));
            left.Add(new Bitmap(Properties.Resources.green_left_5, new Size(100, 100)));
            left.Add(new Bitmap(Properties.Resources.green_left_6, new Size(100, 100)));
            FishControl fishControl = new FishControl(rigth, left, Width - 100, Height - 100, ref collection_control);
            fishControl.Location = new Point(X, Y);
            Controls.Add(fishControl);
        }
    }
}
