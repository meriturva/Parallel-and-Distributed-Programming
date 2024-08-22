using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SagaWithMasstransitShared;
using System;
using System.Threading.Tasks;

namespace SagaWithMasstransitWebsite.Controllers
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
            // Produce and publish new event
            var @event = new NewOrderEvent();
            @event.UserEmail = "diego@bonura.dev";

            Console.WriteLine($"Start new order with id: {@event.OrderId}");
            await _bus.Publish(@event);
        }
    }
}