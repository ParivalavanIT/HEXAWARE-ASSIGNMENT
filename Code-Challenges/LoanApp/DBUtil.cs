using System;
using System.Data.SqlClient;

namespace LoanApp
{
    public class DBUtil
    {
        private static string connectionString = @"Server=RAVEN\SQLEXPRESS;Database=LoanDB;Trusted_Connection=True;";
        // Example: @"Server=.\SQLEXPRESS;Database=LoanDB;Trusted_Connection=True;"

        public static SqlConnection GetDBConn()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                Console.WriteLine("Database connection established successfully.");
                return conn;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to connect to the database: " + ex.Message);
                return null;
            }
        }
    }
}
