using ElectronicGadgetsTechShop.Exceptions;
using ElectronicGadgetsTechShop.Interfaces;
using ElectronicGadgetsTechShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop;
using TechShop.Exceptions;

namespace ElectronicGadgetsTechShop.Services
{
    // Service class that handles product management operations including CRUD and search functionality
    public class ProductService : IProductService
    {
        // Adds a new product to the system
        // Returns true if product was successfully added
        // Throws DatabaseSqlException if database operation fails
        public bool AddProduct(Products product)
        {
            try
            {
                return DataBaseConnector.InsertProduct(product);
            }
            catch (Exception ex)
            {
                throw new ExceptionHandling.DatabaseSqlException("Failed to add product: " + ex.Message);
            }
        }

        // Updates an existing product's information
        // Returns true if product was successfully updated
        // Throws DatabaseSqlException if database operation fails
        public bool UpdateProduct(Products product)
        {
            try
            {
                return DataBaseConnector.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                throw new ExceptionHandling.DatabaseSqlException("Failed to update product: " + ex.Message);
            }
        }

        // Retrieves a product by its unique identifier
        // Returns the product if found
        // Throws InvalidDataException if product doesn't exist
        // Throws DatabaseSqlException if database operation fails
        public Products GetProductById(int productId)
        {
            try
            {
                var product = DataBaseConnector.GetProductById(productId);
                if (product == null)
                {
                    throw new ExceptionHandling.InvalidDataException($"Product with ID {productId} does not exist.");
                }
                return product;
            }
            catch (Exception ex)
            {
                throw new ExceptionHandling.DatabaseSqlException("Failed to retrieve product: " + ex.Message);
            }
        }

        // Searches for products matching the given keyword
        // Returns a list of matching products
        // Throws DatabaseSqlException if database operation fails
        public List<Products> SearchProducts(string keyword)
        {
            try
            {
                return DataBaseConnector.SearchProducts(keyword);
            }
            catch (Exception ex)
            {
                throw new ExceptionHandling.DatabaseSqlException("Failed to search products: " + ex.Message);
            }
        }

        // Retrieves all active products in the system
        // Returns a list of all products
        // Throws DatabaseSqlException if database operation fails
        public List<Products> GetAllProducts()
        {
            try
            {
                return DataBaseConnector.GetAllProducts();
            }
            catch (Exception ex)
            {
                throw new ExceptionHandling.DatabaseSqlException("Failed to retrieve products: " + ex.Message);
            }
        }

        // Removes a product from active inventory by marking it as discontinued
        // Does not physically delete the product record
        public void RemoveProduct(int productId)
        {
            DataBaseConnector.DiscontinueProduct(productId);
        }
    }
}