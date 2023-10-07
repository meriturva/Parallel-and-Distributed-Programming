using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Events.Handlers
{
    public class SendEmailOnNewUpdateInventaryOnNewOrderOrder : INotificationHandler<NewOrderEvent>
    {
        public async Task Handle(NewOrderEvent request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Updating inventary");
            await Task.Delay(1000);
            Console.WriteLine("Updated inventary");
        }
    }
}
