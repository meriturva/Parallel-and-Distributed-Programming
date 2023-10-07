using MediatR;
using System;

namespace EventsOutOfProcess
{
    public class NewOrderEvent : INotification
    {
        public DateTime Created = DateTime.Now;
    }
}
