using DocumentStoreManagement.Core.Models;
using MediatR;

namespace DocumentStoreManagement.Services.Queries.OrderQueries
{
    /// <summary>
    /// Query class to get order date statistics
    /// </summary>
    /// <param name="From"></param>
    /// <param name="To"></param>
    public record GetOrderByDateStatisticsQuery(DateTime From, DateTime To) : IRequest<IEnumerable<Order>>;
}

