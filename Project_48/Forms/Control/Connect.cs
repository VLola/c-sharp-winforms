using Dapper;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Project_48.Forms.Control
{
    public static class Connect
    { 
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Users;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static void Registration(string email, string pass)
        {
            if (CheckEmail(email)) MessageBox.Show("User exists!");
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    User reg = new User();
                    reg.Email = email;
                    reg.Password = Crypt.Generate(pass);
                    connection.Insert(reg);
                    MessageBox.Show("Registration successful!");
                }
            }
        }
        public static bool Login(string email, string pass)
        {
            bool check = false;
            foreach (var it in GetUsers()) if (it.Email == email && Crypt.Veryfy(pass, it.Password)) check = true;
            return check;
        }
        public static bool CheckEmail(string email)
        {
            bool check = false;
            foreach (var it in GetUsers()) if (it.Email == email) check = true;
            return check;
        }
        public static List<User> GetUsers()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<User>($"SELECT * FROM Users").ToList();
            }
        }
    }
}
