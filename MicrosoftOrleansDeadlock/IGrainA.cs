using Orleans;
using System.Threading.Tasks;

namespace MicrosoftOrleansDeadlock
{
    public interface IGrainA : IGrainWithStringKey
    {
        Task<string> SayHelloToGrainB();
    }
}
