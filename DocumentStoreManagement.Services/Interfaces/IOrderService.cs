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
        Task Create(Order order);
        Task Update(Order order);
        Task Delete(string id);
    }
}
