using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace EventsOutOfProcess
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Create channel
            var channel = Channel.CreateUnbounded<NewOrderEvent>();
            builder.Services.AddSingleton(channel);
            builder.Services.AddSingleton(channel.Reader);
            builder.Services.AddSingleton(channel.Writer);

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            Task.Run(() => Consumer.ConsumeWithWhileAsync(channel.Reader));

            app.Run();
        }
    }
}