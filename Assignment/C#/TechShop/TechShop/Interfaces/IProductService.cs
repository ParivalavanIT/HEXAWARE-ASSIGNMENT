using TechShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop;

namespace TechShop.Interfaces
{
    // Interface for product management operations
    public interface IProductService
    {
        // Adds a new product to the system
        // Parameters:
        //   product: The product details to add
        // Returns: True if product was successfully added, false otherwise
        bool AddProduct(Products product);

        // Updates an existing product's information
        // Parameters:
        //   product: The updated product details
        // Returns: True if product was successfully updated, false otherwise
        bool UpdateProduct(Products product);

        // Retrieves a product by its unique identifier
        // Parameters:
        //   productId: The unique identifier of the product
        // Returns: The requested product object if found
        Products GetProductById(int productId);

        // Searches for products based on search criteria
        // Parameters:
        //   searchTerm: The search term to match against product names or descriptions
        // Returns: List of products matching the search criteria
        List<Products> SearchProducts(string searchTerm);

        // Retrieves all available products in the system
        // Returns: List of all available products
        List<Products> GetAllProducts();
    }
}