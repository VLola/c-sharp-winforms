namespace Project_62_ClientSocket
{
    public partial class Form1 : Form
    {
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
            Symbols.Width = 50;
            Symbols.Text = "+";
            Calculate.Location = new Point(450,50);
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
            //if(Symbols.Text != null) (double x, double y, string a) variable = ((Double.Parse(X.Text), Double.Parse(Y.Text)), Symbols.Text);
            //Answer.Text = variable.ToString();
        }

    }
}