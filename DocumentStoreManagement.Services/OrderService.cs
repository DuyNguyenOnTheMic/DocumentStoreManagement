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
    public class OrderService : IOrderService
    {
        private readonly IMediator _mediator;

        public OrderService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            // Get order list
            return await _mediator.Send(new GetOrderListQuery());
        }

        public async Task<Order> GetById(string id)
        {
            // Get order by id
            return await _mediator.Send(new GetOrderByIdQuery(id));
        }

        public async Task<Order> Create(OrderDTO orderDTO)
        {
            // Get order details from DTO
            ICollection<OrderDetailsDTO> orderDetailsDTO = orderDTO.OrderDetailsDTOs;

            // Create a list to store order details
            List<OrderDetail> orderDetails = new();

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

        public async Task Delete(Order order)
        {
            // Delete order
            await _mediator.Send(new DeleteOrderCommand(order));
        }

        public async Task Update(OrderDTO orderDTO)
        {
            // Get order details from DTO
            ICollection<OrderDetailsDTO> orderDetailsDTO = orderDTO.OrderDetailsDTOs;

            // Create a list to store order details
            List<OrderDetail> orderDetails = new();

            // Loop through each order details to map and create a list of order details
            foreach (OrderDetailsDTO item in orderDetailsDTO)
            {
                // Check if document exists
                Document document = await _mediator.Send(new GetDocumentByIdQuery(item.DocumentId)) ?? throw new Exception("Document id not found!");

                // Generate new order details id
                string orderDetailsId = ObjectId.GenerateNewId().ToString();

                orderDetails.Add(new OrderDetail()
                {
                    Id = item.Id ?? orderDetailsId,
                    Quantity = item.Quantity,
                    Total = document.UnitPrice * item.Quantity,
                    DocumentId = item.DocumentId,
                    OrderId = orderDTO.Id
                });
            }

            // Map order DTO
            Order order = new()
            {
                Id = orderDTO.Id,
                FullName = orderDTO.FullName,
                PhoneNumber = orderDTO.PhoneNumber,
                BorrowDate = orderDTO.BorrowDate,
                ReturnDate = orderDTO.ReturnDate,
                Status = orderDTO.Status,
                OrderDetails = orderDetails
            };

            // Create new order
            await _mediator.Send(new UpdateOrderCommand(order));
        }

        public async Task<IEnumerable<Order>> GetDateStatistics(DateTime from, DateTime to)
        {
            // Create new order
            return await _mediator.Send(new GetOrderDateStatisticsQuery(from, to));
        }
    }
}
