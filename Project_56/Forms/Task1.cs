using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Project_56.Forms
{
    public partial class Task1 : Form
    {
        private Button button = new Button();
        private TextBox text_start = new TextBox();
        private TextBox text_end = new TextBox();
        private Label label_start = new Label();
        private Label label_end = new Label();
        private TextBox number = new TextBox();
        private Label number_output = new Label();
        private bool check_close_form = false;
        public Task1()
        {
            InitializeComponent();

            button.Text = "Start";
            button.Size = new Size(75, 23);
            button.Location = new Point(10, 10);
            button.Click += Start_Click;

            text_start.Size = new Size(75, 23);
            text_start.Location = new Point(60, 60);

            text_end.Size = new Size(75, 23);
            text_end.Location = new Point(60, 110);

            label_start.Text = "Start:";
            label_start.Location = new Point(10, 65);

            label_end.Text = "End:";
            label_end.Location = new Point(10, 115);

            number.Size = new Size(75, 23);
            number.Location = new Point(60, 165);
            number.ReadOnly = true;

            number_output.Text = "number:";
            number_output.Location = new Point(10, 170);


            Controls.Add(button);
            Controls.Add(text_start);
            Controls.Add(text_end);
            Controls.Add(label_start);
            Controls.Add(label_end);
            Controls.Add(number);
            Controls.Add(number_output);
            FormClosed += Task1_FormClosed;
        }


        private void Task1_FormClosed(object sender, FormClosedEventArgs e)
        {
            check_close_form = true;
        }

        private void Start_Click(object sender, EventArgs e)
        {
            uint start_number;
            uint end_number;
            if (text_start.Text == "" || text_start.Text == "0") start_number = 2u;
            else start_number = Convert.ToUInt32(text_start.Text);

            if (text_end.Text == "" || text_end.Text == "0") end_number = 0u;
            else end_number = Convert.ToUInt32(text_end.Text);
            new Thread(()=> { Generation(start_number, end_number); }).Start();
        }
        private void Generation(uint start_number, uint end_number)
        {
            if (end_number != 0u)
            {
                for (var i = start_number; i < end_number; i++)
                {
                    if (check_close_form) break;
                    if (IsPrimeNumber(i))
                    {
                        if (number.InvokeRequired)
                        {
                            number.Invoke(new Action(() => { ChangeText(i.ToString()); }));
                        }

                    }
                }
            }
            else {
                uint i = start_number;
                while (!check_close_form)
                {
                    if (IsPrimeNumber(i))
                    {
                        if (number.InvokeRequired)
                        {
                            number.Invoke(new Action(()=> { ChangeText(i.ToString()); }));
                        }

                    }
                    i++;
                }
            } 
        }
        private void ChangeText(string text)
        {
            number.Text = text;
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
    }
}
