using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TechShop.Exceptions
{
    class ExceptionHandling
    {
        public class InvalidDataException : Exception
        {
            public InvalidDataException(string message) : base(message)
            {
            }
        }

        public class PaymentFailedException : Exception
        {
            public PaymentFailedException(string message) : base(message)
            {
            }
        }

        public class IncompleteOrderException : Exception
        {
            public IncompleteOrderException(string message) : base(message)
            {
            }
        }

        public class DatabaseSqlException : Exception
        {
            public DatabaseSqlException(string message) : base(message)
            {
            }
        }

        public class InsufficientStockException : Exception
        {
            public InsufficientStockException(string message) : base(message) { }
        }


        public static bool IsValidEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}