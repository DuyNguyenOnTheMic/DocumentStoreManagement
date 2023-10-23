using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocumentStoreManagement.Controllers
{
    /// <summary>
    /// Test controller
    /// </summary>
    /// <remarks>
    /// Add dependencies to controller
    /// </remarks>
    /// <param name="bookRepository"></param>
    /// <param name="orderRepository"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class TestController(IRepository<Book> bookRepository, IRepository<Order> orderRepository) : ControllerBase
    {
        private readonly IRepository<Book> _bookRepository = bookRepository;
        private readonly IRepository<Order> _orderRepository = orderRepository;

        /// <summary>
        /// Method to get books by Entity Framework
        /// </summary>
        [HttpGet("books")]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            // Get list of books
            return await _bookRepository.GetAllAsync();
        }

        /// <summary>
        /// Method to get orders by Entity Framework
        /// </summary>
        [HttpGet("orders")]
        public async Task<IEnumerable<Order>> GetOrders()
        {
            // Get list of orders
            return await _orderRepository.GetAllAsync();
        }

        /// <summary>
        /// Method to get orders with include by Entity Framework
        /// </summary>
        [HttpGet("orders/include")]
        public async Task<IEnumerable<Order>> GetOrdersWithInclude()
        {
            // Get list of orders with include
            return await _orderRepository.GetAllWithIncludeAsync(x => x.OrderDetails);
        }
    }
}
