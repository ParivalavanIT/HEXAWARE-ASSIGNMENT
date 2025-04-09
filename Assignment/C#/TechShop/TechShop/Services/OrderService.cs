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
    public class OrderService : IOrderService
    {
        public bool PlaceOrder(Orders order)
        {
            try
            {
                if (order == null)
                {
                    throw new ExceptionHandling.IncompleteOrderException("Order is incomplete or null.");
                }

                return DataBaseConnector.ProcessingOrder(order);
            }
            catch (Exception ex)
            {
                throw new ExceptionHandling.DatabaseSqlException("Failed to place order: " + ex.Message);
            }
        }

        public void CancelOrder(int orderId)
        {
            try
            {
                Orders.CancelOrder(orderId, "Cancelled");
            }
            catch (Exception ex)
            {
                throw new ExceptionHandling.DatabaseSqlException("Failed to cancel order: " + ex.Message);
            }
        }



        public List<string> GetOrderStatusByCustomerId(int customerId)
        {
            try
            {
                return DataBaseConnector.GetCustomerOrderStatuses(customerId);
            }
            catch (Exception ex)
            {
                throw new ExceptionHandling.DatabaseSqlException("Failed to get order status: " + ex.Message);
            }
        }

        public void AddOrder(Orders order)
        {
            throw new NotImplementedException();
        }

        public void AddOrderDetail(int orderId, Products product, int quantity)
        {
            throw new NotImplementedException();
        }

        public void RemoveCancelledOrders()
        {
            DataBaseConnector.RemoveCancelledOrders();
        }

        public void SortOrdersByDate(bool ascending = true)
        {
            throw new NotImplementedException();
        }

        bool IOrderService.CancelOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public List<Orders> GetAllOrders()
        {
            throw new NotImplementedException();
        }
    }
}