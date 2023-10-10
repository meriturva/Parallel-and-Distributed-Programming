using EventsOutOfProcessByDatabaseShared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Formats.Asn1;
using System.Linq;
using System.Reflection;
using System.Text.Json;
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
                      // Add services to the container.
                      services.AddDbContext<EventBusContext>(
                          options => options.UseSqlServer(@"Server=localhost,1433;Database=EventBus;User Id=sa;Password=Unicam123!;")
                      );

                      services.AddMediatR(cfg =>
                      {
                          cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                          // To make execution in parallel
                          //cfg.NotificationPublisherType = typeof(TaskWhenAllPublisher);
                      });

                      services.AddHostedService<Worker>();

                      // give the service 120 seconds to shut down gracefully before whacking it forcefully
                      services.Configure<HostOptions>(options => options.ShutdownTimeout = TimeSpan.FromSeconds(120));
                  });
    }
}