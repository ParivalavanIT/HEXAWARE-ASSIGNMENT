using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Models;

namespace TechShop.Interfaces
{
    /// <summary>
    /// Interface defining payment processing operations for the TechShop system
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Processes a payment transaction
        /// </summary>
        /// <param name="payment">The payment record containing transaction details</param>
        /// <returns>True if payment was successfully processed, false otherwise</returns>
        bool ProcessPayment(PaymentRecord payment);

        /// <summary>
        /// Records a completed payment in the system
        /// </summary>
        /// <param name="payment">The payment record to be stored</param>
        void RecordPayment(PaymentRecord payment);
    }
}