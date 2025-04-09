using System;

namespace LoanApp
{
    using System;

    namespace LoanApp
    {
        class Program
        {
            static void Main(string[] args)
            {
                ILoanRepository loanRepo = new ILoanRepositoryImpl();

                while (true)
                {
                    Console.WriteLine("\n=== Loan Management System ===");
                    Console.WriteLine("1. Apply Loan");
                    Console.WriteLine("2. Get All Loans");
                    Console.WriteLine("3. Get Loan by ID");
                    Console.WriteLine("4. Loan Repayment");
                    Console.WriteLine("5. Exit");
                    Console.Write("Enter your choice: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            ApplyLoan(loanRepo);
                            break;

                        case "2":
                            loanRepo.GetAllLoans();
                            break;

                        case "3":
                            Console.Write("Enter Loan ID: ");
                            int loanId;
                            if (int.TryParse(Console.ReadLine(), out loanId))
                            {
                                try
                                {
                                    loanRepo.GetLoanById(loanId);
                                }
                                catch (InvalidLoanException ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            break;

                        case "4":
                            LoanRepayment(loanRepo);
                            break;

                        case "5":
                            Console.WriteLine("Exiting... Thank you!");
                            return;

                        default:
                            Console.WriteLine("Invalid choice! Please try again.");
                            break;
                    }
                }
            }

            private static void ApplyLoan(ILoanRepository loanRepo)
            {
                Console.Write("Enter Loan Type (HomeLoan/CarLoan): ");
                string loanType = Console.ReadLine();

                Console.Write("Enter Customer ID: ");
                int customerId = int.Parse(Console.ReadLine());

                Console.Write("Enter Principal Amount: ");
                decimal principal = decimal.Parse(Console.ReadLine());

                Console.Write("Enter Interest Rate (%): ");
                decimal rate = decimal.Parse(Console.ReadLine());

                Console.Write("Enter Loan Term (months): ");
                int term = int.Parse(Console.ReadLine());

                Console.Write("Enter Credit Score: ");
                int creditScore = int.Parse(Console.ReadLine());

                Loan loan;
                Customer customer = new Customer();
                if (loanType.Equals("HomeLoan", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Write("Enter Property Address: ");
                    string address = Console.ReadLine();

                    Console.Write("Enter Property Value: ");
                    int value = int.Parse(Console.ReadLine());

                    loan = new HomeLoan(0, customer, principal, rate, term,  address, value);
                }
                else if (loanType.Equals("CarLoan", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Write("Enter Car Model: ");
                    string model = Console.ReadLine();

                    Console.Write("Enter Car Value: ");
                    int value = int.Parse(Console.ReadLine());

                    loan = new CarLoan(0, customerId, principal, rate, term,  model, value);
                }
                else
                {
                    Console.WriteLine("Invalid loan type.");
                    return;
                }

                Console.Write("Do you want to apply for the loan? (Yes/No): ");
                string confirmation = Console.ReadLine();

                if (confirmation.Equals("Yes", StringComparison.OrdinalIgnoreCase))
                {
                    loanRepo.ApplyLoan(loan);
                    Console.WriteLine("Loan application submitted successfully.");

                    // Calculate interest and EMI on spot
                    decimal interest = loanRepo.CalculateInterest(loan.PrincipalAmount, loan.InterestRate, loan.LoanTerm);
                    Console.WriteLine($"Calculated Interest: {interest}");

                    decimal emi = loanRepo.CalculateEMI(loan.PrincipalAmount, loan.InterestRate, loan.LoanTerm);
                    Console.WriteLine($"Estimated Monthly EMI: {emi}");

                    // Update loan status based on credit score
                    loanRepo.LoanStatus(loanId);
                }
                else
                {
                    Console.WriteLine("Loan application cancelled.");
                }
            }

            private static void LoanRepayment(ILoanRepository loanRepo)
            {
                Console.Write("Enter Loan ID: ");
                int loanId = int.Parse(Console.ReadLine());

                Console.Write("Enter repayment amount: ");
                decimal amount = decimal.Parse(Console.ReadLine());

                try
                {
                    loanRepo.LoanRepayment(loanId, amount);
                }
                catch (InvalidLoanException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

}
