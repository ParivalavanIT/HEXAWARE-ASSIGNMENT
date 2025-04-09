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
    // Service class that handles payment processing and recording operations
    public class PaymentService : IPaymentService
    {
        // Processes a payment transaction and records it in the database
        // Validates payment data and amount before processing
        // Returns true if payment was successfully processed
        // Throws InvalidDataException for null payment or invalid amount
        // Throws PaymentFailedException if payment processing fails
        // Throws DatabaseSqlException for database operation errors
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

        // Records a completed payment transaction in the database
        // This method is typically called after successful payment processing
        public void RecordPayment(PaymentRecord payment)
        {
            DataBaseConnector.RecordPayment(payment);
        }
    }
}