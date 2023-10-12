using DocumentStoreManagement.Core.Models;
using MediatR;

namespace DocumentStoreManagement.Services.Queries.OrderQueries
{
    public record GetOrderByIdQuery(string Id) : IRequest<Order>;
}
