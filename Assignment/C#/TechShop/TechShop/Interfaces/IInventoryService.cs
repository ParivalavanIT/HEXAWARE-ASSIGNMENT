using TechShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop;

namespace TechShop.Interfaces
{
    public interface IInventoryService
    {
        void AddToInventory(int productId, Inventory inventory);
        void UpdateInventory(int productId, int quantityChange);
        void ViewInventory();

    }
}