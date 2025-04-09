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
    public class Customers
    {
        //Attributes..
        private int customer_id { get; set; }
        private string first_name { get; set; }
        private string? last_name { get; set; }
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

        private string cust_phone;
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
        private string? cust_address { get; set; }

        //Get functions for access in database
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

        //Methods
        // To calcuate total orders present
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

        // To update customer information
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