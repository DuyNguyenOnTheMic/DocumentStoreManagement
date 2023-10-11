using DocumentStoreManagement.Core.DTOs;
using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DocumentStoreManagement.Controllers
{
    /// <summary>
    /// Order Management API Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly DbContext _context;

        public OrdersController(IUnitOfWork unitOfWork, IOrderService orderService, IGenericRepository<Order> orderRepository, DbContext context) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _orderService = orderService;
            _orderRepository = orderRepository;
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _context.Set<Order>().Include(x => x.OrderDetails).ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(string id)
        {
            Order order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(string id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            await _orderRepository.UpdateAsync(order);

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> PostOrder(OrderDTO orderDTO)
        {
            Order order = await _orderService.Create(orderDTO);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            Order order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            await _orderRepository.RemoveAsync(order);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        private async Task<bool> OrderExists(string id)
        {
            return await _orderRepository.CheckExistsAsync(e => e.Id == id);
        }
    }
}
