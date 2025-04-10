using PayXpert.Context;
using PayXpert.Interfaces;
using PayXpert.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace PayXpert.Services
{
    public class TaxService : ITaxService
    {
        public void CalculateTax(int employeeID, int taxYear)
        {
            // Logic to calculate tax based on employeeID and taxYear
            throw new NotImplementedException();
        }

        public Tax GetTaxById(int taxID)
        {
            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    string query = "SELECT * FROM Tax WHERE TaxID = @TaxID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TaxID", taxID);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Tax
                            {
                                TaxID = Convert.ToInt32(reader["TaxID"]),
                                EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                TaxYear = Convert.ToInt32(reader["TaxYear"]),
                                TaxableIncome = Convert.ToDecimal(reader["TaxableIncome"]),
                                TaxAmount = Convert.ToDecimal(reader["TaxAmount"])
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the tax record.", ex);
            }

            return null;
        }

        public List<Tax> GetTaxesForEmployee(int employeeID)
        {
            List<Tax> taxes = new List<Tax>();

            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    string query = "SELECT * FROM Tax WHERE EmployeeID = @EmployeeID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            taxes.Add(new Tax
                            {
                                TaxID = Convert.ToInt32(reader["TaxID"]),
                                EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                TaxYear = Convert.ToInt32(reader["TaxYear"]),
                                TaxableIncome = Convert.ToDecimal(reader["TaxableIncome"]),
                                TaxAmount = Convert.ToDecimal(reader["TaxAmount"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving taxes for the employee.", ex);
            }

            return taxes;
        }

        public List<Tax> GetTaxesForYear(int taxYear)
        {
            List<Tax> taxes = new List<Tax>();

            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    string query = "SELECT * FROM Tax WHERE TaxYear = @TaxYear";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TaxYear", taxYear);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            taxes.Add(new Tax
                            {
                                TaxID = Convert.ToInt32(reader["TaxID"]),
                                EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                TaxYear = Convert.ToInt32(reader["TaxYear"]),
                                TaxableIncome = Convert.ToDecimal(reader["TaxableIncome"]),
                                TaxAmount = Convert.ToDecimal(reader["TaxAmount"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving taxes for the year.", ex);
            }

            return taxes;
        }
    }
}