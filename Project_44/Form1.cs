using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Project_44
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            UpdateTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PhoneBook;Trusted_Connection=True";
                try
                {
                    string FirstName = textBox1.Text;
                    string LastName = textBox2.Text;
                    string Phone = textBox3.Text;
                    string insertString = "INSERT INTO [Table] (FirstName,LastName,Phone) VALUES (@FirstName, @LastName, @Phone);";
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(insertString, conn);
                    cmd.Parameters.AddWithValue("@FirstName", FirstName);
                    cmd.Parameters.AddWithValue("@LastName", LastName);
                    cmd.Parameters.AddWithValue("@Phone", Phone);
                    cmd.ExecuteNonQuery();

                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                        UpdateTable();
                    }
                }
            }
            else MessageBox.Show("The field is empty");
        }
        private void UpdateTable()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PhoneBook;Trusted_Connection=True";
            try
            {
                conn.Open();
                string command = "SELECT * FROM [Table]";
                SqlDataAdapter adapt = new SqlDataAdapter(command, conn);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}
