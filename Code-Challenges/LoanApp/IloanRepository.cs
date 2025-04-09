using LoanApp;

public interface ILoanRepository
{
    void ApplyLoan(Loan loan);
    decimal CalculateInterest(int loanId);
    decimal CalculateInterest(decimal principalAmount, decimal interestRate, int loanTerm);
    void LoanStatus(int loanId);
    decimal CalculateEMI(int loanId);
    decimal CalculateEMI(decimal principalAmount, decimal interestRate, int loanTerm);
    void LoanRepayment(int loanId, decimal amount);
    List<Loan> GetAllLoan();
    Loan GetLoanById(int loanId);
}
