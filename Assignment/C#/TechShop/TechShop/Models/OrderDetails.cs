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
    public class OrderDetails
    {

        //Attributes

        private int order_details_id { get; set; }

        // adding composition for OrderDetails with products and orders tables..

        private Orders _orders { get; set; }
        private Products _products { get; set; }
        public Products GetProduct()
        {
            return _products;
        }
        private int quantity { get; set; }
        private double discount { get; set; } = 0;

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


        //getter methods for database
        public int GetOrderDetailId() => order_details_id;
        public int GetQuantity() => quantity;
        public int GetOrderId() => _orders.order_id;
        public int GetProductId() => _products.GetProductId();



        //Parameterized Constructor
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

        //Methods
        // To calculate subtotal
        public double CalculateSubtotal()
        {
            return quantity * _products.productprice * (1 - discount / 100);
        }

        // To get info about order details
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

        // To update quantity of products
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

        // To apply a discount
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