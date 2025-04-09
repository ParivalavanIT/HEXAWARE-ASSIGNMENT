public class InvalidLoanException : Exception
{
    public InvalidLoanException() : base("Invalid loan ID. Loan not found.")
    {
    }

    public InvalidLoanException(string message) : base(message)
    {
    }
}