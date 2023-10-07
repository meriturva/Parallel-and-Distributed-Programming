using System.Threading;

namespace MessagePassing
{
    public class Consumer
    {
        public int Elaborate(int a, int b)
        {
            // Just sleep
            Thread.Sleep(1000);

            return a + b;
        }
    }
}
