using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Project_62_Client
{
    public partial class Form1 : Form
    {
        const int PORT = 8088;
        const string IP = "127.0.0.1";
        IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse(IP), PORT);

        Button Calculate = new Button();
        TextBox X = new TextBox();
        TextBox Y = new TextBox();
        ComboBox Symbols = new ComboBox();
        Label Answer = new Label();
        public Form1()
        {
            InitializeComponent();
            X.Location = new Point(50, 50);
            Y.Location = new Point(300, 50);
            Symbols.Location = new Point(200, 50);
            Symbols.Items.Add("+");
            Symbols.Items.Add("-");
            Symbols.Items.Add("/");
            Symbols.Items.Add("*");
            Symbols.Width = 50;
            Symbols.Text = "+";
            Calculate.Location = new Point(450, 50);
            Calculate.Text = "=";
            Calculate.Click += Calculate_Click;
            Answer.Location = new Point(600, 50);
            Answer.BorderStyle = BorderStyle.Fixed3D;
            Controls.Add(X);
            Controls.Add(Symbols);
            Controls.Add(Y);
            Controls.Add(Calculate);
            Controls.Add(Answer);
        }


        private void Calculate_Click(object? sender, EventArgs e)
        {
            SendMessage();
        }

        private void SendMessage()
        {
            try {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(iPEnd);
                (string x, string y, string symbol) variable = (X.Text, Y.Text, Symbols.Text);
                string message = JsonConvert.SerializeObject(variable);

                byte[] data = Encoding.Unicode.GetBytes(message);
                socket.Send(data);
                int bytes = 0;
                byte[] buffer = new byte[1024];
                StringBuilder builder = new StringBuilder();
                do
                {
                    bytes = socket.Receive(buffer);
                    builder.Append(Encoding.Unicode.GetString(buffer, 0, bytes));
                } while (socket.Available > 0);
                Answer.Text = builder.ToString();
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch { }
        }
    }
}