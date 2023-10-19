using MassTransit;
using Microsoft.AspNetCore.Mvc;
using DistributedAppWithMassTransitShared;
using System.Threading.Tasks;

namespace DistributedAppWithMassTransitProducer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IBus _bus;

        public OrderController(IBus bus)
        {
            _bus = bus;
        }

        [HttpGet]
        public async Task NewOrderAsync()
        {
            // Produce a new event and sent to channel
            var @event = new NewOrderEvent();
            @event.UserEmail = "diego@bonura.dev";

            await _bus.Publish(@event);
        }
    }
}