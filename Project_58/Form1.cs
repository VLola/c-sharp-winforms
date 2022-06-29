using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_58
{
    public partial class Form1 : Form
    {
        Label label = new Label();
        Label label_errors = new Label();
        Label label_start = new Label();
        Label label_close = new Label();
        TextBox textBox = new TextBox();
        TextBox errors = new TextBox();
        Button button_start = new Button();
        Button button_close = new Button();
        ListBox list_start = new ListBox();
        ListBox list_close = new ListBox();
        public List<string> start { get; set; } = new List<string>();
        public List<string> close { get; set; } = new List<string>();
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
            label_errors.Location = new Point(20, 300);

            errors.Location = new Point(120, 300);
            errors.Size = new Size(Width - 200, Height - 400);
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

            label_start.Text = "White list:";
            label_start.Location = new Point(20, 100);

            label_close.Text = "Black list:";
            label_close.Location = new Point(320, 100);

            list_start.Location = new Point(120, 100);
            list_start.Width = (Width / 2) - 150;
            label_close.Location = new Point(list_start.Width + 150, 100);
            list_close.Location = new Point(list_start.Width + 250, 100);
            list_close.Width = (Width / 2) - 180;

            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
            SizeChanged += Form1_SizeChanged;

            Controls.Add(label);
            Controls.Add(label_errors);
            Controls.Add(textBox);
            Controls.Add(errors);
            Controls.Add(button_start);
            Controls.Add(button_close);
            Controls.Add(list_start);
            Controls.Add(list_close);
            Controls.Add(label_start);
            Controls.Add(label_close);
            StartProcessAsync();
            CloseProcessAsync();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            errors.Size = new Size(Width - 200, Height - 400);
            list_start.Width = (Width / 2) - 150;
            label_close.Location = new Point(list_start.Width + 150, 100);
            list_close.Location = new Point(list_start.Width + 250, 100);
            list_close.Width = (Width / 2) - 180;
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
                list_start.Items.Clear();
                list_start.Items.AddRange(start.ToArray());
                if(!close.Contains(textBox.Text)) close.Add(textBox.Text);
                list_close.Items.Clear();
                list_close.Items.AddRange(close.ToArray());
                textBox.Text = "";
            }
        }

        private void Button_start_Click(object sender, EventArgs e)
        {
            if (textBox.Text != "")
            {
                close.Remove(textBox.Text);
                list_close.Items.Clear();
                list_close.Items.AddRange(close.ToArray());
                if (!start.Contains(textBox.Text)) start.Add(textBox.Text);
                list_start.Items.Clear();
                list_start.Items.AddRange(start.ToArray());
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
                        string[] array = new string[0];
                        Invoke(new Action(() =>
                        {
                            array = start.ToArray();
                        }));
                        if(array.Length > 0)
                            foreach (var iterator in array)
                            {
                                bool check_start = false;
                                foreach (var it in Process.GetProcesses()) if (it.ProcessName == iterator) check_start = true;
                                if (!check_start) Process.Start(iterator);
                                await Task.Delay(1000);
                            }
                        await Task.Delay(1);
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
                        string[] array = new string[0];
                        Invoke(new Action(() =>
                        {
                            array = close.ToArray();
                        }));
                        if (array.Length > 0)
                            foreach (var iterator in array)
                            {
                                foreach (var it in Process.GetProcesses()) if (it.ProcessName == iterator) it.Kill();
                                await Task.Delay(1000);
                            }
                        await Task.Delay(1);
                    }
                }
                catch {
                    CloseProcessAsync();
                }
            });
        }
    }
}
