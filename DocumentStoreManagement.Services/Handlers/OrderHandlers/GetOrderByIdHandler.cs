using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Queries.OrderQueries;
using MediatR;

namespace DocumentStoreManagement.Services.Handlers.OrderHandlers
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, Order>
    {
        private readonly IGenericRepository<Order> _orderRepository;

        public GetOrderByIdHandler(IGenericRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Hanlder to find order by id
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        public async Task<Order> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetByIdAsync(query.Id);
        }
    }
}
