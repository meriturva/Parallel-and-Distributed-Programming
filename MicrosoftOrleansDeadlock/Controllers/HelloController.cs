using Microsoft.AspNetCore.Mvc;
using Orleans;
using System.Threading.Tasks;

namespace MicrosoftOrleansDeadlock.Controllers
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

        [HttpGet]
        public async Task<string> SayHelloAsync()
        {
            var grainA = _grainFactory.GetGrain<IGrainA>("my-id");
            return await grainA.SayHelloToGrainB();
        }
    }
}