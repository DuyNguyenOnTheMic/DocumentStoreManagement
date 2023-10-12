using DocumentStoreManagement.Core.Models;
using MediatR;

namespace DocumentStoreManagement.Services.Queries.OrderQueries
{
    /// <summary>
    /// Query class to get all orders
    /// </summary>
    public record GetOrderListQuery : IRequest<IEnumerable<Order>>;
}
