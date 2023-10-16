namespace DocumentStoreManagement.Services.MessageBroker
{
    public interface IRabbitMQProducer
    {
        public void SendOrderMessage<T>(T message);
    }
}
