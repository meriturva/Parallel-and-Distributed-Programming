using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Hosting;
using System.Threading.Tasks;

namespace MicrosoftOrleansRequestContextSilo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
              Host.CreateDefaultBuilder(args)
            .UseOrleans(siloBuilder =>
            {
                siloBuilder.UseLocalhostClustering();
                siloBuilder.UseDashboard();
            });
    }
}