using System;
using System.Data;
using System.Data.SqlClient;

namespace LoanApp
{
    public class ILoanRepositoryImpl : ILoanRepository
    {
        public void ApplyLoan(Loan loan)
        {
            using (SqlConnection conn = DBUtil.GetDBConn())
            {
                if (conn == null) return;

                string query = @"INSERT INTO Loan (CustomerId, PrincipalAmount, InterestRate, LoanTerm, LoanType, LoanStatus)
                                     VALUES (@CustomerId, @PrincipalAmount, @InterestRate, @LoanTerm, @LoanType, @LoanStatus)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", loan.Customer.CustomerId);
                    cmd.Parameters.AddWithValue("@PrincipalAmount", loan.PrincipalAmount);
                    cmd.Parameters.AddWithValue("@InterestRate", loan.InterestRate);
                    cmd.Parameters.AddWithValue("@LoanTerm", loan.LoanTerm);
                    cmd.Parameters.AddWithValue("@LoanType", loan.LoanType);
                    cmd.Parameters.AddWithValue("@LoanStatus", loan.LoanStatus); // Must be: Pending, Approved, Rejected

                    try
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Loan successfully applied.");
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error while applying loan: " + ex.Message);
                    }
                }
            }
        }

        public void GetAllLoan()
        {
            using (SqlConnection conn = DBUtil.GetDBConn())
            {
                if (conn == null) return;

                string query = "SELECT * FROM Loan";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"LoanId: {reader["LoanId"]}, CustomerId: {reader["CustomerId"]}, " +
                                          $"Type: {reader["LoanType"]}, Status: {reader["LoanStatus"]}, " +
                                          $"Amount: {reader["PrincipalAmount"]}, Term: {reader["LoanTerm"]} months");
                    }
                }
            }
        }

        public Loan GetLoanById(int loanId)
        {
            using (SqlConnection conn = DBUtil.GetDBConn())
            {
                if (conn == null) return null;

                string query = "SELECT * FROM Loan WHERE LoanId = @LoanId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LoanId", loanId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Loan
                            {
                                LoanId = loanId,
                                Customer = null, // Assuming Customer object is fetched elsewhere
                                PrincipalAmount = (decimal)reader["PrincipalAmount"],
                                InterestRate = (decimal)reader["InterestRate"],
                                LoanTerm = (int)reader["LoanTerm"],
                                LoanType = reader["LoanType"].ToString(),
                                LoanStatus = reader["LoanStatus"].ToString()
                            };
                        }
                        else
                        {
                            throw new InvalidLoanException("Loan ID not found.");
                        }
                    }
                }
            }
        }

        public void LoanRepayment(int loanId, decimal amount)
        {
            using (SqlConnection conn = DBUtil.GetDBConn())
            {
                if (conn == null) return;

                string checkQuery = "SELECT PrincipalAmount FROM Loan WHERE LoanId = @LoanId";

                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@LoanId", loanId);

                    object result = checkCmd.ExecuteScalar();

                    if (result == null)
                        throw new InvalidLoanException("Loan ID not found.");

                    decimal currentAmount = (decimal)result;
                    decimal newAmount = currentAmount - amount;

                    string updateQuery = "UPDATE Loan SET PrincipalAmount = @NewAmount WHERE LoanId = @LoanId";

                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@NewAmount", newAmount);
                        updateCmd.Parameters.AddWithValue("@LoanId", loanId);
                        updateCmd.ExecuteNonQuery();

                        Console.WriteLine($"Repayment of {amount} done. Remaining balance: {newAmount}");
                    }
                }
            }
        }

        public void LoanStatus(int loanId)
        {
            using (SqlConnection conn = DBUtil.GetDBConn())
            {
                if (conn == null) return;

                string query = "SELECT CreditScore FROM Customer WHERE CustomerId = (SELECT CustomerId FROM Loan WHERE LoanId = @LoanId)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LoanId", loanId);

                    object result = cmd.ExecuteScalar();

                    if (result == null)
                    {
                        Console.WriteLine("Customer not found for this loan.");
                        return;
                    }

                    int creditScore = (int)result;
                    string status = creditScore >= 700 ? "Approved" : "Rejected";

                    string updateQuery = "UPDATE Loan SET LoanStatus = @Status WHERE LoanId = @LoanId";

                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@Status", status);
                        updateCmd.Parameters.AddWithValue("@LoanId", loanId);
                        updateCmd.ExecuteNonQuery();

                        Console.WriteLine($"Loan status updated to: {status}");
                    }
                }
            }
        }

        public decimal CalculateInterest(decimal principal, decimal rate, int term)
        {
            decimal interest = (principal * rate * term) / (100 * 12);
            return decimal.Round(interest, 2);
        }

        public decimal CalculateInterest(int loanId)
        {
            Loan loan = GetLoanById(loanId);
            if (loan == null) throw new InvalidLoanException("Loan ID not found.");
            return CalculateInterest(loan.PrincipalAmount, loan.InterestRate, loan.LoanTerm);
        }

        public decimal CalculateEMI(decimal principal, decimal rate, int term)
        {
            decimal monthlyRate = rate / (12 * 100);
            decimal emi = (principal * monthlyRate * (decimal)Math.Pow(1 + (double)monthlyRate, term)) /
                          (decimal)(Math.Pow(1 + (double)monthlyRate, term) - 1);
            return decimal.Round(emi, 2);
        }

        public decimal CalculateEMI(int loanId)
        {
            Loan loan = GetLoanById(loanId);
            if (loan == null) throw new InvalidLoanException("Loan ID not found.");
            return CalculateEMI(loan.PrincipalAmount, loan.InterestRate, loan.LoanTerm);
        }

        public List<Loan> GetAllLoans()
        {
            List<Loan> loans = new List<Loan>();

            using (SqlConnection conn = DBUtil.GetDBConn())
            {
                if (conn == null) return loans;

                string query = "SELECT * FROM Loan";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        loans.Add(new Loan
                        {
                            LoanId = (int)reader["LoanId"],
                            Customer = null, // Assuming Customer object is fetched elsewhere
                            PrincipalAmount = (decimal)reader["PrincipalAmount"],
                            InterestRate = (decimal)reader["InterestRate"],
                            LoanTerm = (int)reader["LoanTerm"],
                            LoanType = reader["LoanType"].ToString(),
                            LoanStatus = reader["LoanStatus"].ToString()
                        });
                    }
                }
            }

            return loans;
        }
    }
}
