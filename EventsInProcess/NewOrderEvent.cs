using MediatR;
using System;

namespace Events
{
    public class NewOrderEvent: INotification
    {
        public DateTime Created = DateTime.Now;
    }
}
