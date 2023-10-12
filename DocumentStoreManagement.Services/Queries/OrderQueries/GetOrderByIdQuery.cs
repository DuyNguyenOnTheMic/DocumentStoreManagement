using DocumentStoreManagement.Core.Models;
using MediatR;

namespace DocumentStoreManagement.Services.Queries.OrderQueries
{
    /// <summary>
    /// Query class to find order by id
    /// </summary>
    /// <param name="Id"></param>
    public record GetOrderByIdQuery(string Id) : IRequest<Order>;
}
