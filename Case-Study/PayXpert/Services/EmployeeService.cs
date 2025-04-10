using PayXpert.Context;
using PayXpert.Models;
using PayXpert.Interfaces;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace PayXpert.Services
{
    public class EmployeeService : IEmployeeService
    {
        public Employee GetEmployeeById(int employeeID)
        {
            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    string query = "SELECT * FROM Employee WHERE EmployeeID = @EmployeeID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Employee
                            {
                                EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                Gender = reader["Gender"].ToString(),
                                Email = reader["Email"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                Address = reader["Address"].ToString(),
                                Position = reader["Position"].ToString(),
                                JoiningDate = Convert.ToDateTime(reader["JoiningDate"]),
                                TerminationDate = reader["TerminationDate"] != DBNull.Value ? Convert.ToDateTime(reader["TerminationDate"]) : (DateTime?)null
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the employee.", ex);
            }

            return null;
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    string query = "SELECT * FROM Employee";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(new Employee
                            {
                                EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                Gender = reader["Gender"].ToString(),
                                Email = reader["Email"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                Address = reader["Address"].ToString(),
                                Position = reader["Position"].ToString(),
                                JoiningDate = Convert.ToDateTime(reader["JoiningDate"]),
                                TerminationDate = reader["TerminationDate"] != DBNull.Value ? Convert.ToDateTime(reader["TerminationDate"]) : (DateTime?)null
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving all employees.", ex);
            }

            return employees;
        }

        public void AddEmployee(Employee employee)
        {
            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    string query = @"INSERT INTO Employee 
                                     (EmployeeID, FirstName, LastName, DateOfBirth, Gender, Email, PhoneNumber, Address, Position, JoiningDate, TerminationDate)
                                     VALUES (@EmployeeId,@FirstName, @LastName, @DateOfBirth, @Gender, @Email, @PhoneNumber, @Address, @Position, @JoiningDate, @TerminationDate)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeID);
                    cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                    cmd.Parameters.AddWithValue("@Email", employee.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", employee.Address);
                    cmd.Parameters.AddWithValue("@Position", employee.Position);
                    cmd.Parameters.AddWithValue("@JoiningDate", employee.JoiningDate);
                    cmd.Parameters.AddWithValue("@TerminationDate", (object)employee.TerminationDate ?? DBNull.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding the employee.", ex);
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    string query = @"UPDATE Employee 
                                     SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, 
                                         Gender = @Gender, Email = @Email, PhoneNumber = @PhoneNumber, 
                                         Address = @Address, Position = @Position, JoiningDate = @JoiningDate, 
                                         TerminationDate = @TerminationDate
                                     WHERE EmployeeID = @EmployeeID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                    cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                    cmd.Parameters.AddWithValue("@Email", employee.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", employee.Address);
                    cmd.Parameters.AddWithValue("@Position", employee.Position);
                    cmd.Parameters.AddWithValue("@JoiningDate", employee.JoiningDate);
                    cmd.Parameters.AddWithValue("@TerminationDate", (object)employee.TerminationDate ?? DBNull.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the employee.", ex);
            }
        }

        public void RemoveEmployee(int employeeID)
        {
            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    string query = "DELETE FROM Employee WHERE EmployeeID = @EmployeeID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while removing the employee.", ex);
            }
        }
    }
}