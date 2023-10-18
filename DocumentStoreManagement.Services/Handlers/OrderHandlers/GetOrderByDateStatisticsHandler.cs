using DocumentStoreManagement.Core;
using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Queries.OrderQueries;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.OrderHandlers
{
    public class GetOrderByDateStatisticsHandler : IRequestHandler<GetOrderByDateStatisticsQuery, IEnumerable<Order>>
    {
        private readonly IQueryRepository<Order> _orderRepository;

        public GetOrderByDateStatisticsHandler(IQueryRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Handler to get orders by dates
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Order>> Handle(GetOrderByDateStatisticsQuery request, CancellationToken cancellationToken)
        {
            // Format datetime into sql server datetime query
            string format = "yyyy-MM-dd HH:mm:ss.fff";
            string fromFormatted = request.From.ToString(format);
            string toFormatted = request.To.ToString(format);

            return await _orderRepository.GetBetweenDatesAsync(CustomConstants.OrdersTable, nameof(Order.BorrowDate), fromFormatted, toFormatted);
        }
    }
}
