using EventsOutOfProcessByDatabaseShared;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventsOutOfProcessByDatabaseProducer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly EventBusContext _eventBusContext;

        public OrderController(EventBusContext eventBusContext)
        {
            _eventBusContext = eventBusContext;
        }

        [HttpGet]
        public async Task NewOrder()
        {
            // Produce a new event and sent to channel
            var @event = new NewOrderEvent();
            @event.UserEmail = "diego@bonura.dev";

            var content = JsonSerializer.Serialize(@event, @event.GetType());
            var typeName = @event.GetType().FullName!;

            var message = new Message()
            {
                Type = typeName,
                Content = content
            };

            _eventBusContext.Add(message);
            await _eventBusContext.SaveChangesAsync();
        }
    }
}