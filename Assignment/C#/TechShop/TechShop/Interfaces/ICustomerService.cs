using TechShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop;

namespace TechShop.Interfaces
{
    public interface ICustomerService
    {
        bool RegisterCustomer(Customers customer);
    }
}