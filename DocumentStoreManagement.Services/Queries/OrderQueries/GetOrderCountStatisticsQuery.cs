using DocumentStoreManagement.Core.DTOs;
using MediatR;

namespace DocumentStoreManagement.Services.Queries.OrderQueries
{
    /// <summary>
    /// Query class to get order count statistics
    /// </summary>
    /// <param name="From"></param>
    /// <param name="To"></param>
    public record GetOrderCountStatisticsQuery(DateTime From, DateTime To) : IRequest<IEnumerable<OrderStatisticsDTO>>;
}
