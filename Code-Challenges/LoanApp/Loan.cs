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
        public int LoanId { get; set; }
        public Customer Customer { get; set; }
        public decimal PrincipalAmount { get; set; }
        public decimal InterestRate { get; set; }
        public int LoanTerm { get; set; } // months
        public string LoanType { get; set; } // "HomeLoan" or "CarLoan"
        public string LoanStatus { get; set; } // "Pending", "Approved"

        public Loan() { }

        public Loan(int loanId, Customer customer, decimal principalAmount, decimal interestRate, int loanTerm, string loanType)
        {
            LoanId = loanId;
            Customer = customer;
            PrincipalAmount = principalAmount;
            InterestRate = interestRate;
            LoanTerm = loanTerm;
            LoanType = loanType;
            LoanStatus = "Pending";
        }

        public void PrintInfo()
        {
            Console.WriteLine($"Loan ID: {LoanId}, Customer: {Customer.Name}, Amount: {PrincipalAmount}, Rate: {InterestRate}, Term: {LoanTerm} months, Type: {LoanType}, Status: {LoanStatus}");
        }
    }

    public class HomeLoan : Loan
{
    public string PropertyAddress { get; set; }
    public int PropertyValue { get; set; }

    public HomeLoan() {}

    public HomeLoan(int loanId, Customer customer, decimal principal, decimal rate, int term, string propertyAddress, int propertyValue)
        : base(loanId, customer, principal, rate, term, "HomeLoan")
    {
        PropertyAddress = propertyAddress;
        PropertyValue = propertyValue;
    }

    public new void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"Property Address: {PropertyAddress}, Property Value: {PropertyValue}");
    }
}


}
