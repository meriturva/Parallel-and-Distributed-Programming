using System;
using System.Threading;

namespace MessagePassing
{
    public class Producer
    {
        public void Start()
        {
            var consumer = new Consumer();
            int i = 0;
            while (true)
            {
                var result = consumer.Elaborate(i, i);
                Console.WriteLine($"Counter: {i} with result: {result}");
                i++;
            }
        }
    }
}
