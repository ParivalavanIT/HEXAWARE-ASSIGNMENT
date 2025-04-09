using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LoanApp
{
    public class Loan
    {
        protected int loanId;
        protected Customer customer;
        protected decimal principalAmount;
        protected decimal interestRate;
        protected int loanTerm; // Loan Tenure in months
        protected string loanType; // CarLoan, HomeLoan
        protected string loanStatus; // Pending, Approved

        // Default constructor
        public Loan()
        {
            loanStatus = "Pending";
        }

        // Overloaded constructor with parameters
        public Loan(int loanId, Customer customer, decimal principalAmount, decimal interestRate, int loanTerm, string loanType)
        {
            this.loanId = loanId;
            this.customer = customer;
            this.principalAmount = principalAmount;
            this.interestRate = interestRate;
            this.loanTerm = loanTerm;
            this.loanType = loanType;
            this.loanStatus = "Pending";
        }

        // Getters and Setters
        public int LoanId
        {
            get { return loanId; }
            set { loanId = value; }
        }

        public Customer Customer
        {
            get { return customer; }
            set { customer = value; }
        }

        public decimal PrincipalAmount
        {
            get { return principalAmount; }
            set { principalAmount = value; }
        }

        public decimal InterestRate
        {
            get { return interestRate; }
            set { interestRate = value; }
        }

        public int LoanTerm
        {
            get { return loanTerm; }
            set { loanTerm = value; }
        }

        public string LoanType
        {
            get { return loanType; }
            set { loanType = value; }
        }

        public string LoanStatus
        {
            get { return loanStatus; }
            set { loanStatus = value; }
        }

        // Method to print all information of attributes
        public virtual void PrintInformation()
        {
            Console.WriteLine("Loan Information:");
            Console.WriteLine($"Loan ID: {loanId}");
            Console.WriteLine($"Principal Amount: {principalAmount:C}");
            Console.WriteLine($"Interest Rate: {interestRate}%");
            Console.WriteLine($"Loan Term: {loanTerm} months");
            Console.WriteLine($"Loan Type: {loanType}");
            Console.WriteLine($"Loan Status: {loanStatus}");

            if (customer != null)
            {
                customer.PrintInformation();
            }
        }
    }

    // HomeLoan subclass
    public class HomeLoan : Loan
    {
        private string propertyAddress;
        private int propertyValue;

        // Default constructor
        public HomeLoan() : base()
        {
            this.loanType = "HomeLoan";
        }

        // Overloaded constructor with parameters
        public HomeLoan(int loanId, Customer customer, decimal principalAmount, decimal interestRate, int loanTerm,
                       string propertyAddress, int propertyValue)
                       : base(loanId, customer, principalAmount, interestRate, loanTerm, "HomeLoan")
        {
            this.propertyAddress = propertyAddress;
            this.propertyValue = propertyValue;
        }

        // Getters and Setters
        public string PropertyAddress
        {
            get { return propertyAddress; }
            set { propertyAddress = value; }
        }

        public int PropertyValue
        {
            get { return propertyValue; }
            set { propertyValue = value; }
        }

        // Override method to print all information of attributes
        public override void PrintInformation()
        {
            base.PrintInformation();
            Console.WriteLine("Home Loan Specific Information:");
            Console.WriteLine($"Property Address: {propertyAddress}");
            Console.WriteLine($"Property Value: {propertyValue:C}");
        }
    }

    // CarLoan subclass
    public class CarLoan : Loan
    {
        private string carModel;
        private int carValue;

        // Default constructor
        public CarLoan() : base()
        {
            this.loanType = "CarLoan";
        }

        // Overloaded constructor with parameters
        public CarLoan(int loanId, Customer customer, decimal principalAmount, decimal interestRate, int loanTerm,
                      string carModel, int carValue)
                      : base(loanId, customer, principalAmount, interestRate, loanTerm, "CarLoan")
        {
            this.carModel = carModel;
            this.carValue = carValue;
        }

        // Getters and Setters
        public string CarModel
        {
            get { return carModel; }
            set { carModel = value; }
        }

        public int CarValue
        {
            get { return carValue; }
            set { carValue = value; }
        }

        // Override method to print all information of attributes
        public override void PrintInformation()
        {
            base.PrintInformation();
            Console.WriteLine("Car Loan Specific Information:");
            Console.WriteLine($"Car Model: {carModel}");
            Console.WriteLine($"Car Value: {carValue:C}");
        }
    }
}
