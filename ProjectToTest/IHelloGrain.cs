using Orleans;
using System.Threading.Tasks;

namespace ProjectToTest
{
    public interface IHelloGrain : IGrainWithStringKey
    {
        Task<string> SayHello(string greeting);
    }
}
