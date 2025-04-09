using System;

namespace LoanApp
{
    public class LoanManagement
    {
        public static void Main(string[] args)
        {
            ILoanRepository loanRepo = new ILoanRepositoryImpl();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nLoan Management System");
                Console.WriteLine("1. Apply for a Loan");
                Console.WriteLine("2. View All Loans");
                Console.WriteLine("3. View Loan by ID");
                Console.WriteLine("4. Process Loan Status");
                Console.WriteLine("5. Calculate EMI");
                Console.WriteLine("6. Make Loan Repayment");
                Console.WriteLine("7. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ApplyForLoan(loanRepo);
                        break;
                    case "2":
                        loanRepo.GetAllLoan();
                        break;
                    case "3":
                        ViewLoanById(loanRepo);
                        break;
                    case "4":
                        ProcessLoanStatus(loanRepo);
                        break;
                    case "5":
                        CalculateEMI(loanRepo);
                        break;
                    case "6":
                        MakeLoanRepayment(loanRepo);
                        break;
                    case "7":
                        exit = true;
                        Console.WriteLine("Thank you for using the Loan Management System.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private static void ApplyForLoan(ILoanRepository loanRepo)
        {
            try
            {
                Console.WriteLine("\nApplying for a Loan");
                Console.WriteLine("1. Home Loan");
                Console.WriteLine("2. Car Loan");
                Console.Write("Select loan type: ");
                string loanTypeChoice = Console.ReadLine();

                // Get customer details
                Console.Write("Enter Customer ID: ");
                int customerId = int.Parse(Console.ReadLine());
                Console.Write("Enter Customer Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter Email Address: ");
                string emailAddress = Console.ReadLine();
                Console.Write("Enter Phone Number: ");
                string phoneNumber = Console.ReadLine();
                Console.Write("Enter Address: ");
                string address = Console.ReadLine();
                Console.Write("Enter Credit Score: ");
                int creditScore = int.Parse(Console.ReadLine());

                Customer customer = new Customer(customerId, name, emailAddress, phoneNumber, address, creditScore);

                // Get loan details
                Console.Write("Enter Loan ID: ");
                int loanId = int.Parse(Console.ReadLine());
                Console.Write("Enter Principal Amount: ");
                decimal principalAmount = decimal.Parse(Console.ReadLine());
                Console.Write("Enter Interest Rate (%): ");
                decimal interestRate = decimal.Parse(Console.ReadLine());
                Console.Write("Enter Loan Term (months): ");
                int loanTerm = int.Parse(Console.ReadLine());

                // Create specific loan type
                if (loanTypeChoice == "1")
                {
                    Console.Write("Enter Property Address: ");
                    string propertyAddress = Console.ReadLine();
                    Console.Write("Enter Property Value: ");
                    int propertyValue = int.Parse(Console.ReadLine());

                    HomeLoan homeLoan = new HomeLoan(loanId, customer, principalAmount, interestRate, loanTerm, propertyAddress, propertyValue);
                    loanRepo.ApplyLoan(homeLoan);
                }
                else if (loanTypeChoice == "2")
                {
                    Console.Write("Enter Car Model: ");
                    string carModel = Console.ReadLine();
                    Console.Write("Enter Car Value: ");
                    int carValue = int.Parse(Console.ReadLine());

                    CarLoan carLoan = new CarLoan(loanId, customer, principalAmount, interestRate, loanTerm, carModel, carValue);
                    loanRepo.ApplyLoan(carLoan);
                }
                else
                {
                    Console.WriteLine("Invalid loan type selection.");
                }
            }
            catch (Exception ex)
            {
                
Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void ViewLoanById(ILoanRepository loanRepo)
        {
            try
            {
                Console.Write("Enter Loan ID: ");
                int loanId = int.Parse(Console.ReadLine());

                try
                {
                    loanRepo.GetLoanById(loanId);
                }
                catch (InvalidLoanException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void ProcessLoanStatus(ILoanRepository loanRepo)
        {
            try
            {
                Console.Write("Enter Loan ID: ");
                int loanId = int.Parse(Console.ReadLine());

                try
                {
                    loanRepo.LoanStatus(loanId);
                }
                catch (InvalidLoanException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void CalculateEMI(ILoanRepository loanRepo)
        {
            try
            {
                Console.WriteLine("\nCalculate EMI");
                Console.WriteLine("1. Calculate EMI for existing loan");
                Console.WriteLine("2. Calculate EMI for a potential loan");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Enter Loan ID: ");
                    int loanId = int.Parse(Console.ReadLine());

                    try
                    {
                        decimal emi = loanRepo.CalculateEMI(loanId);
                        Console.WriteLine($"Monthly EMI: {emi:C}");
                    }
                    catch (InvalidLoanException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (choice == "2")
                {
                    Console.Write("Enter Principal Amount: ");
                    decimal principalAmount = decimal.Parse(Console.ReadLine());
                    Console.Write("Enter Interest Rate (%): ");
                    decimal interestRate = decimal.Parse(Console.ReadLine());
                    Console.Write("Enter Loan Term (months): ");
                    int loanTerm = int.Parse(Console.ReadLine());

                    decimal emi = loanRepo.CalculateEMI(principalAmount, interestRate, loanTerm);
                    Console.WriteLine($"Monthly EMI: {emi:C}");
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void MakeLoanRepayment(ILoanRepository loanRepo)
        {
            try
            {
                Console.Write("Enter Loan ID: ");
                int loanId = int.Parse(Console.ReadLine());

                try
                {
                    // First verify the loan exists and show EMI details
                    decimal emi = loanRepo.CalculateEMI(loanId);
                    Console.WriteLine($"Monthly EMI for this loan: {emi:C}");

                    Console.Write("Enter payment amount: ");
                    decimal amount = decimal.Parse(Console.ReadLine());

                    loanRepo.LoanRepayment(loanId, amount);
                }
                catch (InvalidLoanException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

}
