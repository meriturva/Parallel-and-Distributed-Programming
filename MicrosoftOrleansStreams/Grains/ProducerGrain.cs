using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using Orleans.Streams;
using System;
using System.Threading.Tasks;

namespace MicrosoftOrleansStreams.Grains
{
    public class ProducerGrain : Grain, IProducerGrain
    {
        private readonly ILogger<IProducerGrain> _logger;

        private IAsyncStream<int>? _stream;
        private IDisposable? _timer;

        private int _counter = 0;

        public ProducerGrain(ILogger<IProducerGrain> logger)
        {
            _logger = logger;
        }

        public Task StartProducing(string ns, Guid key)
        {
            if (_timer is not null)
                throw new Exception("This grain is already producing events");

            // Get the stream
            var streamId = StreamId.Create(ns, key);
            _stream = this.GetStreamProvider("StreamProvider")
                .GetStream<int>(streamId);

            // Register a timer that produce an event every second
            var period = TimeSpan.FromMilliseconds(500);
            _timer = RegisterTimer(TimerTick, null, period, period);

            _logger.LogInformation("I will produce a new event every {Period}", period);

            return Task.CompletedTask;
        }

        private async Task TimerTick(object _)
        {
            var value = _counter++;
            _logger.LogInformation("Sending event {EventNumber}", value);
            if (_stream is not null)
            {
                await _stream.OnNextAsync(value);
            }
        }

        public Task StopProducing()
        {
            if (_timer is not null)
            {
                _timer.Dispose();
                _timer = null;
            }

            if (_stream is not null)
            {
                _stream = null;
            }

            return Task.CompletedTask;
        }
    }
}