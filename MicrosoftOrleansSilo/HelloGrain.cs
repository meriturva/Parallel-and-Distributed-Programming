using Microsoft.Extensions.Logging;
using Orleans;
using System.Threading.Tasks;

namespace MicrosoftOrleansSilo
{
    public sealed class HelloGrain : Grain, IHelloGrain
    {
        private readonly ILogger<HelloGrain> _logger;
        private int counter = 0;

        public HelloGrain(ILogger<HelloGrain> logger)
        {
            _logger = logger;
        }

        public async Task<string> SayHello(string greeting)
        {
            counter++;
            _logger.LogInformation("Start say Hello for {grainId} with counter {counter}", IdentityString, counter);
            await Task.Delay(1000);
            return $"Hello, {greeting}!";
        }
    }
}