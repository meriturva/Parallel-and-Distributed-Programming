using MicosoftOrleansRequestContextShared;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using Orleans.Runtime;
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

        [HttpGet("{userName}")]
        public async Task<string> SayHelloAsync(string userName)
        {
            RequestContext.Set("UserRole", "Admin");
            var friend = _grainFactory.GetGrain<IHelloGrain>(userName);
            return await friend.SayHello("Good morning!");
        }
    }
}