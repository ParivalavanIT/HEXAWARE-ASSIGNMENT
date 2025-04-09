using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Models;

namespace TechShop.Interfaces
{
    public interface IPaymentService
    {
        bool ProcessPayment(PaymentRecord payment);
        void RecordPayment(PaymentRecord payment);
    }
}