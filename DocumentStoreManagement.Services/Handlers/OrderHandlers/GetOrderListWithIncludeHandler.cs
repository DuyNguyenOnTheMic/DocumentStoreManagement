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
            return await _orderRepository.GetAllAsync(CustomConstants.OrdersTable);
        }
    }
}
