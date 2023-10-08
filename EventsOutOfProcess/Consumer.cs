using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace EventsOutOfProcess
{
    public class Consumer
    {
        public static async ValueTask ConsumeWithWhileAsync(ChannelReader<NewOrderEvent> reader)
        {
            while (true)
            {
                // May throw ChannelClosedException if
                // the parent channel's writer signals complete.
                var @event = await reader.ReadAsync();
                //Thread.Sleep(10000);
                Console.WriteLine(@event);
            }
        }
    }
}
