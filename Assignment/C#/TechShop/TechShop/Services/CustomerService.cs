using ElectronicGadgetsTechShop.Interfaces;
using ElectronicGadgetsTechShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop;

namespace ElectronicGadgetsTechShop.Services
{
    // Service class that handles customer management operations
    public class CustomerService : ICustomerService
    {
        // Registers a new customer in the system
        // Throws ArgumentNullException when customer object is null
        public void RegisterCustomer(Customers customer)
        {
            if (customer == null)
                throw new ArgumentNullException("Customer cannot be null.");

            DataBaseConnector.InsertCustomer(customer);
        }

        bool ICustomerService.RegisterCustomer(Customers customer)
        {
            throw new NotImplementedException();
        }
    }
}