using Microsoft.Extensions.Logging;
using Orleans;
using System.Threading.Tasks;

namespace MicrosoftOrleansShared
{
    public sealed class HelloGrain : Grain, IHelloGrain
    {
        private readonly ILogger<HelloGrain> _logger;
        private int internalStateCounter = 0;

        public HelloGrain(ILogger<HelloGrain> logger)
        {
            _logger = logger;
        }

        public async Task<string> SayHello(string greeting)
        {
            internalStateCounter++;
            _logger.LogInformation("Start say Hello for {grainId} with internal state {internalStateCounter}", IdentityString, internalStateCounter);
            await Task.Delay(10000);
            return $"Hello, {greeting}!";
        }
    }
}