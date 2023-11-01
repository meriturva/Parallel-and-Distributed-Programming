using Orleans;
using System.Threading.Tasks;

namespace MicrosoftOrleansSilo
{
    public interface IGrainA : IGrainWithStringKey
    {
        Task<string> SayHelloToGrainB();
    }
}
