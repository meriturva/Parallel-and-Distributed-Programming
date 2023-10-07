using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Events.Handlers
{
    public class SendEmailOnNewOrder : INotificationHandler<NewOrderEvent>
    {
        public async Task Handle(NewOrderEvent request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Sending confirm email to user");
            await Task.Delay(2000);
            Console.WriteLine("Sent confirm email to user");
        }
    }
}
