using TechShop.Exceptions;
using TechShop.Interfaces;
using TechShop.Models;
using TechShop.Services;
using System.Data.SqlClient;
using TechShop.Exceptions;
using TechShop.Models;
using static TechShop.Exceptions.ExceptionHandling;

namespace TechShop
{
    public class StoreManager
    {
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private readonly IInventoryService _inventoryService;
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;


        public StoreManager(IProductService productService, ICustomerService customerService,
                    IInventoryService inventoryService, IOrderService orderService, IPaymentService paymentService)
        {
            _productService = productService;
            _customerService = customerService;
            _inventoryService = inventoryService;
            _orderService = orderService;
            _paymentService = paymentService;
        }




        public void DisplayAllProducts()
        {
            try
            {
                List<Products> products = _productService.GetAllProducts();

                if (products.Count == 0)
                {
                    Console.WriteLine("No products available.");
                    return;
                }

                Console.WriteLine("\nAvailable Products:");
                Console.WriteLine("-------------------------------------------------");
                foreach (Products product in products)
                {
                    Console.WriteLine($"ID: {product.GetProductId()}");
                    Console.WriteLine($"Name: {product.GetProductName()}");
                    Console.WriteLine($"Description: {product.GetProductDesc()}");
                    Console.WriteLine($"Price: ₹{product.GetProductPrice()}");
                    Console.WriteLine("-------------------------------------------------");
                }
            }
            catch (ExceptionHandling.DatabaseSqlException ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error: " + ex.Message);
            }
        }

        public void DisplayAllOrders()
        {
            try
            {
                List<Orders> orders = DataBaseConnector.GetAllOrders();

                Console.WriteLine("\n--- All Orders ---");

                if (orders == null || orders.Count == 0)
                {
                    Console.WriteLine("No orders found.");
                    return;
                }

                foreach (Orders order in orders)
                {
                    Console.WriteLine($"Order ID     : {order.order_id}");
                    Console.WriteLine($"Customer ID  : {order.GetCustomer().GetCustomerId()}");
                    Console.WriteLine($"Order Date   : {order.GetOrderDate()}");
                    Console.WriteLine($"Total Amount : ₹{order.GetTotalAmount()}");
                    Console.WriteLine($"Status       : {order.GetOrderStatus()}");
                    Console.WriteLine("----------------------------------------");
                }
            }
            catch (ExceptionHandling.DatabaseSqlException ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error: " + ex.Message);
            }
        }


        public Products GetProductById(int prodId)
        {
            try
            {
                Products product = _productService.GetProductById(prodId);

                if (product == null)
                {
                    throw new ExceptionHandling.InvalidDataException($"Product with ID {prodId} not found.");
                }

                return product;
            }
            catch (ExceptionHandling.InvalidDataException ex)
            {
                Console.WriteLine("Validation Error: " + ex.Message);
            }
            catch (ExceptionHandling.DatabaseSqlException ex)
            {
                Console.WriteLine("Database Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error: " + ex.Message);
            }

            return null;
        }

        public void AddProduct(Products product)
        {
            try
            {
                _productService.AddProduct(product);
                Console.WriteLine("Product added successfully.");
            }
            catch (ExceptionHandling.InvalidDataException ex)
            {
                Console.WriteLine("Data Validation Error: " + ex.Message);
            }
            catch (ExceptionHandling.DatabaseSqlException ex)
            {
                Console.WriteLine("Database Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error: " + ex.Message);
            }
        }

        public bool UpdateProduct(Products updatedProduct)
        {
            try
            {
                bool updated = _productService.UpdateProduct(updatedProduct);
                if (updated)
                {
                    Console.WriteLine("Product updated successfully.");
                }
                else
                {
                    Console.WriteLine("Product update failed. Product not found.");
                }
                return updated;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error: " + ex.Message);
                return false;
            }
        }

        public bool RemoveProduct(int productId)
        {
            try
            {
                _productService.RemoveProduct(productId);
                Console.WriteLine($"Product '{productId}' removed successfully.");
                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error: " + ex.Message);
                return false;
            }
        }




        public void AddOrder(Orders order)
        {
            try
            {
                _orderService.AddOrder(order);
                Console.WriteLine("Order added successfully.");
            }
            catch (ExceptionHandling.InvalidDataException ex)
            {
                Console.WriteLine("Invalid Data: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error: " + ex.Message);
            }
        }


        public void AddOrderDetail(int orderId, Products product, int quantity)
        {
            try
            {
                _orderService.AddOrderDetail(orderId, product, quantity);
                Console.WriteLine("Order detail added successfully.");
            }
            catch (ExceptionHandling.InvalidDataException ex)
            {
                Console.WriteLine("Invalid Data: " + ex.Message);
            }
            catch (ExceptionHandling.InsufficientStockException ex)
            {
                Console.WriteLine("Stock Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error: " + ex.Message);
            }
        }




        public void RemoveCancelledOrders()
        {
            try
            {
                _orderService.RemoveCancelledOrders();
                Console.WriteLine("Cancelled orders removed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error removing cancelled orders: " + ex.Message);
            }
        }

        public void SortOrdersByDate(bool ascending = true)
        {
            try
            {
                _orderService.SortOrdersByDate(ascending);
                Console.WriteLine($"Orders sorted by date ({(ascending ? "ascending" : "descending")}).");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sorting orders: " + ex.Message);
            }
        }

        public void AddToInventory(int productId, Inventory inventory)
        {
            try
            {
                _inventoryService.AddToInventory(productId, inventory);
                Console.WriteLine("Inventory added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding inventory: " + ex.Message);
            }
        }

        public void UpdateInventory(int productId, int quantityChange)
        {
            try
            {
                _inventoryService.UpdateInventory(productId, quantityChange);
                Console.WriteLine("Inventory updated successfully.");
            }
            catch (ExceptionHandling.DatabaseSqlException ex)
            {
                Console.WriteLine("Database Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating inventory: " + ex.Message);
            }
        }

        public List<Products> SearchProducts(string name)
        {
            try
            {
                return _productService.SearchProducts(name);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error searching for products: " + ex.Message);
                return new List<Products>();
            }
        }

        public void RecordPayment(PaymentRecord payment)
        {
            try
            {
                _paymentService.RecordPayment(payment);
                Console.WriteLine("Payment recorded successfully.");
            }
            catch (ExceptionHandling.PaymentFailedException ex)
            {
                Console.WriteLine("Payment Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error recording payment: " + ex.Message);
            }
        }



    }
}