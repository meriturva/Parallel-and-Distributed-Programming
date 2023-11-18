using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using System.Threading.Tasks;

namespace MicosoftOrleansRequestContextShared
{
    public sealed class HelloGrain : Grain, IHelloGrain
    {
        private readonly ILogger<HelloGrain> _logger;

        public HelloGrain(ILogger<HelloGrain> logger)
        {
            _logger = logger;
        }

        public async Task<string> SayHello(string greeting)
        {
            var role = RequestContext.Get("UserRole");
            _logger.LogInformation("Start say Hello for {grainId} with internal role {role}", IdentityString, role);
            await Task.Delay(100);
            return $"Hello, {greeting}!";
        }
    }
}