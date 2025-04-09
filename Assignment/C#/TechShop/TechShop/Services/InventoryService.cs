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
    public class InventoryService : IInventoryService
    {
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