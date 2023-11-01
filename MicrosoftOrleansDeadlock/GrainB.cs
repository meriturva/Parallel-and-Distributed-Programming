using Microsoft.Extensions.Logging;
using Orleans;
using System.Threading.Tasks;

namespace MicrosoftOrleansSilo
{
    public sealed class GrainB : Grain, IGrainB
    {
        private readonly ILogger<GrainA> _logger;

        public GrainB(ILogger<GrainA> logger)
        {
            _logger = logger;
        }

        public async Task<string> SayHelloToGrainA()
        {
            var id = this.GetPrimaryKeyString();
            _logger.LogInformation("Start say Hello for {grainId}", id);
            await Task.Delay(1000);

            var grainA = this.GrainFactory.GetGrain<IGrainA>(id);

            await grainA.SayHelloToGrainB();

            return $"Done!";
        }
    }
}