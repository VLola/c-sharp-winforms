using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using Project_66_Server.Model;
using Dapper.Contrib.Extensions;
using Dapper;

namespace Project_66_Server.DataBase
{
    internal static class Connect
    {
        public static string connectionStringUser = @"Data Source=DESKTOP-TBFG5D3\SQLEXPRESS;Initial Catalog=Project_66;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static bool RegistrationUser(string name, string pass)
        {
            if (CheckUser(name)) return false;
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionStringUser))
                {
                    User reg = new User();
                    reg.Name = name;
                    reg.Password = Crypt.Generate(pass);
                    connection.Insert(reg);
                    return true;
                }
            }
        }
        public static bool LoginUser(string name, string pass)
        {
            foreach (var it in GetUsers()) if (it.Name == name && Crypt.Veryfy(pass, it.Password)) return true;
            return false;
        }
        public static bool CheckUser(string name)
        {
            foreach (var it in GetUsers()) if (it.Name == name) return true;
            return false;
        }
        public static List<User> GetUsers()
        {
            using (IDbConnection connection = new SqlConnection(connectionStringUser))
            {
                return connection.Query<User>($"SELECT * FROM Users").ToList();
            }
        }
    }
}
