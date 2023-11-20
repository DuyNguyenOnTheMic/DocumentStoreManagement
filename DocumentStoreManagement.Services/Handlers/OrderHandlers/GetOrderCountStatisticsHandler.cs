using DocumentStoreManagement.Core;
using DocumentStoreManagement.Core.DTOs;
using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Services.Queries.OrderQueries;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.OrderHandlers
{
    public class GetOrderCountStatisticsHandler(IQueryRepository<OrderStatisticsDTO> orderRepository) : IRequestHandler<GetOrderCountStatisticsQuery, IEnumerable<OrderStatisticsDTO>>
    {
        private readonly IQueryRepository<OrderStatisticsDTO> _orderRepository = orderRepository;

        public async Task<IEnumerable<OrderStatisticsDTO>> Handle(GetOrderCountStatisticsQuery request, CancellationToken cancellationToken)
        {
            // Format datetime into sql server datetime query
            string format = "yyyy-MM-dd HH:mm:ss.fff";
            string fromFormatted = request.From.ToString(format);
            string toFormatted = request.To.ToString(format);

            // Declare table and column names for query
            string table = CustomConstants.OrdersTable;
            string borrowDate = nameof(OrderStatisticsDTO.BorrowDate);
            string orderCount = nameof(OrderStatisticsDTO.OrderCount);

            // Get orders group by borrow date to get count
            string query = $@"SELECT date_trunc('day', ""{borrowDate}"") AS ""{borrowDate}"", COUNT(*) AS ""{orderCount}"" "
                           + $@"FROM {table} "
                           + $@"WHERE ""{borrowDate}"" BETWEEN '{fromFormatted}' AND '{toFormatted}'"
                           + $@"GROUP BY date_trunc('day', ""{borrowDate}"") "
                           + $@"ORDER BY date_trunc('day', ""{borrowDate}"") DESC";

            return await _orderRepository.GetAsync(query);
        }
    }
}
