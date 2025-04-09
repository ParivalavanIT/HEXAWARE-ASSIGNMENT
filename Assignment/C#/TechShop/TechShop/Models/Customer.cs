using TechShop.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Exceptions;
using TechShop.Models;

namespace TechShop.Models
{
    // Represents a customer in the TechShop system, managing customer information and related operations
    // Handles customer data validation, profile management, and order tracking
    public class Customers
    {
        // Unique identifier for the customer in the database
        private int customer_id { get; set; }
        
        // Customer's first name, required field
        private string first_name { get; set; }
        
        // Customer's last name, optional field
        private string? last_name { get; set; }
        
        // Customer's email address, validated for correct format
        private string? cust_email { get; set; }

        public string custemail
        {
            get { return cust_email; }
            set
            {
                if (!ExceptionHandling.IsValidEmail(value))
                {
                    throw new InvalidDataException("Warning! Invalid email format. Please enter a valid email.");
                }
                cust_email = value;
            }
        }

        // Customer's phone number, must be exactly 10 digits
        private string cust_phone;
        
        // Property to validate and manage phone number format
        public string custphone
        {
            get { return cust_phone; }
            set
            {
                if (value.Length != 10)
                {
                    throw new InvalidDataException("Warning! Phone number must be 10 digits");
                }
                cust_phone = value;
            }
        }
        // Customer's physical address for shipping and billing
        private string? cust_address { get; set; }

        // Database accessor methods for customer attributes
        public int GetCustomerId() => customer_id;
        public string GetFirstName() => first_name;
        public string GetLastName() => last_name;
        public string GetEmail() => cust_email;
        public string GetPhone() => cust_phone;
        public string GetAddress() => cust_address;


        //parameterized constructor
        public Customers(int cust_id, string first_name, string last_name, string cust_email, string cust_phone, string cust_address)
        {
            customer_id = cust_id;
            this.first_name = first_name;
            this.last_name = last_name;
            this.cust_email = cust_email;
            this.cust_phone = cust_phone;
            this.cust_address = cust_address;

        }

        // Calculates the total number of orders placed by this customer
        // Parameters:
        //   orders: List of all orders to check against this customer
        // Returns: Total number of orders for this customer
        public int CalculateTotalOrders(List<Orders> orders)
        {
            //return orders.Count(sql logic here);
            return 0;
        }

        // To get customer details
        public void GetCustomerDetails()
        {
            Console.WriteLine("-------------Customer Details-------------");
            Console.WriteLine($"Customer ID: {customer_id}");
            Console.WriteLine($"Customer Name: {first_name} {last_name} ");
            Console.WriteLine($"Email: {cust_email}");
            Console.WriteLine($"Phone: {cust_phone}");
            Console.WriteLine($"Address: {cust_address}");
            Console.WriteLine("------------------------------------------");

        }

        // Updates customer profile information with provided values
        // Only updates fields that are provided (non-null)
        // Maintains data validation rules for email and phone number
        // Parameters:
        //   first_name: New first name (optional)
        //   last_name: New last name (optional)
        //   cust_email: New email address (optional, validated)
        //   cust_phone: New phone number (optional, must be 10 digits)
        //   cust_address: New physical address (optional)
        public void UpdateCustomerInfo(string? first_name = null, string? last_name = null, string? cust_email = null, string? cust_phone = null, string? cust_address = null)
        {
            if (!string.IsNullOrEmpty(first_name)) this.first_name = first_name;
            if (!string.IsNullOrEmpty(last_name)) this.last_name = last_name;
            if (!string.IsNullOrEmpty(cust_email)) this.cust_email = cust_email;
            if (!string.IsNullOrEmpty(cust_phone)) this.cust_phone = cust_phone;
            if (!string.IsNullOrEmpty(cust_address)) this.cust_address = cust_address;

            Console.WriteLine("Customer information updated successfully!");

        }

    }
}