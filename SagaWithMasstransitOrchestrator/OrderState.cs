using MassTransit;
using System;

namespace SagaWithMasstransitOrchestrator
{
    public class OrderState : SagaStateMachineInstance, ISagaVersion
    {
        public Guid CorrelationId { get; set; } = default!;
        public Guid? ProcessingId { get; set; }
        public int CurrentState { get; set; }
        public Guid? RequestId { get; set; }
        public Uri ResponseAddress { get; set; }
        public int Version { get; set; }
    }
}
