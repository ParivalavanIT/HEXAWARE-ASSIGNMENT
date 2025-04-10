using Microsoft.Data.SqlClient;

namespace PayXpert.Context
{
    public static class DatabaseContext
    {
        private static string connectionString =
            "Data Source=RAVEN\\SQLEXPRESS;Initial Catalog=EmployeePayrollDB;Integrated Security=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}



