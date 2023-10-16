using MediatR;
using System;

namespace EventsOutOfProcessByDatabaseShared
{
    public class NewOrderEvent : INotification
    {
        public DateTime Created { get; set; } = DateTime.Now;
        public string UserEmail { get; set; } = default!;
    }
}
