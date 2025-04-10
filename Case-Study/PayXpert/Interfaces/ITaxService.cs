
using PayXpert.Models;

namespace PayXpert.Interfaces
{
    public interface ITaxService
    {
        void CalculateTax(int employeeID, int taxYear);
        Tax GetTaxById(int taxID);
        List<Tax> GetTaxesForEmployee(int employeeID);
        List<Tax> GetTaxesForYear(int taxYear);
    }
}