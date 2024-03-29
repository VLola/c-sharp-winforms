﻿using System;
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
    public partial class FoodControl : UserControl
    {
        public bool Ready { get; set; }
        public bool Selected { get; set; }
        public bool Eaten { get; set; }
        private int FormHeigth { get; set; }
        private Timer timer = new Timer();
        public FoodControl((int, int) location,int height)
        {
            FormHeigth = height;
            Location = new Point(location.Item1, location.Item2);
            BackColor = Color.Transparent;
            Size = new Size(20, 20);
            InitializeComponent();
            DoubleBuffered = true;
            PictureBox pictureBox = new PictureBox();
            pictureBox.BackColor = Color.Transparent;
            pictureBox.Image = RandomImage();
            pictureBox.BackColor = Color.Transparent;
            Controls.Add(pictureBox);
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Location.Y < FormHeigth)
            {
                Location = new Point(Location.X, Location.Y + 5);
            }
            else
            {
                Ready = true;
                timer.Stop();
            }
        }
        private static readonly Random random = new Random();
        public static Image RandomImage()
        {
            if (random.Next(1, 3) == 1) return new Bitmap(Properties.Resources.food_1, new Size(20, 20));
            else return new Bitmap(Properties.Resources.food_2, new Size(20, 20));
        }
    }
}
