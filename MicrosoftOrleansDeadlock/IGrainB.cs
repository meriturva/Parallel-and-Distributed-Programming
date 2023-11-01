using Orleans;
using System.Threading.Tasks;

namespace MicrosoftOrleansSilo
{
    public interface IGrainB : IGrainWithStringKey
    {
        Task<string> SayHelloToGrainA();
    }
}
