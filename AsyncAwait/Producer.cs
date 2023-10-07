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

                // Two task
                //var result1 = await consumer.ElaborateAsync(i, i);
                //Console.WriteLine($"Counter1: {i} with result: {result1}");
                //var result2 = await consumer.ElaborateAsync(i, i);
                //Console.WriteLine($"Counter2: {i} with result: {result2}");

                // A chain of tasks
                //var result = await (consumer.ElaborateAsync(i, i).ContinueWith(async (result1) =>
                //{
                //    Console.WriteLine($"Counter1: {i} with result: {result1.Result}");
                //    return await consumer.ElaborateAsync(i, i);
                //}));
                //Console.WriteLine($"Counter2: {i} with result: {result.Result}");

                // Two task in parallel
                //var taskResult1 = consumer.ElaborateAsync(i, i);
                //var taskResult2 = consumer.ElaborateAsync(i, i);
                //Task.WaitAll(new Task[] { taskResult1, taskResult2 });
                //Console.WriteLine($"Counter1: {i} with result: {taskResult1.Result}");
                //Console.WriteLine($"Counter2: {i} with result: {taskResult2.Result}");
                i++;
            }
        }
    }
}
