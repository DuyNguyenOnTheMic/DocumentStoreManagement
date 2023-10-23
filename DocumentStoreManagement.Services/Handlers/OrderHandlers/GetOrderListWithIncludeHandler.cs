using DocumentStoreManagement.Core;
using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Queries.OrderQueries;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.OrderHandlers
{
    public class GetOrderListWithIncludeHandler(IQueryRepository<Order> orderRepository) : IRequestHandler<GetOrderListWithIncludeQuery, IEnumerable<Order>>
    {
        private readonly IQueryRepository<Order> _orderRepository = orderRepository;

        public async Task<IEnumerable<Order>> Handle(GetOrderListWithIncludeQuery request, CancellationToken cancellationToken)
        {
            string orderDetailsTableName = CustomConstants.OrderDetailsTable.Trim('"');
            return await _orderRepository.GetAllWithIncludeAsync(CustomConstants.OrdersTable, orderDetailsTableName);
        }
    }
}
