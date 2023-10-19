using System;

namespace DistributedAppWithMassTransitShared
{
    public class NewOrderEvent
    {
        public DateTime Created { get; set; } = DateTime.Now;
        public string UserEmail { get; set; } = default!;
    }
}
