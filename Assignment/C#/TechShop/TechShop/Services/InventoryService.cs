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
    // Service class implementing inventory management operations
    public class InventoryService : IInventoryService
    {
        // Displays the current inventory status for all products
        // Throws DatabaseSqlException when database operation fails
        public void ViewInventory()
        {
            try
            {
                DataBaseConnector.ViewInventory();
            }
            catch (Exception ex)
            {
                throw new ExceptionHandling.DatabaseSqlException("Error viewing inventory: " + ex.Message);
            }
        }

        // Updates the quantity of a product in inventory
        // Parameters:
        //   productId: The unique identifier of the product
        //   quantity: The new quantity to set
        // Returns: True if update was successful, false otherwise
        // Throws: 
        //   - InvalidDataException when quantity is invalid
        //   - DatabaseSqlException when database operation fails
        public bool UpdateInventory(int productId, int quantity)
        {
            try
            {
                if (quantity <= 0)
                {
                    throw new ExceptionHandling.InvalidDataException("Quantity must be greater than zero.");
                }

                return DataBaseConnector.UpdateStock(productId, quantity);
            }
            catch (Exception ex)
            {
                throw new ExceptionHandling.DatabaseSqlException("Error updating inventory: " + ex.Message);
            }
        }

        // Adds a new inventory record for a product
        // Parameters:
        //   productId: The unique identifier of the product
        //   inventory: The inventory details to add
        public void AddToInventory(int productId, Inventory inventory)
        {
            throw new NotImplementedException();
        }

        void IInventoryService.UpdateInventory(int productId, int quantityChange)
        {
            throw new NotImplementedException();
        }
    }
}