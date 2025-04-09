using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TechShop.Exceptions.ExceptionHandling;

namespace TechShop.Models
{
    public class PaymentRecord
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; } // Paid, Failed, Pending


        //getter methods for database
        public int GetPaymentId() => PaymentId;
        public int GetOrderId() => OrderId;
        public double GetAmount() => Amount;
        public string GetStatus() => Status;

        //parameterized constructor
        public PaymentRecord(int paymentId, int orderId, double amount)
        {
            PaymentId = paymentId;
            OrderId = orderId;
            Amount = amount;
            Status = "Pending";
        }

        public void ProcessPayment(bool isSuccess)
        {
            Status = isSuccess ? "Paid" : throw new PaymentFailedException("Payment Declined");
        }

    }
}