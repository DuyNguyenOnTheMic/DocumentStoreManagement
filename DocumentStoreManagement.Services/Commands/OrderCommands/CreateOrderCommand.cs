using DocumentStoreManagement.Core.Models;
using MediatR;

namespace DocumentStoreManagement.Services.Commands.OrderCommands
{
    /// <summary>
    /// Command class to create order
    /// </summary>
    public record CreateOrderCommand(Order Order) : IRequest;
}
