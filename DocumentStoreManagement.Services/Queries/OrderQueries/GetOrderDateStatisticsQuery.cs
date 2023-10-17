using DocumentStoreManagement.Core.Models;
using MediatR;

namespace DocumentStoreManagement.Services.Queries.OrderQueries
{
    /// <summary>
    /// Query class to get order date statistics
    /// </summary>
    public record GetOrderDateStatisticsQuery(DateTime From, DateTime To) : IRequest<IEnumerable<Order>>;
}

