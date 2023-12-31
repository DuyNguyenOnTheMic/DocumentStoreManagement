﻿using DocumentStoreManagement.Core.Models;
using MediatR;

namespace DocumentStoreManagement.Services.Commands.OrderCommands
{
    /// <summary>
    /// Command class to create order
    /// </summary>
    /// <param name="Order"></param>
    public record CreateOrderCommand(Order Order) : IRequest<Order>;
}
