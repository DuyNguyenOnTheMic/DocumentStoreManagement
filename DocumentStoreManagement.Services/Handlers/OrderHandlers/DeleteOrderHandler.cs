using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Commands.OrderCommands;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.OrderHandlers
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IRepository<Order> _orderRepository;

        public DeleteOrderHandler(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Handler to delete order
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            await _orderRepository.RemoveAsync(command.Order);
        }
    }
}
