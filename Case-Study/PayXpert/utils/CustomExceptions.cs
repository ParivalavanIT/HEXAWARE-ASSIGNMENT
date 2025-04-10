using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.utils
{
    public class EmployeeNotFoundException : Exception
    {
        public EmployeeNotFoundException(string message) : base(message)
        {
        }
    }
    public class PayrollGenerationException : Exception
    {
        public  PayrollGenerationException  (string message) : base(message)
        {
        }
    }
    public class TaxCalculationException : Exception
    {
        public TaxCalculationException(string message) : base(message)
        {
        }
    }
    public class InvalidInputException:Exception
    {
        public InvalidInputException(string message) : base(message)
        {
        }
    }
    public class  DatabaseConnectionException :Exception
    {
        public DatabaseConnectionException(string message) : base(message) { }
    }
}
