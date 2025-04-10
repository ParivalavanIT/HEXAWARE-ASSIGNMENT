using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.Models
{
    public class Tax
    {
        private int taxID;
        private int employeeID;
        private int taxYear;
        private decimal taxableIncome;
        private decimal taxAmount;

        public Tax() { }

        public Tax(int taxID, int employeeID, int taxYear, decimal taxableIncome, decimal taxAmount)
        {
            this.taxID = taxID;
            this.employeeID = employeeID;
            this.taxYear = taxYear;
            this.taxableIncome = taxableIncome;
            this.taxAmount = taxAmount;
        }

        public int TaxID { get => taxID; set => taxID = value; }
        public int EmployeeID { get => employeeID; set => employeeID = value; }
        public int TaxYear { get => taxYear; set => taxYear = value; }
        public decimal TaxableIncome { get => taxableIncome; set => taxableIncome = value; }
        public decimal TaxAmount { get => taxAmount; set => taxAmount = value; }
    }
}
