using Microsoft.AspNetCore.Mvc;
using MicrosoftOrleansSilo;
using Orleans;
using System.Linq;
using System.Threading.Tasks;

namespace SagaWithMasstransitWebsite.Controllers
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