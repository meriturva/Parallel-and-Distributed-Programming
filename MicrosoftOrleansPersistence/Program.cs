using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans.Hosting;
using StackExchange.Redis;
using System;

namespace MicrosoftOrleansPersistence
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Website: Waiting");

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Host.UseOrleans(siloBuilder =>
            {
                siloBuilder.UseLocalhostClustering();
                siloBuilder.AddRedisGrainStorageAsDefault(options =>
                {
                    options.ConfigurationOptions = new ConfigurationOptions();
                    options.ConfigurationOptions.EndPoints.Add("localhost", 6379);
                });
            });

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}