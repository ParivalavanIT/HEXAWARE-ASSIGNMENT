using TechShop.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Exceptions;
using TechShop.Models;
using static TechShop.Exceptions.ExceptionHandling;

namespace TechShop.Models
{
    // Class represents individual line items within an order
    // Responsible for managing product quantities, pricing, and discounts for each order item
    public class OrderDetails
    {
        // Unique identifier for this order detail entry
        private int order_details_id { get; set; }

        // Reference to the parent order containing this detail
        private Orders _orders { get; set; }
        
        // Reference to the product being ordered
        private Products _products { get; set; }
        public Products GetProduct()
        {
            return _products;
        }

        // Number of units ordered for this product
        private int quantity { get; set; }
        
        // Percentage discount applied to this item (0-100)
        private double discount { get; set; } = 0;

        // Property to validate and ensure positive order quantities
        public int orderquantity
        {
            get { return quantity; }
            set
            {
                if (value < 0)
                {
                    throw new InsufficientStockException("Warning! Quantity must be Positive");
                }
                else quantity = value;
            }
        }

        // Property to validate and manage discount percentage between 0-100
        public double Discount
        {
            get { return discount; }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ExceptionHandling.InvalidDataException("Invalid discount! Must be between 0 and 100.");
                }
                discount = value;
            }
        }

        // Database accessor methods for retrieving order detail information
        public int GetOrderDetailId() => order_details_id;
        public int GetQuantity() => quantity;
        public int GetOrderId() => _orders.order_id;
        public int GetProductId() => _products.GetProductId();

        // Constructor initializes a new order detail with required information
        public OrderDetails(int order_details_id, Orders _orders, Products _products, int quantity)
        {
            this.order_details_id = order_details_id;
            this._orders = _orders;
            if (_products == null)
            {
                throw new IncompleteOrderException("Error: The order must have a valid product reference.");
            }

            this._products = _products;
            orderquantity = quantity;
        }

        // Calculates the final price for this line item after applying any discounts
        public double CalculateSubtotal()
        {
            return quantity * _products.productprice * (1 - discount / 100);
        }

        // Displays detailed information about this order item including pricing
        public void GetOrderDetailInfo()
        {
            Console.WriteLine("------------ Order Details ------------");
            Console.WriteLine($"Order ID   : {order_details_id}");
            Console.WriteLine($"Product    : {_products.product_name}");
            Console.WriteLine($"Quantity   : {quantity}");
            Console.WriteLine($"Unit Price : {_products.productprice:C}");
            Console.WriteLine($"Discount   : {discount}%");
            Console.WriteLine($"Subtotal   : {CalculateSubtotal()}");
            Console.WriteLine("--------------------------------------");
        }

        // Updates the quantity for this order item with validation
        public void UpdateQuantity(int new_quantity)
        {
            try
            {
                this.orderquantity = new_quantity;
                Console.WriteLine($"Quantity updated to {quantity}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating quantity: {ex.Message}");
            }
        }

        // Applies a validated discount percentage to this order item
        public void AddDiscount(double discountPercentage)
        {
            try
            {
                this.Discount = discountPercentage;
                Console.WriteLine($"Discount of {discount}% is applied!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error applying discount: {ex.Message}");
            }
        }
    }
}