using DocumentStoreManagement.Core.Models;
using MediatR;

namespace DocumentStoreManagement.Services.Commands.OrderCommands
{
    /// <summary>
    /// Command class to update order
    /// </summary>
    /// <param name="Order"></param>
    public record UpdateOrderCommand(Order Order) : IRequest;
}
