﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_58
{
    public partial class Form1 : Form
    {
        Label label = new Label();
        Label label_errors = new Label();
        TextBox textBox = new TextBox();
        TextBox errors = new TextBox();
        Button button_start = new Button();
        Button button_close = new Button();
        List<string> start = new List<string>();
        List<string> close = new List<string>();
        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label.Text = "Name programm:";
            label.Location = new Point(20, 20);

            textBox.Location = new Point(120, 15);

            label_errors.Text = "Errors:";
            label_errors.Location = new Point(20, 70);

            errors.Location = new Point(20, 100);
            errors.Size = new Size(Width - 100, Height - 200);
            errors.Multiline = true;
            errors.ScrollBars = ScrollBars.Vertical;

            button_start.Text = "Start programm";
            button_start.AutoSize = true;
            button_start.Location = new Point(250, 15);
            button_start.Click += Button_start_Click;

            button_close.Text = "Close programm";
            button_close.AutoSize = true;
            button_close.Location = new Point(350, 15);
            button_close.Click += Button_close_Click;

            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
            SizeChanged += Form1_SizeChanged;

            Controls.Add(label);
            Controls.Add(label_errors);
            Controls.Add(textBox);
            Controls.Add(errors);
            Controls.Add(button_start);
            Controls.Add(button_close);
            StartProcessAsync();
            CloseProcessAsync();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            errors.Size = new Size(Width - 100, Height - 200);
        }

        private void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {
            Invoke(new Action(() =>
            {
                errors.Text += e.Exception.Message + "\n";
            }));
        }

        private void Button_close_Click(object sender, EventArgs e)
        {
            if(textBox.Text != "")
            {
                start.Remove(textBox.Text);
                close.Add(textBox.Text);
                textBox.Text = "";
            }
        }

        private void Button_start_Click(object sender, EventArgs e)
        {
            if (textBox.Text != "")
            {
                close.Remove(textBox.Text);
                start.Add(textBox.Text);
                textBox.Text = "";
            }
        }

        private async void StartProcessAsync()
        {
            await Task.Run(async () => {
                try
                {
                    while (true)
                    {
                        List<string> list_start = new List<string>();
                        Invoke(new Action(() =>
                        {
                            list_start = start;
                        }));
                        foreach (var iterator in list_start)
                        {
                            bool check_start = false;
                            foreach (var it in Process.GetProcesses())
                            {
                                if (it.ProcessName == iterator)
                                {
                                    check_start = true;
                                }
                            }
                            if (!check_start) Process.Start(iterator);
                            await Task.Delay(1000);
                        }
                        await Task.Delay(1000);
                    }
                }
                catch {
                    StartProcessAsync();
                }
            });
        }

        private async void CloseProcessAsync()
        {
            await Task.Run(async () => {
                try
                {
                    while (true)
                    {
                        List<string> list_close = new List<string>();
                        Invoke(new Action(() =>
                        {
                            list_close = close;
                        }));
                        foreach (var iterator in list_close)
                        {
                            foreach (var it in Process.GetProcesses())
                            {
                                if (it.ProcessName == iterator)
                                {
                                    it.Kill();
                                }
                            }
                            await Task.Delay(1000);
                        }
                        await Task.Delay(1000);
                    }
                }
                catch {
                    CloseProcessAsync();
                }
            });
        }
    }
}
