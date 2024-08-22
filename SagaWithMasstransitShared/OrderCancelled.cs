using System;

namespace SagaWithMasstransitShared
{
    public record OrderCancelled(Guid OrderId, string Reason);
}
