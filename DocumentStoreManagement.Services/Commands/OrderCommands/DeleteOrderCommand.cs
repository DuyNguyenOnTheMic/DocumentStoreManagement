using DocumentStoreManagement.Core.Models;
using MediatR;

namespace DocumentStoreManagement.Services.Commands.OrderCommands
{
    /// <summary>
    /// Command class to delete order
    /// </summary>
    /// <param name="Id"></param>
    public record DeleteOrderCommand(Order Order) : IRequest;
}
