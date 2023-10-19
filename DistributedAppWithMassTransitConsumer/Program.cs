using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace DistributedAppWithMassTransitConsumer
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
                      services.AddMassTransit(x =>
                      {
                          x.AddConsumer<MessageConsumer>();
                          // elided...
                          x.UsingRabbitMq((context, cfg) =>
                          {
                              cfg.Host("localhost", "/", h =>
                              {
                                  h.Username("guest");
                                  h.Password("guest");
                              });
                              cfg.ConfigureEndpoints(context);
                          });
                      });
                  });
    }
}