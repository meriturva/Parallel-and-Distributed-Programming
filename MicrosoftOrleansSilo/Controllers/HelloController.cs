using Microsoft.AspNetCore.Mvc;
using Orleans;
using System.Linq;
using System.Threading.Tasks;

namespace MicrosoftOrleansSilo.Controllers
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

        [HttpGet("{userName}")]
        public async Task<string> SayHelloAsync(string userName)
        {
            var friend = _grainFactory.GetGrain<IHelloGrain>(userName);
            return await friend.SayHello("Good morning!");
        }

        [HttpGet("massiveSameId")]
        public async Task SayHelloToSameIdAsync()
        {

            await Parallel.ForEachAsync(Enumerable.Repeat(true, 100), async (_, _) =>
            {
                var friend = _grainFactory.GetGrain<IHelloGrain>("user_1");
                await friend.SayHello("Good morning!");
            });
        }

        [HttpGet("massiveDifferentIds")]
        public async Task SayHelloToDifferentsIdsAsync()
        {

            await Parallel.ForEachAsync(Enumerable.Repeat(true, 100).Select((x, i) => i), async (i, _) =>
            {
                var friend = _grainFactory.GetGrain<IHelloGrain>($"user_{i}");
                await friend.SayHello("Good morning!");
            });
        }
    }
}