using Orleans;
using System.Threading.Tasks;

namespace MicosoftOrleansRequestContextShared
{
    public interface IHelloGrain : IGrainWithStringKey
    {
        Task<string> SayHello(string greeting);
    }
}
