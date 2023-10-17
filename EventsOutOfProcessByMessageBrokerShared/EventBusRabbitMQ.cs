using ProtoBuf;
using RabbitMQ.Client;
using System;
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

            // Standard json serializer
            string message = JsonSerializer.Serialize(@event, typeof(NewOrderEvent));
            var bodyBytes = Encoding.UTF8.GetBytes(message);

            //byte[] bodyBytes;
            //using (var memory = new MemoryStream())
            //{
            //    Serializer.Serialize(memory, @event);
            //    bodyBytes = memory.ToArray();
            //}

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: string.Empty,
                     routingKey: "task_queue",
                     basicProperties: properties,
                     body: bodyBytes);

        }
    }
}
