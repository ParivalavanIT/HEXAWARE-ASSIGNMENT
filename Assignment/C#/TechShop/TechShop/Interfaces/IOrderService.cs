using TechShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop;

namespace TechShop.Interfaces
{
    /// <summary>
    /// Interface defining order management operations for the TechShop system
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Places a new order in the system
        /// </summary>
        /// <param name="order">The order details to be placed</param>
        /// <returns>True if order was successfully placed, false otherwise</returns>
        bool PlaceOrder(Orders order);

        /// <summary>
        /// Cancels an existing order
        /// </summary>
        /// <param name="orderId">The unique identifier of the order to cancel</param>
        /// <returns>True if order was successfully cancelled, false otherwise</returns>
        bool CancelOrder(int orderId);

        /// <summary>
        /// Adds a new order to the system
        /// </summary>
        /// <param name="order">The order object containing all required information</param>
        void AddOrder(Orders order);

        /// <summary>
        /// Adds a product detail to an existing order
        /// </summary>
        /// <param name="orderId">The unique identifier of the order</param>
        /// <param name="product">The product to add to the order</param>
        /// <param name="quantity">The quantity of the product to add</param>
        void AddOrderDetail(int orderId, Products product, int quantity);

        /// <summary>
        /// Removes all cancelled orders from the system
        /// </summary>
        void RemoveCancelledOrders();

        /// <summary>
        /// Sorts all orders by their date
        /// </summary>
        /// <param name="ascending">True for ascending order, false for descending</param>
        void SortOrdersByDate(bool ascending = true);

        /// <summary>
        /// Retrieves all orders in the system
        /// </summary>
        /// <returns>List of all orders</returns>
        List<Orders> GetAllOrders();

        /// <summary>
        /// Retrieves the status of all orders for a specific customer
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer</param>
        /// <returns>List of order status strings for the customer</returns>
        List<string> GetOrderStatusByCustomerId(int customerId);
    }
}