using Orleans;
using System.Threading.Tasks;

namespace ProjectToTest
{
    public sealed class HelloGrain : Grain, IHelloGrain
    {
        public HelloGrain()
        {
        }

        public async Task<string> SayHello(string greeting)
        {
            await Task.Delay(100);
            return $"Hello, {greeting}!";
        }
    }
}