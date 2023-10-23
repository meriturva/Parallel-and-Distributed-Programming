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

                      // Observability
                      services.AddOpenTelemetry()
                           .ConfigureResource(r =>
                           {
                               r.AddService("MassTransit Consumer",
                                           serviceVersion: "MyVersion",
                                           serviceInstanceId: Environment.MachineName);
                           })
                            .WithTracing(builder => builder
                                .SetSampler(new AlwaysOnSampler())
                                .AddAspNetCoreInstrumentation()
                                .AddHttpClientInstrumentation()
                                .AddSource(DiagnosticHeaders.DefaultListenerName) // MassTransit ActivitySource
                                .AddConsoleExporter()
                                .AddOtlpExporter(opt =>
                                {
                                    opt.Endpoint = new Uri("http://localhost:4317");
                                })
                            )
                           .WithMetrics(builder => builder
                               .AddAspNetCoreInstrumentation()
                               .AddHttpClientInstrumentation()
                               .AddMeter(InstrumentationOptions.MeterName)
                               .AddConsoleExporter()
                               .AddOtlpExporter(opt =>
                               {
                                   opt.Endpoint = new Uri("http://localhost:4317");
                               })
                       );
                  });
    }
}