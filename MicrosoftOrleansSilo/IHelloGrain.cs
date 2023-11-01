using Orleans;
using System.Threading.Tasks;

namespace MicrosoftOrleansSilo
{
    public interface IHelloGrain : IGrainWithStringKey
    {
        Task<string> SayHello(string greeting);
    }
}
