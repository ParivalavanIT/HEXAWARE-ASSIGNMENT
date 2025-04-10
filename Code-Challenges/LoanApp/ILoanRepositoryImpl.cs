using System;
using System.Data;
using System.Data.SqlClient;

namespace LoanApp
{
    public class ILoanRepositoryImpl : ILoanRepository
    {
        public void ApplyLoan(Loan loan)
        {
            Console.WriteLine("Loan Details:");
            loan.PrintInformation();

            Console.Write("Do you want to confirm the loan application? (Yes/No): ");
            string confirmation = Console.ReadLine();

            if (confirmation.Equals("Yes", StringComparison.OrdinalIgnoreCase))
            {
                using (SqlConnection connection = DBUtil.GetDBConn())
                {
                    // First, check if the customer exists, if not, add the customer
                    string checkCustomerQuery = "SELECT COUNT(*) FROM Customer WHERE customer_id = @CustomerId";
                    using (SqlCommand checkCommand = new SqlCommand(checkCustomerQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@CustomerId", loan.Customer.CustomerId);
                        int customerExists = (int)checkCommand.ExecuteScalar();

                        if (customerExists == 0)
                        {
                            // Insert customer into database
                            string insertCustomerQuery = "INSERT INTO Customer (customer_id, name, email_address, phone_number, address, credit_score) " +
                                                        "VALUES (@CustomerId, @Name, @Email, @Phone, @Address, @CreditScore)";
                            using (SqlCommand customerCommand = new SqlCommand(insertCustomerQuery, connection))
                            {
                                customerCommand.Parameters.AddWithValue("@CustomerId", loan.Customer.CustomerId);
                                customerCommand.Parameters.AddWithValue("@Name", loan.Customer.Name);
                                customerCommand.Parameters.AddWithValue("@Email", loan.Customer.EmailAddress);
                                customerCommand.Parameters.AddWithValue("@Phone", loan.Customer.PhoneNumber);
                                customerCommand.Parameters.AddWithValue("@Address", loan.Customer.Address);
                                customerCommand.Parameters.AddWithValue("@CreditScore", loan.Customer.CreditScore);
                                customerCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    // Insert loan into database
                    string insertLoanQuery = "INSERT INTO Loan (loan_id, customer_id, principal_amount, interest_rate, loan_term, loan_type, loan_status) " +
                                            "VALUES (@LoanId, @CustomerId, @PrincipalAmount, @InterestRate, @LoanTerm, @LoanType, @LoanStatus)";
                    using (SqlCommand loanCommand = new SqlCommand(insertLoanQuery, connection))
                    {
                        loanCommand.Parameters.AddWithValue("@LoanId", loan.LoanId);
                        loanCommand.Parameters.AddWithValue("@CustomerId", loan.Customer.CustomerId);
                        loanCommand.Parameters.AddWithValue("@PrincipalAmount", loan.PrincipalAmount);
                        loanCommand.Parameters.AddWithValue("@InterestRate", loan.InterestRate);
                        loanCommand.Parameters.AddWithValue("@LoanTerm", loan.LoanTerm);
                        loanCommand.Parameters.AddWithValue("@LoanType", loan.LoanType);
                        loanCommand.Parameters.AddWithValue("@LoanStatus", loan.LoanStatus);
                        loanCommand.ExecuteNonQuery();
                    }

                    // Insert specific loan type information
                    if (loan is HomeLoan homeLoan)
                    {
                        string insertHomeLoanQuery = "INSERT INTO HomeLoan (loan_id, property_address, property_value) " +
                                                    "VALUES (@LoanId, @PropertyAddress, @PropertyValue)";
                        using (SqlCommand homeLoanCommand = new SqlCommand(insertHomeLoanQuery, connection))
                        {
                            homeLoanCommand.Parameters.AddWithValue("@LoanId", homeLoan.LoanId);
                            homeLoanCommand.Parameters.AddWithValue("@PropertyAddress", homeLoan.PropertyAddress);
                            homeLoanCommand.Parameters.AddWithValue("@PropertyValue", homeLoan.PropertyValue);
                            homeLoanCommand.ExecuteNonQuery();
                        }
                    }
                    else if (loan is CarLoan carLoan)
                    {
                        string insertCarLoanQuery = "INSERT INTO CarLoan (loan_id, car_model, car_value) " +
                                                  "VALUES (@LoanId, @CarModel, @CarValue)";
                        using (SqlCommand carLoanCommand = new SqlCommand(insertCarLoanQuery, connection))
                        {
                            carLoanCommand.Parameters.AddWithValue("@LoanId", carLoan.LoanId);
                            carLoanCommand.Parameters.AddWithValue("@CarModel", carLoan.CarModel);
                            carLoanCommand.Parameters.AddWithValue("@CarValue", carLoan.CarValue);
                            carLoanCommand.ExecuteNonQuery();
                        }
                    }

                    Console.WriteLine("Loan application submitted successfully!");
                }
            }
            else
            {
                Console.WriteLine("Loan application cancelled.");
            }
        }

        public decimal CalculateInterest(int loanId)
        {
            Loan loan = GetLoanById(loanId);
            return CalculateInterest(loan.PrincipalAmount, loan.InterestRate, loan.LoanTerm);
        }

        public decimal CalculateInterest(decimal principalAmount, decimal interestRate, int loanTerm)
        {
            // Interest = (Principal Amount * Interest Rate * Loan Tenure) / 12
            return (principalAmount * interestRate * loanTerm) / 1200;
        }

        public void LoanStatus(int loanId)
        {
            using (SqlConnection connection = DBUtil.GetDBConn())
            {
                string query = "SELECT c.credit_score, l.loan_status FROM Loan l " +
                              "JOIN Customer c ON l.customer_id = c.customer_id " +
                              "WHERE l.loan_id = @LoanId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LoanId", loanId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int creditScore = reader.GetInt32(0);
                            string currentStatus = reader.GetString(1);
                            reader.Close();

                            string newStatus = creditScore > 650 ? "Approved" : "Rejected";

                            // Update the loan status in the database
                            string updateQuery = "UPDATE Loan SET loan_status = @Status WHERE loan_id = @LoanId";
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@Status", newStatus);
                                updateCommand.Parameters.AddWithValue("@LoanId", loanId);
                                updateCommand.ExecuteNonQuery();
                            }

                            Console.WriteLine($"Loan (ID: {loanId}) is {newStatus}. Credit Score: {creditScore}");
                        }
                        else
                        {
                            throw new InvalidLoanException();
                        }
                    }
                }
            }
        }

        public decimal CalculateEMI(int loanId)
        {
            Loan loan = GetLoanById(loanId);
            return CalculateEMI(loan.PrincipalAmount, loan.InterestRate, loan.LoanTerm);
        }

        public decimal CalculateEMI(decimal principalAmount, decimal interestRate, int loanTerm)
        {
            // EMI = [P * R * (1+R)^N] / [(1+R)^N-1]
            // R: Monthly Interest Rate (Annual Interest Rate / 12 / 100)
            decimal r = interestRate / 12 / 100;
            decimal numerator = principalAmount * r * (decimal)Math.Pow((double)(1 + r), loanTerm);
            decimal denominator = (decimal)Math.Pow((double)(1 + r), loanTerm) - 1;

            return numerator / denominator;
        }

        public void LoanRepayment(int loanId, decimal amount)
        {
            decimal emiAmount = CalculateEMI(loanId);

            if (amount < emiAmount)
            {
                Console.WriteLine($"Payment rejected. Amount ({amount:C}) is less than a single EMI ({emiAmount:C}).");
                return;
            }

            int noOfEmiPaid = (int)(amount / emiAmount);
            decimal totalPaid = noOfEmiPaid * emiAmount;

            using (SqlConnection connection = DBUtil.GetDBConn())
            {
                // Here you would typically update some payment tracking table
                // For this example, we'll just print the details
                Console.WriteLine($"Payment of {totalPaid:C} accepted.");
                Console.WriteLine($"Number of EMIs paid: {noOfEmiPaid}");
                Console.WriteLine($"EMI amount: {emiAmount:C}");
            }
        }

        public List<Loan> GetAllLoan()
        {
            List<Loan> loans = new List<Loan>();
            List<Loan> specificLoans = new List<Loan>(); // Temporary list for specific loan types

            using (SqlConnection connection = DBUtil.GetDBConn())
            {
                string query = "SELECT l.*, c.* FROM Loan l JOIN Customer c ON l.customer_id = c.customer_id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Extract loan and customer details as you're already doing
                            // ...
                            // Add base loan objects to loans list
                            loans.Add(loan);
                        }
                    }
                }

                // Create a new list for the specific loan types
                for (int i = 0; i < loans.Count; i++)
                {
                    Loan loan = loans[i];

                    if (loan.LoanType == "HomeLoan")
                    {
                        // Get HomeLoan specific details
                        // ...
                        specificLoans.Add(homeLoan);
                    }
                    else if (loan.LoanType == "CarLoan")
                    {
                        // Get CarLoan specific details
                        // ...
                        specificLoans.Add(carLoan);
                    }
                    else
                    {
                        specificLoans.Add(loan);
                    }
                }
            }

            // Print all loans
            foreach (Loan loan in specificLoans)
            {
                loan.PrintInformation();
                Console.WriteLine("----------------------------");
            }

            return specificLoans;
        }

        public Loan GetLoanById(int loanId)
        {
            using (SqlConnection connection = DBUtil.GetDBConn())
            {
                string query = "SELECT l.*, c.* FROM Loan l JOIN Customer c ON l.customer_id = c.customer_id WHERE l.loan_id = @LoanId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LoanId", loanId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Extract loan details
                            decimal principalAmount = reader.GetDecimal(reader.GetOrdinal("principal_amount"));
                            decimal interestRate = reader.GetDecimal(reader.GetOrdinal("interest_rate"));
                            int loanTerm = reader.GetInt32(reader.GetOrdinal("loan_term"));
                            string loanType = reader.GetString(reader.GetOrdinal("loan_type"));
                            string loanStatus = reader.GetString(reader.GetOrdinal("loan_status"));

                            // Extract customer details
                            int customerId = reader.GetInt32(reader.GetOrdinal("customer_id"));
                            string name = reader.GetString(reader.GetOrdinal("name"));
                            string emailAddress = reader.GetString(reader.GetOrdinal("email_address"));
                            string phoneNumber = reader.GetString(reader.GetOrdinal("phone_number"));
                            string address = reader.GetString(reader.GetOrdinal("address"));
                            int creditScore = reader.GetInt32(reader.GetOrdinal("credit_score"));

                            // Create customer object
                            Customer customer = new Customer(customerId, name, emailAddress, phoneNumber, address, creditScore);

                            // Create base loan object
                            Loan loan = new Loan(loanId, customer, principalAmount, interestRate, loanTerm, loanType);
                            loan.LoanStatus = loanStatus;

                            reader.Close();

                            // Fetch specific details for HomeLoan and CarLoan
                            if (loanType == "HomeLoan")
                            {
                                string homeLoanQuery = "SELECT * FROM HomeLoan WHERE loan_id = @LoanId";
                                using (SqlCommand homeLoanCommand = new SqlCommand(homeLoanQuery, connection))
                                {
                                    homeLoanCommand.Parameters.AddWithValue("@LoanId", loanId);
                                    using (SqlDataReader homeLoanReader = homeLoanCommand.ExecuteReader())
                                    {
                                        if (homeLoanReader.Read())
                                        {
                                            string propertyAddress = homeLoanReader.GetString(homeLoanReader.GetOrdinal("property_address"));
                                            int propertyValue = homeLoanReader.GetInt32(homeLoanReader.GetOrdinal("property_value"));

                                            HomeLoan homeLoan = new HomeLoan(
                                                loanId, customer, principalAmount,
                                                interestRate, loanTerm, propertyAddress, propertyValue
                                            );
                                            homeLoan.LoanStatus = loanStatus;

                                            loan = homeLoan;
                                        }
                                    }
                                }
                            }
                            else if (loanType == "CarLoan")
                            {
                                string carLoanQuery = "SELECT * FROM CarLoan WHERE loan_id = @LoanId";
                                using (SqlCommand carLoanCommand = new SqlCommand(carLoanQuery, connection))
                                {
                                    carLoanCommand.Parameters.AddWithValue("@LoanId", loanId);
                                    using (SqlDataReader carLoanReader = carLoanCommand.ExecuteReader())
                                    {
                                        if (carLoanReader.Read())
                                        {
                                            string carModel = carLoanReader.GetString(carLoanReader.GetOrdinal("car_model"));
                                            int carValue = carLoanReader.GetInt32(carLoanReader.GetOrdinal("car_value"));

                                            CarLoan carLoan = new CarLoan(
                                                loanId, customer, principalAmount,
                                                interestRate, loanTerm, carModel, carValue
                                            );
                                            carLoan.LoanStatus = loanStatus;

                                            loan = carLoan;
                                        }
                                    }
                                }
                            }

                            loan.PrintInformation();
                            return loan;
                        }
                        else
                        {
                            throw new InvalidLoanException();
                        }
                    }
                }
            }
        }
    }

}
