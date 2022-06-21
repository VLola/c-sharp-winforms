using System;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Speech.Synthesis;
using System.Threading;

namespace Project_43
{
    public partial class Form2 : Form
    {
        Thread thread;
        static SpeechSynthesizer synth;
        Cover cover;
        public string path = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName, "");
        public Form2(Cover COVER)
        {
            InitializeComponent();
            cover = COVER;
            ReadBook();
        }
        public void ReadBook()
        {
            double count = 1;
            string temp = "";
            cover.list = new ArrayList();
            foreach (string line in File.ReadLines(path + "/Books/" + cover.name + ".txt"))
            {
                temp += line;
                temp += Environment.NewLine;
                if (count % 15 == 0)
                {
                    cover.list.Add(temp);
                    temp = "";
                }
                count += 1;
            }

            SetBook();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cover.page < cover.list.Count - 3)
            {
                cover.page += 2;
                SetBook();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(cover.page != 0)
            {
                cover.page -= 2;
                SetBook();
            }
        }
        public void SetBook()
        {
            textBox1.Text = cover.list[cover.page].ToString();
            textBox2.Text = cover.list[cover.page + 1].ToString();
            label1.Text = (cover.page + 1).ToString();
            label2.Text = (cover.page + 2).ToString();
        }
        private void Synth_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            MessageBox.Show("Speech end", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Equals(String.Empty) && button4.Visible)
            {
                button3.Visible = false;
                button5.Visible = true;
                StartThread();
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            button3.Visible = true;
            button5.Visible = false;
            synth.Dispose();
            thread.Abort();
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            if (!textBox2.Text.Equals(String.Empty) && button3.Visible)
            {
                button4.Visible = false;
                button6.Visible = true;
                StartThread();
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            button4.Visible = true;
            button6.Visible = false;
            synth.Dispose();
            thread.Abort();
        }
        private void StartThread()
        {
            thread = new Thread(new ThreadStart(StartSynth));
            thread.Start();
        }
        private void StartSynth()
        {
            synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            synth.SpeakCompleted += Synth_SpeakCompleted;
            if (!button3.Visible) synth.Speak(textBox1.Text);
            if (!button5.Visible) synth.Speak(textBox2.Text);
        }

    }
}
