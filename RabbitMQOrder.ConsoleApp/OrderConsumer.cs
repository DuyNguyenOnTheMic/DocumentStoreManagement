using DocumentStoreManagement.Core.Interfaces;
using DocumentStoreManagement.Core.Models;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQOrder.ConsoleApp
{
    public class OrderConsumer(IRepository<Order> mongoOrderRepository) : IHostedService
    {
        private readonly IRepository<Order> _mongoOrderRepository = mongoOrderRepository;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Here we specify the Rabbit MQ Server. we use rabbitMQ docker image and use it
            ConnectionFactory factory = new()
            {
                HostName = "localhost"
            };

            // Create the RabbitMQ connection using connection factory details as i mentioned above
            IConnection connection = factory.CreateConnection();

            // Here we create channel with session and model
            using IChannel channel = connection.CreateChannel();

            // Declare the queue after mentioning name and a few property related to that
            channel.QueueDeclare("order", exclusive: false);

            // Set Event object which listen message from channel which is sent by producer
            EventingBasicConsumer consumer = new(channel);
            consumer.Received += async (model, eventArgs) =>
            {
                byte[] body = eventArgs.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Order message received: {message}");

                // Insert new order
                await InsertOrder(message);
            };

            // Read the message
            channel.BasicConsume(queue: "order", autoAck: true, consumer: consumer);
            Console.ReadKey();

            return Task.CompletedTask;
        }

        private async Task InsertOrder(string message)
        {
            // Insert order into MongoDB
            Order order = JsonConvert.DeserializeObject<Order>(message) ?? throw new Exception("Deserialize order failed!");
            await _mongoOrderRepository.AddAsync(order);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Implement any cleanup logic here
            // ...

            return Task.CompletedTask;
        }
    }
}