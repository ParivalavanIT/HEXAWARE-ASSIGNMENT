
using PayXpert.Models;

namespace PayXpert.Interfaces
{
    public interface IFinancialRecordService
    {
        void AddFinancialRecord(int employeeID, string description, decimal amount, string recordType);
        FinancialRecord GetFinancialRecordById(int recordID);
        List<FinancialRecord> GetFinancialRecordsForEmployee(int employeeID);
        List<FinancialRecord> GetFinancialRecordsForDate(DateTime recordDate);
    }
}