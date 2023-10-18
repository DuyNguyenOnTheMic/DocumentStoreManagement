using DocumentStoreManagement.Core.DTOs;
using DocumentStoreManagement.Core.Models;

namespace DocumentStoreManagement.Services.Interfaces
{
    /// <summary>
    /// Interface for order service
    /// </summary>
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAll();
        Task<Order> GetById(string id);
        Task<IEnumerable<Order>> GetWithInclude();
        Task<Order> Create(OrderDTO orderDTO);
        Task Update(OrderDTO order);
        Task Delete(Order order);
        Task<IEnumerable<Order>> GetDateStatistics(DateTime from, DateTime to);
    }
}
