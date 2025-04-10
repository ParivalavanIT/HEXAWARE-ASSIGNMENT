
using PayXpert.Models;

namespace PayXpert.Interfaces
{
    public interface IPayrollService
    {
        void GeneratePayroll(int employeeID, DateTime startDate, DateTime endDate);
        Payroll GetPayrollById(int payrollID);
        List<Payroll> GetPayrollsForEmployee(int employeeID);
        List<Payroll> GetPayrollsForPeriod(DateTime startDate, DateTime endDate);
    }
}