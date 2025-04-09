using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop;

namespace TechShop.Models
{
 
    // Represents an order in the TechShop system, managing order details and status.
    // This class manages order details, status tracking, and provides functionality for order processing and management.
    public class Orders
    {
        // Unique identifier for the order, used as the primary key in the database
        public int order_id { get; set; }
        
        // Customer associated with this order through a composition relationship
        // Maintains the link between orders and their respective customers
        private Customers _customer { get; set; }
        
        // Date when the order was placed, used for order tracking and reporting
        private DateTime order_date { get; set; }
        
        // Total amount of the order, represents the final cost including any applicable discounts
        private double total_amount { get; set; }
        
        // Sets the total amount for the order
        // amount: The amount to set as the total
        public void SetTotalAmount(double amount)
        {
            total_amount = amount;
        }
        
        // Current status of the order (e.g., Pending, Completed, Cancelled)
        private string order_status { get; set; }
        
        // Sets the status of the order
        // status: The new status to set
        public void SetOrderStatus(string status)
        {
            order_status = status;
        }
        
        // List of order details associated with this order
        private List<OrderDetails> orderDetailsList = new List<OrderDetails>();
        
        // Gets the list of order details
        public List<OrderDetails> GetOrderDetailsList
        {
            get { return orderDetailsList; }
        }
        
        // Getter methods for database operations
        public int GetOrderId() => order_id;
        public Customers GetCustomer() => _customer;
        public DateTime GetOrderDate() => order_date;
        public double GetTotalAmount() => total_amount;
        public string GetOrderStatus() => order_status;
        
        // Initializes a new instance of the Orders class
        // order_id: The unique identifier for the order
        // _customer: The customer placing the order
        // order_date: The date of the order
        // total_amount: The total amount of the order
        public Orders(int order_id, Customers _customer, DateTime order_date, double total_amount)
        {
            this.order_id = order_id;
            this._customer = _customer;
            this.order_date = order_date;
            this.total_amount = total_amount;
            this.order_status = "Pending"; // Default status for new orders
        }
        
        // Calculates the total amount of the order based on all order details
        // Returns the total amount calculated from all order details
        public double CalculateTotalAmount()
        {
            double total = 0;
            foreach (var detail in orderDetailsList)
            {
                total += detail.CalculateSubtotal();
            }
            return total;
        }
        
        // Displays the order details to the console
        public void GetOrderDetails()
        {
            Console.WriteLine("-------------Order Details--------------");
            Console.WriteLine($"Order ID: {order_id}");
            Console.WriteLine($"Order Date: {order_date}");
            Console.WriteLine($"Total Amount: {total_amount}");
            Console.WriteLine($"Order Status: {order_status}");
            Console.WriteLine("-----------------------------------------");
        }
        
        // Updates the order details with new values
        // order_date: Optional new order date
        // total_amount: Optional new total amount
        // order_status: Optional new order status
        public void UpdateOrderStatus(DateTime? order_date = null, double? total_amount = null, string? order_status = null)
        {
            if (order_date != null) this.order_date = order_date.Value;
            if (total_amount != null) this.total_amount = total_amount.Value;
            if (!string.IsNullOrEmpty(order_status)) this.order_status = order_status;
        
            Console.WriteLine("Order Details updated successfully!");
        }
        
        // Adds a new order detail to the order
        // detail: The order detail to add
        // Throws ArgumentNullException when detail is null
        public void AddOrderDetail(OrderDetails detail)
        {
            if (detail == null)
            {
                throw new ArgumentNullException("Warning! No valid records found!");
            }
        
            orderDetailsList.Add(detail);
        }
        
        // Cancels an order by updating its status in the database
        // order_id: The ID of the order to cancel
        // order_status: The current order status
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