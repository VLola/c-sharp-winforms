﻿using Project_60.MyControls;
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
            AddButton();
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
            FoodControl foodControl = new FoodControl((e.X, e.Y), Height - 100);
            collection_control.Add(foodControl);
            Controls.Add(collection_control[collection_control.Count - 1]);
        }

        private void AddButton()
        {
            Button button = new Button();
            button.Location = new Point(10, 10);
            button.Text = "add fish";
            button.AutoSize = true;
            button.Click += Button_Click;
            Controls.Add(button);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            AddFish();
        }
        
        private void AddBackground()
        {
            Bitmap objBitmap = new Bitmap(Properties.Resources.fon, new Size(Width, Height));
            BackgroundImage = objBitmap;
        }
        private void AddFish()
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
            fishControl.Location = new Point(RandomNumber(0, Width - 100), RandomNumber(0, Height - 100));
            Controls.Add(fishControl);
        }
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public static int RandomNumber(int min, int max)
        {
            lock (syncLock) return random.Next(min, max);
        }
    }
}
