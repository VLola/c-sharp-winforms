﻿using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Project_56.Forms
{
    public partial class Task4 : Form
    {
        ManualResetEvent manualReset = new ManualResetEvent(true);
        ManualResetEvent manualResetFibonacci = new ManualResetEvent(true);
        private Thread thread;
        private Thread thread_fibonacci;
        private Button button = new Button();
        private Button button_fibonacci = new Button();
        private Button button_stop = new Button();
        private Button button_fibonacci_stop = new Button();
        private CheckBox pause = new CheckBox();
        private CheckBox fibonacci_pause = new CheckBox();
        private TextBox text_start = new TextBox();
        private TextBox text_end = new TextBox();
        private Label label_start = new Label();
        private Label label_end = new Label();
        private TextBox number = new TextBox();
        private TextBox number_fibonacci = new TextBox();
        private Label number_output = new Label();
        private Label number_output_fibonaci = new Label();
        private bool check_close_form = false;
        public Task4()
        {
            InitializeComponent();

            button.Text = "Start";
            button.Location = new Point(10, 10);
            button.Click += Start_Click;

            button_fibonacci.Text = "Start Fibonacci";
            button_fibonacci.AutoSize = true;
            button_fibonacci.Location = new Point(100, 10);
            button_fibonacci.Click += StartFibonacci_Click;

            button_stop.Text = "Stop";
            button_stop.Location = new Point(10, 35);
            button_stop.Click += Stop_Click;

            button_fibonacci_stop.Text = "Stop Fibonacci";
            button_fibonacci_stop.AutoSize = true;
            button_fibonacci_stop.Location = new Point(100, 35);
            button_fibonacci_stop.Click += StopFibonacci_Click;

            pause.Text = "Pause";
            pause.AutoSize = true;
            pause.Location = new Point(200, 170);
            pause.CheckedChanged += Pause_Click;

            fibonacci_pause.Text = "Pause";
            fibonacci_pause.AutoSize = true;
            fibonacci_pause.Location = new Point(200, 215);
            fibonacci_pause.CheckedChanged += PauseFibonacci_Click;


            text_start.Location = new Point(70, 60);

            text_end.Location = new Point(70, 110);

            label_start.Text = "Start:";
            label_start.Location = new Point(10, 65);

            label_end.Text = "End:";
            label_end.Location = new Point(10, 115);

            number.Location = new Point(70, 165);
            number.ReadOnly = true;

            number_fibonacci.Location = new Point(70, 210);
            number_fibonacci.ReadOnly = true;

            number_output.Text = "Number:";
            number_output.Location = new Point(10, 170);

            number_output_fibonaci.Text = "Fibonacci:";
            number_output_fibonaci.Location = new Point(10, 215);


            Controls.Add(button);
            Controls.Add(text_start);
            Controls.Add(text_end);
            Controls.Add(label_start);
            Controls.Add(label_end);
            Controls.Add(number);
            Controls.Add(number_output);
            Controls.Add(button_fibonacci);
            Controls.Add(number_fibonacci);
            Controls.Add(number_output_fibonaci);
            Controls.Add(button_stop);
            Controls.Add(button_fibonacci_stop);
            Controls.Add(pause);
            Controls.Add(fibonacci_pause);
            FormClosed += Task1_FormClosed;
        }


        private void Task1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (fibonacci_pause.Checked) manualResetFibonacci.Set();
            if (pause.Checked) manualReset.Set();
            check_close_form = true;
        }

        private void PauseFibonacci_Click(object sender, EventArgs e)
        {
            if (thread_fibonacci != null)
            {
                if (thread_fibonacci.ThreadState != ThreadState.Unstarted)
                {
                    if (fibonacci_pause.Checked) manualResetFibonacci.Reset();
                    else manualResetFibonacci.Set();
                }
            }
        }
        private void Pause_Click(object sender, EventArgs e)
        {
            if (thread != null)
            {
                if(thread.ThreadState != ThreadState.Unstarted)
                {
                    if (pause.Checked) manualReset.Reset();
                    else manualReset.Set();
                }
            }

        }
        private void StopFibonacci_Click(object sender, EventArgs e)
        {
            thread_fibonacci.Abort();
        }
        private void Stop_Click(object sender, EventArgs e)
        {
            thread.Abort();
        }
        private void Start_Click(object sender, EventArgs e)
        {
            uint start_number;
            uint end_number;
            if (text_start.Text == "" || text_start.Text == "0") start_number = 2u;
            else start_number = Convert.ToUInt32(text_start.Text);

            if (text_end.Text == "" || text_end.Text == "0") end_number = 0u;
            else end_number = Convert.ToUInt32(text_end.Text);
            thread = new Thread(() => { Generation(start_number, end_number); });
            thread.Start();
        }
        private void StartFibonacci_Click(object sender, EventArgs e)
        {
            uint start_number;
            uint end_number;
            if (text_start.Text == "" || text_start.Text == "0") start_number = 2u;
            else start_number = Convert.ToUInt32(text_start.Text);

            if (text_end.Text == "" || text_end.Text == "0") end_number = 0u;
            else end_number = Convert.ToUInt32(text_end.Text);
            thread_fibonacci = new Thread(() => { GenerationFibonacci(start_number, end_number); });
            thread_fibonacci.Start();
        }
        private void Generation(uint start_number, uint end_number)
        {
            if (end_number != 0u)
            {
                for (var i = start_number; i <= end_number; i++)
                {
                    if (check_close_form) break;
                    if (IsPrimeNumber(i)) Invoke(new Action(() => { ChangeText(i.ToString()); }));
                    Thread.Sleep(100);
                    manualReset.WaitOne();
                }
            }
            else
            {
                uint i = start_number;
                while (!check_close_form)
                {
                    if (IsPrimeNumber(i)) Invoke(new Action(() => { ChangeText(i.ToString()); }));
                    i++;
                    Thread.Sleep(100);
                    manualReset.WaitOne();
                }
            }
        }
        private void GenerationFibonacci(uint start_number, uint end_number)
        {
            if (end_number != 0u)
            {
                for (var i = start_number; i <= end_number; i++)
                {
                    if (check_close_form) break;
                    Invoke(new Action(() => { ChangeTextFibonacci(isFibonacci(i).ToString()); }));
                    Thread.Sleep(100);
                    manualResetFibonacci.WaitOne();
                }
            }
            else
            {
                uint i = start_number;
                while (!check_close_form)
                {
                    Invoke(new Action(() => { ChangeTextFibonacci(isFibonacci(i).ToString()); }));
                    i++;
                    Thread.Sleep(100);
                    manualResetFibonacci.WaitOne();
                }
            }
        }
        private void ChangeText(string text)
        {
            number.Text = text;
        }
        private void ChangeTextFibonacci(string text)
        {
            number_fibonacci.Text = text;
        }

        public static bool IsPrimeNumber(uint n)
        {
            var result = true;

            if (n > 1)
            {
                for (var i = 2u; i < n; i++)
                {
                    if (n % i == 0)
                    {
                        result = false;
                        break;
                    }
                }
            }
            else
            {
                result = false;
            }

            return result;
        }
        public static uint isFibonacci(uint n)
        {
            uint a = 0;
            uint b = 1;

            for (uint i = 0; i < n; i++)
            {
                uint temp = a;
                a = b;
                b = temp + b;
            }
            return a;
        }
    }
}
