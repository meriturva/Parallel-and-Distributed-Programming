using Microsoft.AspNetCore.Mvc;
using MicrosoftOrleansStreams.Grains;
using Orleans;
using System;
using System.Threading.Tasks;

namespace MicrosoftOrleansWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloController : ControllerBase
    {
        private readonly IGrainFactory _grainFactory;

        public HelloController(IGrainFactory grainFactory)
        {
            _grainFactory = grainFactory;
        }

        [HttpGet("start")]
        public async Task StartStreamAsync()
        {
            var key = Guid.NewGuid();
            var producer = _grainFactory.GetGrain<IProducerGrain>("my-producer");
            await producer.StartProducing("StreamNamespace", key);
        }


        [HttpGet("stop")]
        public async Task StopStreamAsync()
        {
            var producer = _grainFactory.GetGrain<IProducerGrain>("my-producer");
            await producer.StopProducing();
        }
    }
}