using Project_56.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_56
{
    public partial class Form1 : Form
    {
        private Button ButtonTask1 = new Button();
        private Button ButtonTask2 = new Button();
        private Button ButtonTask3 = new Button();
        private Button ButtonTask4 = new Button();
        private Button ButtonTask5 = new Button();
        public Form1()
        {
            InitializeComponent();

            ButtonTask1.Text = "Task 1";
            ButtonTask1.Size = new Size(75, 23);
            ButtonTask1.Location = new Point(50,10);
            ButtonTask1.Click += Task1_Click;

            ButtonTask2.Text = "Task 2";
            ButtonTask2.Size = new Size(75, 23);
            ButtonTask2.Location = new Point(50, 60);
            ButtonTask2.Click += Task2_Click;

            ButtonTask3.Text = "Task 3";
            ButtonTask3.Size = new Size(75, 23);
            ButtonTask3.Location = new Point(50, 110);
            ButtonTask3.Click += Task3_Click;

            ButtonTask4.Text = "Task 4";
            ButtonTask4.Size = new Size(75, 23);
            ButtonTask4.Location = new Point(50, 160);
            ButtonTask4.Click += Task4_Click;

            ButtonTask5.Text = "Task 5";
            ButtonTask5.Size = new Size(75, 23);
            ButtonTask5.Location = new Point(50, 210);
            ButtonTask5.Click += Task5_Click;

            Controls.Add(ButtonTask1);
            Controls.Add(ButtonTask2);
            Controls.Add(ButtonTask3);
            Controls.Add(ButtonTask4);
            Controls.Add(ButtonTask5);
        }
        private void Task1_Click(object sender, EventArgs e)
        {
            Task1 task = new Task1();
            task.ShowDialog();
        }
        private void Task2_Click(object sender, EventArgs e)
        {
            Task2 task = new Task2();
            task.ShowDialog();
        }
        private void Task3_Click(object sender, EventArgs e)
        {
            Task3 task = new Task3();
            task.ShowDialog();
        }
        private void Task4_Click(object sender, EventArgs e)
        {
            Task4 task = new Task4();
            task.ShowDialog();
        }
        private void Task5_Click(object sender, EventArgs e)
        {
            Task5 task = new Task5();
            task.ShowDialog();
        }
    }
}
