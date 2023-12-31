﻿using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Commands.OrderCommands;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.OrderHandlers
{
    /// <inheritdoc/>
    public class UpdateOrderHandler(IRepository<Order> orderRepository) : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IRepository<Order> _orderRepository = orderRepository;

        /// <summary>
        /// Handler to update order
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            await _orderRepository.UpdateAsync(command.Order);
        }
    }
}
