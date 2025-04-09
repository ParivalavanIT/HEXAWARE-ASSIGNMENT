using TechShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop;

namespace TechShop.Interfaces
{
    // Interface for inventory management operations
    public interface IInventoryService
    {
        // Displays the current inventory status for all products
        void ViewInventory();

        // Updates the quantity of a product in inventory
        // Parameters:
        //   productId: The unique identifier of the product
        //   quantityChange: The amount to change the quantity by
        void UpdateInventory(int productId, int quantityChange);

        // Updates the quantity of a product in inventory
        // Parameters:
        //   productId: The unique identifier of the product
        //   quantity: The new quantity to set
        // Returns: True if update was successful, false otherwise
        bool UpdateInventory(int productId, int quantity);

        // Adds a new inventory record for a product
        // Parameters:
        //   productId: The unique identifier of the product
        //   inventory: The inventory details to add
        void AddToInventory(int productId, Inventory inventory);
    }
}