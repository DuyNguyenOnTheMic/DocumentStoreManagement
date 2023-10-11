using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Commands.OrderCommands;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.OrderHandlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IGenericRepository<Order> _orderRepository;

        public CreateOrderHandler(IGenericRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Handler to create order
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        public async Task Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            await _orderRepository.AddAsync(command.Order);
        }
    }
}
