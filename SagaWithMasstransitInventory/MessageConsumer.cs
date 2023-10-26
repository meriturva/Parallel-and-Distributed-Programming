using MassTransit;
using Microsoft.Extensions.Logging;
using SagaWithMasstransitShared;
using System;
using System.Threading.Tasks;

namespace SagaWithMasstransitInventory
{
    public class MessageConsumer : IConsumer<ProcessOrder>
    {
        readonly ILogger<MessageConsumer> _logger;

        public MessageConsumer(ILogger<MessageConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ProcessOrder> context)
        {
            _logger.LogInformation("Processing order", context.Message.OrderId);

            await Task.Delay(10000);

            if (Random.Shared.Next(2) == 1)
            {
                await context.RespondAsync(new OrderProcessed(context.Message.OrderId));
            }
            else
            {
                await context.RespondAsync(new OrderCancelled(context.Message.OrderId, "Item not available"));
            }

            _logger.LogInformation("Processed order", context.Message.OrderId);
        }
    }
}
