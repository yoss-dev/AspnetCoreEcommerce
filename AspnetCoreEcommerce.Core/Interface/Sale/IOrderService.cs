using AspnetCoreEcommerce.Core.Domain.Sale;
using System;
using System.Collections.Generic;

namespace AspnetCoreEcommerce.Core.Interface.Sale
{
    public interface IOrderService
    {
        /// <summary>
        /// Get all Orders
        /// </summary>
        /// <returns>List of Order entities</returns>
        IList<Order> GetAllOrders();

        /// <summary>
        /// Get Order by Id
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <returns>Order Entity</returns>
        Order GetOrderById(Guid id);

        /// <summary>
        /// Get order by orderId
        /// </summary>
        /// <param name="orderId">Order Id</param>
        /// <returns>Order entity</returns>
        Order GetOrderByOrderId(string orderId);

        /// <summary>
        /// Get all orders by user id
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>List of Orders entities</returns>
        IList<Order> GetAllOrdersByUserId(Guid userId);

        /// <summary>
        /// Insert Order
        /// </summary>
        /// <param name="order">Order entity</param>
        void InsertOrder(Order order);

        /// <summary>
        /// Update Order
        /// </summary>
        /// <param name="order">Order Entity</param>
        void UpdateOrder(Order order);

        /// <summary>
        /// Delete all Orders
        /// </summary>
        /// <param name="ordersIds">List of order ids</param>
        void DeleteOrders(IList<Guid> ordersIds);


    }
}
