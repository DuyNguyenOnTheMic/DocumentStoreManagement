﻿using DocumentStoreManagement.Core.DTOs;
using DocumentStoreManagement.Core.Models;
using DocumentStoreManagement.Services.Interfaces;
using DocumentStoreManagement.Services.MessageBroker;
using Microsoft.AspNetCore.Mvc;

namespace DocumentStoreManagement.Controllers
{
    /// <summary>
    /// Order Management API Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IRabbitMQProducer _rabbitMQProducer;

        /// <summary>
        /// Add dependencies to controller
        /// </summary>
        /// <param name="orderService"></param>
        /// <param name="rabbitMQProducer"></param>
        public OrdersController(IOrderService orderService, IRabbitMQProducer rabbitMQProducer)
        {
            _orderService = orderService;
            _rabbitMQProducer = rabbitMQProducer;
        }

        /// <summary>
        /// Gets the order list from database
        /// </summary>
        /// <returns>A list of all orders</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/orders
        ///
        /// </remarks>
        [HttpGet]
        public async Task<IEnumerable<Order>> GetOrders()
        {
            // Get list of orders
            return await _orderService.GetAll();
        }

        /// <summary>
        /// Gets an order bases on order id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A order matches input id</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/orders/{id}
        ///
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(string id)
        {
            // Get order by id
            Order order = await _orderService.GetById(id);
            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        /// <summary>
        /// Updates an order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedOrder"></param>
        /// <returns>An updated order</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT api/orders/{id}
        ///     {
        ///         "id": "id",
        ///         "fullName": "John Doe",
        ///         "phoneNumber": "0123456789",
        ///         "borrowDate": "2023-10-11T07:29:20.408Z",
        ///         "returnDate": "2023-10-12T07:29:20.409Z",
        ///         "status": 0,
        ///         "orderDetailsDTOs": [
        ///             {
        ///                 "id": "Order Details Id"
        ///                 "quantity": 2,
        ///                 "documentId": "Document Id"
        ///             }
        ///         ]
        ///     }
        ///
        /// </remarks>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(string id, OrderDTO updatedOrder)
        {
            // Return bad request if ids don't match
            if (id != updatedOrder.Id)
            {
                return BadRequest();
            }

            try
            {
                // Update order
                await _orderService.Update(updatedOrder);
            }
            catch (Exception e)
            {
                // Check if order exists
                if (!await OrderExists(id))
                {
                    // Return order not found error
                    return NotFound();
                }

                // Return error message
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Creates an order
        /// </summary>
        /// <param name="newOrder"></param>
        /// <returns>A newly created order</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/orders
        ///     {
        ///         "fullName": "John Doe",
        ///         "phoneNumber": "0123456789",
        ///         "borrowDate": "2023-10-11T07:29:20.408Z",
        ///         "returnDate": "2023-10-12T07:29:20.409Z",
        ///         "status": 1,
        ///         "orderDetailsDTOs": [
        ///             {
        ///                 "quantity": 2,
        ///                 "documentId": "Document Id"
        ///             }
        ///         ]
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult> PostOrder(OrderDTO newOrder)
        {
            Order order;
            try
            {
                // Add a new order
                order = await _orderService.Create(newOrder);

                // Send the inserted order data to the queue and consumer will listening this data from queue
                _rabbitMQProducer.SendOrderMessage(order);
            }
            catch (Exception e)
            {
                // Check if order exists
                if (await OrderExists(newOrder.Id))
                {
                    // Return order already exists error
                    return Conflict();
                }

                // Return error message
                return BadRequest(e.Message);
            }
            return new ObjectResult(order) { StatusCode = StatusCodes.Status201Created };
        }

        /// <summary>
        /// Deletes an order
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Order is deleted</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE api/orders/{id}
        ///
        /// </remarks>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            // Get order by id
            Order order = await _orderService.GetById(id);
            if (order == null)
            {
                return NotFound();
            }

            // Delete order
            await _orderService.Delete(order);

            return NoContent();
        }

        /// <summary>
        /// Finds orders by dates
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>A list of orders filtered by dates</returns>
        [HttpGet("statistics/date")]
        public async Task<IEnumerable<Order>> GetDateStatistics(DateTime from, DateTime to)
        {
            // Get list of orders by dates
            return await _orderService.GetDateStatistics(from, to);
        }

        #region Helpers
        /// <summary>
        /// Check if order exists method
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Boolean</returns>
        private async Task<bool> OrderExists(string id)
        {
            return await _orderService.GetById(id) != null;
        }
        #endregion
    }
}
