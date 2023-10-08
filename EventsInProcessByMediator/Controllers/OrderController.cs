using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventsInProcessByMediator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IPublisher _publisher;

        public OrderController(IPublisher publisher)
        {
            _publisher = publisher;
        }

        [HttpGet]
        public async Task NewOrder()
        {
            var @event = new NewOrderEvent();
            await _publisher.Publish(@event);
        }
    }
}