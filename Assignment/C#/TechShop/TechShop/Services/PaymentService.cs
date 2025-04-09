using ElectronicGadgetsTechShop.Exceptions;
using ElectronicGadgetsTechShop.Interfaces;
using ElectronicGadgetsTechShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop;
using TechShop.Exceptions;

namespace ElectronicGadgetsTechShop.Services
{
    public class PaymentService : IPaymentService
    {
        public bool ProcessPayment(PaymentRecord payment)
        {
            try
            {
                if (payment == null)
                {
                    throw new ExceptionHandling.InvalidDataException("Payment information is missing.");
                }

                if (payment.GetAmount() <= 0)
                {
                    throw new ExceptionHandling.InvalidDataException("Payment amount must be greater than zero.");
                }

                bool result = DataBaseConnector.InsertPaymentRecord(payment);
                if (!result)
                {
                    throw new ExceptionHandling.PaymentFailedException("Failed to process payment.");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new ExceptionHandling.DatabaseSqlException("Error processing payment: " + ex.Message);
            }
        }

        public void RecordPayment(PaymentRecord payment)
        {
            DataBaseConnector.RecordPayment(payment);
        }
    }
}