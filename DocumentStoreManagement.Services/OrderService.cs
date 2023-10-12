using DocumentStoreManagement.Core.DTOs;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Commands.OrderCommands;
using DocumentStoreManagement.Services.Interfaces;
using DocumentStoreManagement.Services.Queries.OrderQueries;
using MediatR;

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

            // Loop through each order details to map and create a list of order details
            foreach (OrderDetailsDTO item in orderDetailsDTO)
            {
                orderDetails.Add(new OrderDetail()
                {
                    Id = Guid.NewGuid().ToString(),
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    Total = item.Total,
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
            return await _mediator.Send(new CreateOrderCommand(order));
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
