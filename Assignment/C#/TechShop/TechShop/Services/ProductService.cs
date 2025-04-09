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
    public class ProductService : IProductService
    {
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

        public void RemoveProduct(int productId)
        {
            DataBaseConnector.DiscontinueProduct(productId);
        }
    }
}