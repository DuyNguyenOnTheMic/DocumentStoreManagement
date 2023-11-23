using DocumentStoreManagement.Core.DTOs;
using DocumentStoreManagement.Core.Models;

namespace DocumentStoreManagement.Services.Interfaces
{
    /// <summary>
    /// Interface for order service
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns>A list of orders</returns>
        Task<IEnumerable<Order>> GetAll();

        /// <summary>
        /// Find order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An order</returns>
        Task<Order> GetById(string id);

        /// <summary>
        /// Get all orders with include
        /// </summary>
        /// <returns>A list of orders with child table included</returns>
        Task<IEnumerable<Order>> GetWithInclude();

        /// <summary>
        /// Create new order
        /// </summary>
        /// <param name="orderDTO"></param>
        /// <returns>A newly created order</returns>
        Task<Order> Create(OrderDTO orderDTO);

        /// <summary>
        /// Update an order
        /// </summary>
        /// <param name="order"></param>
        /// <returns>Nothing</returns>
        Task Update(Order order);

        /// <summary>
        /// Delete an order
        /// </summary>
        /// <param name="order"></param>
        /// <returns>Nothing</returns>
        Task Delete(Order order);

        /// <summary>
        /// Get orders by date statistics
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>A list of orders between 2 dates</returns>
        Task<IEnumerable<Order>> GetByDateStatistics(DateTime from, DateTime to);

        /// <summary>
        /// Get order count statistics
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>A list of order statistics</returns>
        Task<IEnumerable<OrderStatisticsDTO>> GetCountStatistics(DateTime from, DateTime to);
    }
}
