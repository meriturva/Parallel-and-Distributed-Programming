using System.Threading.Tasks;

namespace AsyncAwait
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var producer = new Producer();
            await producer.StartAsync();
        }
    }
}