using MassTransit;
using SagaWithMasstransitShared;
using System;

namespace SagaWithMasstransitOrchestrator
{
    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        public State Pending { get; private set; }
        public State Accepted { get; private set; }
        public State Cancelled { get; set; }

        public Event<NewOrderEvent> NewOrderEvent { get; private set; }
        public Event<OrderProcessed> OrderProcessed { get; private set; }
        public Event<OrderCancelled> OrderCancelled { get; private set; }


        public OrderStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => NewOrderEvent, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => OrderProcessed, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => OrderCancelled, x => x.CorrelateById(context => context.Message.OrderId));

            Initially(
                When(NewOrderEvent)
                    .Then(context =>
                    {
                        context.Saga.ProcessingId = Guid.NewGuid();
                    })
                    .Publish(context => new ProcessOrder(context.Saga.CorrelationId))
                    .TransitionTo(Pending)
                    .Then(context => Console.Out.WriteLineAsync($"From New to Pending: {context.Saga.CorrelationId}"))
            );

            During(Pending,
                When(OrderProcessed)
                    .TransitionTo(Accepted)
                    .Then(context => Console.Out.WriteLineAsync($"From Pending to Accepted: {context.Saga.CorrelationId}"))
                    .Finalize(),
                When(OrderCancelled)
                    .TransitionTo(Cancelled)
                    .Then(context => Console.Out.WriteLineAsync($"From Pending to Faulted: {context.Saga.CorrelationId} for reason: {context.Message.Reason}"))
                    .Finalize()
                );

            SetCompletedWhenFinalized();
        }
    }
}
