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
            // Declare variables
            Order order;
            OrderDetail orderDetail;
            string orderTable = CustomConstants.OrdersTable;
            string orderDetailTable = CustomConstants.OrderDetailsTable;

            // Query from orders table join with orderDetails
            string query =
                $@"SELECT
                    o.*,
	                od.""{nameof(orderDetail.Id)}"",
	                od.""{nameof(orderDetail.Quantity)}"",
	                od.""{nameof(orderDetail.Total)}"",
	                od.""{nameof(orderDetail.DocumentId)}""
                FROM 
                    {orderTable} o
                INNER JOIN 
                    {orderDetailTable} od ON o.""{nameof(order.Id)}"" = od.""{nameof(orderDetail.OrderId)}"";";

            return await _orderRepository.GetAsync(query);
        }
    }
}
