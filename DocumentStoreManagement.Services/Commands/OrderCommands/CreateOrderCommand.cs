using DocumentStoreManagement.Core.Models;
using MediatR;

namespace DocumentStoreManagement.Services.Commands.OrderCommands
{
    /// <summary>
    /// Command class to create document
    /// </summary>
    public record CreateOrderCommand(Order Order) : IRequest;
}
