using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.Models
{
    public class FinancialRecord
    {
        private int recordID;
        private int employeeID;
        private DateTime recordDate;
        private string description;
        private decimal amount;
        private string recordType;

        public FinancialRecord() { }

        public FinancialRecord(int recordID, int employeeID, DateTime recordDate, string description,
                               decimal amount, string recordType)
        {
            this.recordID = recordID;
            this.employeeID = employeeID;
            this.recordDate = recordDate;
            this.description = description;
            this.amount = amount;
            this.recordType = recordType;
        }

        public int RecordID { get => recordID; set => recordID = value; }
        public int EmployeeID { get => employeeID; set => employeeID = value; }
        public DateTime RecordDate { get => recordDate; set => recordDate = value; }
        public string Description { get => description; set => description = value; }
        public decimal Amount { get => amount; set => amount = value; }
        public string RecordType { get => recordType; set => recordType = value; }
    }
}
