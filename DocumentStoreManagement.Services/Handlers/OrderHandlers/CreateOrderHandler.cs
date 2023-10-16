using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Commands.OrderCommands;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.OrderHandlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Order>
    {
        /// <summary>
        /// Handler to create order
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        public async Task<Order> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return command.Order;
        }
    }
}
