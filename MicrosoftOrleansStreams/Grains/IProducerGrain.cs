using Orleans;
using System;
using System.Threading.Tasks;

namespace MicrosoftOrleansStreams.Grains
{
    public interface IProducerGrain : IGrainWithStringKey
    {
        Task StartProducing(string ns, Guid key);

        Task StopProducing();
    }
}