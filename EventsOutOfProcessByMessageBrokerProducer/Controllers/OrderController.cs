using EventsOutOfProcessByMessageBrokerShared;
using Microsoft.AspNetCore.Mvc;

namespace EventsOutOfProcessByMessageBrokerProducer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IEventBus _eventBus;

        public OrderController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [HttpGet]
        public void NewOrder()
        {
            // Produce a new event and sent to channel
            var @event = new NewOrderEvent();
            @event.UserEmail = "diego@bonura.dev";

            _eventBus.Publish(@event);
        }

        [HttpGet("massive")]
        public void MassiveNewOrder()
        {
            for (int i = 0; i < 1000; i++)
            {
                // Produce a new event and sent to channel
                var @event = new NewOrderEvent();
                @event.UserEmail = $"diego_{i}@bonura.dev";

                _eventBus.Publish(@event);
            }
        }
    }
}