using EventsOutOfProcessByDatabaseShared;
using MediatR;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EventsOutOfProcessByDatabaseConsumer
{
    class Worker : BackgroundService
    {
        private readonly EventBusContext _eventBusContext;
        private readonly IPublisher _publisher;

        public Worker(EventBusContext eventBusContext, IPublisher publisher)
        {
            _eventBusContext = eventBusContext;
            _publisher = publisher;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (true)
            {
                var messageToElaborate = _eventBusContext.Set<Message>().Where(m => m.ProcessedOn == null).OrderBy(m => m.OccurredOn).FirstOrDefault();
                if (messageToElaborate != null)
                {
                    var type = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic).SelectMany(a => a.GetTypes()).FirstOrDefault(t => t.FullName == messageToElaborate.Type);
                    var domainEvent = (INotification)JsonSerializer.Deserialize(messageToElaborate.Content, type);

                    await _publisher.Publish(domainEvent);

                    messageToElaborate.ProcessedOn = DateTime.Now;
                    await _eventBusContext.SaveChangesAsync();
                }

                await Task.Delay(1000);
            }
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
