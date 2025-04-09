using System;
using System.Data.SqlClient;

namespace LoanApp
{
    public class DBUtil
    {
        private static readonly string connectionString = "Data Source=RAVEN\\SQLEXPRESS;Initial Catalog=LoanDB;Integrated Security=True";

        public static SqlConnection GetDBConn()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database connection error: {ex.Message}");
                throw;
            }
        }
    }
}
