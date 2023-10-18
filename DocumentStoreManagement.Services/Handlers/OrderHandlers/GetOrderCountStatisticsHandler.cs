using DocumentStoreManagement.Core;
using DocumentStoreManagement.Core.DTOs;
using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Queries.OrderQueries;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.OrderHandlers
{
    public class GetOrderCountStatisticsHandler : IRequestHandler<GetOrderCountStatisticsQuery, IEnumerable<OrderStatisticsDTO>>
    {
        private readonly IQueryRepository<Order> _orderRepository;

        public GetOrderCountStatisticsHandler(IQueryRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderStatisticsDTO>> Handle(GetOrderCountStatisticsQuery request, CancellationToken cancellationToken)
        {
            // Format datetime into sql server datetime query
            string format = "yyyy-MM-dd HH:mm:ss.fff";
            string fromFormatted = request.From.ToString(format);
            string toFormatted = request.To.ToString(format);

            // Get orders group by borrow date to get count
            var orders = await _orderRepository.GetBetweenDatesAsync(CustomConstants.OrdersTable, nameof(Order.BorrowDate), fromFormatted, toFormatted);
            var orderStatistics =
                orders.GroupBy(o => o.BorrowDate.Date)
                      .Select(g => new OrderStatisticsDTO { BorrowDate = g.Key, OrderCount = g.Count() })
                      .OrderByDescending(o => o.BorrowDate);

            return orderStatistics;
        }
    }
}
