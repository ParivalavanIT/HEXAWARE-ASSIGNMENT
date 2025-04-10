using PayXpert.Context;
using PayXpert.Interfaces;
using PayXpert.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace PayXpert.Services
{
    public class PayrollService : IPayrollService
    {
        public void GeneratePayroll(int employeeID, DateTime startDate, DateTime endDate)
        {
            // Logic to calculate payroll based on employeeID, startDate, and endDate
            throw new NotImplementedException();
        }

        public Payroll GetPayrollById(int payrollID)
        {
            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    string query = "SELECT * FROM Payroll WHERE PayrollID = @PayrollID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@PayrollID", payrollID);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Payroll
                            {
                                PayrollID = Convert.ToInt32(reader["PayrollID"]),
                                EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                PayPeriodStartDate = Convert.ToDateTime(reader["PayPeriodStartDate"]),
                                PayPeriodEndDate = Convert.ToDateTime(reader["PayPeriodEndDate"]),
                                BasicSalary = Convert.ToDecimal(reader["BasicSalary"]),
                                OvertimePay = Convert.ToDecimal(reader["OvertimePay"]),
                                Deductions = Convert.ToDecimal(reader["Deductions"]),
                                NetSalary = Convert.ToDecimal(reader["NetSalary"])
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the payroll.", ex);
            }

            return null;
        }

        public List<Payroll> GetPayrollsForEmployee(int employeeID)
        {
            List<Payroll> payrolls = new List<Payroll>();

            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    string query = "SELECT * FROM Payroll WHERE EmployeeID = @EmployeeID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            payrolls.Add(new Payroll
                            {
                                PayrollID = Convert.ToInt32(reader["PayrollID"]),
                                EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                PayPeriodStartDate = Convert.ToDateTime(reader["PayPeriodStartDate"]),
                                PayPeriodEndDate = Convert.ToDateTime(reader["PayPeriodEndDate"]),
                                BasicSalary = Convert.ToDecimal(reader["BasicSalary"]),
                                OvertimePay = Convert.ToDecimal(reader["OvertimePay"]),
                                Deductions = Convert.ToDecimal(reader["Deductions"]),
                                NetSalary = Convert.ToDecimal(reader["NetSalary"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving payrolls for the employee.", ex);
            }

            return payrolls;
        }

        public List<Payroll> GetPayrollsForPeriod(DateTime startDate, DateTime endDate)
        {
            List<Payroll> payrolls = new List<Payroll>();

            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    string query = "SELECT * FROM Payroll WHERE PayPeriodStartDate >= @StartDate AND PayPeriodEndDate <= @EndDate";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            payrolls.Add(new Payroll
                            {
                                PayrollID = Convert.ToInt32(reader["PayrollID"]),
                                EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                PayPeriodStartDate = Convert.ToDateTime(reader["PayPeriodStartDate"]),
                                PayPeriodEndDate = Convert.ToDateTime(reader["PayPeriodEndDate"]),
                                BasicSalary = Convert.ToDecimal(reader["BasicSalary"]),
                                OvertimePay = Convert.ToDecimal(reader["OvertimePay"]),
                                Deductions = Convert.ToDecimal(reader["Deductions"]),
                                NetSalary = Convert.ToDecimal(reader["NetSalary"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving payrolls for the period.", ex);
            }

            return payrolls;
        }
    }
}