using DocumentStoreManagement.Core.Models;
using MediatR;

namespace DocumentStoreManagement.Services.Queries.OrderQueries
{
    /// <summary>
    /// Query class to get all orders with include
    /// </summary>
    public record GetOrderListWithIncludeQuery : IRequest<IEnumerable<Order>>;
}
