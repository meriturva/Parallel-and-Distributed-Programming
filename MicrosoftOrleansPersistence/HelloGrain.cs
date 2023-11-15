using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using System.Threading;
using System.Threading.Tasks;

namespace MicrosoftOrleansPersistence
{
    public sealed class HelloGrain : Grain, IHelloGrain
    {
        private readonly ILogger<HelloGrain> _logger;
        private readonly IPersistentState<HelloState> _helloState;

        public HelloGrain(
            [PersistentState("hello")] IPersistentState<HelloState> helloState,
            ILogger<HelloGrain> logger)
        {
            _logger = logger;
            _helloState = helloState;
        }

        public override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            return base.OnActivateAsync(cancellationToken);
        }

        public async Task<string> SayHello(string greeting)
        {
            _helloState.State.Counter++;
            _logger.LogInformation("Start say Hello for {grainId} with counter {counter}", IdentityString, _helloState.State.Counter);
            await Task.Delay(1000);

            // Store state
            await _helloState.WriteStateAsync();

            DeactivateOnIdle();
            return $"Hello, {greeting}!";
        }

        public override Task OnDeactivateAsync(DeactivationReason reason, CancellationToken cancellationToken)
        {
            return base.OnDeactivateAsync(reason, cancellationToken);
        }
    }
}