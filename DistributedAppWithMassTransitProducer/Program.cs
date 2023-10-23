using MassTransit;
using MassTransit.Logging;
using MassTransit.Monitoring;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System;

namespace DistributedAppWithMassTransitProducer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddMassTransit(x =>
            {
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
            builder.Services.AddOpenTelemetry()
                .ConfigureResource(r =>
                {
                    r.AddService("MassTransit Producer",
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
                
           // );

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}