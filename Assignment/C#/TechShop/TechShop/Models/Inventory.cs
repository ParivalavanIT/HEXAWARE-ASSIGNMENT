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
    // Class responsible for tracking and managing product stock levels
    // Handles inventory operations like stock updates and availability checks
    public class Inventory
    {
        // Unique ID for this inventory record
        private int inventory_id { get; set; }

        // Reference to the associated product
        private Products _product { get; set; }

        // Current stock quantity
        private int quantity_in_stock { get; set; }

        // Property to access and validate stock quantity
        public int QuantityInStock
        {
            get { return quantity_in_stock; }
        }

        // Last time the inventory was updated
        private DateTime last_stock_update { get; set; }

        // Methods to access inventory data
        public int GetInventoryId() => inventory_id;
        public int GetProductId() => _product.GetProductId();

        // Initialize inventory record with required data
        public Inventory(int inventory_id, Products _product, int quantity_in_stock)
        {
            this.inventory_id = inventory_id;
            this._product = _product;
            this.quantity_in_stock = quantity_in_stock;
            last_stock_update = DateTime.Now;
        }

        // Get the product associated with this inventory
        public Products GetProduct()
        {
            return _product;
        }

        // Get current stock level
        public int GetQuantityInStock()
        {
            return quantity_in_stock;
        }

        // Add stock to inventory with validation
        public void AddToInventory(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ExceptionHandling.InvalidDataException("Warning! Invalid quantity to add. Must be greater than zero. ");
            }
            quantity_in_stock += quantity;
            last_stock_update = DateTime.Now;
            Console.WriteLine($"Added {quantity} units. New stock: {quantity_in_stock}");
        }

        // Remove stock from inventory with validation
        public bool RemoveFromInventory(int quantity)
        {
            if (quantity <= 0)
            {
                Console.WriteLine("Invalid quantity to remove.");
                return false;
            }
            if (quantity > quantity_in_stock)
            {
                throw new InsufficientStockException("Insufficient stock for the requested quantity.");
            }
            quantity_in_stock -= quantity;
            last_stock_update = DateTime.Now;
            Console.WriteLine($"Removed {quantity} units. Remaining stock: {quantity_in_stock}");
            return true;
        }

        // Check if product is out of stock
        public bool ListOutOfStockProducts()
        {
            return QuantityInStock == 0;
        }

        // Display all products and their quantities
        public static void ListAllProducts()
        {
            // query to retur all products details...
        }
    }
}