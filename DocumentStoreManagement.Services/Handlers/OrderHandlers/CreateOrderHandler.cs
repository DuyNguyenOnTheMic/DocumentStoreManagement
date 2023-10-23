using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Commands.OrderCommands;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.OrderHandlers
{
    public class CreateOrderHandler(IRepository<Order> orderRepository) : IRequestHandler<CreateOrderCommand, Order>
    {
        private readonly IRepository<Order> _orderRepository = orderRepository;

        /// <summary>
        /// Handler to create order
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        public async Task<Order> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            Order order = command.Order;
            await _orderRepository.AddAsync(order);
            return order;
        }
    }
}
