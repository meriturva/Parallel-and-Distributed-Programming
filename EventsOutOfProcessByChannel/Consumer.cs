using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace EventsOutOfProcessByChannel
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
                // Simulate some work
                Console.WriteLine($"Event elaborating {@event.Created}");
                Thread.Sleep(5000);
                Console.WriteLine($"Event elaborated {@event.Created}");
            }
        }
    }
}
