using MassTransit;
using MassTransit.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
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

            builder.Services.AddOpenTelemetry()
                .ConfigureResource(r =>
                {
                    r.AddService("MassTransit Producer",
                                serviceVersion: "MyVersion",
                                serviceInstanceId: Environment.MachineName);
                })
                .WithTracing(b => b
                    .AddSource(DiagnosticHeaders.DefaultListenerName) // MassTransit ActivitySource
                    .AddAspNetCoreInstrumentation()
                    .AddConsoleExporter()
                    .AddOtlpExporter()
                    //.AddOtlpExporter(opts =>
                    //{
                    //    opts.Endpoint = new Uri("http://localhost:4318");
                    //    //opts.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf;
                    //    opts.ExportProcessorType = OpenTelemetry.ExportProcessorType.Simple;
                    //})
                );

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