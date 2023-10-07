namespace MessagePassing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var producer = new Producer();
            producer.Start();
        }
    }
}