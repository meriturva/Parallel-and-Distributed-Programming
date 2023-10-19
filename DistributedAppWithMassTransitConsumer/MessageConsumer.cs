using DistributedAppWithMassTransitShared;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DistributedAppWithMassTransitConsumer
{
    public class MessageConsumer : IConsumer<NewOrderEvent>
    {
        readonly ILogger<MessageConsumer> _logger;

        public MessageConsumer(ILogger<MessageConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<NewOrderEvent> context)
        {
            _logger.LogInformation("Received ordine from: {email}", context.Message.UserEmail);

            return Task.CompletedTask;
        }
    }
}
