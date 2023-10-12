using DocumentStoreManagement.Core.Models;
using MediatR;

namespace DocumentStoreManagement.Services.Queries.OrderQueries
{
    public record GetOrderListQuery : IRequest<IEnumerable<Order>>;
}
