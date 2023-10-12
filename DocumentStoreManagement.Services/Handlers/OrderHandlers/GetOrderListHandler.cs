using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Queries.OrderQueries;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.OrderHandlers
{
    public class GetOrderListHandler : IRequestHandler<GetOrderListQuery, IEnumerable<Order>>
    {
        private readonly IGenericRepository<Order> _orderRepository;

        public GetOrderListHandler(IGenericRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> Handle(GetOrderListQuery query, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetAllAsync();
        }
    }
}
