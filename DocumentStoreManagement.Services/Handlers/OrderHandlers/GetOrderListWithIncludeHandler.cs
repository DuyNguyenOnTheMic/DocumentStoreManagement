using DocumentStoreManagement.Core;
using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Queries.OrderQueries;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.OrderHandlers
{
    public class GetOrderListWithIncludeHandler : IRequestHandler<GetOrderListWithIncludeQuery, IEnumerable<Order>>
    {
        private readonly IQueryRepository<Order> _orderRepository;

        public GetOrderListWithIncludeHandler(IQueryRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> Handle(GetOrderListWithIncludeQuery request, CancellationToken cancellationToken)
        {
            string orderDetailsTableName = CustomConstants.OrderDetailsTable.Trim('"');
            return await _orderRepository.GetAllWithIncludeAsync(CustomConstants.OrdersTable, orderDetailsTableName);
        }
    }
}
