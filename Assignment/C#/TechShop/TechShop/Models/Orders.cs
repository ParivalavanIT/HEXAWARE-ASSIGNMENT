using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop;

namespace TechShop.Models
{
    public class Orders
    {
        //Attributes
        public int order_id { get; set; }
        // adding composition for Orders with customers table

        private Customers _customer { get; set; }
        private DateTime order_date { get; set; }
        private double total_amount { get; set; }
        //setter
        public void SetTotalAmount(double amount)
        {
            total_amount = amount;
        }
        private string order_status { get; set; }
        public void SetOrderStatus(string status)
        {
            order_status = status;
        }

        private List<OrderDetails> orderDetailsList = new List<OrderDetails>();
        public List<OrderDetails> GetOrderDetailsList
        {
            get { return orderDetailsList; }
        }

        //Getter methods for database connection

        public int GetOrderId() => order_id;
        public Customers GetCustomer() => _customer;
        public DateTime GetOrderDate() => order_date;
        public double GetTotalAmount() => total_amount;
        public string GetOrderStatus() => order_status;


        //parameterized constructor
        public Orders(int order_id, Customers _customer, DateTime order_date, double total_amount)
        {
            this.order_id = order_id;
            this._customer = _customer;
            this.order_date = order_date;
            this.total_amount = total_amount;
            this.order_status = "Pending"; // it is default here
        }

        // Methods

        // To get total amount of orders
        public double CalculateTotalAmount()
        {
            double total = 0;
            foreach (var detail in orderDetailsList)
            {
                total += detail.CalculateSubtotal();
            }
            return total;
        }

        // To get details or orders
        public void GetOrderDetails()
        {
            Console.WriteLine("-------------Order Details--------------");
            Console.WriteLine($"Order ID: {order_id}");
            Console.WriteLine($"Order Date: {order_date}");
            Console.WriteLine($"Total Amount: {total_amount}");
            Console.WriteLine($"Order Status: {order_status}");
            Console.WriteLine("-----------------------------------------");

        }

        //To update order status
        public void UpdateOrderStatus(DateTime? order_date = null, double? total_amount = null, string? order_status = null)
        {
            if (order_date != null) this.order_date = order_date.Value;
            if (total_amount != null) this.total_amount = total_amount.Value;
            if (!string.IsNullOrEmpty(order_status)) this.order_status = order_status;

            Console.WriteLine("Order Details updated successfully!");
        }

        //To add order details

        public void AddOrderDetail(OrderDetails detail)
        {
            if (detail == null)
            {
                throw new ArgumentNullException("Warning! No valid records found!");
            }

            orderDetailsList.Add(detail);
        }

        //To cancel order
        public static void CancelOrder(int order_id, string order_status)
        {
            try
            {
                using (SqlConnection conn = DataBaseConnector.getConnection())
                {
                    string query = "Update Orders SET order_status = @Status where order_id = @OrderId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", "Cancelled");
                        cmd.Parameters.AddWithValue("@OrderId", order_id);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            order_status = "Cancelled";
                            Console.WriteLine($"Order of {order_id} has been cancelled successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Order of {order_id} cancellation failed. Order not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while cancelling order: {ex.Message}");
            }
        }



    }
}