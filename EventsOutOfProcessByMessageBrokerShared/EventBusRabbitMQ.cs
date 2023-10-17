using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace EventsOutOfProcessByMessageBrokerShared
{
    public class EventBusRabbitMQ : IEventBus
    {
        public void Publish(IEvent @event)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "task_queue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            string message = JsonSerializer.Serialize(@event, typeof(NewOrderEvent));
            var body = Encoding.UTF8.GetBytes(message);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: string.Empty,
                     routingKey: "task_queue",
                     basicProperties: properties,
                     body: body);

        }
    }
}
