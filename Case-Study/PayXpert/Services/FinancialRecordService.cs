using PayXpert.Context;
using PayXpert.Interfaces;
using PayXpert.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace PayXpert.Services
{
    public class FinancialRecordService : IFinancialRecordService
    {
        public void AddFinancialRecord(int employeeID, string description, decimal amount, string recordType)
        {
            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    string query = @"INSERT INTO FinancialRecord 
                                     (EmployeeID, RecordDate, Description, Amount, RecordType)
                                     VALUES (@EmployeeID, @RecordDate, @Description, @Amount, @RecordType)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                    cmd.Parameters.AddWithValue("@RecordDate", DateTime.Now); // Current date
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@RecordType", recordType);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding the financial record.", ex);
            }
        }

        public FinancialRecord GetFinancialRecordById(int recordID)
        {
            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    string query = "SELECT * FROM FinancialRecord WHERE RecordID = @RecordID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@RecordID", recordID);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new FinancialRecord
                            {
                                RecordID = Convert.ToInt32(reader["RecordID"]),
                                EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                RecordDate = Convert.ToDateTime(reader["RecordDate"]),
                                Description = reader["Description"].ToString(),
                                Amount = Convert.ToDecimal(reader["Amount"]),
                                RecordType = reader["RecordType"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the financial record.", ex);
            }

            return null;
        }

        public List<FinancialRecord> GetFinancialRecordsForEmployee(int employeeID)
        {
            List<FinancialRecord> records = new List<FinancialRecord>();

            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    string query = "SELECT * FROM FinancialRecord WHERE EmployeeID = @EmployeeID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            records.Add(new FinancialRecord
                            {
                                RecordID = Convert.ToInt32(reader["RecordID"]),
                                EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                RecordDate = Convert.ToDateTime(reader["RecordDate"]),
                                Description = reader["Description"].ToString(),
                                Amount = Convert.ToDecimal(reader["Amount"]),
                                RecordType = reader["RecordType"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving financial records for the employee.", ex);
            }

            return records;
        }

        public List<FinancialRecord> GetFinancialRecordsForDate(DateTime recordDate)
        {
            List<FinancialRecord> records = new List<FinancialRecord>();

            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    string query = "SELECT * FROM FinancialRecord WHERE CAST(RecordDate AS DATE) = @RecordDate";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@RecordDate", recordDate.Date);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            records.Add(new FinancialRecord
                            {
                                RecordID = Convert.ToInt32(reader["RecordID"]),
                                EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                RecordDate = Convert.ToDateTime(reader["RecordDate"]),
                                Description = reader["Description"].ToString(),
                                Amount = Convert.ToDecimal(reader["Amount"]),
                                RecordType = reader["RecordType"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving financial records for the date.", ex);
            }

            return records;
        }
    }
}