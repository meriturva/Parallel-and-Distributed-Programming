using EventsOutOfProcessByMessageBrokerShared;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EventsOutOfProcessByDatabaseConsumer
{
    class Worker : BackgroundService
    {

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "task_queue",
                                    durable: true,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
            var messageConsumer = new EventingBasicConsumer(channel);

            messageConsumer.Received += async (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                var @event = (NewOrderEvent)JsonSerializer.Deserialize(body, typeof(NewOrderEvent));
                Console.WriteLine($"Received from {@event.UserEmail}");

                await Task.Delay(100);
                //throw new Exception("AAA");
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            channel.BasicConsume(queue: "task_queue",
                                 autoAck: false,
                                 consumer: messageConsumer);

            Console.ReadLine();
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Worker: StartAsync called...");
            return base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Worker: StopAsync called...");
            await base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            Console.WriteLine("Worker: Dispose called...");
            base.Dispose();
        }
    }
}
