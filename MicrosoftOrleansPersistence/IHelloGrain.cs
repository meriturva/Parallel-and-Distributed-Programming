using Orleans;
using System.Threading.Tasks;

namespace MicrosoftOrleansPersistence
{
    public interface IHelloGrain : IGrainWithStringKey
    {
        Task<string> SayHello(string greeting);
    }
}
