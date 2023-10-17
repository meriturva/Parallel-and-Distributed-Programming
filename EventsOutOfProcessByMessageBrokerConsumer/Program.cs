using EventsOutOfProcessByMessageBrokerShared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace EventsOutOfProcessByDatabaseConsumer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();

            Console.WriteLine("Main: Waiting for RunAsync to complete");

            await host.RunAsync();

            Console.WriteLine("Main: RunAsync has completed");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
              Host.CreateDefaultBuilder(args)
                  .UseConsoleLifetime()
                  .ConfigureServices((hostContext, services) =>
                  {
                      services.AddTransient<IEventBus, EventBusRabbitMQ>();
                      services.AddHostedService<Worker>();
                      // give the service 120 seconds to shut down gracefully before whacking it forcefully
                      services.Configure<HostOptions>(options => options.ShutdownTimeout = TimeSpan.FromSeconds(120));
                  });
    }
}