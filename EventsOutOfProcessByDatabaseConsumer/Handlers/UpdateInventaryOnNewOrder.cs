using EventsOutOfProcessByDatabaseShared;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EventsOutOfProcessByDatabaseConsumer.Handlers
{
    public class SendEmailOnNewUpdateInventaryOnNewOrderOrder : INotificationHandler<NewOrderEvent>
    {
        public async Task Handle(NewOrderEvent request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Updating inventary");
            await Task.Delay(2000);
            Console.WriteLine("Updated inventary");
        }
    }
}
