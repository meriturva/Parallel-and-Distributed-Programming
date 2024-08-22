using System;

namespace SagaWithMasstransitShared
{
    public record NewOrderEvent
    {
        public Guid OrderId { get; set; } = Guid.NewGuid();
        public string UserEmail { get; set; }
    }
}
