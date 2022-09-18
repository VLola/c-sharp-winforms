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
                    reg.Power = 0;
                    reg.Defence = 0;
                    reg.Coins = 0;
                    reg.Murders = 0;
                    reg.Deaths = 0;
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
        public static User GetUser(string name)
        {
            using (IDbConnection connection = new SqlConnection(connectionStringUser))
            {
                return connection.QuerySingle<User>("SELECT * FROM Users WHERE Name = @Name", new { name });
            }
        }
        public static int GetCoins(string name)
        {
            using (IDbConnection connection = new SqlConnection(connectionStringUser))
            {
                return connection.QuerySingle<User>("SELECT * FROM Users WHERE Name = @Name", new { name }).Coins;
            }
        }
        public static int GetPower(string name)
        {
            using (IDbConnection connection = new SqlConnection(connectionStringUser))
            {
                return connection.QuerySingle<User>("SELECT * FROM Users WHERE Name = @Name", new { name }).Power;
            }
        }
        public static int GetDefence(string name)
        {
            using (IDbConnection connection = new SqlConnection(connectionStringUser))
            {
                return connection.QuerySingle<User>("SELECT * FROM Users WHERE Name = @Name", new { name }).Defence;
            }
        }
        public static void UpdateDeaths(string name, int value)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Name", name, DbType.String);
            parameters.Add("Deaths", value, DbType.Int32);
            using (IDbConnection connection = new SqlConnection(connectionStringUser))
            {
                connection.Execute("UPDATE Users SET Deaths = @Deaths WHERE Name = @Name", parameters);
            }
        }
        public static void UpdateMurders(string name, int murders, int coins)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Name", name, DbType.String);
            parameters.Add("Murders", murders, DbType.Int32);
            parameters.Add("Coins", coins, DbType.Int32);
            using (IDbConnection connection = new SqlConnection(connectionStringUser))
            {
                connection.Execute("UPDATE Users SET Murders = @Murders, Coins = @Coins WHERE Name = @Name", parameters);
            }
        }
        public static void UpdateCoins(string name, int value)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Name", name, DbType.String);
            parameters.Add("Coins", value, DbType.Int32);
            using (IDbConnection connection = new SqlConnection(connectionStringUser))
            {
                connection.Execute("UPDATE Users SET Coins = @Coins WHERE Name = @Name", parameters);
            }
        }
        public static void UpdateDefence(string name, int defence, int coins)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Name", name, DbType.String);
            parameters.Add("Defence", defence, DbType.Int32);
            parameters.Add("Coins", coins, DbType.Int32);
            using (IDbConnection connection = new SqlConnection(connectionStringUser))
            {
                connection.Execute("UPDATE Users SET Defence = @Defence, Coins = @Coins WHERE Name = @Name", parameters);
            }
        }
        public static void UpdatePower(string name, int power, int coins)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Name", name, DbType.String);
            parameters.Add("Power", power, DbType.Int32);
            parameters.Add("Coins", coins, DbType.Int32);
            using (IDbConnection connection = new SqlConnection(connectionStringUser))
            {
                connection.Execute("UPDATE Users SET Power = @Power, Coins = @Coins WHERE Name = @Name", parameters);
            }
        }
    }
}
