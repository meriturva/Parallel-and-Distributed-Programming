using Microsoft.AspNetCore.Mvc;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace EventsOutOfProcessByChannel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ChannelWriter<NewOrderEvent> _channelWriter;

        public OrderController(ChannelWriter<NewOrderEvent> channelWriter)
        {
            _channelWriter = channelWriter;
        }

        [HttpGet]
        public async Task NewOrder()
        {
            // Produce a new event and sent to channel
            var @event = new NewOrderEvent();
            await _channelWriter.WriteAsync(@event);
        }
    }
}