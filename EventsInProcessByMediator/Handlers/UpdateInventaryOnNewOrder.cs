﻿using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EventsInProcessByMediator.Handlers
{
    public class UpdateInventaryOnNewOrderOrder : INotificationHandler<NewOrderEvent>
    {
        public async Task Handle(NewOrderEvent request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Updating inventary");
            await Task.Delay(2000);
            Console.WriteLine("Updated inventary");
        }
    }
}
