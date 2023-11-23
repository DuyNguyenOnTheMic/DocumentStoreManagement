using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace DocumentStoreManagement.Services.MessageBroker
{
    /// <summary>
    /// RabbitMQ Producer
    /// </summary>
    public class RabbitMQProducer : IRabbitMQProducer
    {
        /// <inheritdoc/>
        public void SendOrderMessage<T>(T message)
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

            // Serialize the message
            string json = JsonConvert.SerializeObject(message);
            byte[] body = Encoding.UTF8.GetBytes(json);

            // Put the data on to the order queue
            channel.BasicPublish(exchange: "", routingKey: "order", body: body);
        }
    }
}
