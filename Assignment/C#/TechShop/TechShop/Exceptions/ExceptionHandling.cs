// Custom exception handling class for TechShop application
public class ExceptionHandling
{
    // Base class for all custom exceptions in TechShop
    public class TechShopException : Exception
    {
        public TechShopException(string message) : base(message) { }
    }

    // Exception thrown when database operations fail
    public class DatabaseSqlException : TechShopException
    {
        public DatabaseSqlException(string message) : base(message) { }
    }

    // Exception thrown when input data is invalid
    public class InvalidDataException : TechShopException
    {
        public InvalidDataException(string message) : base(message) { }
    }

    // Exception thrown when authentication fails
    public class AuthenticationException : TechShopException
    {
        public AuthenticationException(string message) : base(message) { }
    }

    // Exception thrown when payment processing fails
    public class PaymentException : TechShopException
    {
        public PaymentException(string message) : base(message) { }
    }

    // Exception thrown when order processing fails
    public class OrderException : TechShopException
    {
        public OrderException(string message) : base(message) { }
    }

    // Validates email format using regular expression
    // Returns true if email format is valid, false otherwise
    public static bool ValidateEmail(string email)
    {
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return System.Text.RegularExpressions.Regex.IsMatch(email, pattern);
    }
}