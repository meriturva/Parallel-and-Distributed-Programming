using MediatR;
using System;

namespace EventsInProcessByMediator
{
    public class NewOrderEvent : INotification
    {
        public DateTime Created = DateTime.Now;
    }
}
