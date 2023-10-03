using System;
using System.Threading.Tasks;

namespace AsyncAwait
{
    public class Producer
    {
        public async Task StartAsync()
        {
            var consumer = new Consumer();
            int i = 0;
            while (true)
            {
                var result = await consumer.ElaborateAsync(i, i);
                Console.WriteLine($"Counter: {i} with result: {result}");
                i++;
            }
        }
    }
}
