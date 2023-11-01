using Orleans;
using System.Threading.Tasks;

namespace MicrosoftOrleansShared
{
    public interface IHelloGrain : IGrainWithStringKey
    {
        Task<string> SayHello(string greeting);
    }
}
