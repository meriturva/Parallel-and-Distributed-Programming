using Orleans;
using System.Threading.Tasks;

namespace MicrosoftOrleansDeadlock
{
    public interface IGrainB : IGrainWithStringKey
    {
        Task<string> SayHelloToGrainA();
    }
}
