using System.Threading.Tasks;

namespace AsyncAwait
{
    public class Consumer
    {
        public async Task<int> ElaborateAsync(int a, int b)
        {
            await Task.Delay(1000);
            return a + b;
        }
    }
}
