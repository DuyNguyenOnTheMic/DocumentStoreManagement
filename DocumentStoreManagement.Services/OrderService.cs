using DocumentStoreManagement.Core.DTOs;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Commands.OrderCommands;
using DocumentStoreManagement.Services.Interfaces;
using DocumentStoreManagement.Services.Queries.DocumentQueries;
using DocumentStoreManagement.Services.Queries.OrderQueries;
using MediatR;
using MongoDB.Bson;

namespace DocumentStoreManagement.Services
{
    /// <summary>
    /// Order service
    /// </summary>
    /// <param name="mediator"></param>
    public class OrderService(IMediator mediator) : IOrderService
    {
        private readonly IMediator _mediator = mediator;

        /// <inheritdoc/>
        public async Task<IEnumerable<Order>> GetAll()
        {
            // Get order list
            return await _mediator.Send(new GetOrderListQuery());
        }

        /// <inheritdoc/>
        public async Task<Order> GetById(string id)
        {
            // Get order by id
            return await _mediator.Send(new GetOrderByIdQuery(id));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Order>> GetWithInclude()
        {
            // Get orders with order details list
            return await _mediator.Send(new GetOrderListWithIncludeQuery());
        }

        /// <inheritdoc/>
        public async Task<Order> Create(OrderDTO orderDTO)
        {
            // Get order details from DTO
            ICollection<OrderDetailsDTO> orderDetailsDTO = orderDTO.OrderDetailsDTOs;

            // Create a list to store order details
            List<OrderDetail> orderDetails = [];

            // Generate new order id
            string orderId = ObjectId.GenerateNewId().ToString();

            // Loop through each order details to map and create a list of order details
            foreach (OrderDetailsDTO item in orderDetailsDTO)
            {
                // Check if document exists
                Document document = await _mediator.Send(new GetDocumentByIdQuery(item.DocumentId)) ?? throw new Exception("Document id not found!");

                // Generate new order details id
                string orderDetailsId = ObjectId.GenerateNewId().ToString();

                orderDetails.Add(new OrderDetail()
                {
                    Id = orderDetailsId,
                    Quantity = item.Quantity,
                    Total = document.UnitPrice * item.Quantity,
                    DocumentId = document.Id,
                    OrderId = orderId
                });
            }

            // Map order DTO
            Order order = new()
            {
                Id = orderId,
                FullName = orderDTO.FullName,
                PhoneNumber = orderDTO.PhoneNumber,
                BorrowDate = orderDTO.BorrowDate,
                ReturnDate = orderDTO.ReturnDate,
                Status = orderDTO.Status,
                OrderDetails = orderDetails
            };

            // Create new order
            return await _mediator.Send(new CreateOrderCommand(order));
        }

        /// <inheritdoc/>
        public async Task Delete(Order order)
        {
            // Delete order
            await _mediator.Send(new DeleteOrderCommand(order));
        }

        /// <inheritdoc/>
        public async Task Update(Order order)
        {
            // Get order details
            ICollection<OrderDetail> orderDetails = order.OrderDetails;

            // Loop through each order details to check for ids
            foreach (OrderDetail item in orderDetails)
            {
                // Check if document exists
                _ = await _mediator.Send(new GetDocumentByIdQuery(item.DocumentId)) ?? throw new Exception("Document id not found!");

                // Generate new order details id
                string orderDetailsId = ObjectId.GenerateNewId().ToString();

                item.Id ??= orderDetailsId;
            }

            // Update order
            await _mediator.Send(new UpdateOrderCommand(order));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Order>> GetByDateStatistics(DateTime from, DateTime to)
        {
            // Get orders by date
            return await _mediator.Send(new GetOrderByDateStatisticsQuery(from, to));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<OrderStatisticsDTO>> GetCountStatistics(DateTime from, DateTime to)
        {
            // Get orders count
            return await _mediator.Send(new GetOrderCountStatisticsQuery(from, to));
        }
    }
}
