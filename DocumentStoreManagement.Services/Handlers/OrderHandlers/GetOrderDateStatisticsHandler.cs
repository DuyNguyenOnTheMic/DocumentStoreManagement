﻿using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Queries.OrderQueries;
using MediatR;
using MongoDB.Driver;

namespace DocumentStoreManagement.Services.Handlers.OrderHandlers
{
    public class GetOrderDateStatisticsHandler : IRequestHandler<GetOrderDateStatisticsQuery, IEnumerable<Order>>
    {
        private readonly IRepository<Order> _orderRepository;

        public GetOrderDateStatisticsHandler(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Handler to get orders by dates
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Order>> Handle(GetOrderDateStatisticsQuery request, CancellationToken cancellationToken)
        {
            DateTime today = DateTime.Today;
            FilterDefinition<Order> myFilter = Builders<Order>.Filter
                .Where(x => x.BorrowDate >= request.From && x.BorrowDate <= request.To);
            return await _orderRepository.FindAsync(myFilter);
        }
    }
}