using Microsoft.Extensions.Logging;
using Orleans;
using System.Threading.Tasks;

namespace MicrosoftOrleansSilo
{
    public sealed class GrainA : Grain, IGrainA
    {
        private readonly ILogger<GrainA> _logger;

        public GrainA(ILogger<GrainA> logger)
        {
            _logger = logger;
        }

        public async Task<string> SayHelloToGrainB()
        {
            var id = this.GetPrimaryKeyString();
            _logger.LogInformation("Start say Hello for {grainId}", id);
            await Task.Delay(1000);
            
            var grainB = this.GrainFactory.GetGrain<IGrainB>(id);

            await grainB.SayHelloToGrainA();

            return $"Done!";
        }
    }
}