using Microsoft.Win32;
using Project_59.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_59
{
    public partial class Form1 : Form
    {
        private Timer timer_recoordinate = new Timer();
        private Timer timer_start = new Timer();
        private DateTime dateTime = DateTime.Now + TimeSpan.FromSeconds(30);
        private int Y = 70;
        private int X = 10;
        public Form1()
        {
            InitializeComponent();
            BackgroungImage();
            FromRegistry();
            timer_recoordinate.Tick += Timer_Tick;
            timer_recoordinate.Interval = 1000;
            timer_start.Tick += TimerStart_Tick;
            timer_start.Interval = 1000;
            timer_start.Start();
        }

        private void TimerStart_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now > dateTime && !timer_recoordinate.Enabled)
            {
                foreach (ProgramIcon programIcon in Controls)
                {
                    programIcon.MouseClick += ProgramIcon_MouseClick;
                }
                timer_recoordinate.Start();
                timer_start.Stop();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Controls.Count > 0)
            {
                foreach (ProgramIcon programIcon in Controls)
                {
                    (int x, int y) = RandomCoordinates();
                    programIcon.Location = new Point(x, y);
                }
            }
            else System.Diagnostics.Process.Start("shutdown", "/s /t /f 00");
        }
        private (int, int) RandomCoordinates()
        {
            int x = (Width - 100) / 70;
            int x_new = new Random().Next(1, x) * 70;
            int y = (Height - 100) / 70;
            int y_new = new Random().Next(1, y) * 70;
            return (x_new, y_new);
        }
        private void BackgroungImage()
        {
            RegistryKey registry = Registry.CurrentUser;
            RegistryKey myAppKey = registry.OpenSubKey(@"Control Panel\Desktop");
            string name = (string)myAppKey.GetValue("WallPaper");
            BackgroundImage = Image.FromFile(name);
        }
        private void FromRegistry()
        {
            int value = 10;
            RegistryKey registry1 = Registry.CurrentUser;
            value = Start(registry1, value);
            RegistryKey registry2 = Registry.LocalMachine;
            Start(registry2, value);
        }
        private int Start(RegistryKey registry, int value)
        {
            RegistryKey myAppKey = registry.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (var item in myAppKey.GetSubKeyNames())
            {
                RegistryKey AppKey = registry.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\" + item);
                string path = (string)AppKey.GetValue("DisplayIcon");
                string name = (string)AppKey.GetValue("DisplayName");
                if (path != null)
                {
                    if (path.Contains(".ico") && File.Exists(path))
                    {
                        if (value > Height - 100)
                        {
                            X += 70;
                            value = 10;
                        }
                        ProgramIcon programIcon = new ProgramIcon(Icon.ExtractAssociatedIcon(path).ToBitmap(), name);
                        programIcon.Location = new Point(X, value);
                        programIcon.Size = new Size(70, 70);
                        Controls.Add(programIcon);
                        value += Y;
                    }
                }
            }
            return value;
        }
        private void ProgramIcon_MouseClick(object sender, MouseEventArgs e)
        {
            Controls.Remove((ProgramIcon)sender);
        }
    }
}
