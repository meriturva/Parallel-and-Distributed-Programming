using MassTransit;
using MassTransit.Logging;
using MassTransit.Monitoring;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System;
using System.Threading.Tasks;

namespace SagaWithMasstransitInventory
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();

            Console.WriteLine("Inventory: Waiting");

            await host.RunAsync();
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

                      // Observability
                      services.AddOpenTelemetry()
                           .ConfigureResource(r =>
                           {
                               r.AddService("MassTransit Inventory",
                                           serviceVersion: "MyVersion",
                                           serviceInstanceId: Environment.MachineName);
                           })
                            .WithTracing(builder => builder
                                .SetSampler(new AlwaysOnSampler())
                                .AddAspNetCoreInstrumentation()
                                .AddHttpClientInstrumentation()
                                .AddSource(DiagnosticHeaders.DefaultListenerName) // MassTransit ActivitySource
                                .AddOtlpExporter(opt =>
                                {
                                    opt.Endpoint = new Uri("http://localhost:4317");
                                })
                            )
                           .WithMetrics(builder => builder
                               .AddAspNetCoreInstrumentation()
                               .AddHttpClientInstrumentation()
                               .AddMeter(InstrumentationOptions.MeterName)
                               .AddOtlpExporter(opt =>
                               {
                                   opt.Endpoint = new Uri("http://localhost:4317");
                               })
                       );
                  });
    }
}