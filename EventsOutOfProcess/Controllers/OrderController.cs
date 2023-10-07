using Microsoft.AspNetCore.Mvc;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace EventsOutOfProcess.Controllers
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
            var @event = new NewOrderEvent();
            await _channelWriter.WriteAsync(@event);
        }
    }
}